<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="Present.aspx.cs" Inherits="Game.Web.Card.Present" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/present.css" rel="stylesheet"/>
    <link href="/Card/Js/layer_mobile/need/layer.css" rel="stylesheet"/>
    <script src="/Card/Js/layer_mobile/layer.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-content">
        <div>
            身上钻石：
            <span>
                <asp:Label ID="lbDiamond" runat="server" cssClass="ui-count" Text=""></asp:Label>
            </span>
        </div>
        <div>
            今日赠送：
            <span>
                <asp:Label ID="lbPresentDiamond" runat="server" cssClass="ui-price" Text=""></asp:Label>
            </span>
        </div>
    </div>
    <div class="ui-title">
        <span>
            <img src="/Card/Image/nav_ico/4_active.png">
        </span>钻石赠送
    </div>
    <form runat="server">
        <label>
            <span>赠送对象：</span>
            <em id="gameid">
                <asp:TextBox ID="txtGameID" runat="server" placeholder="请输入赠送GameID"></asp:TextBox>
            </em>
        </label>
        <label>
            <span>赠送昵称：</span>
            <em id="account">输入赠送对象验证对象昵称</em>
        </label>
        <label>
            <span>赠送数量：</span>
            <em>
                <asp:TextBox ID="txtPresentCount" runat="server" placeholder="请输入赠送数量"></asp:TextBox>
            </em>
        </label>
        <label>
            <span>赠送备注：</span>
            <em>
                <asp:TextBox ID="txtNote" runat="server" placeholder="备注"></asp:TextBox>
            </em>
        </label>
        <label>
            <span>安全密码：</span>
            <em>
                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" placeholder="请输入安全密码"></asp:TextBox>
            </em>
        </label>
        <label class="ui-present">
            <asp:Button ID="btnPresent" runat="server" Text="" OnClick="btnPresent_Click"/>
        </label>
    </form>
    <script src="/Card/Js/ajaxname.js" type="text/javascript"></script>
</asp:Content>