<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSuperUser.aspx.cs" Inherits="Game.Web.Module.AccountManager.AddSuperUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <link href="../../styles/layout.css" rel="stylesheet" type="text/css"/>
  <script type="text/javascript" src="../../scripts/common.js"></script>
  <script type="text/javascript" src="/scripts/jquery.js"></script>
  <title></title>
  <script>
    $('#form1').on('submit',
      function() {
         if ($('#txtPassword').val() !== $('#txtRePassword')) {
           alert('两次密码不一致');
           return false;
         }
        return true;
      });
  </script>
</head>
<body>
<form id="form1" runat="server">
  <!-- 头部菜单 Start -->
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
    <tr>
      <td width="19" height="25" valign="top" class="Lpd10">
        <div class="arr"></div>
      </td>
      <td width="1232" height="25" valign="top" align="left">你当前位置：用户系统 - 添加超端账号</td>
    </tr>
  </table>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td class="titleOpBg Lpd10">
        <input type="button" value="关闭" class="btn wd1" onclick="window.close();"/>
        <input class="btnLine" type="button"/>
        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1"
                    onclick="btnSave_Click"/>
      </td>
    </tr>
  </table>
  <asp:ScriptManager ID="ScriptManager2" runat="server">
  </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
    <ContentTemplate>
      <table id="table" width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
          <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
            <div class="hg3  pd7">
              超端用户信息
            </div>
          </td>
        </tr>
        <tr>
          <td class="listTdLeft">用户名：</td>
          <td>
            <asp:TextBox runat="server" ID="txtAccounts"></asp:TextBox>
            <span class="hong">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="请输入用户名" ControlToValidate="txtAccounts" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td class="listTdLeft">登录密码：</td>
          <td>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="text"></asp:TextBox>
            <span class="hong">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="请输入登录密码" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td class="listTdLeft">确认密码：</td>
          <td>
            <asp:TextBox ID="txtRePassword" runat="server" CssClass="text"></asp:TextBox>
            <span class="hong">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入确认密码" ControlToValidate="txtRePassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td class="listTdLeft">赠送金币（携带）：</td>
          <td>
            <asp:TextBox ID="txtGrantGold" runat="server" CssClass="text" Text="0"></asp:TextBox>
            <span class="hong">* 请不要超过21亿哦</span>
          </td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td class="titleOpBg Lpd10">
        <input type="button" value="关闭" class="btn wd1" onclick="window.close();"/>
        <input class="btnLine" type="button"/>
        <asp:Button ID="btnSave1" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click"/>
      </td>
    </tr>
  </table>
</form>
</body>
</html>
