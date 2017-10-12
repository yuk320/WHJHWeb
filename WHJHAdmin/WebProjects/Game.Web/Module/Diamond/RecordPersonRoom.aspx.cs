using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using System.Data;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Entity.Platform;

namespace Game.Web.Module.Diamond
{
    public partial class RecordPersonRoom : AdminPage
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
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(StreamCreateTableFeeInfo.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
            DataSet ds = FacadeManage.aidePlatformFacade.GetCreateRoomInfo(IntParam);
            lbCount.Text = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["UCont"].ToString() : "0";
            lbDiamond.Text = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["CTotal"].ToString() : "0";
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
                    ViewState["SearchItems"] = string.Format("WHERE UserID={0} AND PayMode=0", IntParam);
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