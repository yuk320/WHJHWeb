<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionInfo.aspx.cs" Inherits="Game.Web.Module.WebManager.QuestionInfo" %>
<%@ Register Src="/Tools/ImageUploader.ascx" TagName="ImageUploader" TagPrefix="GameImg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/comm.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
            <tr>
                <td width="19" height="25" valign="top" class="Lpd10">
                    <div class="arr"></div>
                </td>
                <td width="1232" height="25" valign="top" align="left">
                    你当前位置：网站系统 - 常见问题
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titleOpBg Lpd10">
                    <input id="btnReturn" type="button" value="返回" class="btn wd1" onclick="Redirect('QuestionList.aspx')" />
                    <asp:Button ID="btnCreate" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
            <tr>
                <td height="35" colspan="4" class="f14 bold Lpd10 Rpd10">
                    <div class="hg3 pd7">
                        常见问题详情</div>
                </td>
            </tr>
            <tr>
                <td class="listTdLeft">问题：</td>
                <td>
                   <asp:TextBox runat="server" ID="txtQuestion"></asp:TextBox>
                  <span class="hong">*</span>
                </td>
            </tr>
            <tr>
                <td class="listTdLeft">答案：</td>
                <td>
                   <asp:TextBox runat="server" ID="txtAnswer"></asp:TextBox>
                  <span class="hong">*</span>
                </td>
            </tr>
            <tr>
              <td class="listTdLeft">排序：</td>
              <td>
                <asp:TextBox ID="txtSortID" runat="server" CssClass="text" Text="0"></asp:TextBox>  
                <span class="hong">*</span>
              </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titleOpBg Lpd10">
                    <input id="btnReturn2" type="button" value="返回" class="btn wd1" onclick="Redirect('QuestionList.aspx')" />
                    <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
