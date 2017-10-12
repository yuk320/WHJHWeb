<%@ Page Title="" Language="C#" MasterPageFile="~/Card/Site.Master" AutoEventWireup="true" CodeBehind="AgentChildList.aspx.cs" Inherits="Game.Web.Card.AgentChildList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Css" runat="server">
    <link href="/Card/Css/agentinfo.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-single">&nbsp;</div>
    <div class="ui-content">
        <div class="ui-leftBox">
            <div>
                <span>我的昵称：</span>
                <em>
                    <asp:Label ID="lbNickName" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>代理级别：</span>
                <em>
                    <asp:Label ID="lbAgentLevel" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>我的代理：</span>
                <em>
                    <asp:HyperLink ID="lblMyAgent" runat="server" Text="0人"></asp:HyperLink>
                </em>
            </div>
            <div>
                <span>真实姓名：</span>
                <em>
                    <asp:Label ID="lbCompellation" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>联系 Q Q：</span>
                <em>
                    <asp:Label ID="lbQQ" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>当前房卡：</span>
                <em>
                    <asp:Label ID="lbDiamond" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>累计售卡：</span>
                <em>
                    <asp:Label ID="lbPresentTotal" runat="server" Text=""></asp:Label>
                </em>
            </div>
        </div>
        <div class="ui-right">
            <div>
                <span>我的 I D：</span>
                <em>
                    <asp:Label ID="lbGameID" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>代理域名：</span>
                <em>
                    <asp:Label ID="lbAgentDomain" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>我的玩家：</span>
                <em>
                    <asp:HyperLink ID="hlMyPlayer" runat="server" Text="0人"></asp:HyperLink>
                </em>
            </div>
            <div>
                <span>联系电话：</span>
                <em>
                    <asp:Label ID="lbContactPhone" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>联系微信：</span>
                <em>
                    <asp:Label ID="lbWCNickName" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>当月售卡：</span>
                <em>
                    <asp:Label ID="lbPresentMonth" runat="server" Text=""></asp:Label>
                </em>
            </div>
            <div>
                <span>&nbsp;</span>
                <em>
                </em>
            </div>
        </div>
        
    </div>
</asp:Content>