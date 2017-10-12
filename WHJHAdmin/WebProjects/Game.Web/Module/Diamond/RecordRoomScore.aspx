<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordRoomScore.aspx.cs" Inherits="Game.Web.Module.Diamond.RecordRoomScore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../scripts/common.js"></script>

    <title>查看战绩</title>
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
                你当前位置：钻石系统 - 查看战绩
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <div id="content">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box" id="list">
            <tr align="center" class="bold">
                <td class="listTitle2">
                    玩家昵称
                </td>
                <td class="listTitle2">
                    游戏输赢
                </td>
                <td class="listTitle2">
                    赢局
                </td>
                <td class="listTitle2">
                    输局
                </td>
                <td class="listTitle2">
                    平局
                </td>
                <td class="listTitle2">
                    逃局
                </td>
            </tr>
            <asp:Repeater ID="rptDataList" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# GetNickNameByUserID(Convert.ToInt32(Eval( "UserID")))%>
                        </td>
                        <td>
                            <%# Eval( "Score")%>
                        </td>
                        <td>
                            <%# Eval( "WinCount")%>
                        </td>
                        <td>
                            <%# Eval( "LostCount")%>
                        </td>
                        <td>
                            <%# Eval( "DrawCount")%>
                        </td>
                        <td>
                            <%# Eval( "FleeCount")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="listBg" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# GetNickNameByUserID(Convert.ToInt32(Eval( "UserID")))%>
                        </td>
                        <td>
                            <%# Eval( "Score")%>
                        </td>
                        <td>
                            <%# Eval( "WinCount")%>
                        </td>
                        <td>
                            <%# Eval( "LostCount")%>
                        </td>
                        <td>
                            <%# Eval( "DrawCount")%>
                        </td>
                        <td>
                            <%# Eval( "FleeCount")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <asp:Literal runat="server" ID="litNoData" Visible="false" Text="<tr class='tdbg'><td colspan='100' align='center'><br>没有任何信息!<br><br></td></tr>"></asp:Literal>
        </table>
    </div>
    </form>
</body>
</html>
