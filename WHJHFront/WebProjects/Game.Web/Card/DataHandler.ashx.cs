using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Game.Entity.NativeWeb;
using Game.Facade.DataStruct;
using Game.Facade.Enum;
using Game.Web.Card.DataStruct;
using Game.Web.Helper;

// ReSharper disable InconsistentNaming

namespace Game.Web.Card
{
    /// <summary>
    /// DataHandler 的摘要说明
    /// </summary>
    public class DataHandler : IHttpHandler, IRequiresSessionState
    {
        #region Fields

        /// <summary>
        /// 实例是否可重复使用
        /// </summary>
        public bool IsReusable => true;

        /// <summary>
        /// 响应实体
        /// </summary>
        private static AjaxJsonValid _ajv;

        /// <summary>
        /// 通用用户标识
        /// </summary>
        private static int UserId { get; set; }

        #endregion

        #region Router

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetString("action").ToLower();
            int version = GameRequest.GetInt("version", 1);

            #region Version 1.0 Router

            if (version == 1)
            {
                switch (action)
                {
                    case "getnicknamebygameid":
                        GetNickNameByGameID(context);
                        break;
                    case "getpaydiamondlist":
                        GetPayDiamondList(context);
                        break;
                    case "getpresentdiamondlist":
                        GetPresentDiamondList(context);
                        break;
                    case "getcostdiamondlist":
                        GetCostDiamondList(context);
                        break;
                    case "getspreadregisterlist":
                        GetSpreadRegisterList(context);
                        break;
                    case "getexchangediamondlist":
                        GetExchangeDiamondList(context);
                        break;
                    case "getunderlist":
                        GetUnderList(context);
                        break;
                    case "getunderdetail":
                        GetUnderDetail(context);
                        break;
                    default:
                        break;
                }
            }

            #endregion

            #region Version 2.0 Router

