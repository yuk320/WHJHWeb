<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Game.Web.Card.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/form.css" rel="stylesheet" />
    <link href="/Card/Js/layer_mobile/need/layer.css" rel="stylesheet" />
    <script src="/Card/Js/layer_mobile/layer.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <label>
         <span>游戏ID：</span>
         <em id="gameid">
            <asp:TextBox ID="txtGameID" runat="server" placeholder="请输入游戏ID"></asp:TextBox>
         </em>
       </label>
        <label>
         <span>用户昵称：</span>
         <em id="account">输入游戏ID验证添加下线昵称</em>
       </label>
       <label>
            <asp:Button ID="btnBind" runat="server" Text="" CssClass="adduser_submit" OnClick="btnBind_Click" />
       </label>
    </form>
    <script src="/Card/Js/ajaxname.js" type="text/javascript"></script>
</asp:Content>
