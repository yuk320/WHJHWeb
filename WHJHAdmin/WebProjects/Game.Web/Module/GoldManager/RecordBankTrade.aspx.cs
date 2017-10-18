using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Game.Web.UI;
using Game.Kernel;
using Game.Utils;
using Game.Facade;
using Game.Entity.Enum;
using Game.Entity.Treasure;
using Game.Entity.Accounts;

namespace Game.Web.Module.GoldManager
{
    public partial class RecordBankTrade : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                RecordInsureDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            RecordInsureDataBind();
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        private void SetCondition(string startDate, string endDate)
        {
            string queryContent = CtrlHelper.GetTextAndFilter(txtSearch);
            int searchType = Convert.ToInt32(ddlSearchType.SelectedValue);
            int tradeType = Convert.ToInt32(ddlTradeType.SelectedValue);

            StringBuilder condition = new StringBuilder("WHERE 1=1");
            if(!string.IsNullOrEmpty(queryContent))
            {
                if(!Utils.Validate.IsPositiveInt(queryContent))
                {
                    ShowError("查询的游戏ID格式错误");
                    return;
                }
                condition.AppendFormat(" AND {0}={1}", searchType==1? "SourceUserID" : "TargetUserID",
                    GetUserIDByGameID(Convert.ToInt32(queryContent)));
            }
            if(tradeType > 0)
            {
                condition.AppendFormat(" AND TradeType={0}", tradeType);
            }
            if(!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                condition.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
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

            startDate = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd") + " 00:00:00";
            endDate = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd") + " 23:59:59";

            SetCondition(startDate, endDate);
            RecordInsureDataBind();
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
            RecordInsureDataBind();
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
            RecordInsureDataBind();
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
            RecordInsureDataBind();
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
            RecordInsureDataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void RecordInsureDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordInsure.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptInsure.DataSource = pagerSet.PageSet;
            rptInsure.DataBind();
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetAccountsInfo(int userid)
        {
            if(userid > 0)
            {
                AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userid);
                if(info != null)
                {
                    return string.Format("{0}（{1}）", info.NickName, info.GameID);
                }
            }
            return "";
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
                    ViewState["Orderby"] = "ORDER BY RecordID DESC";
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