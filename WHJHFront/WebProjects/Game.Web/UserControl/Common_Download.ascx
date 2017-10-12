<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Common_Download.ascx.cs" Inherits="Game.Web.UserControl.Common_Download" %>

<%@ OutputCache Duration="300" Shared="true" VaryByParam="none" %>

<div class="ui-qrcode">
    <div class="ui-block-title"><span></span></div>    
    <img src="<%=qrLink %>">
    <div class="ui-qr-tip"><img src="/image/saoyisao.png"></div>                    
</div>