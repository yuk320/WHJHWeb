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
using Game.Entity.Platform;
using Game.Facade;
using Game.Entity.Enum;

namespace Game.Web.Module.AppManager
{
    public partial class SystemMessageList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SystemMessageDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            SystemMessageDataBind();
        }
        /// <summary>
        /// 批量删除系统消息
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Delete);
            string where = "WHERE ID in (" + StrCIdList + ")";
            int result = FacadeManage.aidePlatformFacade.DeleteSystemMessage(where);
            if(result > 0)
            {
                ShowInfo("删除成功");
                SystemMessageDataBind();
            }
            else
            {
                ShowError("删除失败");
            }
        }
        /// <summary>
        /// 批量冻结系统消息
        /// </summary>
        protected void btnNoEnable_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aidePlatformFacade.NullitySystemMessage(StrCIdList, 1);
            if(result > 0)
            {
                ShowInfo("冻结成功");
                SystemMessageDataBind();
            }
            else
            {
                ShowError("冻结失败");
            }
        }
        /// <summary>
        /// 批量解冻系统消息
        /// </summary>
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aidePlatformFacade.NullitySystemMessage(StrCIdList, 0);
            if(result > 0)
            {
                ShowInfo("解冻成功");
                SystemMessageDataBind();
            }
            else
            {
                ShowError("解冻失败");
            }
        }
        /// <summary>
        ///数据绑定
        /// </summary>
        private void SystemMessageDataBind()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(SystemMessage.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptSystemMessage.DataSource = pagerSet.PageSet;
            rptSystemMessage.DataBind();
        }
        /// <summary>
        /// 获取消息类型名称
        /// </summary>
        /// <param name="messageType"></param>
        /// <returns></returns>
        protected string GetMessageTypeName(int messageType)
        {
            string rValue = "";
            switch(messageType)
            {
                case 1:
                    rValue = "游戏";
                    break;
                case 2:
                    rValue = "房间";
                    break;
                case 3:
                    rValue = "全部";
                    break;
                default:
                    break;
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
                    ViewState["Orderby"] = "ORDER BY ID DESC";
                }
                return (string)ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }                   
    }
}
