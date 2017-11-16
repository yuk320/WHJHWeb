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

namespace Game.Web.Spread
{
    /// <summary>
    /// 获取自定义头像
    /// </summary>
    public class SpreadDataHandle : IHttpHandler, IRequiresSessionState
    {
        private readonly int _userId = GameRequest.GetQueryInt("userid", 0);
        private AjaxJsonValid _ajv;

        public void ProcessRequest(HttpContext context)
        {
            //允许跨站请求域名
            context.Response.ContentType = "application/json";

            string action = GameRequest.GetString("action");
            //签名验证 //接口版本号
            _ajv = new AjaxJsonValid {data = {["apiVersion"] = 20171115}};
            switch (action)
            {
                case "userspreadhome":
                    UserSpreadHome();
                    break;
                default:
                    _ajv.code = (int) ApiCode.VertyParamErrorCode;
                    _ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode)," action");
                    _ajv.SetValidDataValue(false);
                    break;
            }

            context.Response.Write(_ajv.SerializeToJson());
        }

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
            DataSet ds = FacadeManage.aideAccountsFacade.GetUserSpreadHomeDataSet(_userId, spreadReturnType);

            Dictionary<string, object> info = new Dictionary<string, object>
            {
                {"GameID", Convert.ToInt32(ds.Tables[0].Rows[0]["GameID"])},
                {"Lv1Count", Convert.ToInt32(ds.Tables[0].Rows[0]["Lv1Count"])},
                {"Lv2Count", Convert.ToInt32(ds.Tables[0].Rows[0]["Lv2Count"])},
                {"Lv3Count", Convert.ToInt32(ds.Tables[0].Rows[0]["Lv3Count"])},
                {"TotalReturn", Convert.ToInt64(ds.Tables[0].Rows[0]["TotalReturn"])},
                {"TotalReceive", Convert.ToInt64(ds.Tables[0].Rows[0]["TotalReceive"])}
            };
            _ajv.AddDataItem("info",info);
            ArrayList arrayList = new ArrayList();
            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Dictionary<string, object> record = new Dictionary<string, object>()
                    {
                        {"GameID",FacadeManage.aideAccountsFacade.GetGameIDByUserID(Convert.ToInt32(dr["SourceUserID"])) },
                        {"SourceDiamond",Convert.ToInt32(dr["SourceDiamond"]) },
                        {"ReturnType",spreadReturnType==0?"金币":"钻石" },
                        {"ReturnNum",Convert.ToInt32(dr["ReturnNum"]) },
                        {"CollcetDate",Convert.ToDateTime(dr["CollcetDate"]).ToString("yyyy-MM-dd HH:mm:ss") },
                    };
                    arrayList.Add(record);
                }
            }
            _ajv.AddDataItem("record",arrayList);
        }

        public bool IsReusable => false;
    }
}