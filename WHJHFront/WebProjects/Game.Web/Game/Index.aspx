<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.Game.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="/css/common.css"/>
    <link rel="stylesheet" href="/css/base.css"/>
    <link rel="stylesheet" href="/css/download.css"/>
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
        <form runat="server">
        <div class="ui-right fn-right ui-download-box">
            <div class="ui-block-title"><span></span></div>
            <div class="ui-top-box"></div>   
            <div class="ui-content-box">            
            <ul class="ui-gamelist fn-clear">
                <asp:Repeater ID="rpGame" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src="<%# Game.Facade.Fetch.GetUploadFileUrl(imgDomain, Eval("KindIcon").ToString()) %>">
                            <h2><%# Eval("KindName") %></h2>
                            <p><span>简介：</span><%# Eval("KindIntro").ToString().Length>84?(Eval("KindIntro").ToString().Substring(0,84)+"..."):Eval("KindIntro").ToString() %></p>
                            <a target="_blank" href="/Game/Details.aspx?id=<%# Eval("KindID") %>">查看>></a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>  

            <webdiyer:AspNetPager CssClass="ui-pages fn-clear" ID="anpPage" runat="server" AlwaysShow="true" FirstPageText="首页" FirstLastButtonClass="ui-pre-page ui-news-paging-prev" NextPrevButtonClass="ui-news-paging-next other"  
                LastPageText="末页" PageSize="9" NextPageText="下一页" PrevPageText="上一页" ShowBoxThreshold="0" 
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
