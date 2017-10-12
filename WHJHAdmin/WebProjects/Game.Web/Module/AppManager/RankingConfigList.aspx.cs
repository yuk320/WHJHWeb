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
using Game.Entity.NativeWeb;

namespace Game.Web.Module.AppManager
{
    public partial class RankingConfigList : AdminPage
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
        /// 删除配置
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Delete);
            if(StrCIdList.IndexOf(',') >= 0)
            {
                ShowError("只能选择单个配置进行删除");
                return;
            }
            int result = FacadeManage.aideNativeWebFacade.DeleteRankingConfig(Convert.ToInt32(StrCIdList));
            if(result > 0)
            {
                ShowInfo("删除成功", "RankingConfigList.aspx", 1200);
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
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList(RankingConfig.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, "", " ORDER BY TypeID ASC,RankID ASC");
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }
    }
}