<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/css/common.css"/>
    <link rel="stylesheet" href="/css/base.css"/>
    <link rel="stylesheet" href="/js/slider/slider.css"/>
    <link rel="stylesheet" href="/css/index.css"/>                
</head>

<body>
    <qp:Header ID="Header" runat="server" PageID="1" />

    <div class="ui-left-fixed">
        <img src="/image/newplayer.png"/>
        <a href="javascript:;">下载游戏</a>
        <img src="/image/left-fixed-arrow.png"/>
        <a href="javascript:;">创建游戏</a>
        <img src="/image/left-fixed-arrow.png"/>
        <a href="javascript:;">邀请好友</a>
        <img src="/image/left-fixed-arrow.png"/>
        <a href="javascript:;">开始游戏</a>
    </div>

    <div class="ui-banner-box slider-container">
        <div class="ui-banner slider">
            <%=bannerAds %>
        </div>
    </div>

    <form runat="server">
        <div class="ui-content fn-clear">
        <div class="ui-news fn-left">
            <div class="ui-block-title"><span></span></div>
            <div class="ui-top-box"></div>
            <div class="ui-content-box">
                <div class="ui-more"><a href="/News/Index.aspx" target="_blank"><img src="/image/more.png"/></a></div>
                <div class="ui-news-content">
                <div class="ui-recent-news fn-clear">
                    <img src="<%=bannerNews %>" class="fn-left"/>
                    <div class="ui-news-welcome fn-right">
                        <h2 class="fn-clear"><%=title %><span class="fn-right"><%=time %></span></h2>
                        <p><%=content %></p>
                    </div>
                </div>
                <ul class="ui-news-all">
                    <asp:Repeater ID="rpNews" runat="server">
                        <ItemTemplate>
                            <li class="fn-clear"><a href="/News/Details.aspx?id=<%# Eval("NoticeID") %>" target="_blank"><%# Eval("NoticeTitle") %> <%# Eval("IsHot").ToString() == "True"?"<img src=\"/image/news-recent.png\">":"" %>
                                <span><%# Convert.ToDateTime(Eval("PublisherTime")).ToString("yyyy-MM-dd") %></span></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            </div>
            <div class="ui-bottom-box"></div>
        </div>
        <div class="fn-right ui-right">
            <qp:Download ID="Download1" runat="server" />
            <qp:Rank ID="Rank1" runat="server" />
        </div>
    </div>
    </form>

    <qp:Footer ID="Footer" runat="server" />
    <script src="js/jquery/1.11.3/jquery.min.js"></script>
    <script src="js/slider/slider.js"></script>
    <script src="js/index.js"></script>
</body>

</html>