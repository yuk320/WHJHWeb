using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using Game.Entity.Accounts;
using System.Data;
using Game.Facade;
using Game.Entity.Treasure;
using Game.Entity.Platform;

namespace Game.Web.Module.GoldManager
{
    public partial class RecordUserGame : AdminPage
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
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        private void SetCondition(string startDate, string endDate)
        {
            StringBuilder condition = new StringBuilder();
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
            condition.AppendFormat(" WHERE DrawID IN (SELECT DrawID FROM  RecordDrawScore WHERE UserID={0})", typeId == 1 ? GetUserIDByGameID(id) : id);
            if(!string.IsNullOrEmpty(startDate))
                condition.AppendFormat(" AND ConcludeTime >= '{0}' ", startDate);
            if(!string.IsNullOrEmpty(endDate))
                condition.AppendFormat(" AND ConcludeTime < '{0}'", Convert.ToDateTime(endDate).AddDays(1).ToString("yyyy-MM-dd"));
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
            BindData();
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
            BindData();
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
            BindData();
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
            BindData();
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
            BindData();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(RecordDrawInfo.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
            anpPage.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            litTip.Visible = false;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public string GetUserInfo(int userid)
        {
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userid);
            if(info != null)
            {
                return string.Format("<td>{0}</td><td>{1}</td>", info.NickName, info.IsAndroid == 1 ? "是" : "否");
            }
            return "<td></td><td></td>";
        }
        /// <summary>
        /// 子数据绑定
        /// </summary>
        protected void rptDataList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView dataItem = (DataRowView)e.Item.DataItem;
                PagerSet ps = FacadeManage.aideTreasureFacade.GetList(RecordDrawScore.Tablename, 1, 1000, string.Format("WHERE DrawID={0}", dataItem["DrawID"].ToString()), "ORDER BY DrawID DESC");
                if(ps.PageSet.Tables.Count > 0)
                {
                    Repeater repeater = (Repeater)e.Item.FindControl("rptSubData");
                    if(repeater != null)
                    {
                        repeater.DataSource = ps.PageSet;
                        repeater.DataBind();
                    }
                }
            }
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
                    ViewState["Orderby"] = "ORDER BY DrawID DESC";
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