"use strict";

function start(id, value1, value2) {
    $(id).css("background", "linear-gradient(to right, #00FF47 " + value1 / value2 + "%, #A4A4A4 " + value1 / value2 + "%");
}

function updateMonthPayment(id1, id2) {
    var monthPayment = $(".month-payment");
    var sum = $(id1).val();
    var term = $(id2).val();
    var p = 0.015;

    if (sum >= 10000 && sum <= 1000000 && term >= 6 && term <= 120) {
        var res = Number(sum * term * p + Number(sum)) / term;
        var expenses = (sum * term * p).toFixed(2);
        var expenses2 = (Number(expenses) + Number(sum)).toFixed(2);

        monthPayment.text(res.toFixed(2));

        $("#my-row-sum-of-credit").text(sum + " грн");
        $("#expenses").text(expenses + " грн");
        $("#expenses2").text(expenses2 + " грн");
    }
}

function onInput(id1, id2, id3, id4) {
    var value = $(id1).val();
    $(id3).val(value);
    $(id4).val(value);
    $(id2).val(value);
    $(id1).css("background", "linear-gradient(to right, #00FF47 " + value / 10100 + "%, #A4A4A4 " + value / 10100 + "%");
    $(id2).css("background", "linear-gradient(to right, #00FF47 " + value / 10100 + "%, #A4A4A4 " + value / 10100 + "%");
    updateMonthPayment(id3, "#term-value");
}

function onInput2(id1, id2, id3, id4) {
    var value = $(id1).val();
    $(id3).val(value);
    $(id4).val(value);
    $(id2).val(value);
    if (Number(value) < 50) {
        $(id1).css("background", "linear-gradient(to right, #00FF47 " + value / 1.35 + "%, #A4A4A4 " + value / 1.35 + "%");
        $(id2).css("background", "linear-gradient(to right, #00FF47 " + value / 1.35 + "%, #A4A4A4 " + value / 1.35 + "%");
    } else {
        $(id1).css("background", "linear-gradient(to right, #00FF47 " + value / 1.25 + "%, #A4A4A4 " + value / 1.25 + "%");
        $(id2).css("background", "linear-gradient(to right, #00FF47 " + value / 1.25 + "%, #A4A4A4 " + value / 1.25 + "%");
    }
    updateMonthPayment("#sum-value", id3);
}

function onChange(id1, id2, id3, id4) {
    var value = $(id1).val();
    if (Number(value) >= 10000 && Number(value) <= 1000000) {
        $(id3).css("background", "linear-gradient(to right, #00FF47 " + value / 10100 + "%, #A4A4A4 " + value / 10100 + "%");
        $(id4).css("background", "linear-gradient(to right, #00FF47 " + value / 10100 + "%, #A4A4A4 " + value / 10100 + "%");
        $(id3).val(value);
        $(id4).val(value);
        $(id2).val(value);

        updateMonthPayment(id1, "#term-value");
    }
}

function onChange2(id1, id2, id3, id4) {
    var value = $(id1).val();
    if (Number(value) >= 6 && Number(value) <= 120) {
        if (Number(value) < 50) {
            $(id3).css("background", "linear-gradient(to right, #00FF47 " + value / 1.35 + "%, #A4A4A4 " + value / 1.35 + "%");
            $(id4).css("background", "linear-gradient(to right, #00FF47 " + value / 1.35 + "%, #A4A4A4 " + value / 1.35 + "%");
        } else {
            $(id3).css("background", "linear-gradient(to right, #00FF47 " + value / 1.25 + "%, #A4A4A4 " + value / 1.25 + "%");
            $(id4).css("background", "linear-gradient(to right, #00FF47 " + value / 1.25 + "%, #A4A4A4 " + value / 1.25 + "%");
        }
        $(id3).val(value);
        $(id4).val(value);
        $(id2).val(value);

        updateMonthPayment("#sum-value", id1);
    }
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

start("#big-scroll", 15000, 10000);
start("#big-scroll-2", 15, 1.35);
start("#small-scroll", 15000, 10000);
start("#small-scroll-2", 15, 1.35);

$("#big-scroll").on("input", function () {
    onInput("#big-scroll", "#small-scroll", "#sum-value", "#sum-value-2");
});
$("#small-scroll").on("input", function () {
    onInput("#small-scroll", "#big-scroll", "#sum-value", "#sum-value-2");
});
$("#big-scroll-2").on("input", function () {
    onInput2("#big-scroll-2", "#small-scroll-2", "#term-value", "#term-value-2");
});
$("#small-scroll-2").on("input", function () {
    onInput2("#small-scroll-2", "#big-scroll-2", "#term-value", "#term-value-2");
});

$("#sum-value").change(function () {
    onChange("#sum-value", "#sum-value-2", "#big-scroll", "#small-scroll");
});
$("#sum-value-2").change(function () {
    onChange("#sum-value-2", "#sum-value", "#big-scroll", "#small-scroll");
});
$("#term-value").change(function () {
    onChange2("#term-value", "#term-value-2", "#big-scroll-2", "#small-scroll-2");
});
$("#term-value-2").change(function () {
    onChange2("#term-value-2", "#term-value", "#big-scroll-2", "#small-scroll-2");
});

$(".get-credit-animate").click(function () {
    $([document.documentElement, document.body]).animate({
        scrollTop: $(".form").offset().top - 40
    }, 200);
});

$(window).on("load", function () {
    var scrollToValue = getCookie("toscroll");
    if (scrollToValue == "true") {
        $([document.documentElement, document.body]).animate({
            scrollTop: $(".form").offset().top - 40
        }, 1);
    }
});

