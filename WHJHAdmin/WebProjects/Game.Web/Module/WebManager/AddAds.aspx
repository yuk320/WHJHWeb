<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAds.aspx.cs" Inherits="Game.Web.Module.WebManager.AddAds" %>

<%@ Register Src="/Tools/ImageUploader.ascx" TagName="ImageUploader" TagPrefix="GameImg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/comm.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 头部菜单 Start -->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top" class="Lpd10">
                <div class="arr">
                </div>
            </td>
            <td width="1232" height="25" valign="top" align="left">
                你当前位置：网站系统 - 广告管理
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn" type="button" value="返回" class="btn wd1" onclick="Redirect('AdsList.aspx')" />
                <asp:Button ID="btnCreate" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10">
                <div class="hg3 pd7">
                    广告位信息</div>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
               广告位描述：
            </td>
            <td>
                <asp:TextBox ID="txtDescript" runat="server" CssClass="text" MaxLength="500" Width="200px"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入描述" ControlToValidate="txtDescript" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
               广告位排序：
            </td>
            <td>
                <asp:TextBox ID="txtSortID" runat="server" CssClass="text" Width="200px"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入排序" ControlToValidate="txtSortID" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="排序格式不正确" Display="Dynamic" ControlToValidate="txtSortID" ValidationExpression="^\d*$" ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
               广告位类型：
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" Width="205px">
                    <asp:ListItem Text="首页轮播广告" Value="0"></asp:ListItem>
                    <asp:ListItem Text="新闻公告广告" Value="1"></asp:ListItem>
                    <asp:ListItem Text="联系我们广告" Value="2"></asp:ListItem>
                    <asp:ListItem Text="游戏下载广告" Value="5"></asp:ListItem>
                    <asp:ListItem Text="手机大厅广告" Value="3"></asp:ListItem>
                    <asp:ListItem Text="手机弹出广告" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="linkurl" runat="server">
            <td class="listTdLeft">
               广告位链接：
            </td>
            <td>
                <asp:DropDownList ID="ddlLink" runat="server" Width="205px">
                    <asp:ListItem Text="游戏房间界面" Value="ad_to_createroom_action"></asp:ListItem>
                    <asp:ListItem Text="战绩回放界面" Value="ad_to_video_action"></asp:ListItem>
                    <asp:ListItem Text="实名认证界面" Value="ad_to_identify_action"></asp:ListItem>
                    <asp:ListItem Text="商城界面" Value="ad_to_shop_action"></asp:ListItem>
                    <asp:ListItem Text="推广界面" Value="ad_to_spread_action"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtLink" runat="server" CssClass="text" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                广告位图片：
            </td>
            <td style="line-height:35px;">
                <GameImg:ImageUploader ID="upImage" MaxSize="2097152" runat="server" DeleteButtonClass="l2" DeleteButtonText="删除" Folder="/Upload/Initialize" ViewButtonClass="l2" ViewButtonText="查看" TextBoxClass="text"/> <span>[体积：不大于2M]</span>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input id="btnReturn2" type="button" value="返回" class="btn wd1" onclick="Redirect('AdsList.aspx')" />
                <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        $(function () {
            if (window.location.href.indexOf('?param=') < 0) {
                $('#ddlLink').css('display', 'none');
            } 
            $('#ddlType').on('change', function () {
                if ($(this).val() == '4') {
                    $('#linkurl').css('display', 'none');
                } else if ($(this).val() == '3') {
                    $('#linkurl').css('display', 'table-row');
                    $('#ddlLink').css('display', 'block');
                    $('#txtLink').css('display', 'none');
                } else {
                    $('#linkurl').css('display', 'table-row');
                    $('#ddlLink').css('display', 'none');
                    $('#txtLink').css('display', 'block');
                }
            });
        });
    </script>
</body>
</html>
