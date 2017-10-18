using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Web.UI;
using System;
using System.Data;
using System.Text;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentUserList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AgentDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            AgentDataBind();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            int type = Convert.ToInt32(ddlSearchType.SelectedValue);
            int level = Convert.ToInt32(ddlAgentLevel.SelectedValue);
            StringBuilder condition = new StringBuilder(" WHERE 1=1");

            if(!string.IsNullOrEmpty(query))
            {
                if(type == 2)
                {
                    condition.AppendFormat(" AND Compellation='{0}'", query);
                }
                else if(type == 3)
                {
                    condition.AppendFormat(" AND QQAccount='{0}'", query);
                } else if (type == 5)
                {
                    condition.AppendFormat(" AND WCNickName='{0}'", query);
                }
                else
                {
                    if(!Utils.Validate.IsPositiveInt(query))
                    {
                        ShowError("输入查询格式不正确");
                        return;
                    }

                    if(type == 4)
                    {
                        condition.AppendFormat(" AND AgentID = {0}", query);
                    }
                    else
                    {
                        condition.AppendFormat(" AND UserID = {0}", GetUserIDByGameID(Convert.ToInt32(query)));
                    }
                }
            }
            if(level > 0)
            {
                condition.AppendFormat(" AND AgentLevel={0}", level);
            }

            ViewState["SearchItems"] = condition.ToString();
            AgentDataBind();
        }
        /// <summary>
        /// 代理查询
        /// </summary>
        protected void btnDown_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtAgentId);
            int type = Convert.ToInt32(ddlRelation.SelectedValue);

            if(!Utils.Validate.IsPositiveInt(query))
            {
                ShowError("输入查询格式不正确");
                return;
            }
            StringBuilder condition = new StringBuilder(" WHERE 1=1");
            if(type == 1)
            {
                condition.AppendFormat(" AND (AgentID IN(SELECT ParentAgent FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID={0}) OR AgentID IN(SELECT ParentAgent FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID IN(SELECT ParentAgent FROM AccountsAgentInfo WITH(NOLOCK) WHERE AgentID={0})))", query);
            }
            else if(type == 2)
            {
                condition.AppendFormat(" AND (ParentAgent = {0} OR ParentAgent IN (SELECT AgentID FROM AccountsAgentInfo WITH(NOLOCK) WHERE ParentAgent={0}))", query);
            }
            else
            {
                condition.Append(" AND 1=2");
            }
            ViewState["SearchItems"] = condition.ToString();
            AgentDataBind();
        }
        /// <summary>
        /// 批量冻结玩家
        /// </summary>
        protected void btnDongjie_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aideAccountsFacade.NullityAgentUser(StrCIdList, 1);
            if(result > 0)
            {
                ShowInfo("冻结成功");
                AgentDataBind();
            }
            else
            {
                ShowError("冻结失败");
            }
        }
        /// <summary>
        /// 批量解冻玩家
        /// </summary>
        protected void btnJiedong_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aideAccountsFacade.NullityAgentUser(StrCIdList, 0);
            if(result > 0)
            {
                ShowInfo("解冻成功");
                AgentDataBind();
            }
            else
            {
                ShowError("解冻失败");
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="agentid"></param>
        /// <returns></returns>
        protected string GetAccountsInfo(int userid, int agentid)
        {
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userid);
            if(info != null)
            {
                return string.Format("<td>{0}</td><td><a class=\"l\" href=\"javascript:void(0)\" onclick=\"openWindowOwn('AgentUserUpdate.aspx?param={1}', '{2}', 700,490);\">{2}</a></td>", 
                    info.GameID, agentid, info.NickName);
            }
            return "<td></td><td></td>";
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void AgentDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(AccountsAgentInfo.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
            anpPage.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }
        /// <summary>
        /// 获取代理钻石情况
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        protected string GetAgentDiamond(int userid)
        {
            DataSet ds = FacadeManage.aideRecordFacade.GetQueryAgentDiamond(userid);
            DataTable table = ds.Tables[0];
            if (table.Rows.Count <= 0) return "<td>0</td><td>0</td><td>0</td><td>0</td><td>0</td>";
            string diamond = table.Rows[0]["Diamond"]?.ToString() ?? "0";
            string inDiamond = table.Rows[0]["InDiamond"]?.ToString() ?? "0";
            string outDiamond = table.Rows[0]["OutDiamond"]?.ToString() ?? "0";
            string agentDiamond = table.Rows[0]["AgentDiamond"]?.ToString() ?? "0";
            string userDiamond = table.Rows[0]["UserDiamond"]?.ToString() ?? "0";

            return $"<td>{diamond}</td><td>{inDiamond}</td><td>{outDiamond}</td><td>{agentDiamond}</td><td>{userDiamond}</td>";
        }

        /// <summary>
        /// 通过代理ID获取代理信息
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        protected string GetAgentInfo(int agentid)
        {
            if (agentid<=0) return "";
            string nickName = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(FacadeManage.aideAccountsFacade.GetAccountsAgentInfo(agentid).UserID).NickName;
            return
                $"<a class=\"l\" href=\"javascript:void(0)\" onclick=\"openWindowOwn('AgentUserUpdate.aspx?param={agentid}', '{nickName}', 700,490);\">{nickName}</a>";
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if(ViewState["SearchItems"] == null)
                {
                    ViewState["SearchItems"] = "WHERE 1=1";
                }
                return (string)ViewState["SearchItems"];
            }
            set
            {
                ViewState["SearchItems"] = value;
            }
        }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if(ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY AgentID DESC";
                }
                return (string)ViewState["Orderby"];
            }
            set
            {
                ViewState["Orderby"] = value;
            }
        }

    }
}