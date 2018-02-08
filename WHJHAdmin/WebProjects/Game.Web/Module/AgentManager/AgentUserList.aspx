<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentUserList.aspx.cs" Inherits="Game.Web.Module.AgentManager.AgentUserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
                你当前位置：用户系统 - 代理账号
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg">
        <tr>
            <td align="center"  style="width: 80px">
                代理查询：
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="text"></asp:TextBox>
                <asp:DropDownList ID="ddlSearchType" runat="server">
                    <asp:ListItem Text="游戏ID" Value ="1"></asp:ListItem>
                    <asp:ListItem Text="真实姓名" Value ="2"></asp:ListItem>
                    <asp:ListItem Text="QQ账号" Value ="3"></asp:ListItem>
                    <asp:ListItem Text="代理编号" Value ="4"></asp:ListItem>
                    <asp:ListItem Text="微信昵称" Value ="5"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAgentLevel" runat="server">
                    <asp:ListItem Text="全部等级" Value ="0"></asp:ListItem>
                    <asp:ListItem Text="一级代理" Value ="1"></asp:ListItem>
                    <asp:ListItem Text="二级代理" Value ="2"></asp:ListItem>
                    <asp:ListItem Text="三级代理" Value ="3"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="btn wd1" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg Tmg7">
        <tr>
            <td align="center"  style="width: 80px">
                代理编号：
            </td>
            <td>
                <asp:TextBox ID="txtAgentId" runat="server" CssClass="text"></asp:TextBox>
                <asp:DropDownList ID="ddlRelation" runat="server">
                    <asp:ListItem Text="上级代理" Value ="1"></asp:ListItem>
                    <asp:ListItem Text="下级代理" Value ="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnDown" runat="server" Text="代理查询" CssClass="btn wd1" Width="68px" OnClick="btnDown_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="Tmg7">
        <tr>
            <td height="39" class="titleOpBg">
                <input type="button" value="新增" class="btn wd1" onclick="openWindowOwn('AgentUserInfo.aspx', 'addagent', 700, 600)" />
                <asp:Button ID="btnDongjie" runat="server" Text="冻结" CssClass="btn wd1" OnClick="btnDongjie_Click" OnClientClick="return deleteop()" />
                <asp:Button ID="btnJiedong" runat="server" Text="解冻" CssClass="btn wd1" OnClick="btnJiedong_Click" OnClientClick="return deleteop()" />
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
                  管理
                </td>
                <td class="listTitle2">
                    代理编号
                </td>
                <td class="listTitle2">
                    游戏ID
                </td>
                <td class="listTitle2">
                    代理昵称
                </td>
                <td class="listTitle2">
                    真实姓名
                </td>
                <td class="listTitle2">
                    代理域名
                </td>
                <td class="listTitle2">
                    代理等级
                </td>
                <td class="listTitle2">
                    直属上级
                </td>
                <td class="listTitle2">
                    携带钻石
                </td>
                <td class="listTitle2">
                    总转入量
                </td>
                <td class="listTitle2">
                    总转出量
                </td>
                <td class="listTitle2">
                    赠送下级代理钻石
                </td>
                <td class="listTitle2">
                    赠送下级玩家钻石
                </td>
                <td class="listTitle2">
                    代理状态
                </td>
                <td class="listTitle2">
                    代理操作
                </td>
            </tr>
            <asp:Repeater ID="rptDataList" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td style="width: 30px;">
                            <input name='cid' type='checkbox' value='<%# Eval("AgentID").ToString()%>' />
                        </td>
                        <td>
                            <a href="javascript:;" class="l" onclick="openWindowOwn('AgentUserUpdate.aspx?param=<%# Eval( "AgentID") %>', '', 700,490);">编辑</a>
                        </td>
                        <td>
                            <%# Eval( "AgentID" ) %>
                        </td>
                        <%# GetAccountsInfo(Convert.ToInt32(Eval( "UserID" )), Convert.ToInt32(Eval( "AgentID" ))) %>
                        <td>
                            <%# Eval( "Compellation" ) %>
                        </td>
                        <td>
                            <%# Eval( "AgentDomain" ) %>
                        </td>
                        <td>
                            <%# Eval( "AgentLevel" ).ToString()=="1"?"一级代理":Eval( "AgentLevel" ).ToString()=="2"?"二级代理":"三级代理" %>
                        </td>
                        <td>
                            <%# GetAgentInfo( Convert.ToInt32( Eval( "ParentAgent" ) ) ) %>
                        </td>
                        <%# GetAgentDiamond(Convert.ToInt32(Eval( "UserID" ))) %>
                        <td>
                            <%# GetNullityStatus(Convert.ToByte(Eval( "Nullity" ))) %>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0)" onclick="javascript:openWindowOwn('AgentGrantDiamond.aspx?param=<%#Eval("UserID").ToString() %>','GrantDiamond',600,240);">赠送钻石</a>
                            <a class="l" href="javascript:void(0)" onclick="javascript:openWindowOwn('AgentUserUnder.aspx?param=<%#Eval("UserID").ToString() %>','UnderUser',700,600);">查看下线</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="listBg" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td style="width: 30px;">
                            <input name='cid' type='checkbox' value='<%# Eval("AgentID").ToString()%>' />
                        </td>
                      <td>
                        <a href="javascript:;" class="l" onclick="openWindowOwn('AgentUserUpdate.aspx?param=<%# Eval( "AgentID") %>', '', 700,490);">编辑</a>
                      </td>
                      <td>
                            <%# Eval( "AgentID" ) %>
                        </td>
                        <%# GetAccountsInfo(Convert.ToInt32(Eval( "UserID" )), Convert.ToInt32(Eval( "AgentID" ))) %>
                        <td>
                            <%# Eval( "Compellation" ) %>
                        </td>
                        <td>
                            <%# Eval( "AgentDomain" ) %>
                        </td>
                        <td>
                            <%# Eval( "AgentLevel" ).ToString()=="1"?"一级代理":Eval( "AgentLevel" ).ToString()=="2"?"二级代理":"三级代理" %>
                        </td>
                        <td>
                            <%# GetAgentInfo( Convert.ToInt32( Eval( "ParentAgent" ) ) ) %>
                        </td>
                        <%# GetAgentDiamond(Convert.ToInt32(Eval( "UserID" ))) %>
                        <td>
                            <%# GetNullityStatus(Convert.ToByte(Eval( "Nullity" ))) %>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0)" onclick="javascript:openWindowOwn('AgentGrantDiamond.aspx?param=<%#Eval("UserID").ToString() %>','GrantDiamond',600,240);">赠送钻石</a>
                            <a class="l" href="javascript:void(0)" onclick="javascript:openWindowOwn('AgentUserUnder.aspx?param=<%#Eval("UserID").ToString() %>','UnderUser',700,600);">查看下线</a>
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
