<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Record_Sidebar.ascx.cs" Inherits="Game.Web.Card.UserControl.Record_Sidebar" %>
<ul class="ui-tab fn-clean-space">
  <li <%= RecordID==0?"class=\"active\"":"" %>><a href="/Card/PayRecord.aspx">充值记录</a></li>
  <li <%= RecordID==1?"class=\"active\"":"" %>><a href="/Card/RegisterRecord.aspx">注册记录</a></li>
  <li <%= RecordID==2?"class=\"active\"":"" %>><a href="/Card/PresentRecord.aspx">赠送记录</a></li>
  <li <%= RecordID==3?"class=\"active\"":"" %>><a href="/Card/ExchangeRecord.aspx">兑换记录</a></li>
  <li <%= RecordID==4?"class=\"active\"":"" %>><a href="/Card/CostRecord.aspx">消耗记录</a></li>
</ul>