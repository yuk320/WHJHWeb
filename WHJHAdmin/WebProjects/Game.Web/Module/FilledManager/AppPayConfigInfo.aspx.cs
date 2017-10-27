using System;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Treasure;
using Game.Facade;
using System.Globalization;
using Game.Entity.Enum;

namespace Game.Web.Module.FilledManager
{
    public partial class AppPayConfigInfo : AdminPage
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
            AppPayConfig config = new AppPayConfig();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                config = FacadeManage.aideTreasureFacade.GetAppPayConfig(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
            }

            config.AppleID = CtrlHelper.GetText(txtAppleID);
            config.Diamond = CtrlHelper.GetInt(txtCurrency, 0);
            config.PayIdentity = Convert.ToByte(rbIdentity.SelectedValue);
            config.PayName = CtrlHelper.GetText(txtProductName);
            config.PayPrice = Convert.ToDecimal(txtPrice.Text);
            config.PayType = Convert.ToByte(ddlProductType.SelectedValue);
            config.PresentDiamond = CtrlHelper.GetInt(txtOtherPresent,0);
            config.SortID = CtrlHelper.GetInt(txtSortID, 0);
            config.ImageType = Convert.ToByte(rbImage.SelectedValue);

            string where;
            //验证苹果产品标识
            if(config.PayType == 1)
            {
                if(string.IsNullOrEmpty(config.AppleID))
                {
                    ShowError("操作失败，苹果产品标识不能为空");
                    return;
                }
                where = IntParam > 0 ? $"WHERE ConfigID!={IntParam} AND PayType = 1 AND AppleID = '{config.AppleID}'" : $"WHERE PayType = 1 AND AppleID = '{config.AppleID}'";
                if(FacadeManage.aideTreasureFacade.IsExistAppPayConfig(where))
                {
                    ShowError("操作失败，苹果产品标识已存在");
                    return;
                }
            }
            //验证首充是否重复
            if(config.PayIdentity == 2 && IntParam == 0)
            {
                where = $"WHERE PayType = {config.PayType} AND PayIdentity = 2";
                if(FacadeManage.aideTreasureFacade.IsExistAppPayConfig(where))
                {
                    ShowError("操作失败，首充产品仅限配置一个");
                    return;
                }
            }
            int result = IntParam > 0 ? FacadeManage.aideTreasureFacade.UpdateAppPayConfig(config) : FacadeManage.aideTreasureFacade.InsertAppPayConfig(config);
            if(result > 0)
            {
                ShowInfo("配置信息操作成功", "AppPayConfigList.aspx", 1200);
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
                ddlProductType.Enabled = false;
                AppPayConfig config = FacadeManage.aideTreasureFacade.GetAppPayConfig(IntParam);
                if(config != null)
                {
                    CtrlHelper.SetText(txtAppleID, config.AppleID);
                    CtrlHelper.SetText(txtCurrency, config.Diamond.ToString());
                    CtrlHelper.SetText(txtPrice, config.PayPrice.ToString(CultureInfo.InvariantCulture));
                    CtrlHelper.SetText(txtProductName, config.PayName);
                    CtrlHelper.SetText(txtOtherPresent, config.PresentDiamond.ToString());
                    CtrlHelper.SetText(txtSortID, config.SortID.ToString());
                    ddlProductType.SelectedValue = config.PayType.ToString();
                    rbIdentity.SelectedValue = config.PayIdentity.ToString();
                    rbImage.SelectedValue = config.ImageType.ToString();
                    apple.Visible = config.PayType > 0;
                    scale.Visible = config.PayIdentity == 2;
                }
            }
            else
            {
                scale.Visible = false;
                apple.Visible = false;
            }
        }
        /// <summary>
        /// 选择配置模式
        /// </summary>
        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeid = Convert.ToInt32(ddlProductType.SelectedValue);
            apple.Visible = typeid > 0;
        }

        protected void rbIdentity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int payIdentity = Convert.ToInt32(rbIdentity.SelectedValue);
            scale.Visible = payIdentity == 2;
        }
    }
}