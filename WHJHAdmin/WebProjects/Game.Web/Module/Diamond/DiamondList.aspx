<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiamondList.aspx.cs" Inherits="Game.Web.Module.Diamond.DiamondList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/comm.js"></script>
    <script type="text/javascript" src="/scripts/My97DatePicker/WdatePicker.js"></script>
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
                你当前位置：钻石系统 - 钻石管理
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg  Tmg7">
        <tr>
            <td class="listTdLeft" style="width: 80px">
                用户查询：
            </td>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="text"></asp:TextBox>
                <asp:DropDownList ID="ddlSearchType" runat="server">
                    <asp:ListItem Value="1">按游戏ID</asp:ListItem>
                    <asp:ListItem Value="2">按用户标识</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="Button1" runat="server" Text="查询" CssClass="btn wd1" OnClick="btnQuery1_Click" />
                <input type="button" value="钻石前50名" class="btn" style="width:85px;" onclick="openWindowOwn('TopDiamond.aspx', '', 850, 600);" />
                <span class="total-span">钻石总数统计：<asp:Label ID="lbTotal" runat="server" Text="0"></asp:Label></span> 
                <input type="button" value="赠送钻石" class="btn" style="width:85px;" onclick="openWindowOwn('GrantDiamond.aspx', '', 600, 270);" />
            </td>
        </tr>
    </table>
    <div id="content">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box Tmg7" id="list">
            <tr align="center" class="bold">
                <td class="listTitle2">
                    用户标识
                </td>
                <td class="listTitle2">
                    游戏ID
                </td>
                <td class="listTitle2">
                    玩家昵称
                </td>
                <td class="listTitle2">
                    钻石数量
                </td>
                <td class="listTitle2">
                    钻石操作
                </td>
            </tr>
            <asp:Repeater ID="rptDataList" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "UserID" ).ToString( )%>
                        </td>
                        <%# GetAccountsInfo(Convert.ToInt32(Eval( "UserID" ))) %>
                        <td>
                            <%# Eval( "Diamond" ).ToString( )%>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0)" onclick="javascript:openWindowOwn('GrantDiamond.aspx?param=<%#Eval("UserID").ToString() %>','GrantDiamond',600,240);">赠送钻石</a>
                        </td>  
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="listBg" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "UserID" ).ToString( )%>
                        </td>
                        <%# GetAccountsInfo(Convert.ToInt32(Eval( "UserID" ))) %>
                        <td>
                            <%# Eval( "Diamond" ).ToString( )%>
                        </td>
                        <td>
                            <a class="l" href="javascript:void(0)" onclick="javascript:openWindowOwn('GrantDiamond.aspx?param=<%#Eval("UserID").ToString() %>','GrantDiamond',600,240);">赠送钻石</a>
                        </td>  
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <asp:Literal runat="server" ID="litNoData" Visible="false" Text="<tr class='tdbg'><td colspan='100' align='center'><br>没有任何信息!<br><br></td></tr>"></asp:Literal>
        </table>
    </div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" class="page">
                <gsp:AspNetPager ID="anpNews" OnPageChanged="anpPage_PageChanged" runat="server" AlwaysShow="true" FirstPageText="首页" LastPageText="末页"
                    PageSize="20" NextPageText="下页" PrevPageText="上页" ShowBoxThreshold="0" ShowCustomInfoSection="Left" LayoutType="Table"
                    NumericButtonCount="5" CustomInfoHTML="总记录：%RecordCount%　页码：%CurrentPageIndex%/%PageCount%　每页：%PageSize%" UrlPaging="false">
                </gsp:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
