using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Web.UI;
using Game.Kernel;
using System.Data;
using Game.Facade;

namespace Game.Web.Module.DataStatistics
{
    public partial class SystemStat : AdminPage
    {
        #region 窗口事件

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
                BindDataList();
            }
        }

        #endregion

        #region 数据加载

        private void BindDataList()
        {
            DataTable dt = new DataTable();
//            //游戏税收
//            dt = FacadeManage.aideRecordFacade.GetGameRevenue();
//            if ( dt.Rows.Count > 0 )
//            {
//                rptGameTax.DataSource = dt;
//                rptGameTax.DataBind( );
//            }
//            dt.Clear( );
//            //房间税收
//            dt = FacadeManage.aideRecordFacade.GetRoomRevenue();
//            if ( dt.Rows.Count > 0 )
//            {
//                rptRoomTax.DataSource = dt;
//                rptRoomTax.DataBind( );
//            }
//            dt.Clear( );
            //游戏损耗
//            dt = FacadeManage.aideRecordFacade.GetGameWaste();
//            if ( dt.Rows.Count > 0 )
//            {
//                rptGameWast.DataSource = dt;
//                rptGameWast.DataBind( );
//            }
//            dt.Clear( );
//            //房间损耗
//            dt = FacadeManage.aideRecordFacade.GetRoomWaste();
//            if ( dt.Rows.Count > 0 )
//            {
//                rptRoomWast.DataSource = dt;
//                rptRoomWast.DataBind( );
//            }
        }

        private void BindData()
        {
            DataSet ds = FacadeManage.aideTreasureFacade.GetStatInfo();
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //在线统计
                CtrlHelper.SetText(ltOnLineCount, dt.Rows[0]["OnLineCount"].ToString());
                CtrlHelper.SetText(ltDisenableCount, dt.Rows[0]["DisenableCount"].ToString());
                CtrlHelper.SetText(ltAllCount, dt.Rows[0]["AllCount"].ToString());
                CtrlHelper.SetText(ltMobileRegister, dt.Rows[0]["MobileRegister"].ToString());
                CtrlHelper.SetText(ltWebShareRegister, dt.Rows[0]["WebRegister"].ToString());
                CtrlHelper.SetText(ltH5Register, dt.Rows[0]["H5Register"].ToString());

                //游戏币统计
                CtrlHelper.SetText(ltScore, dt.Rows[0]["Score"].ToString());
                CtrlHelper.SetText(ltInsureScore, dt.Rows[0]["InsureScore"].ToString());
                CtrlHelper.SetText(ltAllScore, dt.Rows[0]["AllScore"].ToString());

                //房卡统计
                CtrlHelper.SetText(fkAdminPresent, dt.Rows[0]["FKAdminPresent"].ToString());
                CtrlHelper.SetText(fkCreateRoom, dt.Rows[0]["FKCreateRoom"].ToString());
                CtrlHelper.SetText(fkCreateAARoom, dt.Rows[0]["FKAACreateRoom"].ToString());
                CtrlHelper.SetText(fkExchScore, dt.Rows[0]["FKExchScore"].ToString());
                CtrlHelper.SetText(fkRMBPay, dt.Rows[0]["FKRMBPay"].ToString());
                CtrlHelper.SetText(fkTotal, dt.Rows[0]["FKTotal"].ToString());
                CtrlHelper.SetText(ltTotalDiamondUp, dt.Rows[0]["TotalDiamondUp"].ToString());
                CtrlHelper.SetText(ltTotalDiamondDown, dt.Rows[0]["TotalDiamondDown"].ToString());

                //赠送统计
                CtrlHelper.SetText(ltRegPresent, dt.Rows[0]["RegPresent"].ToString());
                CtrlHelper.SetText(ltWebPresent, dt.Rows[0]["WebPresent"].ToString());
                CtrlHelper.SetText(ltExchGold, dt.Rows[0]["ExchGold"].ToString());
                CtrlHelper.SetText(ltTotalGoldUp, dt.Rows[0]["TotalGoldUp"].ToString());

                //CtrlHelper.SetText(ltOnlinePresent, dt.Rows[0]["OnlinePresent"].ToString());
                //CtrlHelper.SetText(ltSMPresent, dt.Rows[0]["SMPresent"].ToString());

                //税收统计
                CtrlHelper.SetText(ltRevenue, dt.Rows[0]["Revenue"].ToString());
                CtrlHelper.SetText(ltTransferRevenue, dt.Rows[0]["TransferRevenue"].ToString());
                CtrlHelper.SetText(ltGameRevenue, dt.Rows[0]["GameRevenue"].ToString());
                //损耗统计
                CtrlHelper.SetText(ltWaste, dt.Rows[0]["Waste"].ToString());
            }
        }

        #endregion
    }
}