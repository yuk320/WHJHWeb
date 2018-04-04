<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareLink.aspx.cs" Inherits="Game.Web.Mobile.ShareLink" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>
    <%=Title %>
  </title>
  <meta content="text/html; charset=utf-8" http-equiv="content-type" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"
  />
  <meta name="apple-mobile-web-app-capable" content="yes" />
  <meta name="apple-touch-fullscreen" content="yes" />
  <meta name="format-detection" content="telephone=no" />
  <link href="/css/share.css" rel="stylesheet" />
  <link href="/css/weixin.css" rel="stylesheet" />
  <script type="text/javascript" src="/js/zepto/1.1.6/zepto.min.js"></script>
  <script type="text/javascript" src="/js/weixin.js"></script>
</head>

<body>
  <img src="/image/invite-bg.png" alt="" />
  <main>
    <div class="face-box">
      <img src="<%=FaceUrl %>" alt="玩家头像" />
    </div>
    <p>
      <%=Nickname %>
    </p>
    <div class="invite-id">
      推荐ID：
      <span>
        <%=Spread %>
      </span>
    </div>
    <div class="invite-details">
      <p>您的好友向您推荐游戏
        <br/> 填写推荐ID，可获得
        <img src="/image/diamond.png" alt="" />
        <span class="recieve-num">
          <%=Diamond %>
        </span> 奖励</p>
    </div>
    <a href="javascript:;" target="_blank" class="ui-invite-btn"></a>
  </main>

  <div id="weixin-tip" class="ui-weixin-tip fn-hide">
    <img src="/image/live_weixin.png" alt="微信打开" />
    <span title="关闭" class="close">×</span>
  </div>
  <script type="text/javascript">
    var link = '<%=PlatformDownloadUrl %>';
    $(function () {
      var button = $('.ui-invite-btn');
      button.on('click', function () {
        // 判断useragent，当前设备为Android设备
        var loadDateTime = new Date();

        // 设置时间阈值，在规定时间里面没有打开对应App的话，直接去下载地址。
        window.setTimeout(function () {
          var timeOutDateTime = new Date();
          if (timeOutDateTime - loadDateTime < 5000) {

            window.location = link; // Android端URL Schema
          } else {
            window.close();
          }
        },
          2000);
        window.location = "newryclient://";　　 // Android端URL Schema
      });
    });
  </script>
</body>
</html>
