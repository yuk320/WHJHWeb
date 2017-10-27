<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppPayConfigInfo.aspx.cs" Inherits="Game.Web.Module.FilledManager.AppPayConfigInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css"/>

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
                你当前位置：充值系统 - 充值配置
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn" type="button" value="返回" class="btn wd1" onclick="Redirect('AppPayConfigList.aspx')"/>
                <asp:Button ID="btnCreate" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click"/>
            </td>
        </tr>
    </table>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
                <tr>
                    <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
                        <div class="hg3  pd7">
                            产品信息
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="listTdLeft">
                        产品类别：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProductType" runat="server" Width="155" Height="24" CssClass="text" AutoPostBack="True" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                            <asp:ListItem Text="普通充值" Value="0"></asp:ListItem>
                            <asp:ListItem Text="苹果内购" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="apple" runat="server">
                    <td class="listTdLeft">
                        苹果内购标识：
                    </td>
                    <td>
                        <asp:TextBox ID="txtAppleID" runat="server" CssClass="text"></asp:TextBox>
                        <span class="hong">*</span>
                    </td>
                </tr>
                <tr>
                    <td class="listTdLeft">
                        产品名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="text"></asp:TextBox>
                        <span class="hong">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入产品名称" Display="Dynamic" ControlToValidate="txtProductName" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="listTdLeft">
                        产品价格：
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="text"></asp:TextBox>
                        <span class="hong">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入产品价格" Display="Dynamic" ControlToValidate="txtPrice" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="产品价格格式不正确" Display="Dynamic" ControlToValidate="txtPrice" ForeColor="Red" ValidationExpression="^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="listTdLeft">
                        赠送钻石：
                    </td>
                    <td>
                        <asp:TextBox ID="txtCurrency" runat="server" CssClass="text"></asp:TextBox>
                        <span class="hong">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入赠送钻石" Display="Dynamic" ControlToValidate="txtCurrency" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="赠送钻石格式不正确" Display="Dynamic" ControlToValidate="txtCurrency" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <tr>
                    <td class="listTdLeft">
                        排序标识：
                    </td>
                    <td>
                        <asp:TextBox ID="txtSortID" runat="server" CssClass="text"></asp:TextBox>
                        <span class="hong">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入排序标识" Display="Dynamic" ControlToValidate="txtSortID" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="排序标识格式不正确" Display="Dynamic" ControlToValidate="txtSortID" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="listTdLeft">
                        图标类型：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbImage" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="少量钻石图标" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="普通钻石图标" Value="2"></asp:ListItem>
                            <asp:ListItem Text="多数钻石图标" Value="3"></asp:ListItem>
                            <asp:ListItem Text="大量钻石图标" Value="4"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="listTdLeft">
                        充值标志：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbIdentity" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbIdentity_OnSelectedIndexChanged">
                            <asp:ListItem Text="普通" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="推荐" Value="1"></asp:ListItem>
                            <asp:ListItem Text="首充" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="scale" runat="server">
                    <td class="listTdLeft">
                        首冲额外赠送钻石：
                    </td>
                    <td>
                        <asp:TextBox ID="txtOtherPresent" runat="server" CssClass="text" Text="0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入赠送数量" Display="Dynamic" ControlToValidate="txtOtherPresent" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="赠送数量格式不正确" Display="Dynamic" ControlToValidate="txtOtherPresent" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn2" type="button" value="返回" class="btn wd1" onclick="Redirect('AppPayConfigList.aspx')"/>
                <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click"/>
            </td>
        </tr>
    </table>
</form>
</body>
</html>