<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="UpdatePass.aspx.cs" Inherits="Game.Web.Card.UpdatePass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/form.css" rel="stylesheet" />
    <link href="/Card/Js/layer_mobile/need/layer.css" rel="stylesheet" />
    <script src="/Card/Js/layer_mobile/layer.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <label runat="server" id="oldpassword">
          <span>原安全密码：</span>
          <em>
              <asp:TextBox ID="txtLoginPass" TextMode="Password" runat="server" placeholder="请输入原密码"></asp:TextBox>
          </em>
        </label>
        <label>
          <span>新安全密码：</span>
          <em>
            <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" placeholder="请输入新密码"></asp:TextBox>
          </em>
        </label>
        <label>
          <span>确认安全密码：</span>
          <em>
            <asp:TextBox ID="txtRePass" TextMode="Password" runat="server" placeholder="请输入确认密码"></asp:TextBox>
          </em>
        </label>
        <label>
            <asp:Button ID="btnSave" runat="server" CssClass="update_submit" Text="" OnClick="btnSave_Click" />
        </label>
    </form>
</asp:Content>
