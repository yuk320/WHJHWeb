<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="Game.Web.Card.Success" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-correct"><img src="/Card/Image/correct.png"></div>
    <div class="ui-correct-info"><%=TipInfo %></div>
    <div class="ui-correct-link">
        <%=LinkInfo %>
    </div>
</asp:Content>
