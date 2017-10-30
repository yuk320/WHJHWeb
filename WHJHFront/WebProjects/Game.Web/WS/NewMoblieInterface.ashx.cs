using Game.Utils;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Web;
using Game.Facade.Enum;
using Game.Facade.DataStruct;
using Game.Entity.NativeWeb;
using Game.Entity.Treasure;
using Game.Kernel;
using Game.Entity.Platform;
using Game.Web.Helper;
using System.Data;

namespace Game.Web.WS
{
    /// <summary>
    /// NewMoblieInterface 的摘要说明
    /// </summary>
    public class NewMoblieInterface : IHttpHandler
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static AjaxJsonValid _ajv;

        private static int _userid;

        /// <summary>
        /// 统一处理入口（主要验证）
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //允许跨站请求域名
            context.Response.AddHeader("Access-Control-Allow-Origin", AppConfig.MoblieInterfaceDomain);
            //接口返回数据格式
            context.Response.ContentType = "application/json";
            //接口请求类型
            string action = GameRequest.GetQueryString("action").ToLower();

            //获取参数
            _userid = GameRequest.GetQueryInt("userid", 0);
            _ajv = new AjaxJsonValid();
#if !DEBUG //DEBUG情况下不验证
            string time = GameRequest.GetQueryString("time");
            string sign = GameRequest.GetQueryString("sign");

