$(function () {
  // Banner轮播
  $(".slider-container").ikSlider({
    speed: 500,
    touch: false,
    infinite: true,
    delay: 2000,
    responsive: true,
  });

  // 排行榜切换
  var nav = $('.ui-rank-nav li');
  var details = $('.ui-rank-details');
  nav.on('click', function (e) {
    var $this = $(this);
    var index = $this.attr('data-type');
    var length = nav.length;

    for (var i = 0; i < length; i++) {
      var $detail = $(details[i]);
      // nav切换
      if (nav[i] !== this) {
        $(nav[i]).removeClass('active');
      } else if (!$this.hasClass('active')) {
        $this.addClass('active');
      }

      // 排行榜切换
      if ($detail.attr('data-type') !== index) {
        if (!$detail.hasClass('hide')) {
          $detail.addClass('hide');
        }
      } else {
        $detail.removeClass('hide');
      }
    }
  });
});