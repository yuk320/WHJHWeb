<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemMessageInfo.aspx.cs" Inherits="Game.Web.Module.AppManager.SystemMessageInfo" %>
<%@ Register Namespace="YYControls" Assembly="YYControls" TagPrefix="yyc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../styles/layout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/common.js"></script>
    <script type="text/javascript" src="../../scripts/My97DatePicker/WdatePicker.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 头部菜单 Start -->
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="title">
        <tr>
            <td width="19" height="25" valign="top"  class="Lpd10"><div class="arr"></div></td>
            <td width="1232" height="25" valign="top" align="left">你当前位置：系统维护 - 系统消息</td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('SystemMessageList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="listBg2">
        <tr>
            <td height="35" colspan="2" class="f14 bold Lpd10 Rpd10"><div class="hg3  pd7">
                系统消息</div></td>
        </tr>
        <tr style="height:150px;">
            <td class="listTdLeft">消息内容：</td>
            <td>        
                <asp:TextBox ID="txtMessageString" runat="server" CssClass="text" TextMode="MultiLine" Width="520px" Height="120px"></asp:TextBox>               
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="请输入消息内容" ControlToValidate="txtMessageString" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>      
        <tr style="height:270px;">
            <td class="listTdLeft">消息范围：</td>
            <td>        
                <div style="VISIBILITY: inherit; width:525px; height:260px; Z-INDEX: 1; overflow-x:hidden;overflow-y:auto; vertical-align:top; border:1px solid #899ba5; background-color:White;">
                    <yyc:SmartTreeView ID="tvKinds" runat="server" AllowCascadeCheckbox="true" ImageSet="News" 
                        ExpandDepth="1" NodeIndent="15">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </yyc:SmartTreeView>
                </div> 
            </td>
        </tr>   
        <tr>
            <td class="listTdLeft">消息类型：</td>
            <td>        
                <asp:DropDownList ID="ddlMessageType" runat="server" Width="158px">
                    <asp:ListItem Value="1" Text="游戏"></asp:ListItem>
                    <asp:ListItem Value="2" Text="房间"></asp:ListItem>
                    <asp:ListItem Value="3" Text="全部" Selected="True"></asp:ListItem>
                </asp:DropDownList>          
            </td>
        </tr>    
        <tr>
            <td class="listTdLeft">开始时间：</td>
            <td>        
                <asp:TextBox ID="txtStartTime" runat="server" CssClass="text" onfocus="WdatePicker({el:'txtStartTime',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox><img src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtStartTime',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" style="cursor:pointer;vertical-align:middle"/>             
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入开始时间" ControlToValidate="txtStartTime" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>      
        <tr>
            <td class="listTdLeft">结束时间：</td>
            <td>        
                <asp:TextBox ID="txtConcludeTime" runat="server" CssClass="text" onfocus="WdatePicker({el:'txtConcludeTime',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox><img src="../../Images/btn_calendar.gif" onclick="WdatePicker({el:'txtConcludeTime',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" style="cursor:pointer;vertical-align:middle"/>                
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入结束时间" ControlToValidate="txtConcludeTime" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>  
        <tr>
            <td class="listTdLeft">消息频率(秒)：</td>
            <td>        
                <asp:TextBox ID="txtTimeRate" runat="server" CssClass="text"></asp:TextBox>  
                <span class="hong">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请输入消息频率" Display="Dynamic" ControlToValidate="txtTimeRate" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="消息频率格式不正确" Display="Dynamic" ControlToValidate="txtTimeRate" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>             
            </td>
        </tr>  
        <tr>
            <td class="listTdLeft">消息备注：</td>
            <td>        
                <asp:TextBox ID="txtCollectNote" runat="server" CssClass="text" Width="520px"></asp:TextBox>               
            </td>
        </tr> 
        <tr>
            <td class="listTdLeft">
            </td>
            <td class="hong">
                <%--注意：修改成功后游戏中生效的时间为1个小时--%>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titleOpBg Lpd10">
                <input type="button" value="返回" class="btn wd1" onclick="Redirect('SystemMessageList.aspx')" />                           
                <input class="btnLine" type="button" />  
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn wd1" onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

