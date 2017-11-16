using System;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Utils;
using System.Web;
using System.Web.SessionState;
using Game.Entity.NativeWeb;
using Game.Facade.Enum;

namespace Game.Web.Spread
{
    /// <summary>
    /// 获取自定义头像
    /// </summary>
    public class UserFace : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            //允许跨站请求域名
            context.Response.ContentType = "application/json";

            int userid = GameRequest.GetQueryInt("userid", 0);
            //签名验证 //接口版本号
            AjaxJsonValid ajv = new AjaxJsonValid {data = {["apiVersion"] = 20171115}};
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("info","{}");
            ajv.AddDataItem("record","{}");
            context.Response.Write(ajv.SerializeToJson());
        }

        public bool IsReusable => false;
    }
}