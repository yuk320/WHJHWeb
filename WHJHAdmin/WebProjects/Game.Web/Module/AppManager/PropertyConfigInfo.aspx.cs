using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using Game.Facade;
using Game.Kernel;
using System.Collections;
using System.Text;
using Game.Entity.Enum;
using Game.Entity.Platform;

namespace Game.Web.Module.AppManager
{
    public partial class PropertyConfigInfo : AdminPage
    {
        #region 窗口事件

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                //BindArea( );
                BindData( );
            }
        }
        protected void btnSave_Click( object sender, EventArgs e )
        {
            //判断权限
            AuthUserOperationPermission(Permission.Edit);
            GameProperty property = new GameProperty
            {
                ID = IntParam,
                Name = CtrlHelper.GetText(txtName),
                // ExchangeGoldRatio = CtrlHelper.GetInt(txtExchangeGoldRaito, 0),
                ExchangeRatio = CtrlHelper.GetInt(txtExchangeDiamondRaito, 0),
                UseResultsGold = CtrlHelper.GetInt(txtUseResultsGold, 0),
                SortID = CtrlHelper.GetInt(txtSortID, 0),
                BuyResultsGold = CtrlHelper.GetInt(txtBuyResultsGold, 0),
                RegulationsInfo = CtrlHelper.GetText(txtRegulationsInfo),
                Nullity = (byte) (ckbNullity.Checked ? 0 : 1),
                Recommend = (byte) (ckbRecommend.Checked ? 1 : 0)
            };

            try
            {
                FacadeManage.aidePlatformFacade.UpdatePropertyInfo(property);
                ShowInfo("更新成功");
            }
            catch
            {
                ShowInfo("更新失败");
            }
        }
        #endregion

        #region 数据加载

        private void BindData( )
        {
            if(IntParam <= 0)
                return;

            GameProperty property = FacadeManage.aidePlatformFacade.GetPropertyInfo(IntParam);
            if(property == null)
                return;

            CtrlHelper.SetText(txtName, property.Name);
            // CtrlHelper.SetText(txtExchangeGoldRaito, property.ExchangeGoldRatio.ToString());
            CtrlHelper.SetText(txtExchangeDiamondRaito, property.ExchangeRatio.ToString());
            CtrlHelper.SetText(txtBuyResultsGold, property.BuyResultsGold.ToString());
            CtrlHelper.SetText(txtUseResultsGold, property.UseResultsGold.ToString());
            CtrlHelper.SetText(txtRegulationsInfo, property.RegulationsInfo);
            CtrlHelper.SetText(txtSortID,property.SortID.ToString());
            ckbNullity.Checked = property.Nullity == 0;
            ckbRecommend.Checked = property.Recommend == 1;

            //禁止用户操作
            NulityInput(property.Kind);
        }
        /// <summary>
        /// 禁止用户操作
        /// </summary>
        /// <param name="Kind"></param>
        public void NulityInput(int Kind)
        {
            switch(Kind)
            {
                case 1:
                    txtUseResultsGold.Enabled = false;
                    txtBuyResultsGold.Enabled = false;
                    break;
                case 2:
                    txtBuyResultsGold.Enabled = false;
                    txtUseResultsGold.Enabled = true;
                    break;
                case 3:
                    txtUseResultsGold.Enabled = false;
                    txtBuyResultsGold.Enabled = false;
                    break;
                case 4:
                    txtUseResultsGold.Enabled = false;
                    txtBuyResultsGold.Enabled = false;
                    break;
                case 5:
                    txtUseResultsGold.Enabled = false;
                    txtBuyResultsGold.Enabled = false;
                    break;
                case 6:
                    txtBuyResultsGold.Enabled = true;
                    txtUseResultsGold.Enabled = false;
                    break;
                case 12:
                    txtBuyResultsGold.Enabled = false;
                    break;
                default:
                    txtUseResultsGold.Enabled = false;
                    txtBuyResultsGold.Enabled = false;
                    break;
            }
        }
        #endregion
    }
}
