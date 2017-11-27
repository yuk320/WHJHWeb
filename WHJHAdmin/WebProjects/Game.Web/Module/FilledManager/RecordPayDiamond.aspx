<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordPayDiamond.aspx.cs" Inherits="Game.Web.Module.FilledManager.RecordPayDiamond" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../scripts/common.js"></script>

    <script type="text/javascript" src="../../scripts/comm.js"></script>

    <script type="text/javascript" src="../../scripts/My97DatePicker/WdatePicker.js"></script>

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
                你当前位置：充值系统 - 充值记录
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

                <asp:DropDownList ID="ddlGlobalShareInfo" runat="server">
                    <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                    <asp:ListItem Text="手机微信充值" Value="101"></asp:ListItem>
                    <asp:ListItem Text="H5微信充值" Value="102"></asp:ListItem>
                    <asp:ListItem Text="手机支付宝充值" Value="201"></asp:ListItem>
                    <asp:ListItem Text="手机零钱充值" Value="301"></asp:ListItem>
                    <asp:ListItem Text="手机苹果充值" Value="800"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="btn wd1" OnClick="btnQuery_Click" />
                <asp:Button ID="btnQueryTD" runat="server" Text="今天" CssClass="btn wd1" OnClick="btnQueryTD_Click" />
                <asp:Button ID="btnQueryYD" runat="server" Text="昨天" CssClass="btn wd1" OnClick="btnQueryYD_Click" />
                <asp:Button ID="btnQueryTW" runat="server" Text="本周" CssClass="btn wd1" OnClick="btnQueryTW_Click" />
                <asp:Button ID="btnQueryYW" runat="server" Text="上周" CssClass="btn wd1" OnClick="btnQueryYW_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg Tmg7">
        <tr>
            <td class="listTdLeft" style="width: 80px">
                用户查询：
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="text"></asp:TextBox>
                <asp:DropDownList ID="ddlSearchType" runat="server" >
                    <asp:ListItem Value="0">按订单号码</asp:ListItem>
                    <asp:ListItem Value="1">按游戏ID</asp:ListItem>
                    <asp:ListItem Value="2">按用户昵称</asp:ListItem>
                    <asp:ListItem Value="3">按用户标识</asp:ListItem>
                 </asp:DropDownList>
                 <asp:DropDownList ID="ddlPayStatus" runat="server" >
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                    <asp:ListItem Value="0">未支付</asp:ListItem>
                    <asp:ListItem Value="1">已支付</asp:ListItem>
                 </asp:DropDownList>
                <asp:Button ID="btnQueryAcc" runat="server" Text="查询" CssClass="btn wd1" OnClick="btnQueryAcc_Click" />
                <span class="total-span"><asp:Literal ID="ltTotal" runat="server"></asp:Literal></span>
            </td>
        </tr>
    </table>
    <div id="content">
         <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box Tmg7" id="list">
            <tr align="center" class="bold">
                <td class="listTitle">
                    订单时间
                </td>
                <td class="listTitle2">
                    订单号码
                </td>
                <td class="listTitle2">
                    充值类型
                </td>
                <td class="listTitle2">
                    游戏ID
                </td>
                <td class="listTitle2">
                    玩家昵称
                </td>
                <td class="listTitle2">
                    支付金额
                </td>
                <td class="listTitle2">
                    充值前钻石
                </td>
                <td class="listTitle2">
                    充值钻石
                </td>
                <td class="listTitle2">
                    额外赠送钻石
                </td>
                <td class="listTitle2">
                    充值后钻石
                </td>
                <td class="listTitle2">
                    充值状态
                </td>
                <td class="listTitle2">
                    订单地址
                </td>
            </tr>
            <asp:Repeater ID="rptShareInfo" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval("OrderDate")%>
                        </td>
                        <td>
                            <%# Eval("OrderID")%>
                        </td>
                        <td>
                            <%# GetOrderShareName(Convert.ToInt32(Eval("ShareID")))%>
                        </td>
                        <td>
                            <%# Eval("GameID")%>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0);" onclick="openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx?param=<%#Eval("UserID").ToString() %>','<%#Eval("UserID").ToString() %>',850,790);">
                            <%# Eval("NickName")%>
                            </a>
                        </td>
                        <td>
                            <%# Eval("Amount")%>
                        </td>
                        <td>
                            <%# Convert.ToInt32(Eval("OrderStatus"))==1?Eval("BeforeDiamond"):"——"%>
                        </td>
                        <td>
                            <%# Eval("Diamond")%>
                        </td>
                        <td>
                            <%# Eval("OtherPresent")%>
                        </td>
                        <td>
                            <%# Convert.ToInt32(Eval("OrderStatus"))==1?""+(Convert.ToInt32(Eval("BeforeDiamond"))+Convert.ToInt32(Eval("Diamond"))+Convert.ToInt32(Eval("OtherPresent"))):"——"%>
                        </td>
                        <td>
                            <%# GetPayStatus(Convert.ToInt32(Eval("OrderStatus"))) %>
                        </td>
                        <td>
                            <%# Eval("OrderAddress")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval("OrderDate")%>
                        </td>
                        <td>
                            <%# Eval("OrderID")%>
                        </td>
                        <td>
                            <%# GetOrderShareName(Convert.ToInt32(Eval("ShareID")))%>
                        </td>
                        <td>
                            <%# Eval("GameID")%>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0);" onclick="openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx?param=<%#Eval("UserID").ToString() %>','<%#Eval("UserID").ToString() %>',850,790);">
                            <%# Eval("NickName")%>
                            </a>
                        </td>
                        <td>
                            <%# Eval("Amount")%>
                        </td>
                        <td>
                            <%# Convert.ToInt32(Eval("OrderStatus"))==1?Eval("BeforeDiamond"):"——"%>
                        </td>
                        <td>
                            <%# Eval("Diamond")%>
                        </td>
                        <td>
                            <%# Eval("OtherPresent")%>
                        </td>
                        <td>
                            <%# Convert.ToInt32(Eval("OrderStatus"))==1?""+(Convert.ToInt32(Eval("BeforeDiamond"))+Convert.ToInt32(Eval("Diamond"))+Convert.ToInt32(Eval("OtherPresent"))):"——"%>
                        </td>
                        <td>
                            <%# GetPayStatus(Convert.ToInt32(Eval("OrderStatus"))) %>
                        </td>
                        <td>
                            <%# Eval("OrderAddress")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <asp:Literal runat="server" ID="litNoData" Visible="false" Text="<tr class='tdbg'><td colspan='100' align='center'><br>没有任何信息!<br><br></td></tr>"></asp:Literal>
        </table>
    </div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" class="page">
                <gsp:AspNetPager ID="anpNews" runat="server" OnPageChanged="anpNews_PageChanged" AlwaysShow="true" FirstPageText="首页" LastPageText="末页"
                    PageSize="20" NextPageText="下页" PrevPageText="上页" ShowBoxThreshold="0" ShowCustomInfoSection="Left" LayoutType="Table"
                    NumericButtonCount="5" CustomInfoHTML="总记录：%RecordCount%　页码：%CurrentPageIndex%/%PageCount%　每页：%PageSize%">
                </gsp:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
