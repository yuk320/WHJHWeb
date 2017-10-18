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
using Game.Entity.Treasure;
using System.Data;

namespace Game.Web.Module.Diamond
{
    public partial class DiamondList : AdminPage
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
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery1_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            int type = int.Parse(ddlSearchType.SelectedValue);

            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE 1=1");
            if(!string.IsNullOrEmpty(query))
            {
                if(!Utils.Validate.IsPositiveInt(query))
                {
                    ShowError("输入查询格式不正确");
                    return;
                }
                condition.AppendFormat(" AND UserID={0}", type == 1 ? GetUserIDByGameID(Convert.ToInt32(query)) : Convert.ToInt32(query));
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
                return string.Format("<td>{0}</td><td><a class=\"l\" href=\"javascript: void(0)\" onclick=\"openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx?param={1}', 'RecordPersonRoom', 850,790);\">{2}</a></td>", info.GameID, info.UserID, info.NickName);
            }
            return "<td></td><td></td>";
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(UserCurrency.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();

            lbTotal.Text = FacadeManage.aideTreasureFacade.GetTotalUserDiamond(SearchItems).ToString();
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
                    ViewState["Orderby"] = "ORDER BY UserID DESC";
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