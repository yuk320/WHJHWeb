using Game.Entity.Enum;
using Game.Entity.Platform;
using Game.Facade;
using Game.Kernel;
using Game.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Module.AppManager
{
    public partial class MobileKindList : AdminPage
    {
        #region 页面事件

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameKindItemDataBind();
            }
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            GameKindItemDataBind();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Delete);
            int result = FacadeManage.aidePlatformFacade.DeleteMobileKindItem(StrCIdList);
            if (result > 0)
            {
                ShowInfo("删除成功");
                GameKindItemDataBind();
            }
            else
            {
                ShowError("删除失败");
            }
        }

        /// <summary>
        /// 批量启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnable_OnClick(object sender, EventArgs e)
        {
            if (StrCIdList.Equals("")) { ShowError("请选择需要操作的游戏"); return; }
            int result = FacadeManage.aidePlatformFacade.ChangeMobileKindNullity(StrCIdList, 0);
            if (result > 0)
            {
                ShowInfo("设置成功");
                GameKindItemDataBind();
            }
            else
            {
                ShowError("设置失败");
            }
        }

        /// <summary>
        /// 批量禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisable_OnClick(object sender, EventArgs e)
        {
            if (StrCIdList.Equals("")) {ShowError("请选择需要操作的游戏"); return;}
            int result = FacadeManage.aidePlatformFacade.ChangeMobileKindNullity(StrCIdList, 1);
            if (result > 0)
            {
                ShowInfo("设置成功");
                GameKindItemDataBind();
            }
            else
            {
                ShowError("设置失败");
            }
        }

        #endregion

        #region 数据绑定

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void GameKindItemDataBind()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(MobileKindItem.Tablename,
                anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptMobileKindItem.DataSource = pagerSet.PageSet;
            rptMobileKindItem.DataBind();
        }

        #endregion

        #region 方法属性

        /// <summary>
        /// 获取游戏属性
        /// </summary>
        /// <param name="joinID"></param>
        /// <returns></returns>
        protected string GetMarkName(int gameFlag)
        {
            string rValue = "";
            if ((gameFlag & 1) > 0)
            {
                rValue += "ios,";
            }
            if ((gameFlag & 2) > 0)
            {
                rValue += "android,";
            }

            if (rValue != "")
            {
                rValue = rValue.Substring(0, rValue.Length - 1);
            }
            return rValue;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if (ViewState["SearchItems"] == null)
                {
                    ViewState["SearchItems"] = "WHERE 1=1";
                }
                return (string) ViewState["SearchItems"];
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
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY KindID ASC";
                }
                return (string) ViewState["Orderby"];
            }

            set { ViewState["Orderby"] = value; }
        }

        #endregion
    }
}
