using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Kernel;
using Game.Web.UI;
using Game.Facade;
using Game.Entity.Enum;
using System.Data;
using System.Text;
using Game.Entity.Platform;
using Game.Entity.Treasure;
using Game.Entity.Record;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;

namespace Game.Web.Module.AppManager
{
    public partial class RankingRecordList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery1_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            int type = Convert.ToInt32(ddlSearchType.SelectedValue);
            int rank = Convert.ToInt32(ddlRankType.SelectedValue);
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE 1=1");
            if(rank > 0)
            {
                condition.AppendFormat(" AND TypeID={0}", rank);
            }

            if(!string.IsNullOrEmpty(query))
            {
                if(!Utils.Validate.IsPositiveInt(query))
                {
                    ShowError("输入查询格式不正确");
                    return;
                }
                if(type == 1)
                {
                    condition.AppendFormat(" AND UserID={0}", GetUserIDByGameID(Convert.ToInt32(query)));
                }
                else
                {
                    condition.AppendFormat(" AND UserID={0}", query);
                }
            }
            ViewState["SearchItems"] = condition.ToString();
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
                return string.Format("<td>{0}</td><td>{1}</td>", info.GameID, info.NickName);
            }
            return "<td></td><td></td>";
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList(RecordRankingRecevie.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
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
            set { ViewState["SearchItems"] = value; }
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
            set { ViewState["Orderby"] = value; }
        }
    }
}