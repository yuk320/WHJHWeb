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
using Game.Entity.NativeWeb;

namespace Game.Web.Module.AppManager
{
    public partial class RankingConfigInfo : AdminPage
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
            RankingConfig config = new RankingConfig();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                config = FacadeManage.aideNativeWebFacade.GetRankingConfigById(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                if(FacadeManage.aideNativeWebFacade.ExistRankingConfig(config.TypeID, config.RankID))
                {
                    ShowError("相同配置信息已存在");
                    return;
                }
            }

            config.Diamond = CtrlHelper.GetInt(txtDiamond, 0);
            config.RankID = CtrlHelper.GetInt(txtRankID, 0);
            config.ValidityTime = CtrlHelper.GetInt(txtValidityTime, 0);
            config.TypeID = Convert.ToByte(ddlType.SelectedValue);
            config.UpdateTime = DateTime.Now;

            int result = IntParam > 0 ? FacadeManage.aideNativeWebFacade.UpdateRankingConfig(config) : FacadeManage.aideNativeWebFacade.InsertRankingConfig(config);
            if(result > 0)
            {
                ShowInfo("操作成功", "RankingConfigList.aspx", 1200);
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
                RankingConfig config = FacadeManage.aideNativeWebFacade.GetRankingConfigById(IntParam);
                if(config != null)
                {
                    CtrlHelper.SetText(txtDiamond, config.Diamond.ToString());
                    CtrlHelper.SetText(txtRankID, config.RankID.ToString());
                    CtrlHelper.SetText(txtValidityTime, config.ValidityTime.ToString());
                    ddlType.SelectedValue = config.TypeID.ToString();
                }
            }
        }
    }
}