            else if (version == 2)
            {
                try
                {
                    //不需要认证的action
                    string[] unNeedAuthActions = {"agentauth"};
                    string token = GameRequest.GetString("token");
                    string authheader = context.Request.Headers["Authorization"];
                    _ajv = new AjaxJsonValid();
                    _ajv.SetDataItem("apiVersion", 20180316);

                    //排除不需要认证后判断认证是否正确
                    if (!unNeedAuthActions.Contains(action))
                    {
                        if (string.IsNullOrEmpty(token) &&
                            (string.IsNullOrEmpty(authheader) || !authheader.Contains("Bearer")))
                        {
                            _ajv.code = (int) ApiCode.VertyParamErrorCode;
                            _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " token 缺失");
                            context.Response.Write(_ajv.SerializeToJson());
                            return;
                        }
                        string authToken = !string.IsNullOrEmpty(token) ? token : authheader.Replace("Bearer ", "");
                        AgentTokenInfo authInfo = FacadeManage.aideNativeWebFacade.VerifyAgentToken(authToken);
                        if (authInfo == null)
                        {
                            _ajv.code = (int) ApiCode.Unauthorized;
                            _ajv.msg = EnumHelper.GetDesc(ApiCode.Unauthorized);
                            context.Response.Write(_ajv.SerializeToJson());
                            return;
                        }
                        //认证完成后 设全局属性UserID
                        UserId = authInfo.UserID;
                    }

                    switch (action)
                    {
                        case "agentauth":
                            string mobile = GameRequest.GetString("mobile");
                            string pass = GameRequest.GetString("pass"); //MD5
                            if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(pass))
                            {
                                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                                    " mobile、pass 缺失");
                                context.Response.Write(_ajv.SerializeToJson());
                                return;
                            }
                            AgentAuth(mobile, pass);
                            break;
                        case "getinfo":
                            GetAgentInfo();
                            break;
                        case "getnicknamebygameid":
                            int gameId = GameRequest.GetInt("gameid", 0);
                            if (gameId == 0)
                            {
                                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " gameid 缺失");
                                context.Response.Write(_ajv.SerializeToJson());
                                return;
                            }
                            GetNickNameByGameIDV2(gameId);
                            break;
                        case "getrecord":
                            string type = GameRequest.GetString("record");
                            GetRecord(type);
                            break;
                        default:
                            _ajv.code = (int) ApiCode.VertyParamErrorCode;
                            _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),
                                " action 对应接口不存在");
                            context.Response.Write(_ajv.SerializeToJson());
                            return;
                    }

                    context.Response.Write(_ajv.SerializeToJson());
                }
                catch (Exception ex)
                {
                    Log4Net.WriteInfoLog("下面一条为接口故障信息", "Agent_DataHandler");
                    Log4Net.WriteErrorLog(ex, "Agent_DataHandler");
                    _ajv = new AjaxJsonValid
                    {
                        code = (int) ApiCode.LogicErrorCode,
                        msg = EnumHelper.GetDesc(ApiCode.LogicErrorCode)
                    };
                    context.Response.Write(_ajv.SerializeToJson());
                }
            }

            #endregion
        }

        #endregion

        #region Version 1.0 Logic

        /// <summary>
        /// 获取用户昵称
        /// </summary>
        /// <param name="context"></param>
        protected void GetNickNameByGameID(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            int gameid = GameRequest.GetFormInt("gameid", 0);

            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(gameid);
            ajv.SetDataItem("nickname", info != null ? info.NickName : "");
            ajv.SetDataItem("compellation", info != null ? info.Compellation : "");
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取充值钻石记录
        /// </summary>
        /// <param name="context"></param>
        protected void GetPayDiamondList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            //判断登录
            UserTicketInfo uti = Fetch.GetUserCookie();
            if (uti == null || uti.UserID <= 0)
            {
                ajv.code = 0;
                ajv.msg = "登录已失效，请重新打开页面";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            StringBuilder sb = new StringBuilder();
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            string where = string.Format(" WHERE UserID = {0} AND OrderStatus = 1 ", uti.UserID);
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetPayDiamondRecord(where, page, number);
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in pagerSet.PageSet.Tables[0].Rows)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", Fetch.FormatTimeWrap(Convert.ToDateTime(item["PayDate"])));
                    sb.AppendFormat("<td>{0}</td>", item["BeforeDiamond"]);
                    sb.AppendFormat("<td>{0}</td>",
                        Convert.ToInt32(item["Diamond"]) + Convert.ToInt32(item["OtherPresent"]));
                    sb.AppendFormat("<td>{0}</td>", item["Amount"]);
                    sb.Append("</tr>");
                }
                html = sb.ToString();
            }
            else
            {
                html = "<tr><td colspan=\"4\">暂无记录！</td></tr>";
            }
            ajv.SetDataItem("html", html);
            ajv.SetDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取代理赠送钻石记录
        /// </summary>
        /// <param name="context"></param>
        protected void GetPresentDiamondList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            //判断登录
            UserTicketInfo uti = Fetch.GetUserCookie();
            if (uti == null || uti.UserID <= 0)
            {
                ajv.code = 0;
                ajv.msg = "登录已失效，请重新打开页面";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            StringBuilder sb = new StringBuilder();
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            string where = string.Format("WHERE SourceUserID = {0}", uti.UserID);
            PagerSet pagerSet = FacadeManage.aideRecordFacade.GetAgentPresentDiamondRecord(where, page, number);
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in pagerSet.PageSet.Tables[0].Rows)
                {
                    AccountsInfo info =
                        FacadeManage.aideAccountsFacade.GetAccountsInfoByUserID(Convert.ToInt32(item["TargetUserID"]));

                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", Fetch.FormatTimeWrap(Convert.ToDateTime(item["CollectDate"])));
                    sb.AppendFormat("<td>{0}</td>", info != null ? info.GameID.ToString() : "");
                    sb.AppendFormat("<td>({0}){1}</td>", item["SourceDiamond"], item["PresentDiamond"]);
                    sb.AppendFormat("<td>{0}</td>", item["CollectNote"]);
                    sb.Append("</tr>");
                }
                html = sb.ToString();
            }
            else
            {
                html = "<tr><td colspan=\"4\">暂无记录！</td></tr>";
            }
            ajv.SetDataItem("html", html);
            ajv.SetDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取钻石创建房间记录
        /// </summary>
        /// <param name="context"></param>
        protected void GetCostDiamondList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            //判断登录
            UserTicketInfo uti = Fetch.GetUserCookie();
            if (uti == null || uti.UserID <= 0)
            {
                ajv.code = 0;
                ajv.msg = "登录已失效，请重新打开页面";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            StringBuilder sb = new StringBuilder();
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            string where = $"WHERE UserID = {uti.UserID}";
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetCreateRoomCost(where, page, number);
            string html;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in pagerSet.PageSet.Tables[0].Rows)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", Fetch.FormatTimeWrap(Convert.ToDateTime(item["CreateDate"])));
                    sb.AppendFormat("<td>{0}</td>", item["RoomID"]);
                    sb.AppendFormat("<td>{0}</td>", item["CreateTableFee"]);
                    sb.AppendFormat("<td>{0}</td>",
                        !item["DissumeDate"].ToString().Equals("")
                            ? Fetch.FormatTimeWrap(Convert.ToDateTime(item["DissumeDate"]))
                            : "");
                    sb.Append("</tr>");
                }
                html = sb.ToString();
            }
            else
            {
                html = "<tr><td colspan=\"4\">暂无记录！</td></tr>";
            }
            ajv.SetDataItem("html", html);
            ajv.SetDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取代理推广人
        /// </summary>
        /// <param name="context"></param>
        protected void GetSpreadRegisterList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            //判断登录
            UserTicketInfo uti = Fetch.GetUserCookie();
            if (uti == null || uti.UserID <= 0)
            {
                ajv.code = 0;
                ajv.msg = "登录已失效，请重新打开页面";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            StringBuilder sb = new StringBuilder();
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            DataSet ds = FacadeManage.aideAccountsFacade.GetAgentSpreadList(uti.UserID, page, number);
            string html = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", Fetch.FormatTimeWrap(Convert.ToDateTime(item["RegisterDate"])));
                    sb.AppendFormat("<td>{0}</td>", item["GameID"]);
                    sb.AppendFormat("<td>{0}</td>", Fetch.RegisterOrigin(Convert.ToInt32(item["RegisterOrigin"])));
                    sb.AppendFormat("<td>{0}</td>", Convert.ToInt32(item["AgentID"]) > 0 ? "代理商" : "非代理商");
                    sb.Append("</tr>");
                }
                html = sb.ToString();
            }
            else
            {
                html = "<tr><td colspan=\"4\">暂无记录！</td></tr>";
            }
            ajv.SetDataItem("html", html);
            ajv.SetDataItem("total", FacadeManage.aideAccountsFacade.GetAgentSpreadCount(uti.UserID));
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 代理兑换游戏币记录
        /// </summary>
        /// <param name="context"></param>
        protected void GetExchangeDiamondList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            //判断登录
            UserTicketInfo uti = Fetch.GetUserCookie();
            if (uti == null || uti.UserID <= 0)
            {
                ajv.code = 0;
                ajv.msg = "登录已失效，请重新打开页面";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            StringBuilder sb = new StringBuilder();
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);

            PagerSet pagerSet =
                FacadeManage.aideRecordFacade.GetAgentExchangeDiamondRecord($"WHERE UserID = {uti.UserID} ", page,
                    number);
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in pagerSet.PageSet.Tables[0].Rows)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", Fetch.FormatTimeWrap(Convert.ToDateTime(item["CollectDate"])));
                    sb.AppendFormat("<td>{0}</td>", item["PresentGold"]);
                    sb.AppendFormat("<td>{0}</td>", item["ExchDiamond"]);
                    sb.AppendFormat("<td>{0}</td>",
                        Convert.ToInt64(item["CurDiamond"]) - Convert.ToInt64(item["ExchDiamond"]));
                    sb.Append("</tr>");
                }
                html = sb.ToString();
            }
            else
            {
                html = "<tr><td colspan=\"4\">暂无记录！</td></tr>";
            }
            ajv.SetDataItem("html", html);
            ajv.SetDataItem("total", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        public void GetUnderList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            UserTicketInfo uti = Fetch.GetUserCookie();
            if (uti == null || uti.UserID <= 0)
            {
                ajv.code = 0;
                ajv.msg = "登录已失效，请重新打开页面";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            string type = GameRequest.GetQueryString("type");
            string range = GameRequest.GetQueryString("range");
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);
            string sqlMonth = " AND CollectDate >= '" +
                              new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd HH:mm:ss") +
                              "'";
            string sqlRange = range == "month"
                ? sqlMonth
                : "";
            number = range == "all" ? 10 : 50;
            string sqlWhere;
            long pCount = 0;
            UnderList list = new UnderList();
            PagerSet ps;
            switch (type)
            {
                case "user":
                    sqlWhere =
                        $" WHERE SourceUserID = {uti.UserID} AND TargetUserID NOT IN (SELECT UserID FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsAgentInfo) {sqlRange} GROUP BY TargetUserID,SourceUserID ";
                    ps = FacadeManage.aideRecordFacade.GetAgentBelowUserPresentDiamondRecord(sqlWhere, page, number);
                    list.PageCount = ps.PageCount;
                    list.RecordCount = ps.RecordCount;
                    list.PageIndex = ps.PageIndex;
                    list.PageSize = ps.PageSize;
                    if (ps.RecordCount > 0)
                    {
                        foreach (DataRow row in ps.PageSet.Tables[0].Rows)
                        {
                            UnderData data = new UnderData()
                            {
                                UserID = Convert.ToInt32(row["UserID"]),
                                RankID = Convert.ToInt32(row["PageView_RowNo"])
                            };
                            data.GameID = FacadeManage.aideAccountsFacade.GetGameIDByUserID(data.UserID);
                            data.NickName = FacadeManage.aideAccountsFacade.GetNickNameByUserID(data.UserID);
                            data.Diamond = FacadeManage.aideTreasureFacade.GetUserCurrency(data.UserID)?.Diamond ?? 0;
                            if (type == "month")
                            {
                                data.MonthDiamond = Convert.ToInt64(row["SumDiamond"]);
                                data.TotalDiamond =
                                    FacadeManage.aideRecordFacade.GetTotalPresentCount(uti.UserID, data.UserID);
                            }
                            else
                            {
                                data.TotalDiamond = Convert.ToInt64(row["SumDiamond"]);
                                data.MonthDiamond =
                                    FacadeManage.aideRecordFacade.GetTotalPresentCount(uti.UserID, data.UserID,
                                        sqlMonth);
                            }

                            list.dataList.Add(data);
                        }
                    }
                    pCount = FacadeManage.aideRecordFacade.GetAgentBelowAccountsCount(uti.UserID);
                    break;
                case "agent":
                    list.Link = true;
                    if (range == "all")
                    {
                        IList<AccountsAgentInfo> belowList =
                            FacadeManage.aideAccountsFacade.GetAgentBelowAgentList(uti.UserID);
                        list.PageCount = 1;
                        list.RecordCount = belowList?.Count ?? 0;
                        list.PageIndex = 1;
                        list.PageSize = belowList?.Count ?? 0;
                        var iCount = 0;
                        if (belowList != null)
                            foreach (AccountsAgentInfo agentInfo in belowList)
                            {
                                iCount++;
                                UnderData data = new UnderData()
                                {
                                    UserID = agentInfo.UserID,
                                    RankID = iCount
                                };
                                data.GameID = FacadeManage.aideAccountsFacade.GetGameIDByUserID(data.UserID);
                                data.NickName = FacadeManage.aideAccountsFacade.GetNickNameByUserID(data.UserID);
                                data.Diamond = FacadeManage.aideTreasureFacade.GetUserCurrency(data.UserID)?.Diamond ??
                                               0;
                                data.TotalDiamond = FacadeManage.aideRecordFacade.GetTotalPresentCount(uti.UserID,
                                    data.UserID,
                                    sqlMonth);
                                data.MonthDiamond =
                                    FacadeManage.aideRecordFacade.GetTotalPresentCount(uti.UserID, data.UserID,
                                        sqlMonth);

                                list.dataList.Add(data);
                            }
                    }
                    else
                    {
                        sqlWhere =
                            $" WHERE SourceUserID IN ( SELECT UserID FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsAgentInfo WHERE ParentAgent = {uti.AgentID} )  {sqlRange} GROUP BY SourceUserID ";
                        ps =
                            FacadeManage.aideRecordFacade
                                .GetAgentBelowAgentPresentDiamondRecord(sqlWhere, page, number);
                        list.PageCount = ps.PageCount;
                        list.RecordCount = ps.RecordCount;
                        list.PageIndex = ps.PageIndex;
                        list.PageSize = ps.PageSize;
                        if (ps.RecordCount > 0)
                        {
                            foreach (DataRow row in ps.PageSet.Tables[0].Rows)
                            {
                                UnderData data = new UnderData()
                                {
                                    UserID = Convert.ToInt32(row["UserID"]),
                                    RankID = Convert.ToInt32(row["PageView_RowNo"])
                                };
                                data.GameID = FacadeManage.aideAccountsFacade.GetGameIDByUserID(data.UserID);
                                data.NickName = FacadeManage.aideAccountsFacade.GetNickNameByUserID(data.UserID);
                                data.Diamond = FacadeManage.aideTreasureFacade.GetUserCurrency(data.UserID)?.Diamond ??
                                               0;
                                if (type == "month")
                                {
                                    data.MonthDiamond = Convert.ToInt64(row["SumDiamond"]);
                                    data.TotalDiamond =
                                        FacadeManage.aideRecordFacade.GetTotalPresentCount(uti.UserID, data.UserID);
                                }
                                else
                                {
                                    data.TotalDiamond = Convert.ToInt64(row["SumDiamond"]);
                                    data.MonthDiamond =
                                        FacadeManage.aideRecordFacade.GetTotalPresentCount(uti.UserID, data.UserID,
                                            sqlMonth);
                                }

                                list.dataList.Add(data);
                            }
                        }
                    }
                    pCount = FacadeManage.aideAccountsFacade.GetAgentBelowAgentCount(uti.UserID);
                    break;
                default:
                    ajv.msg = "类型参数丢失！";
                    context.Response.Write(ajv.SerializeToJson());
                    return;
            }

            ajv.SetDataItem("list", list.dataList);
            ajv.SetDataItem("total", list.RecordCount);
            if (list.Link) ajv.SetDataItem("link", true);
            ajv.SetDataItem("count", pCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        private static void GetUnderDetail(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            //判断登录
            UserTicketInfo uti = Fetch.GetUserCookie();
            if (uti == null || uti.UserID <= 0)
            {
                ajv.code = 0;
                ajv.msg = "登录已失效，请重新打开页面";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            int userid = GameRequest.GetQueryInt("userid", 0);
            AccountsInfo ai = FacadeManage.aideAccountsFacade.GetAccountsInfoByUserID(userid);
            if (ai.AgentID > 0)
            {
                AccountsAgentInfo aai = FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(ai.AgentID);
                UnderDetail underDetail = new UnderDetail()
                {
                    UserID = ai.UserID,
                    GameID = ai.GameID,
                    NickName = ai.NickName,
                    Compellation = aai.Compellation,
                    QQAccount = aai.QQAccount,
                    ContactAddress = aai.ContactAddress,
                    ContactPhone = aai.ContactPhone,
                    AgentID = ai.AgentID,
                    Diamond = FacadeManage.aideTreasureFacade.GetUserCurrency(ai.UserID)?.Diamond ?? 0
                };
                ajv.SetDataItem("info", underDetail.ToString());
                ajv.SetValidDataValue(true);
            }

            context.Response.Write(ajv.SerializeToJson());
        }

        #endregion

        #region Version 2.0 Logic

        /// <summary>
        /// 代理手机号+安全密码认证 换取 Token
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pass"></param>
        private static void AgentAuth(string mobile, string pass)
        {
            Message msg = FacadeManage.aideAccountsFacade.AgentMobileLogin(mobile, pass, GameRequest.GetUserIP());
            if (msg.Success)
            {
                UserInfo info = msg.EntityList[0] as UserInfo;
                if (info != null)
                {
                    string token =
                        Fetch.SHA256Encrypt(
                            $"<{info.UserID}>,<{info.AgentID}>,<{info.GameID}>,<{Fetch.ConvertDateTimeToUnix(DateTime.Now)}>");

                    FacadeManage.aideNativeWebFacade.SaveAgentToken(info, token);
                    _ajv.SetValidDataValue(true);
                    _ajv.SetDataItem("token", token);
                    _ajv.SetDataItem("expirtAt", DateTime.Now.AddDays(1));
                    return;
                }
            }

            _ajv.code = (int) ApiCode.Unauthorized;
            _ajv.msg = EnumHelper.GetDesc(ApiCode.Unauthorized);
        }

        /// <summary>
        /// 获取代理信息汇总
        /// </summary>
        /// <param name="userId">用户标识</param>
        private static void GetAgentInfo()
        {
            DateTime monthFirst = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            AccountsInfo userInfo = FacadeManage.aideAccountsFacade.GetAccountsInfoByUserID(UserId);
            AccountsAgentInfo agentInfo =
                FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(userInfo.AgentID);
            DataStruct.AgentInfo info = new DataStruct.AgentInfo
            {
                //来源用户表
                UserID = userInfo.UserID,
                GameID = userInfo.GameID,
                AgentID = userInfo.AgentID,
                NickName = userInfo.NickName,
                //来源代理表
                AgentLevel = agentInfo.AgentLevel == 1 ? "一级代理" : (agentInfo.AgentLevel == 2 ? "二级代理" : "三级代理"),
                AgentDomain = agentInfo.AgentDomain,
                Compellation = agentInfo.Compellation,
                ContactAddress = agentInfo.ContactAddress,
                ContactPhone = agentInfo.ContactPhone,
                WCNickName = agentInfo.WCNickName,
                QQAccount = agentInfo.QQAccount,
                //来源各种统计
                MyAgent = FacadeManage.aideAccountsFacade.GetAgentBelowAgentCount(UserId),
                MyPlayer = FacadeManage.aideRecordFacade.GetAgentBelowAccountsCount(UserId),
                CurDiamond = FacadeManage.aideTreasureFacade.GetUserWealth(UserId)?.Diamond ?? 0L,
                PresentToday = FacadeManage.aideRecordFacade.GetAgentPresentOutCount(UserId,
                    $" AND CollectDate>= '{today}'"),
                PresentMonth =
                    FacadeManage.aideRecordFacade.GetAgentPresentOutCount(UserId,
                        $" AND CollectDate>= '{monthFirst}'"),
                PresentTotal =
                    FacadeManage.aideRecordFacade.GetAgentPresentOutCount(UserId),
                IsHasPassword = !agentInfo.Password.Equals("")
            };
            _ajv.SetValidDataValue(true);
            _ajv.SetDataItem("info", info);
        }

        /// <summary>
        /// 根据GameID查询用户昵称（检查对象存在用）
        /// </summary>
        /// <param name="gameId"></param>
        private static void GetNickNameByGameIDV2(int gameId)
        {
            AccountsInfo userInfo = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(gameId);
            if (userInfo?.UserID > 0)
            {
                _ajv.SetDataItem("NickName", userInfo.NickName);
            }
            else
            {
                _ajv.msg = "所查询的GameID不存在";
            }
            _ajv.SetValidDataValue(true);
        }

        private static void GetRecord(string type)
        {
            int number = GameRequest.GetQueryInt("pageSize", 10);
            int page = GameRequest.GetQueryInt("page", 1);
            PagerSet ps;
            string where;
            switch (type)
            {
                case "cost": //创建约战房消耗记录
                    where = $"WHERE UserID = {UserId}";
                    ps = FacadeManage.aidePlatformFacade.GetCreateRoomCost(where, page, number);
                    IList<CostDiamondRecord> costList = new List<CostDiamondRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            costList.Add(new CostDiamondRecord
                            {
                                CreateDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                RoomID = Convert.ToInt32(dr["RoomID"]),
                                CreateTableFee = Convert.ToInt64(dr["CreateTableFee"]),
                                DissumeDate = Convert.ToDateTime(dr["DissumeDate"]).ToString("yyyy-MM-dd HH:mm:ss")
                            });
                        }
                    }
                    _ajv.SetDataItem("record", costList);
                    break;
                case "exchange": //钻石兑换金币记录
                    where = $" WHERE UserID = {UserId}";
                    ps = FacadeManage.aideRecordFacade.GetAgentExchangeDiamondRecord(where, page, number);
                    IList<ExchGoldRecord> exchList = new List<ExchGoldRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            exchList.Add(new ExchGoldRecord
                            {
                                CollectDate = Convert.ToDateTime(dr["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                ExchDiamond = Convert.ToInt64(dr["ExchDiamond"]),
                                PresentGold = Convert.ToInt64(dr["PresentGold"]),
                                CurDiamond = Convert.ToInt64(dr["CurDiamond"]) - Convert.ToInt64(dr["ExchDiamond"])
                            });
                        }
                    }
                    _ajv.SetDataItem("record", exchList);
                    break;
                case "pay": //获取充值记录
                    where = $" WHERE UserID = {UserId} AND OrderStatus = 1 ";
                    ps = FacadeManage.aideTreasureFacade.GetPayDiamondRecord(where, page, number);
                    IList<PayDiamondRecord> payList = new List<PayDiamondRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            payList.Add(new PayDiamondRecord
                            {
                                PayDate = Convert.ToDateTime(dr["CreateDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                PayDiamond = Convert.ToInt64(dr["Diamond"]) + Convert.ToInt64(dr["OtherPresent"]),
                                BeforeDiamond = Convert.ToInt64(dr["BeforeDiamond"]),
                                Amount = Convert.ToDecimal(dr["Amount"])
                            });
                        }
                    }
                    _ajv.SetDataItem("record", payList);
                    break;
                case "present": //钻石赠送记录
                    where = $"WHERE SourceUserID = {UserId}";
                    ps = FacadeManage.aideRecordFacade.GetAgentPresentDiamondRecord(where, page, number);
                    IList<PresentDiamondRecord> presentList = new List<PresentDiamondRecord>();
                    if (ps?.PageCount > 0)
                    {
                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            presentList.Add(new PresentDiamondRecord
                            {
                                CollectDate = Convert.ToDateTime(dr["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                GameID = Convert.ToInt32(dr["GameID"]),
                                SourceDiamond = Convert.ToInt64(dr["SourceDiamond"]),
                                PresentDiamond = Convert.ToInt64(dr["PresentDiamond"]),
                                CollectNote = dr["DissumeDate"].ToString()
                            });
                        }
                    }
                    _ajv.SetDataItem("record", presentList);
                    break;
                case "register": //代理商推广记录
                    ps = new PagerSet();
                    DataSet ds = FacadeManage.aideAccountsFacade.GetAgentSpreadList(UserId, page, number);
                    IList<SpreadRegsiterRecord> regsiterList = new List<SpreadRegsiterRecord>();
                    if (ds?.Tables.Count > 0)
                    {
                        ps.PageSet = ds;
                        ps.RecordCount = Convert.ToInt32(FacadeManage.aideAccountsFacade.GetAgentSpreadCount(UserId));
                        ps.PageCount = ps.RecordCount / number;

                        foreach (DataRow dr in ps.PageSet.Tables[0].Rows)
                        {
                            regsiterList.Add(new SpreadRegsiterRecord
                            {
                                RegisterDate = Convert.ToDateTime(dr["RegisterDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                GameID = Convert.ToInt32(dr["GameID"]),
                                RegisterOrigin = Fetch.RegisterOrigin(Convert.ToInt32(dr["RegisterOrigin"])),
                                AgentState = Convert.ToInt32(dr["AgentID"]) > 0 ? "代理商" : "非代理商"
                            });
                        }
                    }
                    _ajv.SetDataItem("record", regsiterList);
                    break;
                default:
                    _ajv.code = (int) ApiCode.VertyParamErrorCode;
                    _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " type 无对应记录");
                    return;
            }

            _ajv.SetDataItem("pageCount", ps?.PageCount);
            _ajv.SetDataItem("recordCount", ps?.RecordCount);
            _ajv.SetValidDataValue(true);
        }

        #endregion
    }
}
