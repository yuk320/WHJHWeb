<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.Mobile.Index" %>

<!DOCTYPE html>
<html>
    <head runat="server">
        <title><%=Title %></title>
        <meta content="text/html; charset=utf-8" http-equiv="content-type"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
        <meta name="apple-mobile-web-app-capable" content="yes">
        <meta name="apple-touch-fullscreen" content="yes"/>
        <meta name="format-detection" content="telephone=no">
        <script src="/js/reset.js"></script>
        <link href="/css/base.css" rel="stylesheet" type="text/css"/>
        <link href="/css/mindex.css" rel="stylesheet" type="text/css"/>
    </head>
    <body>
    <img src="<%= Mobilebg %>" class="ui-bg" alt="">
    <div class="ui-main">
        <div class="ui-content">
        <div class="ui-top fn-clear">
          <div class="ui-logo fn-left">
            <img src="<%= Mobilelogo %>" alt="">
          </div>
        </div>
        <div class="ui-qrcode">
          <img src="<%= MobileQrcode %>" alt="">          
        </div>
        <a href="<%= PlatformDownloadUrl %>" class="ui-download-btn">
          <img src="<%= Mobiledown %>" alt="">
        </a>
        </div>
      </div>
        <div class="ui-bottom fn-clean-space">
          <a href="http://wpa.b.qq.com/cgi/wpa.php?ln=2&uin=<%= Qq %>" class="ui-qq"><i></i><img src="/image/qq.png" alt="">
          </a>
          <a href="tel:<%= Tel %>" class="ui-phone"><i></i><img src="/image/phone.png" alt="">
          </a>
        </div>
        <div id="weixin-tip" class="ui-weixin-tip fn-hide">
          <p>
            <img src="/image/live_weixin.png" alt="微信打开" />
            <span title="关闭" class="close">×</span>
          </p>
        </div>
        <script type="text/javascript" src="/js/zepto/1.1.6/zepto.min.js"></script>
        <script type="text/javascript" src="/js/mindex.js"></script>
        <script type="text/javascript">
          $(document).ready(function() {
            var msg = "<%=msg %>";
            var action = "<%=action %>";
            if (action === "payreturn" && msg) {
              alert(msg);
              window.location.href = "newryclient://?action=3&msg=" + msg;
            }
          });
        </script>
    </body>
</html>
