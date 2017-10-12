<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Common_Header.ascx.cs" Inherits="Game.Web.UserControl.Common_Header" %>

<%@ OutputCache Duration="300" VaryByParam="none" %>

<div class="top">
    <div class="top-content fn-clear">
      <div class="fn-left left">全新约战模式&nbsp;,&nbsp;尽享游戏带来的竞技乐趣！</div>
      <div class="fn-right right">
        <a class="ui-iconbox" href="/Contact/Index.aspx">联系我们</a>
        <a class="ui-iconbox" href="javascript:AddFavorite()" title="收藏网站">收藏网站</a>
      </div>
    </div>
</div>
<div class="ui-nav">
    <div class="ui-nav-box">
        <div class="ui-logo"><img src="<%= Game.Facade.Fetch.GetUploadFileUrl("/Site/frontlogo.png") %>"></div>
        <ul class="fn-clear">
            <li <%=(PageID==1?"class=\"active\"":"") %>><a href="/Index.aspx">网站首页<span>HOMEPAGE</span></a></li>
            <li <%=(PageID==2?"class=\"active\"":"") %>><a href="/Game/Index.aspx">游戏下载<span>DOWNLOAD</span></a></li>
            <li <%=(PageID==3?"class=\"active\"":"") %>><a href="/News/Index.aspx">新闻公告<span>NEWS BULLETIN</span></a></li>
            <li <%=(PageID==4?"class=\"active\"":"") %>><a href="/Contact/Index.aspx">联系我们<span>CONTACT  US</span></a></li>
        </ul>
    </div>
</div>

<div class="ui-top-home">
    <a id="course-top" href="javascript:;" class="ui-top-home-1"><span>返回顶部</span></a>
    <a href="javascript:;" class="ui-top-home-2 bdsharebuttonbox bdshare-button-style0-16" data-cmd="more" data-bd-bind="1504257167594"><span>分享</span></a>
    <a href="/Contact/Index.aspx" class="ui-top-home-3"><span>咨询</span></a>
    <a href="javascript:AddFavorite()" class="ui-top-home-4"><span>收藏</span></a>
</div>
 

