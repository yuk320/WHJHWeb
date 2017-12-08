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
        <p>输入GameID：<asp:TextBox ID="TextBox1" runat="server" Text="100101"></asp:TextBox></p>
        <p><asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" /></p>
    </form>
    <% } %>
</body>
</html>
