using Game.Facade;
using Game.Utils;
using System;
using System.Text.RegularExpressions;
using Game.Facade.Enum;


namespace Game.Web.Mobile
{
    public partial class MoblieVideoInfo : System.Web.UI.Page
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Access-Control-Allow-Origin", AppConfig.MoblieInterfaceDomain);

            if (IsPostBack) return;
            int userid = GameRequest.GetQueryInt("userid", 0);
            string time = GameRequest.GetQueryString("time");
            string sign = GameRequest.GetQueryString("sign");
            string vNumber = GameRequest.GetQueryString("vNumber");

            if (userid <= 0 || string.IsNullOrEmpty(time) || string.IsNullOrEmpty(sign) ||
                string.IsNullOrEmpty(vNumber))
            {
                Response.Write("抱歉，参数错误");
                return;
            }
            if (!Regex.IsMatch(vNumber, @"^\d{5,}$"))
            {
                Response.Write("抱歉，参数格式异常");
                return;
            }
            AjaxJsonValid ajv = Fetch.VerifySignData(userid + AppConfig.MoblieInterfaceKey + time, sign);
            if (ajv.code == (int) ApiCode.VertySignErrorCode)
            {
                Response.Write(EnumHelper.GetDesc(ApiCode.VertySignErrorCode));
                return;
            }

            byte[] bt = FacadeManage.aideTreasureFacade.GetVideoDataByVNumber(vNumber);
            if (bt == null)
            {
                Response.Write("抱歉，存盘信息不存在");
                return;
            }

            Response.ClearContent();
            Response.ContentType = "video/x-msvideo";
            Response.BinaryWrite(bt);
            Response.End();
        }
    }
}
