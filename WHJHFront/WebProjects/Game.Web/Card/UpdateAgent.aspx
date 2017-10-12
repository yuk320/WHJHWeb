<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="UpdateAgent.aspx.cs" Inherits="Game.Web.Card.UpdateAgent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/form.css" rel="stylesheet" />
    <link href="/Card/Js/layer_mobile/need/layer.css" rel="stylesheet" />
    <script src="/Card/Js/layer_mobile/layer.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <label>
         <span>Q Q 账号：</span>
         <em>
            <asp:TextBox ID="txtQQAccount" runat="server" placeholder="请输入Q Q 账号"></asp:TextBox>
         </em>
       </label>
        <label>
         <span>联系电话：</span>
         <em>
            <asp:TextBox ID="txtPhone" runat="server" placeholder="请输入手机号码"></asp:TextBox>
         </em>
       </label>
       <label>
         <span>联系地址：</span>
         <em>
            <asp:TextBox ID="txtAddress" runat="server" placeholder="请输入联系地址"></asp:TextBox>
         </em>
       </label>
       <label>
            <asp:Button ID="btnSave" runat="server" CssClass="update_submit" Text="" OnClick="btnSave_Click" />
       </label>
    </form>
</asp:Content>
