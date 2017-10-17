/**
 * Created by Administrator on 2016/12/27.
 */
'use strict';

$(function () {
  var orderList = $('#list');
  var page = 0;
  var pages = 1;
  var size = 10;
  var iScroll = new Pull();
  var parameter = {
    options: {},
    canPullDown: true,
    canPullUp: true,
    id: 'wrapper',
    content: 'table',
    pullHeight: 40,
    pullMaxHeight: 60,
    upBeginText: "上拉加载",
    upAfterText: "松开加载下一页",
    downBeginText: "下拉加载",
    downAfterText: "松开加载上一页",
    pullDownAction: function (pull) {
      load(null, pull);
    },
    pullUpAction: function (pull) {
      load(true, pull);
    }
  };
  var ajaxLayer = function () {
    return layer.open({
      type: 2,
      content: "页面加载中...",
      shadeClose: false
    });
  }
  var ths = $('#thead tr').children();

  iScroll.init(parameter);

  var load = function (next, pull) {
    var layerId = ajaxLayer();
    $.ajax({
      url: orderList.attr('data-url'),
      data: {
        page: next ? (page < pages ? ++page : page) : (page > 1 ? --page : page),
        pageSize: size
      },
      success: function (result) {
        if (result.data.valid) {
          pages = Math.max(Math.ceil(result.data.total / size), 1);
          orderList.html(result.data.html);

          // 表格对齐
          var tds = $('#list tr').eq(0).children();
          if (!tds.attr('colspan')) {
            ths.each(function (index, th) {
              if ($(th).text().indexOf('时间') > -1) {
                $(th).width('8rem');
              }
            });

            tds.each(function (index, td) {
              $(td).width(ths.eq(index).width());
            });
          }
        } else {
          alert(result.msg);
        }
        setTimeout(function () {
          layer.close(layerId);
        }, 500);
      },
      complete: function () {
        if (pull) {
          iScroll.loaded(pull);
        }
      }
    });
  };

  load(true);

});