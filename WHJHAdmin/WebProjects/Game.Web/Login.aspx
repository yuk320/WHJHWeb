<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Game.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id=Head1 runat="server">
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <title>网站管理后台 - 管理员登录</title>
</head>
<body style="background-image: url(images/loginBg.jpg);">
    <br /><br /><br /><br /><br /><br /><br /><br /><br />
    <form id="form1" runat="server">
    <table width="705" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="705" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="125">
                            &nbsp;
                        </td>
                        <td width="581">
                            <span style=" padding:110px;"><img src="/Upload/Site/AdminLogo.png" /></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="background-image:url(images/login01.png)" width="705" height="50">
              
            </td>
        </tr>
        <tr>
            <td background="images/login02.png" width="705" height="235">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="301" height="33">
                            &nbsp;
                        </td>
                        <td width="73" height="33" class="f14" align="right">
                            帐 号：
                        </td>
                        <td width="331" height="33">
                            <asp:TextBox ID="txtLoginName" runat="server" CssClass="text"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="301" height="33">
                            &nbsp;
                        </td>
                        <td height="33" class="f14" align="right">
                            密 码：
                        </td>
                        <td height="33">
                            <asp:TextBox ID="txtLoginPass" runat="server" CssClass="text" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="301" height="33">
                            &nbsp;
                        </td>
                        <td height="33" class="f14" align="right">
                            验证码：
                        </td>
                        <td height="33">
                            <asp:TextBox ID="txtVerifyCode" runat="server" CssClass="text"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="301" height="30">
                            &nbsp;
                        </td>
                        <td height="30">
                            &nbsp;
                        </td>
                        <td height="30" class="hui6">
                            验证码请按下图中的数字填写
                        </td>
                    </tr>
                    <tr>
                        <td width="301" height="30">
                            &nbsp;
                        </td>
                        <td height="30">
                            &nbsp;
                        </td>
                        <td height="30">
                            <img src="/Tools/VerifyImagePage.aspx" width="120px" height="40px" id="ImageCheck" style="cursor: pointer" title="点击更换验证码图片!" onclick="ChangeCodeimg();" />
                        </td>
                    </tr>
                    <tr>
                        <td width="301" height="30">
                            &nbsp;
                        </td>
                        <td height="30">
                            &nbsp;
                        </td>
                        <td height="30" class="lan">
                            <a href="javascript:void(0)" class="l" onclick="ChangeCodeimg()">看不清楚？ 换一个</a>
                        </td>
                    </tr>
                    <tr>
                        <td width="301" height="30">
                            &nbsp;
                        </td>
                        <td height="30">
                            &nbsp;
                        </td>
                        <td height="30">
                            <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="images/loginBtn.jpg" Width="86" Height="34" OnClick="btnLogin_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="705" height="54" align="center" background="images/login03.png" style="color: #115287;">
            </td>
        </tr>
        <tr>
            <td>
                <img src="images/login04.png" width="705" height="46" />
            </td>
        </tr>
        <tr>
            <td>
                <img src="images/login05.png" width="705" height="53" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        function ChangeCodeimg() {
            Images = document.getElementById("ImageCheck");
            Images.src = "Tools/VerifyImagePage.aspx?" + Math.random();
        }
    </script>
</body>
</html>
