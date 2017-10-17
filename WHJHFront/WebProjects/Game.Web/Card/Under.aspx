<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="Under.aspx.cs" Inherits="Game.Web.Card.Under" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/record.css" rel="stylesheet"/>
    <link href="/Card/Js/layer_mobile/need/layer.css" rel="stylesheet"/>
    <link href="/Card/Js/iscroll/pullup-refresh.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input id="hidType" type="hidden" value="<%= Type %>"/>
    <p class="ui-tab-num">总<em id="typeLab">代理</em>人数： <span id="pCount"></span>
      <a id="btnAddAgent" href="/Card/AddAgent.aspx" style="float: right;padding-right: 150px;">添加代理</a></p>
    <ul class="ui-tab ui-tab-under fn-clean-space">
        <li data-range="all" class="active">
            <a id="aAll" href="javascript:;">所有</a>
        </li>
        <li data-range="month">
            <a id="aMonth" href="javascript:;">Top50本月售卡</a>
        </li>
        <li data-range="total">
            <a id="aTotal" href="javascript:;">Top50累计售卡</a>
        </li>
    </ul>
    <div class="ui-table-box">
        <form runat="server">
            <table class="ui-detail active">
                <thead id="thead">
                <tr>
                    <th>序号 | 昵称 | I D </th>
                    <th>当前房卡</th>
                    <th id="thMonth">本月售卡</th>
                    <th id="thTotal">累计售卡</th>
                </tr>
                </thead>
            </table>
            <div id="wrapper">
                <table class="ui-detail active">
                    <tbody data-url="DataHandler.ashx?action=getunderlist" id="list">
                    </tbody>
                </table>
            </div>
        </form>
    </div>
    <script src="/Card/Js/iscroll/iscroll-probe.js"></script>
    <script src="/Card/Js/iscroll/pullup-refresh.js"></script>
    <script src="/Card/Js/layer_mobile/layer.js"></script>
    <script src="/Card/Js/under.js"></script>
</asp:Content>