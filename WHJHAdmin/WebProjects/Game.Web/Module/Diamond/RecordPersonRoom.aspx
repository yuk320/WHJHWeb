<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordPersonRoom.aspx.cs" Inherits="Game.Web.Module.Diamond.RecordPersonRoom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../scripts/common.js"></script>

    <title>开房记录</title>
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
                你当前位置：钻石系统 - 个人开房记录
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />&nbsp;&nbsp;
                <span style="color:white; font-weight:bold;">创建房间次数：<asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;
                总消耗钻石数：<asp:Label ID="lbDiamond" runat="server" Text="0"></asp:Label></span>
            </td>
        </tr>
    </table>
    <div id="content">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box" id="list">
            <tr align="center" class="bold">
                <td class="listTitle2">
                    创建日期
                </td>
                <td class="listTitle2">
                    房间ID
                </td>
                <td class="listTitle2">
                    游戏局数
                </td>
                <td class="listTitle2">
                    消耗钻石
                </td>
                <td class="listTitle2">
                    解散时间
                </td>
            </tr>
            <asp:Repeater ID="rptDataList" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "CreateDate")%>
                        </td>
                        <td>
                            <%# Eval( "RoomID" )%>
                        </td>
                        <td>
                            <%# Eval( "CountLimit" )%>
                        </td>
                        <td>
                            <%# Eval( "CreateTableFee" )%>
                        </td>
                        <td>
                            <%# Eval( "DissumeDate")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="listBg" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "CreateDate")%>
                        </td>
                        <td>
                            <%# Eval( "RoomID" )%>
                        </td>
                        <td>
                            <%# Eval( "CountLimit" )%>
                        </td>
                        <td>
                            <%# Eval( "CreateTableFee" )%>
                        </td>
                        <td>
                            <%# Eval( "DissumeDate")%>
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
                    PageSize="15" NextPageText="下页" PrevPageText="上页" ShowBoxThreshold="0" ShowCustomInfoSection="Left" LayoutType="Table"
                    NumericButtonCount="5" CustomInfoHTML="总记录：%RecordCount%　页码：%CurrentPageIndex%/%PageCount%　每页：%PageSize%" UrlPaging="false">
                </gsp:AspNetPager>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
