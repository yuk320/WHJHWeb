using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Treasure;
using Game.Facade;
using System.Data;
using Game.Kernel;
using Game.Entity.Enum;

namespace Game.Web.Module.AppManager
{
    public partial class SpreadConfigInfo : AdminPage
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
            SpreadConfig config = new SpreadConfig();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                config = FacadeManage.aideTreasureFacade.GetSpreadConfig(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                if(FacadeManage.aideTreasureFacade.SpreadConfigCount() >= 5)
                {
                    ShowError("推广配置最多配置5个");
                    return;
                }
            }

            config.PresentDiamond = CtrlHelper.GetInt(txtDiamond, 0);
            config.PresentPropID = Convert.ToInt32(ddlPropID.SelectedValue);
            config.PresentPropNum = config.PresentPropID > 0 ? CtrlHelper.GetInt(txtPropNum, 0) : 0;
            config.PresentPropName = config.PresentPropID == 0 ? "" : ddlPropID.SelectedItem.Text;
            config.SpreadNum = CtrlHelper.GetInt(txtSpreadNum, 0);
            config.UpdateTime = DateTime.Now;

            int result = IntParam > 0 ? FacadeManage.aideTreasureFacade.UpdateSpreadConfig(config) : FacadeManage.aideTreasureFacade.InsertSpreadConfig(config);
            if(result > 0)
            {
                ShowInfo("操作成功", "SpreadConfigList.aspx", 1200);
            }
            else
            {
                ShowError("操作失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            if(IntParam > 0)
            {
                SpreadConfig config = FacadeManage.aideTreasureFacade.GetSpreadConfig(IntParam);
                if(config != null)
                {
                    CtrlHelper.SetText(txtDiamond, config.PresentDiamond.ToString());
                    CtrlHelper.SetText(txtPropNum, config.PresentPropNum.ToString());
                    CtrlHelper.SetText(txtSpreadNum, config.SpreadNum.ToString());
                    ddlPropID.SelectedValue = config.PresentPropID.ToString();
                }
            }
        }
    }
}