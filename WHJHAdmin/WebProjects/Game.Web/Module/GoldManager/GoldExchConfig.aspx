<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoldExchConfig.aspx.cs" Inherits="Game.Web.Module.GoldManager.GoldExchConfig" %>

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
                你当前位置：金币系统 - 兑换管理
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn" type="button" value="返回" class="btn wd1" onclick="Redirect('GoldExchConfigList.aspx')" />
                <asp:Button ID="btnCreate" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
                <div class="hg3  pd7">
                    配置信息</div>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                产品名称：
            </td>
            <td>
                <asp:TextBox ID="txtProductName" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">* （用于显示商品的名称）</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入产品名称" Display="Dynamic" ControlToValidate="txtProductName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
                兑换钻石：
            </td>
            <td>
                <asp:TextBox ID="txtCurrency" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入兑换钻石" Display="Dynamic" ControlToValidate="txtCurrency" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="兑换钻石格式不正确" Display="Dynamic" ControlToValidate="txtCurrency" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
            </td>
        </tr>  
        <tr>
            <td class="listTdLeft">
                赠送金币：
            </td>
            <td>
                <asp:TextBox ID="txtGold" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入赠送金币" Display="Dynamic" ControlToValidate="txtGold" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="赠送金币格式不正确" Display="Dynamic" ControlToValidate="txtGold" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
            </td>
        </tr>  
        <tr>
            <td class="listTdLeft">
                图标类型：
            </td>
            <td>
              <asp:TextBox runat="server" ID="txtImageType" CssClass="text"></asp:TextBox>
              <span class="hong">* （大于0的正整数，具体根据图标决定）</span>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入图标类型" Display="Dynamic" ControlToValidate="txtImageType" ForeColor="Red"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="图标类型格式不正确" Display="Dynamic" ControlToValidate="txtImageType" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
            </td>
        </tr>   
        <tr>
            <td class="listTdLeft">
                排序标识：
            </td>
            <td>
                <asp:TextBox ID="txtSortID" runat="server" CssClass="text"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入排序标识" Display="Dynamic" ControlToValidate="txtSortID" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="排序标识格式不正确" Display="Dynamic" ControlToValidate="txtSortID" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
            </td>
        </tr>    
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn2" type="button" value="返回" class="btn wd1" onclick="Redirect('GoldExchConfigList.aspx')" />
                <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
