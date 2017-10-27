<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="CostRecord.aspx.cs" Inherits="Game.Web.Card.CostRecord" %>
<%@ Register Src="~/Card/UserControl/Record_Sidebar.ascx" TagName="Sidebar" TagPrefix="sd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/record.css" rel="stylesheet"/>
    <link href="/Card/Js/layer_mobile/need/layer.css" rel="stylesheet"/>
    <link href="/Card/Js/iscroll/pullup-refresh.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <sd:Sidebar ID="sideTitle" runat="server"/>
    <div class="ui-table-box">
        <form runat="server">
            <table class="ui-detail active">
                <thead id="thead">
                <tr>
                    <th>创建时间</th>
                    <th>房间ID</th>
                    <th>消耗钻石</th>
                    <th>解散时间</th>
                </tr>
                </thead>
            </table>
            <div id="wrapper">
                <table class="ui-detail active">
                    <tbody data-url="/Card/DataHandler.ashx?action=getcostdiamondlist" id="list">
                    </tbody>
                </table>
            </div>
        </form>
    </div>
    <script src="/Card/Js/iscroll/iscroll-probe.js"></script>
    <script src="/Card/Js/iscroll/pullup-refresh.js"></script>
    <script src="/Card/Js/layer_mobile/layer.js"></script>
    <script src="/Card/Js/iscroll/load.js"></script>
</asp:Content>