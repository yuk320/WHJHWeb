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
using Game.Entity.PlatformManager;

namespace Game.Web.Module.BackManager
{
    public partial class BaseUserList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                UserDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            UserDataBind();
        }
        /// <summary>
        /// 批量删除用户
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Delete);
            int result = FacadeManage.aidePlatformManagerFacade.DeleteUser(StrCIdList);
            if(result > 0)
            {
                ShowInfo("删除成功");
                UserDataBind();
            }
            else
            {
                ShowError("删除失败");
            }
        }
        /// <summary>
        /// 批量冻结用户
        /// </summary>
        protected void btnDongjie_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aidePlatformManagerFacade.NullityUser(StrCIdList, 1);
            if(result > 0)
            {
                ShowInfo("冻结成功");
                UserDataBind();
            }
            else
            {
                ShowError("冻结失败");
            }
        }
        /// <summary>
        /// 批量解冻玩家
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnJiedong_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aidePlatformManagerFacade.NullityUser(StrCIdList, 0);
            if(result > 0)
            {
                ShowInfo("解冻成功");
                UserDataBind();
            }
            else
            {
                ShowError("解冻失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void UserDataBind()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformManagerFacade.GetList(Base_Users.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptUser.DataSource = pagerSet.PageSet;
            rptUser.DataBind();
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
                    ViewState["Orderby"] = "ORDER BY UserID ASC";
                }
                return (string)ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }
    }
}
