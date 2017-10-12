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

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !Page.IsPostBack )
            {
                BindData( );
                BindDataList( );
            }
        }

        #endregion

        #region 数据加载
        private void BindDataList( )
        {
//            DataTable dt = new DataTable( );
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
//            //游戏损耗
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

        private void BindData( )
        {
            DataSet ds = FacadeManage.aideTreasureFacade.GetStatInfo();
            DataTable dt = ds.Tables[0];
            if ( dt.Rows.Count > 0 )
            {
                //在线统计
                CtrlHelper.SetText( ltOnLineCount ,  dt.Rows[0]["OnLineCount"].ToString());
                CtrlHelper.SetText( ltDisenableCount ,  dt.Rows[0]["DisenableCount"].ToString());
                CtrlHelper.SetText( ltAllCount ,  dt.Rows[0]["AllCount"].ToString());
//                CtrlHelper.SetText(ltMobileRegister, dt.Rows[0]["MobileRegister"].ToString());

                //游戏币统计
                CtrlHelper.SetText( ltScore ,  dt.Rows[0]["Score"].ToString());
                CtrlHelper.SetText( ltInsureScore ,  dt.Rows[0]["InsureScore"].ToString());
                CtrlHelper.SetText( ltAllScore ,  dt.Rows[0]["AllScore"].ToString());

                //房卡统计
                CtrlHelper.SetText(fkAdminPresent, dt.Rows[0]["FKAdminPresent"].ToString());
                CtrlHelper.SetText(fkCreateRoom, dt.Rows[0]["FKCreateRoom"].ToString());
                CtrlHelper.SetText(fkCreateAARoom, dt.Rows[0]["FKAACreateRoom"].ToString());
                CtrlHelper.SetText(fkExchScore, dt.Rows[0]["FKExchScore"].ToString());
                CtrlHelper.SetText(fkRMBPay, dt.Rows[0]["FKRMBPay"].ToString());
                CtrlHelper.SetText(fkTotal, dt.Rows[0]["FKTotal"].ToString());

                //赠送统计
                CtrlHelper.SetText(ltRegPresent, dt.Rows[0]["RegPresent"].ToString());
                CtrlHelper.SetText(ltAgentRegPresent, dt.Rows[0]["AgentRegPresent"].ToString());
                CtrlHelper.SetText(ltDBPresent, dt.Rows[0]["DBPresent"].ToString());
                CtrlHelper.SetText(ltQDPresent, dt.Rows[0]["QDPresent"].ToString());

                //CtrlHelper.SetText(ltOnlinePresent, dt.Rows[0]["OnlinePresent"].ToString());
                CtrlHelper.SetText(ltRWPresent, dt.Rows[0]["RWPresent"].ToString());
                CtrlHelper.SetText(ltSMPresent, dt.Rows[0]["SMPresent"].ToString());
                CtrlHelper.SetText(ltDayPresent, dt.Rows[0]["DayPresent"].ToString());
                CtrlHelper.SetText(ltMatchPresent, dt.Rows[0]["MatchPresent"].ToString());
                CtrlHelper.SetText(ltDJPresent, dt.Rows[0]["DJPresent"].ToString());
                CtrlHelper.SetText(ltSharePresent, dt.Rows[0]["SharePresent"].ToString());
                CtrlHelper.SetText(ltLotteryPresent, dt.Rows[0]["LotteryPresent"].ToString());
                CtrlHelper.SetText(ltWebPresent, dt.Rows[0]["WebPresent"].ToString());

                //税收统计
                CtrlHelper.SetText( ltRevenue ,  dt.Rows[0]["Revenue"].ToString());
                CtrlHelper.SetText( ltTransferRevenue ,  dt.Rows[0]["TransferRevenue"].ToString());
                //损耗统计
                CtrlHelper.SetText( ltWaste ,  dt.Rows[0]["Waste"].ToString());
            }
           
        }
        #endregion
    }
}
