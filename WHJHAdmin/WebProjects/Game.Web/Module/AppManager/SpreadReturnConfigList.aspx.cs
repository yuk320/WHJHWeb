using System;
using Game.Kernel;
using Game.Web.UI;
using Game.Facade;
using Game.Entity.Enum;
using Game.Entity.Accounts;
using Game.Entity.Treasure;

namespace Game.Web.Module.AppManager
{
    public partial class SpreadReturnConfigList : AdminPage
    {
        public byte SpreadReturnType;

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SystemStatusInfo spreadReturnCfg =
                    FacadeManage.aideAccountsFacade.GetSystemStatusInfo("SpreadReturnType");
                if (spreadReturnCfg != null) SpreadReturnType = Convert.ToByte(spreadReturnCfg.StatusValue);
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
            int result = FacadeManage.aideTreasureFacade.DeleteSpreadReturnConfig(Convert.ToInt32(StrCIdList));
            if(result > 0)
            {
                ShowInfo("删除成功", "SpreadReturnConfigList.aspx", 1200);
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
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(SpreadReturnConfig.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, "", " ORDER BY SpreadLevel ASC");
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }
    }
}
