<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrantDiamond.aspx.cs" Inherits="Game.Web.Module.Diamond.GrantDiamond" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>赠送钻石</title>
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/scripts/common.js"></script>
  <script type="text/javascript" src="/scripts/jquery.js"></script>

  <script type="text/javascript">
    function getnickname() {
      var gameID = $("#txtGameID").val();
      console.info(gameID);
      if (gameID == "") {
        $("#useridTips").html("请输入GameID").addClass("hong");
        return;
      }
      $.ajax({
        url: '/Tools/Operating.ashx?action=getuserinfo',
        type: 'post',
        data: { GameID: gameID },
        dataType: 'json',
        success: function (result) {
          if (result.data.valid) {
            $("#hidUserID").val(result.data.UserID);
            $("#useridTips").html("成功").removeClass("hong");
          } else {
            $("#hidUserID").val("");
            $("#useridTips").html(result.msg).addClass("hong");
          }
        },
        complete: function () {

        }
      });
    }
  </script>
</head>
<body>
    <!-- 头部菜单 Start -->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top" class="Lpd10">
                <div class="arr">
                </div>
            </td>
            <td width="1232" height="25" valign="top" align="left">
                目前操作功能：钻石系统 - 赠送钻石
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <form runat="server" id="form1">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />
                <asp:Button ID="btnSave" runat="server" Text="确认" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2" style="height:136px;">
        <tr runat="server" id="trGameID">
          <td class="listTdLeft">
            赠送ID：
          </td>
          <td>
            <asp:TextBox ID="txtGameID" runat="server" CssClass="text wd4" MaxLength="7"></asp:TextBox>
            <span class="hong">*</span>
            <input type="button" value="检测" class="btn wd1" onclick="getnickname()" />
            <asp:HiddenField runat="server" ID="hidUserID"/>
            <span id="useridTips"></span>
          </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                赠送钻石数：
            </td>
            <td>
                <asp:TextBox ID="txtDiamond" runat="server" CssClass="text wd4" MaxLength="7"></asp:TextBox>
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入赠送钻石" Display="Dynamic" ControlToValidate="txtDiamond" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="赠送钻石格式不正确" Display="Dynamic" ControlToValidate="txtDiamond" ForeColor="Red" ValidationExpression="^\d{1,8}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                推送消息提醒：
            </td>
            <td>
                <asp:CheckBox ID="cbPull" runat="server"/>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                赠送备注：
            </td>
            <td>
                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Height="50" Width="300"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />
                <asp:Button ID="btnSave1" runat="server" Text="确认" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
