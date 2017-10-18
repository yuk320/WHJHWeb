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
using Game.Facade;
using Game.Entity.Record;
using System.Data;
using Game.Entity.Enum;

namespace Game.Web.Module.Diamond
{
    public partial class RecordDiamondExch : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-") + "01";
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        private void SetCondition()
        {
            string startDate = CtrlHelper.GetTextAndFilter(txtStartDate) + " 00:00:00";
            string endDate = CtrlHelper.GetTextAndFilter(txtEndDate) + " 23:59:59";
            string where = "WHERE 1=1";
            if (startDate != "" && endDate != "")
            {
                where = where + $" AND (CollectDate BETWEEN '{startDate}' AND '{endDate}')";
            }
            ViewState["SearchItems"] = where;
        }

        /// <summary>
        /// 数据查询
        /// </summary> 
        protected void btnQuery1_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            int type = int.Parse(ddlSearchType.SelectedValue);
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE 1=1");

            if (!string.IsNullOrEmpty(query))
            {
                if (!Utils.Validate.IsPositiveInt(query))
                {
                    ShowError("输入的查询格式不正确");
                    return;
                }
                switch (type)
                {
                    case 1:
                        condition.AppendFormat(" AND UserID={0} ", GetUserIDByGameID(Convert.ToInt32(query)));
                        break;
                    case 2:
                        condition.AppendFormat(" AND UserID={0} ", query);
                        break;
                    case 3:
                        condition.AppendFormat(" AND UserID={0} ", GetNickNameByUserID(Convert.ToInt32(query)));
                        break;
                    default:
                        break;
                }
            }
            ViewState["SearchItems"] = condition.ToString();
            BindData();
        }

        /// <summary>
        /// 时间查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 查询今天
        /// </summary>
        protected void btnQueryTD_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetTodayTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetTodayTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            BindData();
        }

        /// <summary>
        /// 查询昨天
        /// </summary>
        protected void btnQueryYD_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetYesterdayTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetYesterdayTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            BindData();
        }

        /// <summary>
        /// 查询本周
        /// </summary>
        protected void btnQueryTW_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetWeekTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetWeekTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            BindData();
        }

        /// <summary>
        /// 查询上周
        /// </summary>
        protected void btnQueryYW_Click(object sender, EventArgs e)
        {
            CtrlHelper.SetText(txtStartDate,
                Convert.ToDateTime(Fetch.GetLastWeekTime().Split('$')[0]).ToString("yyyy-MM-dd"));
            CtrlHelper.SetText(txtEndDate,
                Convert.ToDateTime(Fetch.GetLastWeekTime().Split('$')[1]).ToString("yyyy-MM-dd"));

            BindData();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetAccountsInfo(int userid)
        {
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userid);
            return info != null ? $"<td>{info.GameID}</td><td>{info.NickName}</td>" : "<td></td><td></td>";
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            SetCondition();
            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetList(RecordCurrencyExch.Tablename,
                anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();

            long[] total = FacadeManage.aideRecordFacade.GetTotalDiamondExch(SearchItems);
            lbTotal.Text = "兑换金币:" + total[0] + " 花费钻石:" + total[1];
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if (ViewState["SearchItems"] == null)
                {
                    ViewState["SearchItems"] = "WHERE 1=1";
                }
                return (string) ViewState["SearchItems"];
            }
            set { ViewState["SearchItems"] = value; }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY RecordID DESC";
                }
                return (string) ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }

        /// <summary>
        /// 获取场景描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTypeDesc(object type)
        {
            return EnumHelper.GetDesc(typeof(DiamondExchType), type);
        }
    }
}