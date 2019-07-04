// Write your JavaScript code.
$(function () {
    $('#backtop').on('click', function () {
        $('html').animate({
            scrollTop: '0px'
        }, 300);
    });
});