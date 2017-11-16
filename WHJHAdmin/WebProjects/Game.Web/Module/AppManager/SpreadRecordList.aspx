<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpreadRecordList.aspx.cs" Inherits="Game.Web.Module.AppManager.SpreadRecordList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../scripts/common.js"></script>
    <script type="text/javascript" src="../../scripts/comm.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top" class="Lpd10">
                <div class="arr">
                </div>
            </td>
            <td width="1232" height="25" valign="top" align="left">
                你当前位置：系统维护 - 推广奖励
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="titleQueBg">
        <tr>
            <td class="listTdLeft" style="width: 80px">日期查询：
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="text wd2" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtEndDate\')}'})"></asp:TextBox><img
                                                                                                                                                                                       src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtStartDate',dateFmt:'yyyy-MM-dd',maxDate:'#F{$dp.$D(\'txtEndDate\')}'})"
                                                                                                                                                                                       style="cursor: pointer; vertical-align: middle" />
                至
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="text wd2" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtStartDate\')}'})"></asp:TextBox><img
                                                                                                                                                                                       src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtEndDate',dateFmt:'yyyy-MM-dd',minDate:'#F{$dp.$D(\'txtStartDate\')}'})"
                                                                                                                                                                                       style="cursor: pointer; vertical-align: middle" />
                <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="btn wd1" OnClick="btnQuery_Click" />
                <asp:Button ID="btnQueryTD" runat="server" Text="今天" CssClass="btn wd1" OnClick="btnQueryTD_Click" />
                <asp:Button ID="btnQueryYD" runat="server" Text="昨天" CssClass="btn wd1" OnClick="btnQueryYD_Click" />
                <asp:Button ID="btnQueryTW" runat="server" Text="本周" CssClass="btn wd1" OnClick="btnQueryTW_Click" />
                <asp:Button ID="btnQueryYW" runat="server" Text="上周" CssClass="btn wd1" OnClick="btnQueryYW_Click" />
                <asp:Button ID="btnQueryTM" runat="server" Text="本月" CssClass="btn wd1" OnClick="btnQueryTM_Click" />
                <asp:Button ID="btnQueryYM" runat="server" Text="上月" CssClass="btn wd1" OnClick="btnQueryYM_Click" />
            </td>
        </tr>
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
            </td>
        </tr>
            
    </table>

    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <td height="39" class="titleOpBg">         
            </td>
    </table>
    <div id="content">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="box" id="list">
            <tr align="center" class="bold">
                <td class="listTitle2">
                    奖励时间
                </td>
                <td class="listTitle2">
                    游戏ID
                </td>                
                <td class="listTitle2">
                    游戏昵称
                </td>                
                <td class="listTitle2">
                    奖励推广人（已有推广人）
                </td>  
                <td class="listTitle2">
                    奖励前钻石
                </td>    
                <td class="listTitle2">
                    奖励钻石数
                </td>    
                <td class="listTitle2">
                    奖励道具
                </td>     
                <td class="listTitle2">
                    奖励数量
                </td>     
                <td class="listTitle2">
                    领取地址
                </td>             
            </tr>
            <asp:Repeater ID="rptDataList" runat="server">
                <ItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "CollectDate" )%>
                        </td>   
                        <%# GetAccountsInfo(Convert.ToInt32(Eval( "UserID" ))) %>  
                        <td>
                            <%# Eval( "SpreadNum" )%>（<%# Eval( "UserNum" )%>）
                        </td>  
                        <td>
                            <%# Eval( "CurrentDiamond" )%>
                        </td>  
                        <td>
                            <%# Eval( "PresentDiamond" )%>
                        </td>  
                        <td>
                            <%# Eval( "PresentPropID" ).ToString()!="0"?Eval( "PresentPropName" ).ToString():"" %>
                        </td>  
                        <td>
                            <%# Eval( "PresentPropNum" )%>
                        </td>  
                        <td>
                            <%# Eval( "ClientIP" )%>
                        </td>     
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr align="center" class="list" onmouseover="currentcolor=this.style.backgroundColor;this.style.backgroundColor='#caebfc';this.style.cursor='default';"
                        onmouseout="this.style.backgroundColor=currentcolor">
                        <td>
                            <%# Eval( "CollectDate" )%>
                        </td>   
                        <%# GetAccountsInfo(Convert.ToInt32(Eval( "UserID" ))) %>  
                        <td>
                            <%# Eval( "UserNum" )%>
                        </td>  
                        <td>
                            <%# Eval( "CurrentDiamond" )%>
                        </td>  
                        <td>
                            <%# Eval( "PresentDiamond" )%>
                        </td>  
                        <td>
                            <%# Eval( "PresentPropID" ).ToString()!="0"?Eval( "PresentPropName" ).ToString():"" %>
                        </td>  
                        <td>
                            <%# Eval( "PresentPropNum" )%>
                        </td>  
                        <td>
                            <%# Eval( "ClientIP" )%>
                        </td>                                 
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <asp:Literal runat="server" ID="litNoData" Visible="false" Text="<tr class='tdbg'><td colspan='100' align='center'><br>没有任何信息!<br><br></td></tr>"></asp:Literal>
        </table>
    </div>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="listTitleBg"><span>选择：</span>&nbsp;<a class="l1" href="javascript:SelectAll(true);">全部</a>&nbsp;-&nbsp;<a class="l1" href="javascript:SelectAll(false);">无</a></td>                
            <td align="right" class="page">                
                <gsp:AspNetPager ID="anpNews" runat="server" onpagechanged="anpNews_PageChanged" AlwaysShow="true" FirstPageText="首页" LastPageText="末页" PageSize="20" 
                    NextPageText="下页" PrevPageText="上页" ShowBoxThreshold="0" ShowCustomInfoSection="Left" LayoutType="Table" NumericButtonCount="5"
                    CustomInfoHTML="总记录：%RecordCount%　页码：%CurrentPageIndex%/%PageCount%　每页：%PageSize%">
                </gsp:AspNetPager>
            </td>
        </tr>
    </table> 
    </form>
</body>
</html>
