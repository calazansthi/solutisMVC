// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ajaxStart(function () {
    $(".loading-bar").fadeIn();
    document.body.className = "loading";
});

$(document).ajaxStop(function () {
    $(".loading-bar").fadeOut();
    document.body.className = "";
});