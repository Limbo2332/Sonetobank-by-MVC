

// footer

// youtube icon
"use strict";

$(".footer .icons input[src='/images/MainPage/footer icons/youtube default.png']").hover(function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/youtube default.png']").attr("src", "/images/MainPage/footer icons/youtube hover.png");
}, function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/youtube hover.png']").attr("src", "/images/MainPage/footer icons/youtube default.png");
});

// email icon

$(".footer .icons input[src='/images/MainPage/footer icons/email default.png']").hover(function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/email default.png']").attr("src", "/images/MainPage/footer icons/email hover.png");
}, function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/email hover.png']").attr("src", "/images/MainPage/footer icons/email default.png");
});

// instagram icon

$(".footer .icons input[src='/images/MainPage/footer icons/instagram default.png']").hover(function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/instagram default.png']").attr("src", "/images/MainPage/footer icons/instagram hover.png");
}, function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/instagram hover.png']").attr("src", "/images/MainPage/footer icons/instagram default.png");
});

// facebook icon

$(".footer .icons input[src='/images/MainPage/footer icons/facebook default.png']").hover(function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/facebook default.png']").attr("src", "/images/MainPage/footer icons/facebook hover.png");
}, function () {
    $(".footer .icons input[src='/images/MainPage/footer icons/facebook hover.png']").attr("src", "/images/MainPage/footer icons/facebook default.png");
});

// login

// eye icon

$(".eye-icon").click(function () {
    if ($(".eye-icon").attr("src") == "/images/Login_Register/eye icon click.png") {
        $(".eye-icon").attr("src", "/images/Login_Register/eye icon default.png");
        $("#password").attr("type", "password");
    } else {
        $(".eye-icon").attr("src", "/images/Login_Register/eye icon click.png");
        $("#password").attr("type", "text");
    }
});

$(".eye-icon-2").click(function () {
    if ($(".eye-icon-2").attr("src") == "/images/Login_Register/eye icon click.png") {
        $(".eye-icon-2").attr("src", "/images/Login_Register/eye icon default.png");
        $("#passwordconfirm").attr("type", "password");
    } else {
        $(".eye-icon-2").attr("src", "/images/Login_Register/eye icon click.png");
        $("#passwordconfirm").attr("type", "text");
    }
});

$(window).on("load", function () {
    if ($("#PhoneNumber").val() != "") {
        $("#PhoneNumber").prop("disabled", true);
    }
});

function isValidDate(dayValue, monthValue, yearValue) {

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    if (yearValue % 400 == 0 || yearValue % 100 != 0 && yearValue % 4 == 0) monthLength[1] = 29;

    return dayValue > 0 && dayValue <= monthLength[monthValue - 1];
}

