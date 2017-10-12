<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsBaseInfo.aspx.cs" Inherits="Game.Web.Module.AccountManager.AccountsBaseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户信息</title>
    <link href="/styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/comm.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
</head>
<body>
    <!-- 头部菜单 Start -->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top" class="Lpd10">
                <div class="arr">
                </div>
            </td>
            <td width="1232" height="25" valign="top" align="left" style="width: 1232px; height: 25px; vertical-align: top; text-align: left;">
                目前操作功能：用户信息
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="Tmg7">
        <tr>
            <td height="28">
                <ul>
                    <li class="tab1">用户信息</li>
                    <li class="tab2" onclick="Redirect('DiamondChangeList.aspx?param='+GetRequest('param',0))">钻石流水</li>
                    <li class="tab2" onclick="Redirect('TreasureChangeList.aspx?param='+GetRequest('param',0))">金币流水</li>
                </ul>
            </td>
        </tr>
    </table>
    <form runat="server" id="form1">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="4" class="f14 bold Lpd10 Rpd10">
                <div class="hg3  pd7">
                    资料信息</div>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                用户昵称：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltNickName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
               推广 I D：
            </td>
            <td>
                <asp:Literal ID="ltSpread" runat="server"></asp:Literal>
            </td>
            <td class="listTdLeft">
               代理状态：
            </td>
            <td>
                <asp:Literal ID="ltAgent" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                游戏 I D：
            </td>
            <td style="width:150px;">
                <asp:Literal ID="ltGameID" runat="server"></asp:Literal>
            </td>
            <td class="listTdLeft">
                用户性别：
            </td>
            <td style="width:150px;">
                <asp:Literal ID="ltSex" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                用户头像：
            </td>
            <td colspan="3">
                <asp:Image ID="imgFace" runat="server" Width="96px" Height="96px" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                用户状态：
            </td>
            <td colspan="3">
                <asp:CheckBox ID="ckbNullity" runat="server" Text="冻结帐号" />
                <asp:CheckBox ID="ckbLock" runat="server" Text="锁定客户端" />
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                真实姓名：
            </td>
            <td style="width:150px;">
                <asp:TextBox ID="txtRealName" CssClass="text" MaxLength="16" runat="server"></asp:TextBox>
            </td>
            <td class="listTdLeft">
                身份证号：
            </td>
            <td style="width:150px;">
                <asp:TextBox ID="txtCardNum" CssClass="text" MaxLength="18" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                个性签名：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtUnderWrite" runat="server" MaxLength="60" CssClass="text" Width="590px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="35" colspan="4" class="f14 bold Lpd10 Rpd10">
                <div class="hg3  pd7">
                    平台信息</div>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                网站登录次数：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltWebLogonTimes" runat="server"></asp:Literal>次
                <span style="padding-left: 100px;">大厅登录次数：</span>
                <asp:Literal ID="ltGameLogonTimes" runat="server"></asp:Literal>次
                <span style="padding-left: 10px;"></span>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                在线时长共计：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltOnLineTimeCount" runat="server"></asp:Literal>
                <span style="padding-left: 100px;">游戏时长共计：</span>
                <asp:Literal ID="ltPlayTimeCount" runat="server"></asp:Literal>
                <span style="padding-left: 10px;"></span>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                最后登录时间：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltLastLogonDate" runat="server"></asp:Literal>&nbsp;&nbsp;
                <asp:Literal ID="ltLogonSpacingTime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                最后登录地址：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltLastLogonIP" runat="server"></asp:Literal>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                最后登录机器：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltLastLogonMachine" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                用户注册时间：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltRegisterDate" runat="server"></asp:Literal>&nbsp;&nbsp;
                <asp:Literal ID="ltRegSpacingTime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                用户注册地址：
            </td>
            <td colspan="3">
                <asp:Literal ID="ltRegisterIP" runat="server"></asp:Literal>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                用户注册机器：
            </td>
            <td colspan="3">
               <asp:Literal ID="ltRegisterMachine" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="listTdLeft">
                用户注册来源：
            </td>
            <td colspan="3">
               <asp:Literal ID="ltRegisterOrigin" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />
                <asp:Button ID="btnSave1" runat="server" Text="保存" CssClass="btn wd1" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
