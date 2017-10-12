<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="Game.Web.Module.DataStatistics.UserRegister" %>

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
                你当前位置：统计系统 - 用户钻石统计
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg">
        <tr>
            <td class="listTdLeft" style="width: 80px">
                日期查询：
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="text wd2" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtEndDate\')}'})"></asp:TextBox><img
                    src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtEndDate\')}'})"
                    style="cursor: pointer; vertical-align: middle" />
                至
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="text wd2" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtStartDate\')}'})"></asp:TextBox><img
                    src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtStartDate\')}'})"
                    style="cursor: pointer; vertical-align: middle" />

                <input type="button" id="search" value="查询" class="btn wd1" />
            </td>
        </tr>
    </table>
    <h2 style="text-align: center;margin-bottom: 5px;">每日用户注册统计</h2>
    <div id="content">
    </div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr></tr>
    </table>
    </form>
    <script type="text/javascript">
        var data = [];
        var chart = new G2.Chart({
            id: 'content',
            forceFit: true,
            height: 600,
            plotCfg: {
                margin: [40, 80, 90, 80]
            }
        });
        chart.legend({
            position: 'bottom'
        });
        chart.axis('count', {
            position: 'top',
            formatter: function (dimValue) {
                if (dimValue >= 10000 && dimValue <= 99999) {
                    return (dimValue / 10000).toFixed(0) + '万';
                } else if (dimValue >= 100000 && dimValue <= 999999)
                {
                    return (dimValue / 100000).toFixed(0) + '十万';
                } else if (dimValue >= 1000000 && dimValue <= 9999999) {
                    return (dimValue / 1000000).toFixed(0) + '百万';
                } else if (dimValue >= 10000000 && dimValue <= 99999999) {
                    return (dimValue / 10000000).toFixed(0) + '千万';
                } else if (dimValue >= 100000000 && dimValue <= 999999999) {
                    return (dimValue / 100000000).toFixed(0) + '亿';
                } else if (dimValue >= 1000000000 && dimValue <= 9999999999) {
                    return (dimValue / 1000000000).toFixed(0) + '十亿';
                } else if (dimValue >= 10000000000 && dimValue <= 99999999999) {
                    return (dimValue / 10000000000).toFixed(0) + '百亿';
                } else if (dimValue >= 100000000000 && dimValue <= 999999999999) {
                    return (dimValue / 100000000000).toFixed(0) + '千亿';
                } else if (dimValue >= 1000000000000 && dimValue <= 9999999999999) {
                    return (dimValue / 1000000000000).toFixed(0) + '万亿';
                } else {
                    return dimValue;
                }
            }
        });
        const scale = {
            time: {
                alias: '统计时间',
                type: 'time',
                mask: 'yyyy-mm-dd',
            },
            count: {
                alias: '人数'
            },
            type: {
                type: 'cat'
            }
        };

        chart.source(data, scale);
        chart.line().position('time*count').color('type', ['#ff7f0e', '#2ca02c', '#1f77b4']).shape('smooth').size(2);
        chart.render();

        window.onload = loadData();

        $('#search').on('click', function () {
            loadData();
        });

        function loadData() {
            $.ajax({
                type: "GET",
                url: "/Tools/Operating.ashx?action=getregisterstatictics",
                data: { stime: $("#txtStartDate").val(), etime: $("#txtEndDate").val() },
                success: function (result) {
                    if (result.data.valid) {
                        chart.changeData(result.data.data);
                    }
                }
            });
        }
    </script>
</body>
</html>
