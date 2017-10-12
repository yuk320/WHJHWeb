<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentUserUnder.aspx.cs" Inherits="Game.Web.Module.AgentManager.AgentUserUnder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/common.js"></script>
    <title>下线注册记录</title>
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
                你当前位置：用户系统 - 注册下线
            </td>
        </tr>
    </table>
    <!-- 头部菜单 End -->
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="关闭" class="btn wd1" onclick="window.close();" />&nbsp;&nbsp;
                <span style="color:white; font-size:14px; font-weight:bold;">下线总注册人数：<asp:Label ID="lbTotal" runat="server" Text="0"></asp:Label></span>
            </td>
        </tr>
    </table>
    <div id="content">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box" id="list">
            <tr align="center" class="bold">
                <td class="listTitle2">
                    注册时间
                </td>
                <td class="listTitle2">
                    注册来源
                </td>
                <td class="listTitle2">
                    游戏ID
                </td>
                <td class="listTitle2">
                    用户昵称
                </td>
                <td class="listTitle2">
                    推广级别
                </td>
                <td class="listTitle2">
                    代理状态
                </td>
            </tr>
            <asp:Repeater ID="rptDataList" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "RegisterDate" )%>
                        </td>
                        <td>
                            <%# GetRegisterOrigin(Convert.ToByte(Eval( "RegisterOrigin" ))) %>
                        </td>    
                        <td>
                            <%# Eval( "GameID" )%>
                        </td>
                          <td>
                            <%# Eval( "NickName" )%>
                        </td>   
                         <td>
                            <%# (Convert.ToInt32(Eval( "LevelID" ))-1) %>
                        </td>  
                        <td>
                            <%# Eval("AgentID").ToString()!="0"?"代理商":"非代理商" %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="listBg" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "RegisterDate" )%>
                        </td>
                         <td>
                            <%# GetRegisterOrigin(Convert.ToByte(Eval( "RegisterOrigin" ))) %>
                        </td> 
                        <td>
                            <%# Eval( "GameID" )%>
                        </td>
                          <td>
                            <%# Eval( "NickName" )%>
                        </td>   
                        <td>
                            <%# (Convert.ToInt32(Eval( "LevelID" ))-1) %>
                        </td> 
                        <td>
                            <%# Eval("AgentID").ToString()!="0"?"代理商":"非代理商" %>
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
