using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Game.Entity.Accounts;
using Game.Web.UI;
using Game.Kernel;
using Game.Utils;
using Game.Facade;
using Game.Entity.Treasure;

namespace Game.Web.Module.GoldManager
{
    public partial class RecordGameInOut : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd");
                txtStartDate.Text = time;
                txtEndDate.Text = time;
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            UserInoutDataBind();
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        private void SetCondition(string startDate, string endDate)
        {
            StringBuilder condition = new StringBuilder();
            int typeTime = int.Parse(ddlTimeType.SelectedValue);
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            int typeId = Convert.ToInt32(ddlSearchType.SelectedValue);
            if(string.IsNullOrEmpty(query))
            {
                ShowError("请输入用户信息进行查询");
                return;
            }
            if(!Utils.Validate.IsPositiveInt(query))
            {
                ShowError("输入用户格式不正确");
                return;
            }
            int id = Convert.ToInt32(query);
            condition.AppendFormat(" WHERE UserID={0}", typeId == 1 ? GetUserIDByGameID(id) : id);
            if(startDate != "" && endDate != "")
            {
                switch(typeTime)
                {
                    case 1:
                        condition.AppendFormat(" AND EnterTime BETWEEN '{0}' AND '{1}'", Convert.ToDateTime(startDate), Convert.ToDateTime(endDate).AddDays(1).ToString("yyyy-MM-dd"));
                        break;
                    case 2:
                        condition.AppendFormat(" AND LeaveTime BETWEEN '{0}' AND '{1}'", Convert.ToDateTime(startDate), Convert.ToDateTime(endDate).AddDays(1).ToString("yyyy-MM-dd"));
                        break;
                    case 3:
                        condition.AppendFormat(" AND EnterTime <= '{0}' AND LeaveTime<='{1}'", Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
                        break;
                    default:
                        condition.AppendFormat(" AND EnterTime BETWEEN '{0}' AND '{1}'", startDate, Convert.ToDateTime(endDate).AddDays(1).ToString("yyyy-MM-dd"));
                        break;
                }
            }
            ViewState["SearchItems"] = condition.ToString();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string startDate = CtrlHelper.GetText(txtStartDate);
            string endDate = CtrlHelper.GetText(txtEndDate);
            SetCondition(startDate, endDate);
            UserInoutDataBind();
        }
        /// <summary>
        /// 查询今天
        /// </summary>
        protected void btnQueryTD_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetTodayTime().Split('$')[0].ToString();
            string endDate = Fetch.GetTodayTime().Split('$')[1].ToString();

            CtrlHelper.SetText(txtStartDate, Convert.ToDateTime(startDate).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate, Convert.ToDateTime(endDate).ToString("yyyy-MM-dd"));

            SetCondition(startDate, endDate);
            UserInoutDataBind();
        }
        /// <summary>
        /// 查询昨天
        /// </summary>
        protected void btnQueryYD_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetYesterdayTime().Split('$')[0].ToString();
            string endDate = Fetch.GetYesterdayTime().Split('$')[1].ToString();

            CtrlHelper.SetText(txtStartDate, Convert.ToDateTime(startDate).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate, Convert.ToDateTime(endDate).ToString("yyyy-MM-dd"));

            SetCondition(startDate, endDate);
            UserInoutDataBind();
        }
        /// <summary>
        /// 查询本周
        /// </summary>
        protected void btnQueryTW_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetWeekTime().Split('$')[0].ToString();
            string endDate = Fetch.GetWeekTime().Split('$')[1].ToString();

            CtrlHelper.SetText(txtStartDate, Convert.ToDateTime(startDate).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate, Convert.ToDateTime(endDate).ToString("yyyy-MM-dd"));

            SetCondition(startDate, endDate);
            UserInoutDataBind();
        }
        /// <summary>
        /// 查询上周
        /// </summary>
        protected void btnQueryYW_Click(object sender, EventArgs e)
        {
            string startDate = Fetch.GetLastWeekTime().Split('$')[0].ToString();
            string endDate = Fetch.GetLastWeekTime().Split('$')[1].ToString();

            CtrlHelper.SetText(txtStartDate, Convert.ToDateTime(startDate).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate, Convert.ToDateTime(endDate).ToString("yyyy-MM-dd"));

            SetCondition(startDate, endDate);
            UserInoutDataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void UserInoutDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordUserInout.Tablename,
                anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            litTip.Visible = false;
            rptUserInout.DataSource = pagerSet.PageSet;
            rptUserInout.DataBind();
        }
        /// <summary>
        /// 获取离开原因
        /// </summary>
        public string GetLeaveReason(int typeId)
        {
            switch(typeId)
            {
                case 0:
                    return "常规离开";
                case 1:
                    return "系统原因";
                case 2:
                    return "用户冲突";
                case 3:
                    return "网络原因";
                default:
                    return "人满为患";
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        protected string GetAccountsInfo(int userid)
        {
            AccountsInfo userInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userid);
            if (userInfo == null || userInfo.UserID == 0) return "";
            return $"{userInfo.UserID}|{userInfo.GameID}|{userInfo.NickName}";
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
                    ViewState["Orderby"] = "ORDER BY ID DESC";
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