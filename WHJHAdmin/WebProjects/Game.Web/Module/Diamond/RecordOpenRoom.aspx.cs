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
using Game.Entity.Platform;
using System.Data;

namespace Game.Web.Module.Diamond
{
    public partial class RecordOpenRoom : AdminPage
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
        private void SetCondition(string startDate, string endDate)
        {
            string where = "WHERE PayMode=0";
            if(startDate != "" && endDate != "")
            {
                where = where + string.Format(" AND CreateDate BETWEEN '{0}' AND '{1}'", startDate, endDate);
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
            StringBuilder condition = new StringBuilder(" WHERE PayMode=0 ");

            if(!string.IsNullOrEmpty(query))
            {
                if(type == 2)
                {
                    condition.AppendFormat(" AND NickName='{0}'", query);
                }
                else
                {
                    if(!Utils.Validate.IsPositiveInt(query))
                    {
                        ShowError("输入的查询格式不正确");
                        return;
                    }
                    if(type == 0)
                    {
                        condition.AppendFormat(" AND RoomID={0}", query);
                    }
                    else
                    {
                        condition.AppendFormat(" AND UserID={0}", type == 1? GetUserIDByGameID(Convert.ToInt32(query)):Convert.ToInt32(query));
                    }
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
            string startDate = CtrlHelper.GetTextAndFilter(txtStartDate);
            string endDate = CtrlHelper.GetTextAndFilter(txtEndDate);

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
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetAccountsInfo(int userid)
        {
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userid);
            if(info != null)
            {
                return string.Format("<td>{0}</td><td><a class=\"l\" href=\"javascript:void(0)\" onclick=\"openWindowOwn('RecordPersonRoom.aspx?param={1}', 'RecordPersonRoom', 800, 610);\">{2}</a></td>", info.GameID, info.UserID, info.NickName);
            }
            return "<td></td><td></td>";
        }
        /// <summary>
        /// 获取房间名称
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public string GetRoomName(int roomid)
        {
            GameRoomInfo info = FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(roomid);
            return info != null ? info.ServerName : "";
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(StreamCreateTableFeeInfo.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();

            lbTotal.Text = FacadeManage.aidePlatformFacade.GetTotalCreateRoomDiamond(SearchItems).ToString();
            lbWait.Text = FacadeManage.aidePlatformFacade.GetTotalCreateRoomTable(SearchItems + " AND RoomStatus=0").ToString();
            lbGame.Text = FacadeManage.aidePlatformFacade.GetTotalCreateRoomTable(SearchItems + " AND RoomStatus=1").ToString();
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
                    ViewState["SearchItems"] = " WHERE PayMode=0";
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