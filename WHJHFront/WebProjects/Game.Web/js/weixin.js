var deviceWidth = document.documentElement.clientWidth;
if (deviceWidth > 750) {
    deviceWidth = 750;
}
else if (deviceWidth == 0) {
    deviceWidth = 750
}
document.documentElement.style.fontSize = deviceWidth / 7.5 + 'px';

$(function () {
    var ua = navigator.userAgent.toLowerCase();
    var isWeixin = /MicroMessenger/i.test(ua);
    if (isWeixin) {
        var weixinPopup = $('#weixin-tip');

        // 关闭弹出
        weixinPopup.on('click', '.close', function (e) {
            e.preventDefault();

            weixinPopup.addClass('fn-hide');
        });

        weixinPopup.removeClass('fn-hide');
    }
});