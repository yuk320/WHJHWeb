using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Entity.Platform;
using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AppManager
{
    public partial class SystemSet : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindData();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Edit);

            SystemStatusInfo config = new SystemStatusInfo();
            config.StatusName = CtrlHelper.GetText(txtStatusName);
            config.StatusValue = CtrlHelper.GetInt(txtStatusValue, 0);
            config.StatusString = CtrlHelper.GetText(txtStatusString);
            config.StatusTip = CtrlHelper.GetText(txtStatusTip);
            config.StatusDescription = CtrlHelper.GetText(txtStatusDescription);

            int result = FacadeManage.aideAccountsFacade.UpdateSystemStatusInfo(config);
            if(result > 0)
            {
                ShowInfo("修改成功");
            }
            else
            {
                ShowError("修改失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(SystemStatusInfo.Tablename ,1, 100, SearchItems, Orderby);
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();

            SystemStatusInfo config = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(string.IsNullOrEmpty(StrParam) ? "EnjoinRegister" : StrParam);
            if(config != null)
            {
                CtrlHelper.SetText(txtStatusName, config.StatusName);
                CtrlHelper.SetText(txtStatusValue, config.StatusValue.ToString());
                CtrlHelper.SetText(txtStatusTip, config.StatusTip);
                CtrlHelper.SetText(txtStatusString, config.StatusString);
                CtrlHelper.SetText(txtStatusDescription, config.StatusDescription);
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
                    ViewState["Orderby"] = "ORDER BY SortID ASC";
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
