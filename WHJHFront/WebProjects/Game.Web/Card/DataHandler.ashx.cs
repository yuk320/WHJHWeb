using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Game.Facade.DataStruct;

namespace Game.Web.Card
{
    /// <summary>
    /// DataHandler 的摘要说明
    /// </summary>
    public class DataHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetQueryString("action").ToLower();
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
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取用户昵称
        /// </summary>
        /// <param name="context"></param>
        protected void GetNickNameByGameID(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();

            int gameid = GameRequest.GetFormInt("gameid", 0);

            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountsInfoByGameID(gameid);
            ajv.AddDataItem("nickname", info != null ? info.NickName : "");
            ajv.AddDataItem("compellation", info != null ? info.Compellation : "");
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
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
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
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
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

            string where = string.Format("WHERE UserID = {0}", uti.UserID);
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetCreateRoomCost(where, page, number);
            string html = string.Empty;
            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in pagerSet.PageSet.Tables[0].Rows)
                {
                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", Fetch.FormatTimeWrap(Convert.ToDateTime(item["CreateDate"])));
                    sb.AppendFormat("<td>{0}</td>", item["RoomID"]);
                    sb.AppendFormat("<td>{0}</td>", item["CreateTableFee"]);
                    sb.AppendFormat("<td>{0}</td>",
                        item["DissumeDate"] != null
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
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
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
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", FacadeManage.aideAccountsFacade.GetAgentSpreadCount(uti.UserID));
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
            ajv.AddDataItem("html", html);
            ajv.AddDataItem("total", pagerSet.RecordCount);
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
                    sqlWhere = $" WHERE SourceUserID = {uti.UserID} AND TargetUserID NOT IN (SELECT UserID FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsAgentInfo) {sqlRange} GROUP BY TargetUserID,SourceUserID ";
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
                    pCount = FacadeManage.aideAccountsFacade.GetAgentBelowAgentCount(uti.UserID);
                    break;
                case "agent":
                    sqlWhere =
                        $" WHERE SourceUserID IN ( SELECT UserID FROM WHJHAccountsDBLink.WHJHAccountsDB.dbo.AccountsAgentInfo WHERE ParentAgent = {uti.AgentID} )  {sqlRange} GROUP BY SourceUserID ";
                    ps =
                        FacadeManage.aideRecordFacade.GetAgentBelowAgentPresentDiamondRecord(sqlWhere, page, number);
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
                default:
                    ajv.msg = "类型参数丢失！";
                    context.Response.Write(ajv.SerializeToJson());
                    return;
            }

            ajv.AddDataItem("list", list.dataList);
            ajv.AddDataItem("total", list.RecordCount);
            ajv.AddDataItem("count", pCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}