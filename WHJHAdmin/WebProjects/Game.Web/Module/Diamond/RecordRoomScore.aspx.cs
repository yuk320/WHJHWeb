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
using Game.Entity.Platform;

namespace Game.Web.Module.Diamond
{
    public partial class RecordRoomScore : AdminPage
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
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(PersonalRoomScoreInfo.Tablename, 1, 100, SearchItems, Orderby);
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
                    ViewState["SearchItems"] = string.Format("WHERE RoomID={0}", IntParam);
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