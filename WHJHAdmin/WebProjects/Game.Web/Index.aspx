<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Game.Web.Index" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
    <script type="text/javascript" src="scripts/comm.js"></script>
    <script type="text/javascript" src="scripts/jquery.js"></script>
    <title><%=!SiteTitle.Equals("")?SiteTitle+" - ":"" %>网站后台管理系统</title>
</head>
<body class="warper">
    <table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
        <tbody>
            <tr>
                <td class="topIndex">
                    <div class="logo left">
                        <img src="/Upload/Site/AdminLogo.png" /></div>
                    <div style="height:44px;line-height:44px; font-size:15px; color:#bde9f6;margin-top:20px;">
                        <div style="width:420px;float:left;">
                            <a class="f" href="javascript:openWindow('Module/BackManager/BaseUserUpdate.aspx?param=<%= userExt.UserID %>',500, 354)"
                                class="white12"><span class="cheng"><%= userExt.UserName %></span></a>，欢迎您使用系统管理后台，您的角色是 <span class="cheng"><%= userExt.RoleName %></span>
                        </div>
                        <div style="width:180px;float:right;">
                            <a href="Index.aspx" class="f">后台首页</a>
                            |
                            <a href="LoginOut.aspx" class="f">安全退出</a>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="sidebar_a">
                        <iframe src="Left.aspx" frameborder="0" style="width: 173px; height: 100%; visibility: inherit"></iframe>
                    </div>
                    <div class="sidebar_b">
                        <iframe name="frm_main_content" id="frm_main_content" height="100%" src="right.aspx" frameborder="no" width="100%"></iframe>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <div id="msgBoxDIV" style="position: absolute; display:none; width: 100%; padding-top: 4px; height: 24px; top: 55px; text-align: center;"><span class="msg" id="spnTopMsg"></span></div>
</body>
</html>
