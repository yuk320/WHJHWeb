using System;
using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using Game.Entity.Record;
using Game.Facade;
using Game.Entity.Enum;

namespace Game.Web.Module.AccountManager
{
    public partial class DiamondConsume : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtStartDate.Text = GameRequest.GetQueryString("stime");
                txtEndDate.Text = GameRequest.GetQueryString("etime");
                cbType.DataSource = GetSerialTypeList(typeof(DiamondSerialType),"consume");
                cbType.DataTextField = "Description";
                cbType.DataValueField = "EnumValue";
                cbType.DataBind();
                BindData();
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
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void SetCondition(string startDate, string endDate)
        {
            StringBuilder condition = new StringBuilder();
            condition.AppendFormat(" WHERE UserID={0} AND ChangeDiamond<0", IntParam);
            if(!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                condition.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
            }
            ViewState["SearchItems"] = condition.ToString();
        }
        /// <summary>
        /// 时间查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();

            SetCondition(startDate + " 00:00:00", endDate + " 23:59:59");
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
        /// 数据查询
        /// </summary>
        protected void btnType_Click(object sender, EventArgs e)
        {
            StringBuilder condition = new StringBuilder();
            string typeList = "";
            for(int i = 0; i < cbType.Items.Count; i++)
            {
                if(cbType.Items[i].Selected)
                {
                    typeList = typeList + cbType.Items[i].Value + ",";
                }
            }
            if(typeList == "")
            {
                condition.AppendFormat(" WHERE UserID={0} AND ChangeDiamond<0 ", IntParam);
            }
            else
            {
                typeList = typeList.Substring(0, (typeList.Length - 1));
                condition.AppendFormat(" WHERE UserID={0} AND ChangeDiamond<0 AND TypeID IN({1})", IntParam, typeList);
            }
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            if(!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                condition.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", startDate + " 00:00:00", endDate + " 23:59:59");
            }
            ViewState["SearchItems"] = condition.ToString();
            BindData();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetList(RecordDiamondSerial.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();

            //绑定统计
            lbChange.Text = FacadeManage.aideRecordFacade.GetTotalDiamondChange(SearchItems).ToString();
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
                    string condition = string.Format(" WHERE UserID={0} AND ChangeDiamond<0", IntParam);
                    string startDate = CtrlHelper.GetText(txtStartDate);
                    string endDate = CtrlHelper.GetText(txtEndDate);
                    if(startDate != "" && endDate != "")
                    {
                        condition = condition + string.Format(" AND CollectDate BETWEEN '{0}' AND '{1}'", startDate + " 00:00:00", endDate + " 23:59:59");
                    }
                    ViewState["SearchItems"] = condition;
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
                    ViewState["Orderby"] = "ORDER BY CollectDate DESC";
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