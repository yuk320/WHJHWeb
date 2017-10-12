using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Kernel;
using Game.Web.UI;
using Game.Facade;
using Game.Entity.Enum;
using System.Data;
using System.Text;
using Game.Entity.Platform;
using Game.Entity.Treasure;

namespace Game.Web.Module.AppManager
{
    public partial class SpreadConfigList : AdminPage
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
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 配置删除
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Delete);
            if(StrCIdList.IndexOf(',') >= 0)
            {
                ShowError("只能选择单个配置进行删除");
                return;
            }
            int result = FacadeManage.aideTreasureFacade.DeleteSpreadConfig(Convert.ToInt32(StrCIdList));
            if(result > 0)
            {
                ShowInfo("删除成功", "SpreadConfigList.aspx", 1200);
            }
            else
            {
                ShowError("删除失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(SpreadConfig.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, "", " ORDER BY SpreadNum ASC");
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }
    }
}