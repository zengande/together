$(function () {
    __activityCardsMaxLength = $('.activity-item').length - 3;
    if (__activityCardsMaxLength < 0) {
        __activityCardsMaxLength = 0;
    }

    $('#slogan').typer({
        search: 'do anything',
        replace: ['swimming', 'go hiking', 'go riding']
    });
});
var __activityCardsMaxLength = 0;
var __activityCardsIndex = 0;
function rollNext() {
    if (__activityCardsIndex >= __activityCardsMaxLength - 1) {
        $('.next-btn').hide(0);
    }
    if (__activityCardsIndex >= __activityCardsMaxLength) {
        return;
    }
    $('.pre-btn').show(0);
    $('.cards').removeClass('page-' + __activityCardsIndex);
    __activityCardsIndex++;
    $('.cards').addClass('page-' + __activityCardsIndex);
}
function rollPre() {
    if (__activityCardsIndex <= 1) {
        $('.pre-btn').hide(0);
    }
    if (__activityCardsIndex <= 0) {
        return;
    }

    $('.next-btn').show(0);
    $('.cards').removeClass('page-' + __activityCardsIndex);
    __activityCardsIndex--;
    $('.cards').addClass('page-' + __activityCardsIndex);
}

(function ($) {
    $.fn.typer = function (options) {

        var defaults = $.extend({
            search: '',
            replace: [],
            speed: 50,
            delay: 2000
        }, options);

        var bintext = function (length) {
            var text = '';
            for (var $i = 0; $i <= length; $i++) {
                text = text + Math.floor(Math.random() * 2);
            }
            return text;
        };

        this.each(function () {

            var $this = $(this);
            var $text = $this.data('text');
            var position = 0;

            var indexOf = $text.indexOf(defaults.search);
            var normal = $text.substr(0, indexOf);
            var changer = $text.substr(indexOf, $text.length);

            defaults.replace.push(changer);

            var interval = setInterval(function () {
                var $bintext = '';

                if (position === indexOf) {

                    $bintext = bintext(changer.length - 1);

                    $this.html($text.substr(0, normal.length));
                    $this.append('<span>' + $bintext + '</span>');

                } else if (position > indexOf) {


                    $bintext = bintext($text.length - 1);

                    $this.delay(defaults.speed).find('span').html(
                        changer.substring(0, position - indexOf) +
                        $bintext.substring(position, $bintext.length)
                    );

                } else if (position < indexOf) {

                    $bintext = bintext($text.length - 1);

                    $this.delay(defaults.speed).html(
                        normal.substring(0, position) +
                        $bintext.substring(position, $bintext.length)
                    );

                }

                if (position < $text.length) {
                    position++;
                } else {
                    clearInterval(interval);

                    var index = 0;
                    setInterval(function () {

                        var position = 0;
                        var newText = defaults.replace[index];

                        var changeInterval = setInterval(function () {

                            var $bintext = '';
                            for (var $i = 0; $i <= newText.length - 1; $i++) {
                                $bintext = $bintext + Math.floor(Math.random() * 2);
                            }

                            $this.delay(defaults.speed).find('span').html(
                                newText.substring(0, position) +
                                $bintext.substring(position, $bintext.length)
                            );

                            if (position < $text.length) {
                                position++;
                            } else {
                                clearInterval(changeInterval);
                            }

                        }, defaults.speed);

                        if (index < defaults.replace.length - 1) {
                            index++;
                        } else {
                            index = 0;
                        }
                    }, defaults.delay);


                }
            }, defaults.speed);

        });

    };

})(jQuery);