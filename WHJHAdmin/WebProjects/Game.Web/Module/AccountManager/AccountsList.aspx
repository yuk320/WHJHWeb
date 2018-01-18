<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsList.aspx.cs" Inherits="Game.Web.Module.AccountManager.AccountsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/common.js"></script>
    <script type="text/javascript" src="../../scripts/comm.js"></script>
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
                你当前位置：用户系统 - 用户管理
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg">
        <tr>
            <td align="center"  style="width: 80px">
                账号查询：
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="text"></asp:TextBox>
                <asp:DropDownList ID="ddlSearchType" runat="server">
                    <asp:ListItem Text="游戏ID" Value ="1"></asp:ListItem>
                    <asp:ListItem Text="用户昵称" Value ="2"></asp:ListItem>
                    <asp:ListItem Text="推广人ID" Value ="3"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="btn wd1" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <div class="clear"></div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="Tmg7">
        <tr>
            <td height="39" class="titleOpBg">
                <input type="button" class="btn wd2" value="添加超管" onclick="openWindowOwn('AddSuperUser.aspx','添加超端管理员',600,280)"/>
                <asp:Button ID="btnDongjie" runat="server" Text="冻结" CssClass="btn wd1" OnClick="btnDongjie_Click" OnClientClick="return deleteop()" />
                <asp:Button ID="btnJiedong" runat="server" Text="解冻" CssClass="btn wd1" OnClick="btnJiedong_Click" OnClientClick="return deleteop()" />
                <input class="btnLine" type="button" />
                <asp:Button ID="btnSetSingle" runat="server" Text="批量设置转账权限" CssClass="btn" Width="125px" OnClick="btnSetSingle_Click" OnClientClick="return deleteop()" />
                <asp:Button ID="benCancleSingle" runat="server" Text="批量取消转账权限" CssClass="btn" Width="125px" OnClick="benCancleSingle_Click" OnClientClick="return deleteop()" />
                <input class="btnLine" type="button" />
                <asp:Button ID="btnSetting" runat="server" Text="设置所有人转账权限" CssClass="btn" Width="125px" OnClick="btnSetting_Click" OnClientClick="return confirm('确认要进行此操作吗？')" />
                <asp:Button ID="btnCancle" runat="server" Text="取消所有人转账权限" CssClass="btn" Width="125px" OnClick="btnCancle_Click" OnClientClick="return confirm('确认要进行此操作吗？')" />
            </td>
        </tr>
    </table>
    <div id="content">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box" id="list">
            <tr align="center" class="bold">
                <td class="listTitle">
                    <input type="checkbox" name="chkAll" onclick="SelectAll(this.checked);" />
                </td>
                <td class="listTitle2">
                    用户标识
                </td>
                <td class="listTitle2">
                    游戏ID
                </td>
                <td class="listTitle2">
                    昵称
                </td>
                <td class="listTitle2">
                    性别
                </td>
                <td class="listTitle2">
                    推广人
                </td>
                <td class="listTitle2">
                    注册时间
                </td>
                <td class="listTitle2">
                    注册地址
                </td>
                <td class="listTitle2">
                    登录次数
                </td>
                <td class="listTitle2">
                    最后登录时间
                </td>
                <td class="listTitle2">
                    最后登录地址
                </td>
                <td class="listTitle2">
                    注册来源
                </td>
                <td class="listTitle1">
                    状态
                </td>
                <td class="listTitle1">
                    管理
                </td>
            </tr>
            <asp:Repeater ID="rptDataList" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td style="width: 30px;">
                            <input name='cid' type='checkbox' value='<%# Eval("UserID").ToString()%>' />
                        </td>
                        <td>
                            <%# Eval( "UserID" ).ToString( ) %>
                        </td>
                        <td>
                            <%# Eval( "GameID" ).ToString( ) %>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0);" onclick="openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx?param=<%#Eval("UserID").ToString() %>','<%#Eval("UserID").ToString() %>',850,790);">
                            <%# Eval( "NickName" ).ToString( ) %>
                            </a>
                        </td>
                        <td>
                            <%# Eval( "Gender" ).ToString()=="1"?"男":"女"%>
                        </td>
                         <td>
                            <%# Eval( "SpreaderID" ).ToString()!="0"?GetGameID( Convert.ToInt32( Eval( "SpreaderID" ) ) ):"" %>
                        </td>
                        <td>
                            <%# Eval( "RegisterDate" ).ToString()%>
                        </td>
                        <td>
                           <%# Eval("RegisterIP").ToString()%>
                        </td>
                        <td>
                            <%# Eval( "GameLogonTimes" ).ToString()%>
                        </td>
                        <td>
                            <%# Eval( "LastLogonDate" ).ToString()%>
                        </td>
                        <td>
                            <%# Eval( "LastLogonIP" ).ToString( ) %>
                        </td>
                        <td>
                            <%# GetRegisterOrigin(Convert.ToByte(Eval("RegisterOrigin"))) %>
                        </td>
                        <td>
                            <%# GetNullityStatus((byte)Eval("Nullity"))%>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0)" onclick="openWindowOwn('GrantGameID.aspx?param=<%#Eval("UserID").ToString() %>','_GrantGameID',600,300);">
                                赠送靓号</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="listBg" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td style="width: 30px;">
                            <input name='cid' type='checkbox' value='<%# Eval("UserID").ToString()%>' />
                        </td>
                        <td>
                            <%# Eval( "UserID" ).ToString( ) %>
                        </td>
                        <td>
                            <%# Eval( "GameID" ).ToString( ) %>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0);" onclick="openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx?param=<%#Eval("UserID").ToString() %>','<%#Eval("UserID").ToString() %>',850,790);">
                            <%# Eval( "NickName" ).ToString( ) %>
                            </a>
                        </td>
                        <td>
                            <%# Eval( "Gender" ).ToString()=="1"?"男":"女"%>
                        </td>
                        <td>
                            <%# Eval( "SpreaderID" ).ToString()!="0"?GetGameID( Convert.ToInt32( Eval( "SpreaderID" ) ) ):"" %>
                        </td>
                        <td>
                            <%# Eval( "RegisterDate" ).ToString()%>
                        </td>
                        <td>
                            <%# Eval("RegisterIP").ToString()%>
                        </td>
                        <td>
                            <%# Eval( "GameLogonTimes" ).ToString()%>
                        </td>
                        <td>
                            <%# Eval( "LastLogonDate" ).ToString()%>
                        </td>
                        <td>
                             <%# Eval( "LastLogonIP" ).ToString( ) %>
                        </td>
                        <td>
                            <%# GetRegisterOrigin(Convert.ToByte(Eval("RegisterOrigin"))) %>
                        </td>
                        <td>
                            <%# GetNullityStatus((byte)Eval("Nullity"))%>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0)" onclick="openWindowOwn('GrantGameID.aspx?param=<%#Eval("UserID").ToString() %>','_GrantGameID',600,300);">
                                赠送靓号</a>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <asp:Literal runat="server" ID="litNoData" Visible="false" Text="<tr class='tdbg'><td colspan='100' align='center'><br>没有任何信息!<br><br></td></tr>"></asp:Literal>
        </table>
    </div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="listTitleBg">
                <span>选择：</span>&nbsp;<a class="l1" href="javascript:SelectAll(true);">全部</a>&nbsp;-&nbsp;<a class="l1" href="javascript:SelectAll(false);">无</a>
            </td>
            <td align="right" class="page">
                <gsp:AspNetPager ID="anpPage" OnPageChanged="anpPage_PageChanged" runat="server" AlwaysShow="true" FirstPageText="首页" LastPageText="末页"
                    PageSize="20" NextPageText="下页" PrevPageText="上页" ShowBoxThreshold="0" ShowCustomInfoSection="Left" LayoutType="Table"
                    NumericButtonCount="5" CustomInfoHTML="总记录：%RecordCount%　页码：%CurrentPageIndex%/%PageCount%　每页：%PageSize%" UrlPaging="false">
                </gsp:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
