using System;
using Game.Facade;
using Game.Entity.Accounts;

namespace Game.Web.Card
{
    public partial class AgentInfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 0;
            if (!IsPostBack)
            {
                DateTime monthFirst = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //获取登录信息
                AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByUserID(userTicket.UserID);
                lbNickName.Text = info?.NickName ?? "";
                //获取代理信息
                AccountsAgentInfo agent =
                    FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(userTicket.AgentID);
                if (agent != null)
                {
                    lbAgentDomain.Text = agent.AgentDomain;
                    lbAgentLevel.Text = agent.AgentLevel == 1 ? "一级代理" : agent.AgentLevel == 2 ? "二级代理" : "三级代理";
                    lbCompellation.Text = agent.Compellation;
                    lbContactPhone.Text = agent.ContactPhone;
                    lbQQ.Text = agent.QQAccount;
                    lbWCNickName.Text = agent.WCNickName;
                    lbGameID.Text = userTicket?.GameID.ToString() ?? "";
                    lnkMyAgent.Text = FacadeManage.aideAccountsFacade.GetAgentBelowAgentCount(userTicket.UserID) + "人";
                    lnkMyAgent.NavigateUrl = "Under.aspx?type=agent";
                    lnkMyPlayer.Text = FacadeManage.aideRecordFacade.GetAgentBelowAccountsCount(userTicket.UserID) + "人";
                    lnkMyPlayer.NavigateUrl = "Under.aspx?type=user";

                    long diamond = FacadeManage.aideTreasureFacade.GetUserWealth(userTicket.UserID)?.Diamond ?? 0;
                    long pMonth =
                        FacadeManage.aideRecordFacade.GetAgentPresentOutCount(userTicket.UserID,
                            $" AND CollectDate>= '{monthFirst}'");
                    long pTotal =
                        FacadeManage.aideRecordFacade.GetAgentPresentOutCount(userTicket.UserID);
                    lbDiamond.Text = diamond == 0 ? "0" : diamond.ToString("##,###");
                    lbPresentMonth.Text = pMonth == 0 ? "0" : pMonth.ToString("##,###");
                    lbPresentTotal.Text = pTotal == 0 ? "0" : pMonth.ToString("##,###");
                }
            }
        }
    }
}