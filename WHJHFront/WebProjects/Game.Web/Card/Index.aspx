<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.Card.Index" %>
<%@ Import Namespace="Game.Facade" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
</head>
<body>
    <% if (AppConfig.Mode == AppConfig.CodeMode.Dev)  { %>
    <form runat="server">
        <p>输入手机号码：<asp:TextBox ID="txtPhone" runat="server" Text="12345678901"></asp:TextBox></p>
        <p>输入安全密码：<asp:TextBox ID="txtPassword" runat="server" Text="123456"></asp:TextBox></p>
        <p><asp:Button ID="btnAuth" runat="server" Text="登录" OnClick="btnAuth_OnClick" /></p>
    </form>
    <% } %>
</body>
</html>
