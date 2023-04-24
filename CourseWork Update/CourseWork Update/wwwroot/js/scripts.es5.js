// mainpage

"use strict";

$(document).on("DOMContentLoaded", function () {
    $(".owl-carousel").owlCarousel({
        loop: true,
        margin: 0,
        nav: true,
        items: 1,
        dots: false,
        navText: ["", ""],
        smartSpeed: 1000,
        responsive: {
            0: {
                nav: false
            },
            768: {
                nav: true
            }
        }
    });
});

