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
using Game.Entity.NativeWeb;

namespace Game.Web.Module.WebManager
{
    public partial class KindRuleList : AdminPage
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
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            RPDataBind();
        }
        /// <summary>
        /// 批量删除规则
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Delete);
            int result = FacadeManage.aideNativeWebFacade.DeleteGameRule(StrCIdList);
            if(result > 0)
            {
                ShowInfo("删除成功");
                RPDataBind();
            }
            else
            {
                ShowError("删除失败");
            }
        }
        /// <summary>
        /// 批量禁用规则
        /// </summary>
        protected void btnDongjie_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aideNativeWebFacade.NullityGameRule(StrCIdList, 1);
            if(result > 0)
            {
                ShowInfo("禁用成功");
                RPDataBind();
            }
            else
            {
                ShowError("禁用失败");
            }
        }
        /// <summary>
        /// 批量启用规则
        /// </summary>
        protected void btnJieDong_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aideNativeWebFacade.NullityGameRule(StrCIdList, 0);
            if(result > 0)
            {
                ShowInfo("启用成功");
                RPDataBind();
            }
            else
            {
                ShowError("启用失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void RPDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList(GameRule.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
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
                    ViewState["Orderby"] = "ORDER BY SortID ASC,KindID DESC";
                }
                return (string)ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }
    }
}