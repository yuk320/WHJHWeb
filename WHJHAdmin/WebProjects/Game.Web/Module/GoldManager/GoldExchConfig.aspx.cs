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

namespace Game.Web.Module.GoldManager
{
    public partial class GoldExchConfig : AdminPage
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
            CurrencyExchConfig config = new CurrencyExchConfig();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                config = FacadeManage.aideTreasureFacade.GetCurrencyExch(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
            }

            config.ConfigName = CtrlHelper.GetText(txtProductName);
            config.Diamond = CtrlHelper.GetInt(txtCurrency, 0);
            config.ExchGold = Convert.ToInt64(txtGold.Text);
            config.ImageType = Convert.ToByte(txtImageType.Text);
            config.SortID = CtrlHelper.GetInt(txtSortID, 0);

            //验证是否存在相同钻石配置
            if(IntParam <= 0 && FacadeManage.aideTreasureFacade.IsExistCurrencyExch(config.Diamond))
            {
                ShowError("抱歉，相同额度的钻石已存在");
                return;
            }
            int result = IntParam > 0 ? FacadeManage.aideTreasureFacade.UpdateCurrencyExch(config) : FacadeManage.aideTreasureFacade.InsertCurrencyExch(config);
            if(result > 0)
            {
                ShowInfo("配置信息操作成功", "GoldExchConfigList.aspx", 1200);
            }
            else
            {
                ShowError("配置信息操作失败");
            }

        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            if(IntParam > 0)
            {
                CurrencyExchConfig config = FacadeManage.aideTreasureFacade.GetCurrencyExch(IntParam);
                if(config != null)
                {
                    CtrlHelper.SetText(txtProductName, config.ConfigName);
                    CtrlHelper.SetText(txtCurrency, config.Diamond.ToString());
                    CtrlHelper.SetText(txtGold, config.ExchGold.ToString());
                    CtrlHelper.SetText(txtSortID, config.SortID.ToString());
                    CtrlHelper.SetText(txtImageType,config.ImageType.ToString());
                }
            }
        }
    }
}