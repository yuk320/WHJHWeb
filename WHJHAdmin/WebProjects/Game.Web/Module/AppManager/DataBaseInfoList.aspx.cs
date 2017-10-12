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
using Game.Entity.Platform;

namespace Game.Web.Module.AppManager
{
    public partial class DataBaseInfoList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataBaseDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            DataBaseDataBind();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            string where = "WHERE 1=1";
            if(!string.IsNullOrEmpty(query))
            {
                where = where + string.Format(" AND DBAddr='{0}'", query);
            }
            ViewState["SearchItems"] = where;
            DataBaseDataBind();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Delete);
            int result = FacadeManage.aidePlatformFacade.DeleteDataBase(StrCIdList);
            if(result > 0)
            {
                ShowInfo("删除成功");
                DataBaseDataBind();
            }
            else
            {
                ShowError("删除失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBaseDataBind()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(DataBaseInfo.Tablename, 
                anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataBase.DataSource = pagerSet.PageSet;
            rptDataBase.DataBind();
            
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
                    ViewState["Orderby"] = "ORDER BY DBInfoID ASC";
                }
                return (string)ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }                     
    }
}
