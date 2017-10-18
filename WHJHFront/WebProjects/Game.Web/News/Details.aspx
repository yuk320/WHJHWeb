<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Game.Web.News.Details" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" href="/css/common.css"/>
<link rel="stylesheet" href="/css/base.css"/>
<link rel="stylesheet" href="/css/newsdetail.css"/>
</head>
<body>
    <qp:Header ID="Header" runat="server" PageID="3"/>

    <div class="ui-news-banner">
        <div class="ui-banner-right">
            <div class="ui-banner-left">
               <qp:Banner ID="Banner1" runat="server" TypeID="1" />
            </div>
        </div>
    </div>

    <div class="ui-main fn-clear">
        <div class="ui-left fn-left">
            <qp:Download ID="Download1" runat="server" />
            <qp:Contact ID="Contact1" runat="server" />
        </div>
        <div class="ui-news-introduction fn-right">
                <div class="ui-block-title"><span></span></div>                               
                <div class="ui-top-box"></div>
                <div class="ui-content-box">
                    <h1><%=NewsTitle %></h1>
                    <p style="padding-bottom: 10px;">新闻来源：<%=Resource %>&emsp;发布时间：<%=Time %></p>
                    <hr/>
                    <div style="padding-top: 20px;"><%=Content %></div>
                </div>
                <div class="ui-bottom-box"></div>
            </div>
    </div>
    <qp:Footer ID="Footer" runat="server"/>
</body>
</html>
