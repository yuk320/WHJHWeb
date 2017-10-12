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
using Game.Entity.Enum;
using Game.Facade;
using Game.Entity.Accounts;
using Game.Entity.Record;

namespace Game.Web.Module.WebManager
{
    public partial class UMessagePushList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                RPDataBind();
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            RPDataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void RPDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetList(RecordAccountsUmeng.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptIssue.DataSource = pagerSet.PageSet;
            rptIssue.DataBind();
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