            //签名验证
            _ajv =
Fetch.VerifySignData((context.Request.QueryString["userid"] == null ? "" : _userid.ToString()) + AppConfig.MoblieInterfaceKey + time,
                sign);
//            Log4Net.WriteInfoLog("signStr:"+(context.Request.QueryString["userid"] == null ? "" : _userid.ToString()) + AppConfig.MoblieInterfaceKey + time + " sign:"+sign);
            if (_ajv.code == (int)ApiCode.VertySignErrorCode)
            {
                context.Response.Write(_ajv.SerializeToJson());
                return;
            }
#endif
            //参数验证
            if (context.Request.QueryString["userid"] != null && _userid <= 0)
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " userid 错误");
                context.Response.Write(_ajv.SerializeToJson());
                return;
            }
            //获取其他参数
            int configid = GameRequest.GetQueryInt("configid", 0);
            int typeid = GameRequest.GetQueryInt("typeid", 0);

            switch (action)
            {
                //获取手机端登录数据
                case "getmobilelogindata":
                    GetMobileLoginData();
                    break;
                //获取手机端登录后数据
                case "getmobileloginlater":
                    GetMobileLoginLater();
                    break;
                //获取充值产品列表
                case "getpayproduct":
                    //获取参数
                    int typeId = GameRequest.GetQueryInt("typeid", 0);
                    GetPayProduct(typeId);
                    break;
                //领取推广有效好友奖励
                case "receivespreadaward":
                    //参数验证
                    if (configid <= 0)
                    {
                        _ajv.code = (int) ApiCode.VertyParamErrorCode;
                        _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " configid 错误");
                        context.Response.Write(_ajv.SerializeToJson());
                        return;
                    }
                    ReceiveSpreadAward(configid);
                    break;
                //钻石充值下单
                case "createpayorder":
                    //获取参数
                    string paytype = GameRequest.GetQueryString("paytype");
                    string openid = GameRequest.GetQueryString("openid");

                    //参数验证
                    if (configid <= 0 || paytype.Equals(""))
                    {
                        _ajv.code = (int) ApiCode.VertyParamErrorCode;
                        _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), "");
                        context.Response.Write(_ajv.SerializeToJson());
                        return;
                    }
                    context.Response.Write(CreatePayOrder(configid, paytype, openid).SerializeToJson());
                    return;
                //获取排行榜数据
                case "getrankingdata":
                    //参数验证
                    if (typeid <= 0 || typeid > 7)
                    {
                        _ajv.code = (int) ApiCode.VertyParamErrorCode;
                        _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " typeid 错误");
                        context.Response.Write(_ajv.SerializeToJson());
                        return;
                    }

                    GetRankingData(typeid);
                    break;
                //获取财富信息
                case "getuserwealth":
                    GetUserWealth();
                    break;
                //领取排行奖励
                case "receiverankingaward":
                    //获取参数
                    int dateid = GameRequest.GetQueryInt("dateid", 0);

                    //参数验证
                    if (dateid <= 0 || typeid <= 0)
                    {
                        _ajv.code = (int) ApiCode.VertyParamErrorCode;
                        _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), "");
                        context.Response.Write(_ajv.SerializeToJson());
                        return;
                    }
                    ReceiveRankingAward(dateid, typeid);
                    break;
                //获取游戏列表
                case "getgamelist":
                    GetGameList();
                    break;
                //领取注册赠送奖励
                case "receiveregistergrant":
                    ReceiveRegisterGrant();
                    break;
                //金币流水记录
                case "recordtreasuretrade":
                    RecordTreasureTrade();
                    break;
                //钻石流水记录
                case "recorddiamondstrade":
                    RecordDiamondsTrade();
                    break;
                //钻石兑换金币
                case "diamondexchgold":
                    if (configid <= 0 || typeid < 0)
                    {
                        _ajv.code = (int) ApiCode.VertyParamErrorCode;
                        _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                            " configid或typeid 错误");
                        context.Response.Write(_ajv.SerializeToJson());
                        return;
                    }
                    DiamondExchGold(configid, typeid);
                    break;
                default:
                    _ajv.code = (int) ApiCode.VertyParamErrorCode;
                    _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " action 错误");
                    break;
            }

            context.Response.Write(_ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取手机端登录数据
        /// </summary>
        private static void GetMobileLoginData()
        {
            _ajv.data["apiVersion"] = 20171017;

            ConfigInfo webConfig = Fetch.GetWebSiteConfig();
            string imageServerHost = webConfig.Field2;
            //获取登录数据
            DataSet ds = FacadeManage.aideNativeWebFacade.GetMobileLoginInfo();
            //获取系统配置信息
            MobileSystemConfig config = GetMobileSystemConfig(ds.Tables[0]);
            //获取客服界面配置
            MobileCustomerService mcs = DataHelper.ConvertRowToObject<MobileCustomerService>(ds.Tables[1].Rows[0]);
            //获取系统公告配置
            IList<NoticeMobile> noticelist = DataHelper.ConvertDataTableToObjects<NoticeMobile>(ds.Tables[2]);
            //获取手机广告图
            IList<AdsMobile> plate = DataHelper.ConvertDataTableToObjects<AdsMobile>(ds.Tables[3]);
            IList<AdsMobile> alert = DataHelper.ConvertDataTableToObjects<AdsMobile>(ds.Tables[4]);
            foreach (AdsMobile ads in plate)
            {
                ads.ResourceURL = ads.ResourceURL.IndexOf("http://", StringComparison.Ordinal) < 0
                    ? imageServerHost + ads.ResourceURL
                    : ads.ResourceURL;
            }
            foreach (AdsMobile ads in alert)
            {
                ads.ResourceURL = ads.ResourceURL.IndexOf("http://", StringComparison.Ordinal) < 0
                    ? imageServerHost + ads.ResourceURL
                    : ads.ResourceURL;
            }
            //输出数据
            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("systemConfig", config);
            _ajv.AddDataItem("customerService", mcs);
            _ajv.AddDataItem("systemNotice", noticelist);
            _ajv.AddDataItem("adsList", plate);
            _ajv.AddDataItem("adsAlertList", alert);
        }

        /// <summary>
        /// 获取手机端登录后数据
        /// </summary>
        private static void GetMobileLoginLater()
        {
            //获取登录成功后数据
            DataSet ds = FacadeManage.aideAccountsFacade.GetMobileLoginLaterData(_userid);
            //获取推广链接（线上版本请将第三个参数设置成true，内部版本则为false）
            string shareLink = GetSpreadLink(ds.Tables[0], false);
            //获取注册奖励
            DataTable table = ds.Tables[1];
            int registerGrant = (table != null && table.Rows.Count > 0)
                ? Convert.ToInt32(table.Rows[0]["GrantDiamond"])
                : 0;
            //获取推广配置
            IList<SpreadConfigMobile> spreadList =
                DataHelper.ConvertDataTableToObjects<SpreadConfigMobile>(ds.Tables[2]);
            //获取玩家的排行版信息
            IList<RankingRecevieMobile> rankList =
                DataHelper.ConvertDataTableToObjects<RankingRecevieMobile>(ds.Tables[3]);
            //获取有效好友
            table = ds.Tables[4];
            int friendCount = (table != null && table.Rows.Count > 0) ? Convert.ToInt32(table.Rows[0]["Total"]) : 0;

            //输出信息
            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("sharelink", shareLink);
            _ajv.AddDataItem("registergrant", registerGrant);
            _ajv.AddDataItem("friendcount", friendCount);
            _ajv.AddDataItem("spreadlist", spreadList);
            _ajv.AddDataItem("ranklist", rankList);
        }

        /// <summary>
        /// 获取充值产品列表
        /// </summary>
        /// <param name="typeId"></param>
        private static void GetPayProduct(int typeId)
        {
            _ajv.data["apiVersion"] = 20171028;

            //获取充值数据
            DataSet ds = FacadeManage.aideTreasureFacade.GetAppPayConfigList(typeId, _userid);
            //获取首充状态
            DataTable table = ds.Tables[0];
            bool flag = (table != null && table.Rows.Count == 0);
            //获取充值产品列表
            IList<AppPayConfigMoile> list = DataHelper.ConvertDataTableToObjects<AppPayConfigMoile>(ds.Tables[1]);
            //获取兑换产品列表
            IList<CurrencyExchConfig> gold = DataHelper.ConvertDataTableToObjects<CurrencyExchConfig>(ds.Tables[2]);

            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("isFirst", flag);
            _ajv.AddDataItem("list", list);
            _ajv.AddDataItem("goldList", gold);
        }

        /// <summary>
        /// 领取推广有效好友奖励
        /// </summary>
        private static void ReceiveSpreadAward(int configid)
        {
            //领取奖励
            Message msg =
                FacadeManage.aideTreasureFacade.ReceiveSpreadAward(_userid, configid, GameRequest.GetUserIP());
            _ajv.SetValidDataValue(msg.Success);
            _ajv.msg = msg.Content;
        }

        /// <summary>
        /// 钻石充值下单
        /// </summary>
        /// <param name="configid"></param>
        /// <param name="paytype"></param>
        /// <param name="openid"></param>
        /// <returns>AjaxJsonValid</returns>
        private static AjaxJsonValid CreatePayOrder(int configid, string paytype, string openid)
        {
            //接口特殊版本
            _ajv.data["apiVersion"] = 20171013;
            //下单信息
            OnLinePayOrder order = new OnLinePayOrder
            {
                UserID = _userid,
                ConfigID = configid,
                OrderAddress = GameRequest.GetUserIP()
            };
            switch (paytype)
            {
                case "wx":
                    order.ShareID = 101;
                    order.OrderID = Fetch.GetOrderIDByPrefix("WXAPP");
                    break;
                case "zfb":
                    order.ShareID = 201;
                    order.OrderID = Fetch.GetOrderIDByPrefix("ZFBAPP");
                    break;
                case "hwx":
                    order.ShareID = 102;
                    order.OrderID = Fetch.GetOrderIDByPrefix("HWX");
                    break;
                case "lq":
                    order.ShareID = 301;
                    order.OrderID = Fetch.GetOrderIDByPrefix("360LQ");
                    break;
                default:
                    _ajv.code = (int) ApiCode.VertyParamErrorCode;
                    _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " paytype（充值类型） 错误");
                    return _ajv;
            }

            //下单操作
            Message umsg = FacadeManage.aideTreasureFacade.CreatePayOrderInfo(order);
            if (umsg.Success)
            {
                OnLinePayOrder orderReturn = umsg.EntityList[0] as OnLinePayOrder;
                if (paytype == "wx" || paytype == "hwx")
                {
                    _ajv.AddDataItem("PayPackage",
                        GetWxPayPackage(orderReturn, paytype, openid, GameRequest.GetCurrentFullHost()));
                }
                else if (paytype == "lq")
                {
                    string[] temp = openid.Split('|');
                    _ajv.AddDataItem("PayPackage",
                        LQPay.GetPayPackage(orderReturn, temp[0], temp[1], temp[2], GameRequest.GetCurrentFullHost()));
                }
                _ajv.AddDataItem("OrderID", orderReturn?.OrderID ?? "");
            }
            _ajv.SetValidDataValue(umsg.Success);
            _ajv.code = umsg.MessageID;
            _ajv.msg = umsg.Content;
            return _ajv;
        }

        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        /// <param name="typeid"></param>
        private static void GetRankingData(int typeid)
        {
            //获取排行榜数据
            IList<CacheWealthRank> wealthRank = null;
            IList<CacheConsumeRank> consumeRank = null;
            IList<CacheScoreRank> scoreRank = null;
            DataSet ds = FacadeManage.aideNativeWebFacade.GetDayRankingData(typeid);
            switch (typeid)
            {
                case 1:
                    wealthRank = DataHelper.ConvertDataTableToObjects<CacheWealthRank>(ds.Tables[0]);
                    break;
                case 2:
                    consumeRank = DataHelper.ConvertDataTableToObjects<CacheConsumeRank>(ds.Tables[0]);
                    break;
                case 3:
                    wealthRank = DataHelper.ConvertDataTableToObjects<CacheWealthRank>(ds.Tables[0]);
                    consumeRank = DataHelper.ConvertDataTableToObjects<CacheConsumeRank>(ds.Tables[1]);
                    break;
                case 4:
                    scoreRank = DataHelper.ConvertDataTableToObjects<CacheScoreRank>(ds.Tables[0]);
                    break;
                case 5:
                    wealthRank = DataHelper.ConvertDataTableToObjects<CacheWealthRank>(ds.Tables[0]);
                    scoreRank = DataHelper.ConvertDataTableToObjects<CacheScoreRank>(ds.Tables[1]);
                    break;
                case 6:
                    consumeRank = DataHelper.ConvertDataTableToObjects<CacheConsumeRank>(ds.Tables[0]);
                    scoreRank = DataHelper.ConvertDataTableToObjects<CacheScoreRank>(ds.Tables[1]);
                    break;
                case 7:
                    wealthRank = DataHelper.ConvertDataTableToObjects<CacheWealthRank>(ds.Tables[0]);
                    consumeRank = DataHelper.ConvertDataTableToObjects<CacheConsumeRank>(ds.Tables[1]);
                    scoreRank = DataHelper.ConvertDataTableToObjects<CacheScoreRank>(ds.Tables[2]);
                    break;
            }

            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("WealthRank", wealthRank);
            _ajv.AddDataItem("ConsumeRank", consumeRank);
            _ajv.AddDataItem("ScoreRank", scoreRank);
        }

        /// <summary>
        /// 获取财富信息
        /// </summary>
        private static void GetUserWealth()
        {
            //获取财富信息
            long score = 0;
            long insureScore = 0;
            long diamond = 0;
            UserWealth wealth = FacadeManage.aideTreasureFacade.GetUserWealth(_userid);
            if (wealth != null)
            {
                score = wealth.Score;
                insureScore = wealth.InsureScore;
                diamond = wealth.Diamond;
            }

            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("diamond", diamond);
            _ajv.AddDataItem("score", score);
            _ajv.AddDataItem("insure", insureScore);
        }

        /// <summary>
        /// 领取排行榜奖励
        /// </summary>
        /// <param name="dateid"></param>
        /// <param name="typeid"></param>
        private static void ReceiveRankingAward(int dateid, int typeid)
        {
            //领取排行榜奖励
            Message msg =
                FacadeManage.aideNativeWebFacade.ReceiveRankingAward(_userid, dateid, typeid, GameRequest.GetUserIP());
            if (msg.Success)
            {
                _ajv.SetValidDataValue(true);
                UserCurrency currency = msg.EntityList[0] as UserCurrency;
                _ajv.AddDataItem("Diamond", currency?.Diamond ?? 0);
            }
            _ajv.msg = msg.Content;
        }

        /// <summary>
        /// 获取游戏列表
        /// </summary>
        private static void GetGameList()
        {
            //获取游戏列表
            DataSet ds = FacadeManage.aidePlatformFacade.GetMobileGameAndVersion();
            //获取大厅版本
            string version = "";
            string downloadUrl = "";
            string resVersion = "";
            string iosUrl = "";
            DataRow row = (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0] : null;
            if (row != null)
            {
                downloadUrl = row["Field1"].ToString();
                version = row["Field2"].ToString();
                resVersion = row["Field3"].ToString();
                iosUrl = row["Field4"].ToString();
            }
            //获取游戏列表
            IList<MobileKindItem> list = DataHelper.ConvertDataTableToObjects<MobileKindItem>(ds.Tables[1]);

            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("downloadurl", downloadUrl);
            _ajv.AddDataItem("clientversion", version);
            _ajv.AddDataItem("resversion", resVersion);
            _ajv.AddDataItem("ios_url", iosUrl);
            _ajv.AddDataItem("gamelist", list);
        }

        /// <summary>
        /// 领取注册赠送奖励
        /// </summary>
        private static void ReceiveRegisterGrant()
        {
            //领取注册奖励
            Message msg = FacadeManage.aideTreasureFacade.ReceiveRegisterAward(_userid, GameRequest.GetUserIP());
            if (msg.Success)
            {
                _ajv.SetValidDataValue(true);
                UserWealth wealth = msg.EntityList[0] as UserWealth;
                _ajv.AddDataItem("Score", wealth?.Score ?? 0);
                _ajv.AddDataItem("InsureScore", wealth?.InsureScore ?? 0);
                _ajv.AddDataItem("Diamond", wealth?.Diamond ?? 0);
            }
            _ajv.msg = msg.Content;
        }

        /// <summary>
        /// 金币流水记录
        /// </summary>
        private static void RecordTreasureTrade()
        {
            //获取参数
            int pageIndex = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("size", 10);

            //获取流水记录
            IList<TreasureStream> list = new List<TreasureStream>();
            string where = $" WHERE UserID={_userid}";
            PagerSet ps = FacadeManage.aideRecordFacade.GetGoldStreamList(where, pageIndex, pageSize);
            DataTable table = ps.PageSet.Tables[0];
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow item in table.Rows)
                {
                    TreasureStream stream = new TreasureStream
                    {
                        SerialNumber = item["SerialNumber"].ToString(),
                        SerialTime = Convert.ToDateTime(item["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        BeforeGold = Convert.ToInt64(item["CurScore"]) + Convert.ToInt64(item["CurInsureScore"]),
                        ChangeGold = Convert.ToInt32(item["ChangeScore"]),
                        AfterGold = Convert.ToInt64(item["CurScore"]) + Convert.ToInt64(item["CurInsureScore"]) +
                                    //银行存取操作不需要加上变化值
                                    ((RecordTreasureType) item["TypeID"] == RecordTreasureType.存入银行 ||
                                     (RecordTreasureType) item["TypeID"] == RecordTreasureType.银行取出
                                        ? 0
                                        : Convert.ToInt32(item["ChangeScore"])),
                        TypeName = EnumHelper.GetDesc((RecordTreasureType) item["TypeID"])
                    };
                    list.Add(stream);
                }
            }

            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("list", list);
        }

        /// <summary>
        /// 钻石流水记录
        /// </summary>
        private static void RecordDiamondsTrade()
        {
            int pageIndex = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("size", 10);

            //获取流水记录
            IList<DiamondStream> list = new List<DiamondStream>();
            string where = $" WHERE UserID={_userid}";
            PagerSet ps = FacadeManage.aideRecordFacade.GetDiamondStreamList(where, pageIndex, pageSize);
            DataTable table = ps.PageSet.Tables[0];
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow item in table.Rows)
                {
                    DiamondStream stream = new DiamondStream
                    {
                        SerialNumber = item["SerialNumber"].ToString(),
                        SerialTime = Convert.ToDateTime(item["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        BeforeDiamond = Convert.ToInt64(item["CurDiamond"]),
                        ChangeDiamond = Convert.ToInt32(item["ChangeDiamond"]),
                        AfterDiamond = Convert.ToInt64(item["CurDiamond"]) + Convert.ToInt32(item["ChangeDiamond"]),
                        TypeName = EnumHelper.GetDesc((RecordDiamondType) item["TypeID"])
                    };
                    list.Add(stream);
                }
            }

            _ajv.SetValidDataValue(true);
            _ajv.AddDataItem("list", list);
        }

        /// <summary>
        /// 钻石兑换金币
        /// </summary>
        /// <param name="configid"></param>
        /// <param name="typeid"></param>
        private static void DiamondExchGold(int configid, int typeid)
        {
            Message msg =
                FacadeManage.aideTreasureFacade.DiamondExchangeGold(_userid, configid, typeid, GameRequest.GetUserIP());
            if (msg.Success)
            {
                var dataSet = msg.EntityList[0] as DataSet;
                if (dataSet != null)
                {
                    DiamondExchRecord record =
                        DataHelper.ConvertRowToObject<DiamondExchRecord>(dataSet.Tables[0].Rows[0]);
                    if (record == null) return;
                    _ajv.SetValidDataValue(true);
                    _ajv.AddDataItem("AfterDiamond", record.AfterDiamond);
                    _ajv.AddDataItem("AfterInsureScore", record.AfterInsureScore);
                    _ajv.AddDataItem("AfterScore", record.AfterScore);
                    _ajv.AddDataItem("ExchDiamond", record.ExchDiamond);
                    _ajv.AddDataItem("PresentGold", record.PresentGold);
                }
            }
            _ajv.msg = msg.Content;
        }

        #region 辅助方法

        /// <summary>
        /// 获取手机端配置
        /// </summary>
        private static MobileSystemConfig GetMobileSystemConfig(DataTable table)
        {
            MobileSystemConfig config = new MobileSystemConfig();
            if (table == null || table.Rows.Count <= 0) return config;
            foreach (DataRow item in table.Rows)
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (item["StatusName"].ToString())
                {
                    case "JJOpenMobileMall":
                        config.IsOpenMall = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJPayBindSpread":
                        config.IsPayBindSpread = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJBindSpreadPresent":
                        config.BindSpreadPresent = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJRankingListType":
                        config.RankingListType = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJPayChannel":
                        config.PayChannel = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJDiamondBuyProp":
                        config.DiamondBuyPropCount = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJRealNameAuthentPresent":
                        config.RealNameAuthentPresent = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJEffectiveFriendGame":
                        config.EffectiveFriendGame = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "IOSNotStorePaySwitch":
                        config.IOSNotStorePaySwitch = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "JJGoldBuyProp":
                        config.GoldBuyPropCount = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "EnjoinInsure":
                        config.EnjoinInsure = Convert.ToInt32(item["StatusValue"]);
                        break;
                    case "TransferStauts":
                        config.TransferStauts = Convert.ToInt32(item["StatusValue"]);
                        break;
                }
            }
            return config;
        }

        /// <summary>
        /// 获取推广链接
        /// </summary>
        private static string GetSpreadLink(DataTable table, bool flag)
        {
            string shareLink = string.Empty;
            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                string domain = string.IsNullOrEmpty(AppConfig.FrontSiteDomain)
                    ? GameRequest.GetCurrentFullHost()
                    : AppConfig.FrontSiteDomain;
                //线上版本
                if (flag)
                {
                    if (Convert.ToInt32(row["AgentID"]) > 0)
                    {
                        shareLink = "http://" + row["AgentDomain"] + "/Mobile/ShareLink.aspx";
                    }
                    else
                    {
                        string[] domainStr = domain.Split('.');
                        shareLink = domainStr.Length != 3
                            ? ("http://" + domain + "/Mobile/ShareLink.aspx?g=" + row["GameID"])
                            : ("http://" + row["GameID"] + "." + domainStr[1] + "." + domainStr[2] +
                               "/Mobile/ShareLink.aspx");
                    }
                }
                else
                {
                    shareLink = "http://" + domain + "/Mobile/ShareLink.aspx?g=" + row["GameID"];
                }
            }
            return shareLink;
        }

        /// <summary>
        /// 获取微信预支付信息包
        /// </summary>
        /// <returns></returns>
        private static string GetWxPayPackage(OnLinePayOrder orderReturn, string paytype, string openid,
            string authority)
        {
            string domain = string.IsNullOrEmpty(AppConfig.FrontSiteDomain) ? authority : AppConfig.FrontSiteDomain;
            WxPayInfo wx = new WxPayInfo
            {
                AppID = ApplicationSettings.Get(paytype == "wx" ? "WXAPPID" : "WXNATIVEAPPID"),
                AppSecret = ApplicationSettings.Get(paytype == "wx" ? "WXAPPSECRET" : "WXNATIVESECRET"),
                // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                Body = orderReturn.Amount.ToString(),
                Key = ApplicationSettings.Get(paytype == "wx" ? "WXAPPKEY" : "WXNATIVEKEY"),
                Mchid = ApplicationSettings.Get(paytype == "wx" ? "WXAPPMCHID" : "WXNATIVEMCHID"),
                NotifyUrl = "http://" + domain + "/Notify/" + (paytype == "wx" ? "WxApp.aspx" : "WxWeb.aspx"),
                OpenId = openid,
                OutTradeNo = orderReturn.OrderID,
                TotalFee = (orderReturn.Amount * 100).ToString("F0"),
                TradeType = paytype == "wx" ? "APP" : "JSAPI"
            };
            WxPayHelper helper = new WxPayHelper(wx);
            return helper.GetPrepayIDSign();
        }

        #endregion

        public bool IsReusable => false;
    }
}