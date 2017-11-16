using System;
using Game.Facade;
using Game.Entity.NativeWeb;

namespace Game.Web.UserControl
{
    public partial class Common_Download : System.Web.UI.UserControl
    {
        //公用属性
        protected string QrLink = string.Empty;
        protected string Qrh5Link = string.Empty;

        /// <summary>
        /// 控件加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ConfigInfo info = Fetch.GetWebSiteConfig();
                if(info != null)
                {
                    QrLink = Fetch.GetQrCode(info.Field1, 180);
                    Qrh5Link = Fetch.GetQrCode(info.Field4, 180);
                }
            }
        }

        
    }
}