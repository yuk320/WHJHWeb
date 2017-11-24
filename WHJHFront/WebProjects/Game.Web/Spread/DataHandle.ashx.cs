using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using System.Web;
using System.Web.SessionState;
using Game.Facade.Enum;
using Game.Kernel;

namespace Game.Web.Spread
{
    /// <summary>
    /// 推广中心接口
    /// </summary>
    public class DataHandle : IHttpHandler, IRequiresSessionState
    {
        private readonly int _userId = GameRequest.GetQueryInt("userid", 0);
        private AjaxJsonValid _ajv;

        /// <summary>
        /// 接口主路由
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //允许跨站请求域名
            context.Response.AddHeader("Access-Control-Allow-Origin", AppConfig.MoblieInterfaceDomain);
            context.Response.ContentType = "application/json";

            string action = GameRequest.GetString("action");
            //签名验证 //接口版本号
            _ajv = new AjaxJsonValid();
            _ajv.SetDataItem("apiVersion",20171120);
            if (_userId <= 0)
            {
                _ajv.code = (int) ApiCode.VertyParamErrorCode;
                _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " userid 为空");
                _ajv.SetValidDataValue(false);
                context.Response.Write(_ajv.SerializeToJson());
                return;
            }
            switch (action)
            {
                case "userspreadhome":
                    UserSpreadHome();
                    break;
                case "userspreadreceive":
                    int num = GameRequest.GetInt("num", 0);
                    if (num <= 0)
                    {
                        _ajv.code = (int) ApiCode.VertyParamErrorCode;
                        _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " num（领取数量）");
                        _ajv.SetValidDataValue(false);
                        context.Response.Write(_ajv.SerializeToJson());
                        return;
                    }
                    UserSpreadReveice(num);
                    break;
                default:
                    _ajv.code = (int) ApiCode.VertyParamErrorCode;
                    _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode), " action");
                    _ajv.SetValidDataValue(false);
                    break;
            }

            context.Response.Write(_ajv.SerializeToJson());
        }

        /// <summary>
        /// 玩家推广中心数据
        /// </summary>
        private void UserSpreadHome()
        {
            byte spreadReturnType = 0;
            SystemStatusInfo spreadTypeConfig =
                FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.ConfigInfoKey.SpreadReturnType
                    .ToString());
            if (spreadTypeConfig != null)
            {
                spreadReturnType = Convert.ToByte(spreadTypeConfig.StatusValue);
            }
            DataSet ds = FacadeManage.aideAccountsFacade.GetUserSpreadHomeDataSet(_userId);

            Dictionary<string, object> info = new Dictionary<string, object>
            {
                {"GameID", Convert.ToInt32(ds.Tables[0].Rows[0]["GameID"])},
                {"Lv1Count", Convert.ToInt32(ds.Tables[0].Rows[0]["Lv1Count"])},
                {"Lv2Count", Convert.ToInt32(ds.Tables[0].Rows[0]["Lv2Count"])},
                {"Lv3Count", Convert.ToInt32(ds.Tables[0].Rows[0]["Lv3Count"])},
                {"TotalReturn", Convert.ToInt64(ds.Tables[0].Rows[0]["TotalReturn"])},
                {"TotalReceive", Convert.ToInt64(ds.Tables[0].Rows[0]["TotalReceive"])}
            };
            _ajv.SetDataItem("info", info);
            ArrayList arrayList = new ArrayList();
            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    int userId = Convert.ToInt32(dr["UserID"]);
                    Dictionary<string, object> belowUser = new Dictionary<string, object>()
                    {
                        {"UserID", userId},
                        {"GameID", FacadeManage.aideAccountsFacade.GetGameIDByUserID(userId)},
                        {"NickName", FacadeManage.aideAccountsFacade.GetNickNameByUserID(userId)},
                        {"Level", GetLevelDesc(dr["LevelID"].ToString())}
                    };
                    arrayList.Add(belowUser);
                }
            }
            _ajv.SetDataItem("belowList", arrayList);
            arrayList = new ArrayList();
            if (ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Dictionary<string, object> record = new Dictionary<string, object>()
                    {
                        {
                            "GameID",
                            FacadeManage.aideAccountsFacade.GetGameIDByUserID(Convert.ToInt32(dr["SourceUserID"]))
                        },
                        {"SourceDiamond", Convert.ToInt32(dr["SourceDiamond"])},
                        {"SpreadLevel", GetLevelDesc(dr["SpreadLevel"].ToString())},
                        {"ReturnType", spreadReturnType == 0 ? "金币" : "钻石"},
                        {"ReturnNum", Convert.ToInt32(dr["ReturnNum"])},
                        {"CollectDate", Convert.ToDateTime(dr["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss")},
                    };
                    arrayList.Add(record);
                }
            }
            _ajv.SetDataItem("returnRecord", arrayList);
            arrayList = new ArrayList();
            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    Dictionary<string, object> record = new Dictionary<string, object>()
                    {
                        {"CollectDate", Convert.ToDateTime(dr["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss")},
                        {"ReceiveType", spreadReturnType == 0 ? "金币" : "钻石"},
                        {"ReceiveNum", Convert.ToInt32(dr["ReceiveNum"])},
                        {"ReceiveBefore", Convert.ToInt64(dr["ReceiveBefore"])}
                    };
                    arrayList.Add(record);
                }
            }
            _ajv.SetDataItem("receiveRecord", arrayList);
            _ajv.SetValidDataValue(true);
        }

        /// <summary>
        /// 获取等级描述
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        private static string GetLevelDesc(string levelId)
        {
            switch (levelId)
            {
                case "1":
                    return "一级";
                case "2":
                    return "二级";
                case "3":
                    return "三级";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 玩家领取推广返利
        /// </summary>
        private void UserSpreadReveice(int num)
        {
            Message msg = FacadeManage.aideRecordFacade.UserSpreadReceive(_userId, num);
            if (msg.Success)
            {
                _ajv.msg = "领取成功";
                _ajv.SetValidDataValue(true);
            }
            else
            {
                _ajv.code = msg.MessageID;
                _ajv.msg = msg.Content;
                _ajv.SetValidDataValue(false);
            }
        }

        public bool IsReusable => false;
    }
}
