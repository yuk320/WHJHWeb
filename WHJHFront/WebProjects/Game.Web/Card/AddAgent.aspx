<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="AddAgent.aspx.cs" Inherits="Game.Web.Card.AddAgent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/form.css" rel="stylesheet" />
    <link href="/Card/Js/layer_mobile/need/layer.css" rel="stylesheet" />
    <script src="/Card/Js/layer_mobile/layer.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <label>
         <span>游戏 I D ：</span>
         <em id="gameid">
            <asp:TextBox ID="txtGameID" runat="server" placeholder="请输入游戏ID"></asp:TextBox>
         </em>
       </label>
        <label>
         <span>用户昵称：</span>
         <em id="account">输入游戏ID验证代理昵称</em>
       </label>
        <label>
         <span>真实姓名：</span>
         <em id="compellation">
            <asp:TextBox ID="txtCompellation" runat="server" placeholder="请输入真实姓名"></asp:TextBox>
         </em>
       </label>
       <label>
         <span>代理域名：</span>
         <em>
            <asp:TextBox ID="txtAgentDomain" runat="server" placeholder="请输入代理域名"></asp:TextBox>
         </em>
       </label>
        <label>
         <span>Q Q 账号：</span>
         <em>
            <asp:TextBox ID="txtQQAccount" runat="server" placeholder="请输入QQ账号"></asp:TextBox>
         </em>
       </label>
       <label>
         <span>微信昵称：</span>
         <em id="nickname">
            <asp:TextBox ID="txtWCNickName" runat="server" placeholder="请输入微信昵称"></asp:TextBox>
         </em>
       </label>
        <label>
         <span>联系电话：</span>
         <em>
            <asp:TextBox ID="txtContactPhone" runat="server" placeholder="请输入联系电话"></asp:TextBox>
         </em>
       </label>
        <label>
         <span>联系地址：</span>
         <em>
            <asp:TextBox ID="txtContactAddress" runat="server" placeholder="请输入联系地址"></asp:TextBox>
         </em>
       </label>
        <label>
         <span>代理备注：</span>
         <em>
            <asp:TextBox ID="txtAgentNote" runat="server" placeholder="请输入代理备注"></asp:TextBox>
         </em>
       </label>
       <label>
            <asp:Button ID="btnSave" runat="server" Text="" CssClass="add_submit" OnClick="btnSave_Click" />
       </label>
    </form>
    <script src="/Card/Js/ajaxname.js" type="text/javascript"></script>
</asp:Content>
