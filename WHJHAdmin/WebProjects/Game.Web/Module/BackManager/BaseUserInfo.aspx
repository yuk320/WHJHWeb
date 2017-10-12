<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseUserInfo.aspx.cs" Inherits="Game.Web.Module.BackManager.BaseUserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
            <td width="1232" height="25" valign="top" align="left">你当前位置：后台系统 - 管理员管理</td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="28">
                <ul>
	                <li class="tab2" onclick="Redirect('BaseRoleList.aspx')">角色管理</li>
	                <li class="tab1">用户管理</li>
                </ul>
            </td>
        </tr>
    </table>  
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('BaseUserList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10"><div class="hg3  pd7">
                <asp:Literal ID="litInfo" runat="server"></asp:Literal>用户信息</div></td>
        </tr>
        <tr>
            <td class="listTdLeft">登录帐号：</td>
            <td>        
                <asp:TextBox ID="txtAccounts" runat="server" CssClass="text"></asp:TextBox>    
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入登录帐号" Display="Dynamic" ControlToValidate="txtAccounts" ForeColor="Red"></asp:RequiredFieldValidator>           
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">登录密码：</td>
            <td>        
                <asp:TextBox ID="txtLogonPass" runat="server" CssClass="text" TextMode="Password" onchange="PwdChange()"></asp:TextBox> 
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入登录密码" Display="Dynamic" ControlToValidate="txtLogonPass" ForeColor="Red"></asp:RequiredFieldValidator>  
                <asp:HiddenField ID="hidfLogonPass" runat="server" />            
            </td>
        </tr>   
        <tr>
            <td class="listTdLeft">确认密码：</td>
            <td>        
                <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="text" TextMode="Password"></asp:TextBox>   
                <span class="hong">*</span>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码输入不一致" Display="Dynamic" ControlToValidate="txtConfirmPass" ControlToCompare="txtLogonPass" ForeColor="Red"></asp:CompareValidator>
            </td>
        </tr>      
        <tr>
            <td class="listTdLeft">用户角色：</td>
            <td>        
                <asp:DropDownList ID="ddlRole" runat="server" Width="157px"> 
                </asp:DropDownList>       
            </td>
        </tr>  
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('BaseUserList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    function PwdChange() {
        var pwd2 = document.getElementById("<%=hidfLogonPass.ClientID %>");
        pwd2.value = "********";
    }
</script>