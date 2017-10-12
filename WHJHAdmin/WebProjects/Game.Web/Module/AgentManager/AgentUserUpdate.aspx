<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentUserUpdate.aspx.cs" Inherits="Game.Web.Module.AgentManager.AgentUserUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
            <td width="1232" height="25" valign="top" align="left">你当前位置：用户系统 - 代理账号</td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />                     
                <input class="btnLine" type="button" />  
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
     <table id="table" width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10"><div class="hg3  pd7">
                <asp:Literal ID="litInfo" runat="server"></asp:Literal>代理信息</div></td>
        </tr>
        <tr>
            <td class="listTdLeft">代理级别：</td>
            <td>        
                <asp:DropDownList ID="ddlLevel" runat="server" Enabled="false" Width="156px">
                    <asp:ListItem Text="一级代理" Value="1"></asp:ListItem>
                    <asp:ListItem Text="二级代理" Value="2"></asp:ListItem>
                    <asp:ListItem Text="三级代理" Value="3"></asp:ListItem>
                </asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">真实姓名：</td>
            <td>        
                <asp:TextBox ID="txtCompellation" runat="server" CssClass="text"></asp:TextBox> 
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入真实姓名" ControlToValidate="txtCompellation" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>              
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">代理域名：</td>
            <td>        
                <asp:TextBox ID="txtDomain" runat="server" CssClass="text"></asp:TextBox> 
                <span class="hong">*</span>    
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入代理域名" ControlToValidate="txtDomain" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <br/><label class="lan">二级域名做泛解析 ，一级域名绑定IIS，例如：test.test.com</label> 
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">Q Q 账号：</td>
            <td>        
                <asp:TextBox ID="txtQQAccount" runat="server" CssClass="text"></asp:TextBox>   
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入QQ账号" ControlToValidate="txtQQAccount" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>          
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">微信昵称：</td>
            <td>        
                <asp:TextBox ID="txtWCNickName" data-section="type-2" runat="server" CssClass="text"></asp:TextBox> 
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入微信昵称" ControlToValidate="txtWCNickName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>          
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">联系电话：</td>
            <td>        
                <asp:TextBox ID="txtContactPhone" runat="server" CssClass="text"></asp:TextBox> 
                <span class="hong">*</span>  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入联系电话" ControlToValidate="txtContactPhone" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>        
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="联系电话格式不正确" Display="Dynamic" ControlToValidate="txtContactPhone" ForeColor="Red" ValidationExpression="^\d{11}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">联系地址：</td>
            <td>        
                <asp:TextBox ID="txtContactAddress" runat="server" CssClass="text"></asp:TextBox>   
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请输入联系地址" ControlToValidate="txtContactAddress" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>         
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">代理备注：</td>
            <td>        
                <asp:TextBox ID="txtAgentNote" runat="server" CssClass="text" Width="300px"></asp:TextBox>            
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />                
                <input class="btnLine" type="button" />  
                <asp:Button ID="btnSave1" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
