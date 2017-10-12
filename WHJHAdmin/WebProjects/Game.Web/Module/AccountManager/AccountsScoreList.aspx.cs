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
using Game.Entity;
using System.Data;
using Game.Facade;
using Game.Entity.Enum;
using Game.Entity.Platform;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsScoreList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGameInfo();
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
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            int type = int.Parse(ddlSearchType.SelectedValue);
            StringBuilder condition = new StringBuilder(" WHERE 1=1");
            if(!string.IsNullOrEmpty(query))
            {
                if(!Utils.Validate.IsPositiveInt(query))
                {
                    ShowError("输入查询格式不正确");
                    return;
                }
                switch(type)
                {
                    case 1:
                        condition.AppendFormat(" AND UserID={0}", GetUserIDByGameID(Convert.ToInt32(query)));
                        break;
                    case 2:
                        condition.AppendFormat(" AND Score>={0} ", query);
                        break;
                    case 3:
                        condition.AppendFormat(" AND Score<={0} ", query);
                        break;
                    default:
                        break;
                }
            }
            
            ViewState["SearchItems"] = condition.ToString();
            BindData();
        }
        /// <summary>
        /// 游戏列表绑定
        /// </summary>
        private void BindGameInfo()
        {
            PagerSet ps = FacadeManage.aidePlatformFacade.GetList(GameGameItem.Tablename, 1, 100, "WHERE 1=1", "ORDER BY GameID DESC");
            if(ps.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlGame.DataSource = ps.PageSet.Tables[0];
                ddlGame.DataTextField = "GameName";
                ddlGame.DataValueField = "GameID";
                ddlGame.DataBind();
            }
            ddlGame.Items.Insert(0, new ListItem("选择游戏", "0"));
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            //获取选择的游戏
            int kindID = Convert.ToInt32(ddlGame.SelectedValue);
            if(kindID > 0)
            {
                //PagerSet pagerSet = new TreasureFacade(kindID).GetGameScoreList(anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
                //anpPage.RecordCount = pagerSet.RecordCount;
                //litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
                //rptDataList.DataSource = pagerSet.PageSet;
                //rptDataList.DataBind();
                //litNoSelect.Visible = false;
            }
            else
            {
                litNoSelect.Visible = true;
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
                    ViewState["SearchItems"] = " WHERE 1=1";
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