$(function () {
    var orderList = $('#list');
    var baseUrl = 'DataHandler.ashx?action=getunderlist';
    var page = 0;
    var pages = 1;
    var size = 10;
    var iScroll = new Pull();

    var ths = $('#thead tr').children();
    var ajaxLayer = function () {
        return layer.open({
            type: 2,
            content: "页面加载中...",
            shadeClose: false
        });
    }

    var orderListRender = function (list, link) {
        var html = '';
        if (list.length === 0) {
            html = '<tr><td colspan=4>暂无记录！</td></tr>';
        }
        list.forEach(function (info) {
            html += '<tr><td>';
            if (link) html += '<a href="javascript:;" data-dialog="DataHandler.ashx?action=getunderdetail&userid=' + info.UserID + '">';
            html += info.RankID +
                '|' +
                info.NickName +
                '|' +
                info.GameID;
            if (link) html += '</a>'
            html += '</td><td>' +
                info.Diamond +
                '</td><td>' +
                info.MonthDiamond +
                '</td><td>' +
                info.TotalDiamond +
                '</td></tr>';
        });
        orderList.html(html);
        if(link) {
            $('#list a[data-dialog]').on('touchend click', function (e) {
                var that = $(e.target);
                $.ajax({url:that.attr('data-dialog'),success:function(result){
                    if (result.code===0&&result.data.valid){
                        var info = JSON.parse(result.data.info);
                        layer.open({content:result.data.info});
                    }
                }});
            });
        }
    }

    var load = function (next, pull, pageSize) {
        var layerId = ajaxLayer();
        $.ajax({
            url: orderList.attr('data-url'),
            data: {
                page: next ? (page < pages ? ++page : page) : (page > 1 ? --page : page),
                pageSize: pageSize | size
            },
            success: function (result) {
                if (result.data.valid) {

                    pages = Math.max(Math.ceil(result.data.total / size), 1);
                    orderListRender(result.data.list, result.data.link);
                    $('#pCount').text(result.data.count);
                    // 表格对齐
                    var tds = $('#list tr').eq(0).children();
                    if (!tds.attr('colspan')) {
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

    var parameter = {
        options: {},
        canPullDown: true,
        canPullUp: true,
        id: 'wrapper',
        content: 'table',
        pullHeight: 40,
        pullMaxHeight: 60,
        upBeginText: '上拉加载',
        upAfterText: '松开加载下一页',
        downBeginText: '下拉加载',
        downAfterText: '松开加载上一页',
        pullDownAction: function (pull) {
            load(null, pull);
        },
        pullUpAction: function (pull) {
            load(true, pull);
        }
    };

    iScroll.init(parameter);

    var type = $('#hidType').val();
    var aTip = 'Top50',
        monthTitle = '本月',
        totalTitle = '累计',
        typeTitle = '';
    var thMonth = $('#thMonth'),
        thTotal = $('#thTotal'),
        aMonth = $('#aMonth'),
        aTotal = $('#aTotal'),
        typeLab = $('#typeLab');
    if (type === 'user') {
        typeTitle = '购卡';
        typeLab.text('玩家');
        $('#btnAddAgent').hide();
    } else if (type === 'agent') {
        typeTitle = '售卡';
        typeLab.text('代理');
    }

    thMonth.html(monthTitle + typeTitle);
    thTotal.html(totalTitle + typeTitle);
    aMonth.html(aTip + monthTitle + typeTitle);
    aTotal.html(aTip + totalTitle + typeTitle);
    orderList[0].dataset['url'] = baseUrl + '&type=' + type + '&range=all';

    $('.ui-tab-under li').on('click',
        function (e) {
            //            var range = e.target.attr('data-range');
            $('.ui-tab-under li').each(function (i, e) {
                $(e).removeClass('active');
            });
            var that = $(e.target);
            if (e.target.id) {
                that = $(e.target).parent();
            }
            var range = that.attr('data-range');
            that.addClass('active');
            orderList[0].dataset['url'] = baseUrl + '&type=' + type + '&range=' + range;
            load(true, null, range !== 'all' ? 50 : null);
        });

    load(true);
   
});