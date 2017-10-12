<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Common_Rank.ascx.cs" Inherits="Game.Web.UserControl.Common_Rank" %>

<%@ OutputCache Duration="3600" Shared="true" VaryByParam="none" %>

<div class="ui-rank">
    <div class="ui-top-box"></div>
    <div class="ui-content-box">
        <div class="ui-block-title"><span></span></div>  
        <%--<div class="ui-more"><img src="/image/more.png"></div>--%>
        <ul class="ui-rank-nav fn-clear">
            <%=RankType %>
        </ul>
            <%=RankData %>
    </div>
    <div class="ui-bottom-box"></div>
</div>