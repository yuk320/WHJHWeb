<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemStat.aspx.cs" Inherits="Game.Web.Module.DataStatistics.SystemStat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="../../scripts/common.js"></script>
    <title></title>
    <style type="text/css">
        .gamelist { width: 650px; }

        .gamelist span {
            float: left;
            width: 200px;
            height: 23px;
            text-align: left;
            margin-top: 0px;
            margin-right: 0;
            margin-bottom: 2px;
            margin-left: 0;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
    <tr>
        <td width="19" height="25" valign="top" class="Lpd10">
            <div class="arr">
            </div>
        </td>
        <td width="1232" height="25" valign="top" align="left">
            你当前位置：数据分析 - 系统统计
        </td>
    </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td class="titleOpBg Lpd10">
            <input type="button" id="btnRefresh" class="btn wd1" value="刷新" onclick="javascript:location.href = location.href;"/>
        </td>
    </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
<tr>
    <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
        <div class="hg3  pd7">
            用户统计
        </div>
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        在线人数：
    </td>
    <td>
        <asp:Literal ID="ltOnLineCount" runat="server"></asp:Literal> 个
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        停权用户：
    </td>
    <td>
        <asp:Literal ID="ltDisenableCount" runat="server"></asp:Literal> 个
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        注册总人数：
    </td>
    <td>
        <asp:Literal ID="ltAllCount" runat="server"></asp:Literal> 个
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        手机端注册总人数：
    </td>
    <td>
        <asp:Literal ID="ltMobileRegister" runat="server"></asp:Literal> 个
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        Web推广注册总人数：
    </td>
    <td>
        <asp:Literal ID="ltWebShareRegister" runat="server"></asp:Literal> 个
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        H5注册总人数：
    </td>
    <td>
        <asp:Literal ID="ltH5Register" runat="server"></asp:Literal> 个
    </td>
</tr>
<tr>
    <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
        <div class="hg3  pd7">
            金币统计
        </div>
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        金币总量：
    </td>
    <td>
        <asp:Literal ID="ltScore" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        保险柜总量：
    </td>
    <td>
        <asp:Literal ID="ltInsureScore" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        保险柜+金币总量：
    </td>
    <td>
        <asp:Literal ID="ltAllScore" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
        <div class="hg3  pd7">
            钻石统计
            <span class="total-span">钻石总量：<asp:Literal ID="fkTotal" runat="server"></asp:Literal> 钻石总增长：<asp:Literal runat="server" ID="ltTotalDiamondUp"></asp:Literal> 钻石总消耗：<asp:Literal runat="server" ID="ltTotalDiamondDown"></asp:Literal></span>
        </div>
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        人民币充值总量：
    </td>
    <td>
        <asp:Literal ID="fkRMBPay" runat="server"></asp:Literal> 钻石
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        后台赠送总量：
    </td>
    <td>
        <asp:Literal ID="fkAdminPresent" runat="server"></asp:Literal> 钻石
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        创建房间消耗总量：
    </td>
    <td>
        <asp:Literal ID="fkCreateRoom" runat="server"></asp:Literal> 钻石
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        创建AA房间消耗总量：
    </td>
    <td>
        <asp:Literal ID="fkCreateAARoom" runat="server"></asp:Literal> 钻石
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        兑换金币消耗总量：
    </td>
    <td>
        <asp:Literal ID="fkExchScore" runat="server"></asp:Literal> 钻石
    </td>
</tr>
<tr>
    <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
        <div class="hg3  pd7">
            金币增长统计
            <span class="total-span">金币总增长：<asp:Literal runat="server" ID="ltTotalGoldUp"></asp:Literal></span>
        </div>
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        注册赠送增长：
    </td>
    <td>
        <asp:Literal ID="ltRegPresent" runat="server"></asp:Literal> 金币
    </td>
</tr>
<%--<tr>--%>
<%--    <td class="listTdLeft">--%>
<%--        实名验证赠送：--%>
<%--    </td>--%>
<%--    <td>--%>
<%--        <asp:Literal ID="ltSMPresent" runat="server"></asp:Literal> 金币--%>
<%--    </td>--%>
<%--</tr>--%>
<tr>
    <td class="listTdLeft">
        后台赠送增长：
    </td>
    <td>
        <asp:Literal ID="ltWebPresent" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        钻石兑换金币增长：
    </td>
    <td>
        <asp:Literal ID="ltExchGold" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
        <div class="hg3  pd7">
            税收统计
        </div>
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        总税收：
    </td>
    <td>
        <asp:Literal ID="ltRevenue" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        游戏税收：
    </td>
    <td>
        <asp:Literal ID="ltGameRevenue" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        转账税收：
    </td>
    <td>
        <asp:Literal ID="ltTransferRevenue" runat="server"></asp:Literal> 金币
    </td>
</tr>
<tr>
    <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
        <div class="hg3  pd7">
            损耗统计
        </div>
    </td>
</tr>
<tr>
    <td class="listTdLeft">
        损耗总量：
    </td>
    <td>
        <asp:Literal ID="ltWaste" runat="server"></asp:Literal> 金币
    </td>
</tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td class="titleOpBg Lpd10">
            <input type="button" id="btnRefresh1" class="btn wd1" value="刷新" onclick="javascript:location.href = location.href;"/>
        </td>
    </tr>
</table>
</form>
</body>
</html>