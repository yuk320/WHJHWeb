<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBaseInfoInfo.aspx.cs" Inherits="Game.Web.Module.AppManager.DataBaseInfoInfo" %>

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
            <td width="1232" height="25" valign="top" align="left">你当前位置：系统维护 - 机器管理</td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('DataBaseInfoList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10"><div class="hg3  pd7">机器信息</div></td>
        </tr>
        <tr>
            <td class="listTdLeft">机器名称：</td>
            <td>        
                <asp:TextBox ID="txtInformation" runat="server" CssClass="text"></asp:TextBox>   
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入机器名称" Display="Dynamic" ControlToValidate="txtInformation" ForeColor="Red"></asp:RequiredFieldValidator>            
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">地址：</td>
            <td>        
                <asp:TextBox ID="txtDBAddr" runat="server" CssClass="text" MaxLength="15"></asp:TextBox>  
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入地址" Display="Dynamic" ControlToValidate="txtDBAddr" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="地址格式不正确" Display="Dynamic" ControlToValidate="txtDBAddr" ForeColor="Red" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"></asp:RegularExpressionValidator>             
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">端口：</td>
            <td>        
                <asp:TextBox ID="txtDBPort" runat="server" CssClass="text" MaxLength="9"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入端口" Display="Dynamic" ControlToValidate="txtDBPort" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="端口格式不正确" Display="Dynamic" ControlToValidate="txtDBPort" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>               
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">帐号：</td>
            <td>        
                <asp:TextBox ID="txtDBUser" runat="server" CssClass="text" MaxLength="512"></asp:TextBox>   
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入帐号" Display="Dynamic" ControlToValidate="txtDBUser" ForeColor="Red"></asp:RequiredFieldValidator>            
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">密码：</td>
            <td>        
                <asp:TextBox ID="txtDBPassword" runat="server" CssClass="text" TextMode="Password" onchange="DBPwdChange()" MaxLength="512"></asp:TextBox>  
                <asp:HiddenField ID="hdfDBPassword" runat="server" />  
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入密码" Display="Dynamic" ControlToValidate="txtDBPassword" ForeColor="Red"></asp:RequiredFieldValidator>           
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">机器码：</td>
            <td>        
                <asp:TextBox ID="txtMachineID" runat="server" CssClass="text" MaxLength="32"></asp:TextBox>             
            </td>
        </tr>          
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('DataBaseInfoList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="btnSave1" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<script type="text/javascript">
    function DBPwdChange() {
        document.getElementById("<%=hdfDBPassword.ClientID %>").value = "********";
    }
</script>

