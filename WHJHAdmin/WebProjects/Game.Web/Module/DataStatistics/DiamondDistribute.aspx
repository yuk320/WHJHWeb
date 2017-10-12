<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiamondDistribute.aspx.cs" Inherits="Game.Web.Module.DataStatistics.DiamondDistribute" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="/styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/scripts/jquery.js"></script>
    <script src="/scripts/g2.js"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 头部菜单 Start -->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top" class="Lpd10">
                <div class="arr">
                </div>
            </td>
            <td width="1232" height="25" valign="top" align="left">
                你当前位置：统计系统 - 财富分布统计
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="Tmg7">
        <tr>
            <td height="28">
                <ul>
                    <li class="tab2" onclick="Redirect('WealthDistribute.aspx')">金币分布</li>
                    <li class="tab1">钻石分布</li>
                </ul>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg">
        <tr>
            <td style="color:red; font-size:15px; font-weight:bold; text-align:center;">总计钻石数：<%=diamond %></td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <h2 style="text-align: center;margin:30px auto;">平台钻石分布图</h2>
    <div id="content" data-diamond='<%=djson %>'>
    </div>
    </form>
    <script type="text/javascript">
        var data = $.parseJSON($('#content').attr('data-diamond'));
        var Stat = G2.Stat;
        var chart = new G2.Chart({
            id: 'content',
            forceFit: true,
            height: 500,
        });
        chart.source(data);
        // 重要：绘制饼图时，必须声明 theta 坐标系
        chart.coord('theta', {
            radius: 0.8 // 设置饼图的大小
        });
        chart.legend('name', {
            position: 'bottom',
            itemWrap: true,
            formatter: function (val) {
                for (var i = 0, len = data.length; i < len; i++) {
                    var obj = data[i];
                    if (obj.name === val) {
                        return val + ': ' + obj.value + '%';
                    }
                }
            }
        });
        chart.tooltip({
            title: null,
            map: {
                value: 'value'
            }
        });
        chart.intervalStack()
          .position(Stat.summary.percent('value'))
          .color('name')
          .label('name*..percent', function (name, percent) {
              percent = (percent * 100).toFixed(2) + '%';
              return name + ' ' + percent;
          });
        chart.render();
        // 设置默认选中
        var geom = chart.getGeoms()[0]; // 获取所有的图形
        var items = geom.getData(); // 获取图形对应的数据
        geom.setSelected(items[1]); // 设置选中
    </script>
</body>
</html>
