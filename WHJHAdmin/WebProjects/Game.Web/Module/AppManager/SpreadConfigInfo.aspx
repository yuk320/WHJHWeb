<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpreadConfigInfo.aspx.cs" Inherits="Game.Web.Module.AppManager.SpreadConfigInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../scripts/common.js"></script>

    <script type="text/javascript" src="../../scripts/comm.js"></script>
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
                你当前位置：系统维护 - 推广配置
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn" type="button" value="返回" class="btn wd1" onclick="Redirect('SpreadConfigList.aspx')" />
                <asp:Button ID="btnCreate" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
                <div class="hg3  pd7">
                    推广信息</div>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                推广人数：
            </td>
            <td>
                <asp:TextBox ID="txtSpreadNum" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入推广人数" ControlToValidate="txtSpreadNum" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="推广人数格式不正确" Display="Dynamic" ControlToValidate="txtSpreadNum" ValidationExpression="^[1-9]\d*$" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
                奖励钻石：
            </td>
            <td>
                <asp:TextBox ID="txtDiamond" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入奖励钻石" ControlToValidate="txtDiamond" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="奖励钻石格式不正确" Display="Dynamic" ControlToValidate="txtDiamond" ValidationExpression="^[1-9]\d*$" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
                奖励道具：
            </td>
            <td>
                <asp:DropDownList ID="ddlPropID" runat="server" Width="155" Height="24" CssClass="text">
                    <asp:ListItem Text="无奖励" Value="0"></asp:ListItem>
                    <asp:ListItem Text="大喇叭" Value="306"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
                奖励数量：
            </td>
            <td>
                <asp:TextBox ID="txtPropNum" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入奖励数量" ControlToValidate="txtPropNum" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="奖励数量格式不正确" Display="Dynamic" ControlToValidate="txtPropNum" ValidationExpression="^\d*$" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr>  
    </table>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn2" type="button" value="返回" class="btn wd1" onclick="Redirect('SpreadConfigList.aspx')" />
                <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
