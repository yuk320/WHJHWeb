using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using System.Text;

using Game.Kernel;
using Game.Utils;
using Game.Facade;
using System.Data;
using Game.Entity.Treasure;

namespace Game.Web.Module.FilledManager
{
    public partial class RecordPayDiamond : AdminPage
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
                ShareInfoDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            ShareInfoDataBind();
        }
        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        private void SetCondition(string startDate, string endDate)
        {
            int service = int.Parse(ddlGlobalShareInfo.SelectedValue);
            StringBuilder condition = new StringBuilder("WHERE 1=1");
            if(service > 0)
            {
                condition.AppendFormat(" AND ShareID={0}", service);
            }
            if(!string.IsNullOrEmpty(startDate)&& !string.IsNullOrEmpty(endDate))
            {
                condition.AppendFormat(" AND OrderDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
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
            SetCondition(startDate + " 00:00:00", endDate + " 23:59:59");
            ShareInfoDataBind();
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
            ShareInfoDataBind();
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
            ShareInfoDataBind();
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
            ShareInfoDataBind();
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
            ShareInfoDataBind();
        }
        /// <summary>
        /// 用户查询
        /// </summary>
        protected void btnQueryAcc_Click(object sender, EventArgs e)
        {
            int type = Convert.ToInt32(ddlSearchType.SelectedValue);
            int status = Convert.ToInt32(ddlPayStatus.SelectedValue);
            string queryContent = CtrlHelper.GetTextAndFilter(txtSearch);

            StringBuilder condition = new StringBuilder("WHERE 1=1");
            if(status >= 0)
            {
                condition.AppendFormat(" AND OrderStatus='{0}'", status);
            }
            if(!string.IsNullOrEmpty(queryContent))
            {
                if(type == 0)
                {
                    condition.AppendFormat(" AND OrderID='{0}'", queryContent);
                }
                else if(type == 2)
                {
                    condition.AppendFormat(" AND NickName='{0}'", queryContent);
                }
                else
                {
                    if(!Utils.Validate.IsPositiveInt(queryContent))
                    {
                        ShowError("输入的查询格式不正确");
                        return;
                    }
                    if(type == 1)
                    {
                        condition.AppendFormat(" AND GameID={0}", queryContent);
                    }
                    else
                    {
                        condition.AppendFormat(" AND UserID={0}", queryContent);
                    }
                }
            }

            ViewState["SearchItems"] = condition.ToString();
            ShareInfoDataBind();
        }
        /// <summary>
        /// 充值类型
        /// </summary>
        /// <param name="shareID"></param>
        /// <returns></returns>
        public string GetOrderShareName(int shareID)
        {
            switch(shareID)
            {
                case 101:
                    return "手机微信充值";
                case 102:
                    return "H5微信充值";
                case 201:
                    return "手机支付宝充值";
                case 301:
                    return "手机零钱支付";
                case 800:
                    return "手机苹果充值";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 充值状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetPayStatus(int status)
        {
            if(status == 1)
            {
                return "<span>已支付</span>";
            }
            else
            {
                return "<span class='hong'>未支付</span>";
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void ShareInfoDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(OnLinePayOrder.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptShareInfo.DataSource = pagerSet.PageSet;
            rptShareInfo.DataBind();
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
                    string condition = "WHERE 1=1";
                    string startDate = CtrlHelper.GetText(txtStartDate);
                    string endDate = CtrlHelper.GetText(txtEndDate);
                    if(startDate != "" && endDate != "")
                    {
                        condition = condition + string.Format(" AND OrderDate BETWEEN '{0}' AND '{1}'", startDate + " 00:00:00", endDate + " 23:59:59");
                    }
                    ViewState["SearchItems"] = condition.ToString();
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
                    ViewState["Orderby"] = "ORDER BY OnLineID DESC";
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