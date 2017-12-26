<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.Contact.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="stylesheet" href="/css/common.css"/>
    <link rel="stylesheet" href="/css/base.css"/>
    <link rel="stylesheet" href="/css/contact.css"/>
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

    <div class="ui-main-contact">
        <div class="ui-block-title"><span></span></div>
        <div class="ui-top-box"></div>
        <div class="ui-content-box">
            <div class="ui-contact-leader">
                <p>
                    <%=contactIntro %>
                </p>
            </div>
            <ul class="ui-contact-list fn-clear">
                <li class="ui-contact-1">
                    <h2>联系电话</h2>
                    <p>
                        <%=contactPhone %>
                    </p>
                </li>
                <li class="ui-contact-2">
                    <h2>联系邮箱</h2>
                    <p>
                        <%=contactEmail %>
                    </p>
                </li>
                <li class="ui-contact-3">
                    <h2>微信公众号</h2>
                    <p>
                        <%=contactWeChat %>
                    </p>
                </li>
                <li class="ui-contact-4">
                    <h2>联系QQ</h2>
                    <p>
                        <%=contactQQ %>
                    </p>
                </li>
            </ul>

            <div class="ui-location" data-point="<%=baiduAddress %>">
                <h2>
                    <span>公司地址：</span> <img src="/image/location.png">
                  <input id="hidAddress" type="hidden" value="<%=contactAddress %>"/>
                    <%=contactAddress %>
                </h2>
                <div class="ui-border"><div id="ui-baidu-map"></div></div>
            </div>
        </div>
        <div class="ui-bottom-box"></div>        
    </div>

    <qp:Footer ID="Footer" runat="server" />
    <script src="../js/jquery/1.11.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/getscript?v=1.4&amp;ak=&amp;services=&amp;t=20150522093217"></script>
    <script src="../js/baidumap/baidumap.js"></script>
</body>

</html>