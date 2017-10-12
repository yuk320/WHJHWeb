<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="SetPassword.aspx.cs" Inherits="Game.Web.Card.SetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-correct"><img src="<%=TipImg %>"></div>
    <div class="ui-correct-info"><%=TipInfo %></div>
    <div class="ui-correct-link">
        <a href="/Card/UpdatePass.aspx">修改安全密码</a>&nbsp;
        <a href="/Card/Present.aspx">继续赠送钻石</a></div>
</asp:Content>
