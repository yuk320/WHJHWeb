<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UMessagePushAll.aspx.cs" Inherits="Game.Web.Module.WebManager.UMessagePushAll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>推送消息</title>
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/common.js"></script>
    <script type="text/javascript" src="../../scripts/jquery.js"></script>
    <script type="text/javascript" src="../../scripts/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <!-- 头部菜单 Start -->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top" class="Lpd10">
                <div class="arr">
                </div>
            </td>
            <td width="1232" height="25" valign="top" align="left">
                目前操作功能：网站系统 - 发送消息
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <form runat="server" id="form1">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />
                <asp:Button ID="btnSave" runat="server" Text="确认" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2" style="height:324px;">
            <tr>
                <td class="listTdLeft">
                    推送对象：
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                        <asp:ListItem Text="全部用户" Value="0"></asp:ListItem>
                        <asp:ListItem Text="代理商用户" Value="1"></asp:ListItem>
                        <asp:ListItem Text="普通用户" Value="2"></asp:ListItem>
                        <asp:ListItem Text="安卓用户" Value="3"></asp:ListItem>
                        <asp:ListItem Text="苹果用户" Value="4"></asp:ListItem>
                        <asp:ListItem Text="注册时间" Value="5"></asp:ListItem>
                        <asp:ListItem Text="未登录天数" Value="6"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="gameid" runat="server">
                <td class="listTdLeft">
                    代理商游戏ID：
                </td>
                <td>
                    <asp:TextBox ID="txtGameID" runat="server" CssClass="text wd8" Text="0"></asp:TextBox>&nbsp;
                    <span class="hong">0 表示给所有代理商发推送消息，否则该代理商一级下线发推送消息</span>
                </td>
            </tr>
            <tr id="time" runat="server">
                <td class="listTdLeft">
                    注册时间：
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="text wd2" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtEndDate\')}'})"></asp:TextBox><img
                    src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtEndDate\')}'})"
                    style="cursor: pointer; vertical-align: middle" />
                至
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="text wd2" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtStartDate\')}'})"></asp:TextBox><img
                    src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtStartDate\')}'})"
                    style="cursor: pointer; vertical-align: middle" />
                    <span class="hong">不填 表示全部推送</span>
                </td>
            </tr>
            <tr id="logincount" runat="server">
                <td class="listTdLeft">
                    未登录天数：
                </td>
                <td>
                    <asp:TextBox ID="txtNoLoginDay" runat="server" CssClass="text wd8" Text="0"></asp:TextBox>&nbsp;
                    <span class="hong">0 表示表示全部推送</span>
                </td>
            </tr>
            <tr>
                <td class="listTdLeft">
                    消息描述：
                </td>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="50" Width="300" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="listTdLeft">
                    推送时间：
                </td>
                <td>
                    <asp:TextBox ID="txtTime" runat="server" CssClass="text wd8" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox><img src="../../Images/btn_calendar.gif"
                            onclick="WdatePicker({el:'txtTime',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" style="cursor: pointer; vertical-align: middle" />
                </td>
            </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />
                <asp:Button ID="btnSave1" runat="server" Text="确认" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
