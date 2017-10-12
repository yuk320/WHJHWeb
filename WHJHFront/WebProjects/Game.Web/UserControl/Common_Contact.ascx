<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Common_Contact.ascx.cs" Inherits="Game.Web.UserControl.Common_Contact" %>

<%@ OutputCache Duration="3600" Shared="true" VaryByParam="none" %>

<div class="ui-contactus">
    <div class="ui-block-title"><span></span></div>
    <div class="ui-top-box"></div>               
    <div class="ui-content-box">               
        <a href="http://wpa.b.qq.com/cgi/wpa.php?ln=2&uin=<%=qq %>" target="_blank">
         <img src="/image/contact-qq.png" class="ui-qq">
        </a>
        <a href="javascript:;">
            <img src="/image/contact-phone.png" class="ui-phone">
        </a>
        <p class="ui-service-phone"><img src="/image/hotline.png" title="客服热线" /><span><%=phone %></span></p>
    </div>
    <div class="ui-bottom-box"></div>
</div>