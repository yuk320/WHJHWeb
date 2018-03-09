<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Common_Download.ascx.cs" Inherits="Game.Web.UserControl.Common_Download" %>

<%@ OutputCache Duration="300" Shared="true" VaryByParam="none" %>
<%@ Import Namespace="Game.Facade" %>
<% if (AppConfig.Mode == AppConfig.CodeMode.Demo)
   { %>
  <style>
    .ui-qrcode-right, .ui-qrcode-left {
      width: 50%;
      margin-top: 77px;
      text-align: center;
      display: inline-block;
    }

    .ui-qrcode-right img { margin-right: 7px; }

    .ui-qrcode-left img { margin-left: 7px; }

    .ui-qrcode-right img + img, .ui-qrcode-left img + img {
      width: 180px;
      height: 180px;
      margin-top: 10px;
      margin-bottom: 9px;
      padding: 5px;
      border: 1px solid #ccc;
      border-radius: 5px;
      box-shadow: 0 0 10px rgba(0, 0, 0, .25);
    }
  </style>
  <div class="ui-qrcode fn-clean-space">
    <div class="ui-block-title">
      <span></span>
    </div>
    <div class="ui-qrcode-left">
      <img src="/image/download-title-lua.png" alt=""/>
      <img src="<%= QrLink %>" alt="">
    </div>
    <div class="ui-qrcode-right">
      <img src="/image/download-title-h5.png" alt=""/>
      <img src="<%= Qrh5Link %>" alt="">
    </div>
    <div class="ui-qr-tip">
      <img src="/image/saoyisao.png" alt="">
    </div>
  </div>
<% }
   else
   { %>
  <div class="ui-qrcode">
    <div class="ui-block-title">
      <span></span>
    </div>
    <img src="<%= QrLink %>" alt="">
    <div class="ui-qr-tip">
      <img src="/image/saoyisao.png" alt="">
    </div>
  </div>
<% } %>
