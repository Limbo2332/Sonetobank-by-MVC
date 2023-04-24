function start(id, value1, value2) {
    $(id).css(
        "background",
        "linear-gradient(to right, #00FF47 " +
        value1 / value2 +
        "%, #A4A4A4 " +
        value1 / value2 +
        "%"
    );
    let depositSum = $("#sum-value").val();
    document.cookie = "depositSum=" + depositSum.toString();
}
function onInput(id1, id2) {
    let value = $(id1).val();
    let min = Number($("#big-scroll").attr("min"));
    if (min == 500) {
        if (value < 30000) {
            $(id1).css(
                "background",
                "linear-gradient(to right, #00FF47 " +
                value / 5000 +
                "%, #A4A4A4 " +
                value / 5000 +
                "%"
            );
        } else if (value < 175000) {
            $(id1).css(
                "background",
                "linear-gradient(to right, #00FF47 " +
                value / 9000 +
                "%, #A4A4A4 " +
                value / 9000 +
                "%"
            );
        } else {
            $(id1).css(
                "background",
                "linear-gradient(to right, #00FF47 " +
                value / 10025 +
                "%, #A4A4A4 " +
                value / 10025 +
                "%"
            );
        }
    } else {
        $(id1).css(
            "background",
            "linear-gradient(to right, #00FF47 " +
            value / 10100 +
            "%, #A4A4A4 " +
            value / 10100 +
            "%"
        );
    }

    $(id2).val(value);
    let depositSum = $("#sum-value").val();
    document.cookie = "depositSum=" + depositSum.toString();
    updateSum();
}
function onChange(id1, id2) {
    let value = $(id1).val();
    if (Number(value) >= 10000 && Number(value) <= 1000000) {
        $(id2).css(
            "background",
            "linear-gradient(to right, #00FF47 " +
            value / 10100 +
            "%, #A4A4A4 " +
            value / 10100 +
            "%"
        );
        $(id2).val(value);
        updateSum();
    }
    let depositSum = $("#sum-value").val();
    document.cookie = "depositSum=" + depositSum.toString();
}

function updateSum() {
    let sum = $("#sum-value").val();
    let term = $("#term").text();
    let percent = $("#percent").text().split(" ");

    let beforeTax = (
        (Number(sum) * Number(percent[0]) * term * 30.5) /
        36500
    ).toFixed(2);

    let tax = (beforeTax * 0.18).toFixed(2);
    let miliraty = (beforeTax * 0.015).toFixed(2);

    $("#before-tax").text(beforeTax + " грн");
    $("#tax").text(tax + " грн");
    $("#military").text(miliraty + " грн");
    $("#after-tax").text((beforeTax - tax - miliraty).toFixed(2) + " грн");
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

start("#big-scroll", 15000, 5000);

$("#big-scroll").on("input", function () {
    onInput("#big-scroll", "#sum-value");
});
$("#sum-value").change(function () {
    onChange("#sum-value", "#big-scroll");
});

$(".btn-scroll").click(function () {
    $([document.documentElement, document.body]).animate(
        {
            scrollTop: $(".right-side").offset().top,
        },
        200
    );
});

$(window).on("load", function () {
    let scrollToValue = getCookie("toscroll");
    if (scrollToValue == "true") {
        $([document.documentElement, document.body]).animate(
            {
                scrollTop: $(".right-side").offset().top,
            },
            1
        );
    }
});


