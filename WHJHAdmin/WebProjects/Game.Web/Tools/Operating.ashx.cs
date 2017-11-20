using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Game.Utils;
using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Entity.Platform;
using Game.Kernel;
using System.Data;
using Game.Entity.Enum;
using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Entity.Record;
using System.Web.Script.Serialization;

namespace Game.Web.Tools
{
    /// <summary>
    /// 后台涉及的一些异步操作
    /// </summary>
    public class Operating : IHttpHandler,IRequiresSessionState
    {
        public AjaxJsonValid ajv = new AjaxJsonValid();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            //执行操作
            string action = GameRequest.GetQueryString("action").ToLower();
            switch(action)
            {
                case "getuserinfo":
                    GetUserInfo(context);
                    break;
                case "getplatformdiamond":
                    GetPlatformDiamond(context);
                    break;
                case "getplatformagentdiamond":
                    GetPlatformAgentDiamond(context);
                    break;
                case "getplatformcostdiamond":
                    GetPlatformCostDiamond(context);
                    break;
                case "getplatformproductdiamond":
                    GetPlatformProductDiamond(context);
                    break;
                case "getregisterstatictics":
                    GetRegisterStatictics(context);
                    break;
                case "getrevenuestatictics":
                    GetRevenueStatictics(context);
                    break;
                case "getwastestatictics":
                    GetWasteStatictics(context);
                    break;
                case "getuseronlinestatictics":
                    GetUserOnlineStatictics(context);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 通过游戏ID获取游戏信息
        /// </summary>
        /// <param name="context"></param>
        private void GetUserInfo(HttpContext context)
        {
            int gameID = GameRequest.GetFormInt("GameID", 0);
            if(gameID == 0)
            {
                ajv.msg = "游戏ID输入非法";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByGameId(gameID);
            if( info?.UserID > 0)
            {
                ajv.SetValidDataValue(true);
                ajv.AddDataItem("UserID", info.UserID);
                ajv.AddDataItem("Accounts", info.Accounts);
                ajv.AddDataItem("NickName", info.NickName);
                ajv.AddDataItem("Compellation", info.Compellation);
            }
            else
            {
                ajv.msg = "未找到该用户信息";
            }
            context.Response.Write(ajv.SerializeToJson());
        }
        /// <summary>
        /// 获取平台钻石信息
        /// </summary>
        /// <param name="context"></param>
        private void GetPlatformDiamond(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            DateTime sDate = Convert.ToDateTime(stime);
            DateTime eDate = Convert.ToDateTime(etime);
            if(sDate >= eDate)
            {
                return;
            }
            IList<RecordEveryDayCurrency> list = FacadeManage.aideRecordFacade.GetRecordEveryDayCurrency(Fetch.GetDateID(sDate), Fetch.GetDateID(eDate));
            List<DiamondChart> data = new List<DiamondChart>();
            if(list != null && list.Count > 0)
            {
                DiamondChart chart = null;
                StringBuilder sb = new StringBuilder();
                foreach(RecordEveryDayCurrency item in list)
                {
                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.TotalDiamond;
                    chart.type = "平台玩家总钻石数";
                    data.Add(chart);

                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = (item.PayDiamond + item.AdminPresentDiamond + item.SysPresentDiamond);
                    chart.type = "平台产生总钻石数";
                    data.Add(chart);
                }
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }
        /// <summary>
        /// 获取平台代理钻石
        /// </summary>
        /// <param name="context"></param>
        private void GetPlatformAgentDiamond(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            DateTime sDate = Convert.ToDateTime(stime);
            DateTime eDate = Convert.ToDateTime(etime);
            if(sDate >= eDate)
            {
                return;
            }
            IList<RecordEveryDayCurrency> list = FacadeManage.aideRecordFacade.GetRecordEveryDayCurrency(Fetch.GetDateID(sDate), Fetch.GetDateID(eDate));
            List<DiamondChart> data = new List<DiamondChart>();
            if(list != null && list.Count > 0)
            {
                DiamondChart chart = null;
                StringBuilder sb = new StringBuilder();
                foreach(RecordEveryDayCurrency item in list)
                {
                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.FirstDiamond;
                    chart.type = "一级代理总钻石数";
                    data.Add(chart);

                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.SecondDiamond;
                    chart.type = "二级代理总钻石数";
                    data.Add(chart);

                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.ThirdDiamond;
                    chart.type = "三级代理总钻石数";
                    data.Add(chart);
                }
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }
        /// <summary>
        /// 获取平台消耗钻石
        /// </summary>
        /// <param name="context"></param>
        private void GetPlatformCostDiamond(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            DateTime sDate = Convert.ToDateTime(stime);
            DateTime eDate = Convert.ToDateTime(etime);
            if(sDate >= eDate)
            {
                return;
            }
            IList<RecordEveryDayCurrency> list = FacadeManage.aideRecordFacade.GetRecordEveryDayCurrency(Fetch.GetDateID(sDate), Fetch.GetDateID(eDate));
            List<DiamondChart> data = new List<DiamondChart>();
            if(list != null && list.Count > 0)
            {
                DiamondChart chart = null;
                StringBuilder sb = new StringBuilder();
                foreach(RecordEveryDayCurrency item in list)
                {
                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.RoomCostDiamond;
                    chart.type = "创建房间消耗总钻石数";
                    data.Add(chart);

                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.PropCostDiamond;
                    chart.type = "喇叭购买消耗总钻石数";
                    data.Add(chart);

                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.AAGCostDiamond;
                    chart.type = "AA制游戏消耗总钻石数";
                    data.Add(chart);
                }
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }
        /// <summary>
        /// 获取平台产生钻石
        /// </summary>
        /// <param name="context"></param>
        private void GetPlatformProductDiamond(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            DateTime sDate = Convert.ToDateTime(stime);
            DateTime eDate = Convert.ToDateTime(etime);
            if(sDate >= eDate)
            {
                return;
            }
            IList<RecordEveryDayCurrency> list = FacadeManage.aideRecordFacade.GetRecordEveryDayCurrency(Fetch.GetDateID(sDate), Fetch.GetDateID(eDate));
            List<DiamondChart> data = new List<DiamondChart>();
            if(list != null && list.Count > 0)
            {
                DiamondChart chart = null;
                StringBuilder sb = new StringBuilder();
                foreach(RecordEveryDayCurrency item in list)
                {
                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.AdminPresentDiamond;
                    chart.type = "后台赠送钻石数";
                    data.Add(chart);

                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.PayDiamond;
                    chart.type = "充值赠送钻石数";
                    data.Add(chart);

                    chart = new DiamondChart();
                    chart.time = item.CollectDate.ToString("yyyy-MM-dd");
                    chart.diamond = item.SysPresentDiamond;
                    chart.type = "系统奖励钻石数";
                    data.Add(chart);
                }
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }
        /// <summary>
        /// 获取用户注册统计
        /// </summary>
        /// <param name="context"></param>
        private void GetRegisterStatictics(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            DateTime sDate = Convert.ToDateTime(stime);
            DateTime eDate = Convert.ToDateTime(etime);
            if(sDate >= eDate)
            {
                return;
            }
            IList<SystemStreamInfo> list = FacadeManage.aideAccountsFacade.GetRecordEveryDayRegister(Fetch.GetDateID(sDate), Fetch.GetDateID(eDate));
            List<StatisticsChart> data = new List<StatisticsChart>();
            if(list != null && list.Count > 0)
            {
                StatisticsChart sc = null;
                foreach(SystemStreamInfo item in list)
                {
                    sc = new StatisticsChart();
                    sc.time = item.CollectDate.ToString("yyyy-MM-dd");
                    sc.count = item.WebRegisterSuccess;
                    sc.type = "网站注册人数";
                    data.Add(sc);

                    sc = new StatisticsChart();
                    sc.time = item.CollectDate.ToString("yyyy-MM-dd");
                    sc.count = item.GameRegisterSuccess;
                    sc.type = "APP注册人数";
                    data.Add(sc);

                    sc = new StatisticsChart();
                    sc.time = item.CollectDate.ToString("yyyy-MM-dd");
                    sc.count = (item.WebRegisterSuccess + item.GameRegisterSuccess);
                    sc.type = "总注册人数";
                    data.Add(sc);
                }
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }

        private void GetRevenueStatictics(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if (string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            stime = stime + " 00:00:00";
            etime = etime + " 23:59:59";
            IList<StatisticsRevenue> insureList = FacadeManage.aideTreasureFacade.GetDayInsureRevenue(stime, etime);
            IList<StatisticsRevenue> gameList = FacadeManage.aideTreasureFacade.GetDayGameRevenue(stime, etime);
            IList<StatisticsChart> data = new List<StatisticsChart>();
            SortedDictionary<string,long> totalList = new SortedDictionary<string, long>();
            if (insureList != null && insureList.Count > 0)
            {
                foreach (StatisticsRevenue item in insureList)
                {
                    var sc = new StatisticsChart
                    {
                        time = item.TimeDate,
                        count = item.Revenue,
                        type = "银行税收（包含存、取、转）"
                    };
                    if (totalList.ContainsKey(item.TimeDate))
                    {
                        totalList[item.TimeDate] += item.Revenue;
                    }
                    else
                    {
                        totalList.Add(item.TimeDate,item.Revenue);
                    }
                    data.Add(sc);
                }
            }
            if (gameList != null && gameList.Count > 0)
            {
                foreach (StatisticsRevenue item in gameList)
                {
                    var sc = new StatisticsChart
                    {
                        time = item.TimeDate,
                        count = item.Revenue,
                        type = "游戏税收"
                    };
                    if (totalList.ContainsKey(item.TimeDate))
                    {
                        totalList[item.TimeDate] += item.Revenue;
                    }
                    else
                    {
                        totalList.Add(item.TimeDate, item.Revenue);
                    }
                    data.Add(sc);
                }
            }
            foreach (var total in totalList)
            {
                data.Add(new StatisticsChart()
                {
                    time = total.Key,
                    type = "总税收",
                    count = total.Value
                });
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取损耗统计
        /// </summary>
        /// <param name="context"></param>
        private void GetWasteStatictics(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");
            int kindid = GameRequest.GetQueryInt("kindid", 0);

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            stime = stime + " 00:00:00";
            etime = etime + " 23:59:59";
            IList<StatisticsWaste> list = FacadeManage.aideTreasureFacade.GetDayWaste(stime, etime, kindid>0?$" AND KindID = {kindid} | , KindID, ServerID ":" | ,KindID ");
            List<StatisticsChart> data = new List<StatisticsChart>();
            if(list != null && list.Count > 0)
            {
                data.AddRange(list.Select(item => new StatisticsChart
                {
                    time = item.TimeDate,
                    count = item.Waste,
                    type = "游戏损耗" + (item.KindId > 0
                               ? " - " + FacadeManage.aidePlatformFacade.GetMobileKindItemInfo(item.KindId)
                                     ?.KindName ?? ""
                               : "") + (item.ServerId > 0
                               ? " - " + FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(item.ServerId)
                                     ?.ServerName ?? ""
                               : "")
                }));
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }
        /// <summary>
        /// 获取游戏税收统计
        /// </summary>
        /// <param name="context"></param>
        private void GetGameRevenueStatictics(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            stime = stime + " 00:00:00";
            etime = etime + " 23:59:59";
            IList<StatisticsRevenue> list = FacadeManage.aideTreasureFacade.GetDayGameRevenue(stime, etime);
            List<StatisticsChart> data = new List<StatisticsChart>();
            if(list != null && list.Count > 0)
            {
                StatisticsChart sc = null;
                foreach(StatisticsRevenue item in list)
                {
                    sc = new StatisticsChart();
                    sc.time = item.TimeDate;
                    sc.count = item.Revenue;
                    sc.type = "游戏税收";
                    data.Add(sc);
                }
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }
        /// <summary>
        /// 获取在线人数统计
        /// </summary>
        /// <param name="context"></param>
        private void GetUserOnlineStatictics(HttpContext context)
        {
            string stime = GameRequest.GetQueryString("stime");
            string etime = GameRequest.GetQueryString("etime");

            if(string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                return;
            }
            stime = stime + " 00:00:00";
            etime = etime + " 23:59:59";
            IList<StatisticsOnline> list = FacadeManage.aidePlatformFacade.GetUserOnlineStatistics(stime, etime);
            List<StatisticsChart> data = new List<StatisticsChart>();
            if(list != null && list.Count > 0)
            {
                StatisticsChart sc = null;
                foreach(StatisticsOnline item in list)
                {
                    sc = new StatisticsChart();
                    sc.time = item.DTime.ToString("yyyy-MM-dd HH:mm:ss");
                    sc.count = item.RUser;
                    sc.type = "在线人数";
                    data.Add(sc);
                }
            }
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("data", data);
            context.Response.Write(ajv.SerializeToJson());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
