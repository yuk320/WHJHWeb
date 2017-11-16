<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpreadReturnConfigInfo.aspx.cs" Inherits="Game.Web.Module.AppManager.SpreadReturnConfigInfo" %>

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
                你当前位置：系统维护 - 推广返利配置
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn" type="button" value="返回" class="btn wd1" onclick="Redirect('SpreadReturnConfigList.aspx');" />
                <asp:Button ID="btnCreate" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
                <div class="hg3  pd7">
                    推广返利信息</div>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                推广级别：
            </td>
            <td>
                <asp:TextBox ID="txtSpreadLevel" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入推广级别" ControlToValidate="txtSpreadLevel" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="推广级别格式不正确" Display="Dynamic" ControlToValidate="txtSpreadLevel" ValidationExpression="^[1-9]\d*$" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
                返利比例：
            </td>
            <td>
                <asp:TextBox ID="txtPresentScale" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">(‰) *</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入返利比例" ControlToValidate="txtPresentScale" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="返利比例格式不正确" Display="Dynamic" ControlToValidate="txtPresentScale" ValidationExpression="^[1-9]\d*$|^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
                返利类型：
            </td>
            <td>
                <%= SpreadReturnType==0?"金币":"钻石" %>
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
                启用禁用：
            </td>
            <td>
                <asp:RadioButtonList ID="rblNullity" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="启用" Value="False" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="禁用" Value="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>  
    </table>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn2" type="button" value="返回" class="btn wd1" onclick="Redirect('SpreadReturnConfigList.aspx');" />
                <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
