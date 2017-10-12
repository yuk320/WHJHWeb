<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sitemap.aspx.cs" Inherits="Game.Web.Sitemap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="stylesheet" href="/css/common.css">
    <link rel="stylesheet" href="/css/base.css">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>

<body>
    <qp:Header ID="Header" runat="server" PageID="4" />

    <div class="ui-news-banner">
        <div class="ui-banner-right">
            <div class="ui-banner-left">
                <qp:Banner ID="Banner1" runat="server" TypeID="2" />
            </div>
        </div>
    </div>

    <div class="ui-main-sitemap">
        <div class="ui-block-title"><span></span></div>
        <div class="ui-top-box"></div>
        <div class="ui-content-box ui-sitemap">
            <%=content %>
        </div>
        <div class="ui-bottom-box"></div>        
    </div>

    <qp:Footer ID="Footer" runat="server" />
</body>

</html>
