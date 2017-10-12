<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.News.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" href="/css/common.css"/>
<link rel="stylesheet" href="/css/base.css"/>
<link rel="stylesheet" href="/css/news.css"/>
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
         <form runat="server">
             <div class="ui-news-all-content fn-right">
            <div class="ui-block-title"><span></span></div> 
            <div class="ui-top-box"></div>   
            <%--<div class="ui-more"><img src="/image/more.png"></div>--%>
            <div class="ui-content-box">
            <asp:Repeater ID="rpNew" runat="server">
                <ItemTemplate>
                    <div <%# Container.ItemIndex==0?"class=\"ui-news-details current fn-clear\"":"class=\"ui-news-details fn-clear\"" %> >
                        <div class="ui-news-time fn-left">
                            <p><%# Convert.ToDateTime(Eval("PublisherTime")).ToString("MM-dd") %></p>
                            <p><%# Convert.ToDateTime(Eval("PublisherTime")).ToString("yyyy") %></p>
                        </div>
                        <img src="/image/news-time.png"  class="ui-news-time-img fn-left">
                        <div class="ui-news-content fn-right">
                            <h2><%# Eval("NoticeTitle").ToString().Length>24?(Eval("NoticeTitle").ToString().Substring(0,24)+"..."):Eval("NoticeTitle").ToString()  %></h2>
                            <p><%# Eval("MoblieContent").ToString().Length>90?(Eval("MoblieContent").ToString().Substring(0,90)+"..."):Eval("MoblieContent").ToString()  %></p>
                            <a class="ui-look-details" href="/News/Details.aspx?id=<%# Eval("NoticeID") %>" target="_blank">查看详情</a>
                        </div>
                    </div> 
                </ItemTemplate>
            </asp:Repeater>

            <webdiyer:AspNetPager ID="anpPage" CssClass="ui-pages fn-clear" runat="server" AlwaysShow="true" FirstPageText="首页" FirstLastButtonClass="ui-news-paging-prev ui-pre-page" NextPrevButtonClass="ui-news-paging-prev other"  
                LastPageText="末页" PageSize="8" NextPageText="下一页" PrevPageText="上一页" ShowBoxThreshold="0" 
                LayoutType="Table" NumericButtonCount="5" CustomInfoHTML="共 %PageCount% 页" ShowPageIndexBox="Never"
                UrlPaging="true" ShowCustomInfoSection="Never" CurrentPageButtonClass="current" MoreButtonClass="ui-page-more">
            </webdiyer:AspNetPager>
         </div>
         <div class="ui-bottom-box"></div>         
        </div>
         </form>
     </div>

    <qp:Footer ID="Footer" runat="server"/>
</body>
</html>
