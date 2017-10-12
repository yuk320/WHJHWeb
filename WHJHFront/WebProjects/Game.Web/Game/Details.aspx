<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Game.Web.Game.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="/css/common.css">
    <link rel="stylesheet" href="/css/base.css">
    <link rel="stylesheet" href="/css/gamedetail.css">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
</head>
<body>
    <qp:Header ID="Header" runat="server" PageID="2"/>

    <div class="ui-news-banner">
        <div class="ui-banner-right">
            <div class="ui-banner-left">
                <qp:Banner ID="Banner1" runat="server" TypeID="5" />
            </div>
        </div>
    </div>
    <div class="ui-main fn-clear">
 
        <div class="ui-left fn-left">
            <qp:Download ID="Download1" runat="server" />
            <qp:Contact ID="Contact1" runat="server" />
        </div>
        <div class="ui-game-introduction fn-right">
            <div class="ui-block-title"><span></span></div>                               
            <div class="ui-top-box"></div>
            <div class="ui-content-box">
                <div><%=content %></div>
            </div>
            <div class="ui-bottom-box"></div>
        </div>
    </div>

    <qp:Footer ID="Footer" runat="server"/>
</body>
</html>
