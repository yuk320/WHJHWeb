<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameGameItemInfo.aspx.cs" Inherits="Game.Web.Module.AppManager.GameGameItemInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../scripts/common.js"></script>

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
                你当前位置：系统维护 - 游戏管理
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('GameGameItemList.aspx')" />
                <input class="btnLine" type="button" />
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
                <div class="hg3  pd7">
                    模块信息</div>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                模块标识：
            </td>
            <td>
                <asp:TextBox ID="txtGameID" runat="server" CssClass="text" MaxLength="9"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入模块标识" Display="Dynamic" ControlToValidate="txtGameID" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="模块标识格式不正确" Display="Dynamic" ControlToValidate="txtGameID" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                模块名称：
            </td>
            <td>
                <asp:TextBox ID="txtGameName" runat="server" CssClass="text" MaxLength="31"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入模块名称" Display="Dynamic" ControlToValidate="txtGameName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                数据库名：
            </td>
            <td>
                <asp:TextBox ID="txtDataBaseName" runat="server" CssClass="text" MaxLength="31"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入数据库名" Display="Dynamic" ControlToValidate="txtDataBaseName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                数据库地址：
            </td>
            <td>
                <asp:DropDownList ID="ddlDataBaseAddr" runat="server" Width="158px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                服务端版本：
            </td>
            <td>
                <asp:TextBox ID="txtServerVersion" runat="server" CssClass="text" Text="0.0.0.0"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请输入服务端版本" Display="Dynamic" ControlToValidate="txtServerVersion" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="服务端版本格式不正确" Display="Dynamic" ControlToValidate="txtServerVersion" ForeColor="Red" ValidationExpression="^[0-9].[0-9].[0-9].[0-9]$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                客户端版本：
            </td>
            <td>
                <asp:TextBox ID="txtClientVersion" runat="server" CssClass="text" Text="0.0.0.0"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="请输入客户端版本" Display="Dynamic" ControlToValidate="txtClientVersion" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="客户端版本格式不正确" Display="Dynamic" ControlToValidate="txtClientVersion" ForeColor="Red" ValidationExpression="^[0-9].[0-9].[0-9].[0-9]$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                服务端名称：
            </td>
            <td>
                <asp:TextBox ID="txtServerDLLName" runat="server" CssClass="text" MaxLength="32"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入服务端名称" Display="Dynamic" ControlToValidate="txtServerDLLName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                客户端名称：
            </td>
            <td>
                <asp:TextBox ID="txtClientExeName" runat="server" CssClass="text" MaxLength="32"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入客户端名称" Display="Dynamic" ControlToValidate="txtClientExeName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('GameGameItemList.aspx')" />
                <input class="btnLine" type="button" />
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
