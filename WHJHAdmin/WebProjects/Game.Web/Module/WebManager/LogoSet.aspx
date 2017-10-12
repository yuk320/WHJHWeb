<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogoSet.aspx.cs" Inherits="Game.Web.Module.WebManager.LogoSet" %>

<%@ Register Src="~/Themes/TabSiteConfig.ascx" TagName="Config" TagPrefix="qp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top" class="Lpd10">
                <div class="arr">
                </div>
            </td>
            <td width="1232" height="25" valign="top" align="left">
                你当前位置：网站系统 - 站点设置
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="Lpd10 Rpd10 hong f14">
                * 温馨提示：修改图片资源无需重启网站前台服务器即可立即生效！
            </td>
        </tr>
    </table>
    <qp:Config runat="server" ID="config"></qp:Config>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td class="listTdLeft">
                网站前台LOGO：
            </td>
            <td>
                <asp:FileUpload ID="fuFrontLogo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
            </td>
            <td>
                <asp:Image ID="igFrontLogo" Width="120px" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                网站后台LOGO：
            </td>
            <td>
                <asp:FileUpload ID="fuAdminLogo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
            </td>
            <td>
                <asp:Image ID="igAdminLogo" Width="120px" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                前台置顶图片：
            </td>
            <td>
                <asp:FileUpload ID="fuTopLogo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
            </td>
            <td>
                <asp:Image ID="igTopLogo" Width="120px" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                二维码内嵌图：
            </td>
            <td>
                <asp:FileUpload ID="fuQrSmall" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
            </td>
            <td>
                <asp:Image ID="igQrSmall" Width="120px" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height:10px;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
