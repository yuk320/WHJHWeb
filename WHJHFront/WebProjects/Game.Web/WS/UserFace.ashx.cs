using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using System.Web;
using System.Web.SessionState;
using Game.Facade.Enum;

namespace Game.Web.WS
{
    /// <summary>
    /// 获取自定义头像
    /// </summary>
    public class UserFace : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            //允许跨站请求域名
            context.Response.AddHeader("Access-Control-Allow-Origin", AppConfig.MoblieInterfaceDomain);
            context.Response.ContentType = "application/json";

            int userid = GameRequest.GetQueryInt("userid", 0);
            int customId = GameRequest.GetInt("customid", 0);
            string time = GameRequest.GetQueryString("time");
            string sign = GameRequest.GetQueryString("sign");

            //签名验证
            AjaxJsonValid ajv = Fetch.VerifySignData(userid + AppConfig.MoblieInterfaceKey + time, sign);
            if (ajv.code == (int) ApiCode.VertySignErrorCode)
            {
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            //参数验证
            if (userid <= 0 || customId <= 0)
            {
                ajv.code = (int)ApiCode.VertyParamErrorCode;
                ajv.msg = string.Format(EnumHelper.GetDesc(ApiCode.VertyParamErrorCode),"");
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            AccountsFace faceModel = FacadeManage.aideAccountsFacade.GetAccountsFace(customId);
            if (faceModel == null || faceModel.UserID != userid)
            {
                ajv.msg = "抱歉，头像参数无效";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            ajv.SetValidDataValue(true);
            ajv.AddDataItem("UserID", faceModel.UserID);
            ajv.AddDataItem("FaceUrl", faceModel.FaceUrl);
            context.Response.Write(ajv.SerializeToJson());
        }

        public bool IsReusable => false;
    }
}