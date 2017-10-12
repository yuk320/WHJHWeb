<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Right.aspx.cs" Inherits="Game.Web.Right" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>欢迎页</title>
<link rel="stylesheet" type="text/css" href="/styles/layout.css">
<style>
    .welcome-bd{ padding: 10px;}
    .welcome-box{ border: 1px solid #dcdcdc; background: url(/images/welcome_i.jpg);position: relative;padding:40px 40px 133px;}
    .welcome-i{
        position: absolute;bottom: -1px;right: -1px; height: 133px; width: 248px; background: url(images/welcome_bj.jpg);
    }
    .welcome-box h2{ font-size: 14px; margin-bottom: 15px;}
    .welcome-box h2 strong{ color: red;}
    .welcome-list{ height: 30px; line-height: 30px; display:block;}
    .welcome-list label{ width: 110px; text-align: right;float: left; margin-right: 5px;}
</style>
</head>

<body>
    <div class="welcome-bd">
        <div class="welcome-box">
            <i class="welcome-i"></i>
            <h2>尊敬的用户 <strong><%= userExt.UserName %></strong>  【<%= userExt.RoleName %>】，欢迎登录系统管理后台！</h2>
            <div class="welcome-list"><label>系统版本：</label>6.8.0.1</div>
            <div class="welcome-list"><label>上次登录时间：</label><%= userExt.PreLogintime %></div>
            <div class="welcome-list"><label>上次登录地址：</label><%= userExt.PreLoginIP %></div>
            <div class="welcome-list"><label>登录次数：</label><%= userExt.LoginTimes %> 次</div>
        </div>
    </div>
</body>
</html>
