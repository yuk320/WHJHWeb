<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileKindInfo.aspx.cs" Inherits="Game.Web.Module.AppManager.MobileKindInfo" %>

<!DOCTYPE html>

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
            <td width="19" height="25" valign="top"  class="Lpd10"><div class="arr"></div></td>
            <td width="1232" height="25" valign="top" align="left">你当前位置：游戏系统 - 游戏管理</td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('MobileKindList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10"><div class="hg3  pd7">
                游戏信息</div></td>
        </tr>
        <tr>
            <td class="listTdLeft">选择模块：</td>
            <td>        
                <asp:DropDownList ID="ddlKind" runat="server" CssClass="text" Width="155px" Height="25px"></asp:DropDownList>
            </td>
        </tr>   
        <tr>
            <td class="listTdLeft">安装包名：</td>
            <td>        
                <asp:TextBox ID="txtModuleName" runat="server" CssClass="text" MaxLength="32"></asp:TextBox>
                <span class="hong">*</span><span class="lan">填写游戏安装包的名称</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入安装包名" ControlToValidate="txtModuleName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>             
            </td>
        </tr>    
        <tr>
            <td class="listTdLeft">模块版本号：</td>
            <td>        
                <asp:TextBox ID="txtClientVersion" runat="server" CssClass="text" Text="0.0.0.0"></asp:TextBox> 
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请输入模块版本号" Display="Dynamic" ControlToValidate="txtClientVersion" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="模块版本号格式不正确" Display="Dynamic" ControlToValidate="txtClientVersion" ForeColor="Red" ValidationExpression="^[0-9].[0-9].[0-9].[0-9]$"></asp:RegularExpressionValidator>
            </td>
        </tr>     
        <tr>
            <td class="listTdLeft">资源版本号：</td>
            <td>        
                <asp:TextBox ID="txtResVersion" runat="server" CssClass="text"></asp:TextBox> 
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入资源版本号" ControlToValidate="txtResVersion" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="请输入正确资源版本号" Display="Dynamic" ControlToValidate="txtResVersion" ValidationExpression="^([0-9]){1,9}?$" ForeColor="Red"></asp:RegularExpressionValidator>               
            </td>
        </tr>  
        <tr>
            <td class="listTdLeft">游戏排序：</td>
            <td>        
                <asp:TextBox ID="txtSortID" runat="server" CssClass="text" MaxLength="9" Text="0"></asp:TextBox> 
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入排序" ControlToValidate="txtSortID" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="排序格式不正确" Display="Dynamic" ControlToValidate="txtSortID" ValidationExpression="^\d*$" ForeColor="Red"></asp:RegularExpressionValidator>               
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height:20px;"></td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('MobileKindList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
