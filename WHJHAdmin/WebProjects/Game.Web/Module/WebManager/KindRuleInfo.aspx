<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KindRuleInfo.aspx.cs" Inherits="Game.Web.Module.WebManager.KindRuleInfo" %>
<%@ Register Src="/Tools/ImageUploader.ascx" TagName="ImageUploader" TagPrefix="GameImg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/comm.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script type="text/javascript" src="../../scripts/kindEditor/kindeditor.js"></script>
    <title></title>
    <script type="text/javascript">
        /*
        * 设置图片文件
        */
        function setImgFilePath(frID, uploadPath) {
            document.getElementById(frID).contentWindow.setUploadFileView(uploadPath);
        }
        
        KE.show({
            id: 'txtRule',
            imageUploadJson: '/Tools/KindEditorUpload.ashx?type=rules',
            fileManagerJson: '/Tools/KindEditorFileManager.ashx?type=rules',
            allowFileManager: true
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
            <tr>
                <td width="19" height="25" valign="top" class="Lpd10">
                    <div class="arr"></div>
                </td>
                <td width="1232" height="25" valign="top" align="left">
                    你当前位置：网站系统 - 游戏规则
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titleOpBg Lpd10">
                    <input id="btnReturn" type="button" value="返回" class="btn wd1" onclick="Redirect('KindRuleList.aspx')" />
                    <asp:Button ID="btnCreate" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
            <tr>
                <td height="35" colspan="4" class="f14 bold Lpd10 Rpd10">
                    <div class="hg3 pd7">
                        规则详情</div>
                </td>
            </tr>
            <tr>
                <td class="listTdLeft">游戏名称：</td>
                <td>
                    <asp:DropDownList ID="ddlGame" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="listTdLeft">游戏图标：</td>
                <td>
                   <GameImg:ImageUploader ID="upImage" MaxSize="2097152" runat="server" DeleteButtonClass="l2" DeleteButtonText="删除" Folder="/Upload/RuleIoc" ViewButtonClass="l2" ViewButtonText="查看" TextBoxClass="text"/> <span>[体积：不大于2M]</span>
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
            <tr>
                <td class="listTdLeft">游戏简介：</td>
                <td>
                   <asp:TextBox ID="txtIntro" runat="server" CssClass="text" Width="633px" MaxLength="500" Height="100px" TextMode="MultiLine"></asp:TextBox>  
                   <span class="hong">*</span>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" ControlToValidate="txtIntro" Display="Dynamic" ErrorMessage="请输入游戏简介"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="listTdLeft">游戏规则：</td>
                <td colspan="3">
                   <asp:TextBox ID="txtRule" runat="server" CssClass="text" Width="640px" MaxLength="1000" Height="200px" TextMode="MultiLine"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td height="20px" colspan="2"></td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titleOpBg Lpd10">
                    <input id="btnReturn2" type="button" value="返回" class="btn wd1" onclick="Redirect('KindRuleList.aspx')" />
                    <asp:Button ID="btnSave2" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
