using System;
using Game.Facade;
using Game.Entity.NativeWeb;

namespace Game.Web.UserControl
{
    public partial class Common_Download : System.Web.UI.UserControl
    {
        //公用属性
        protected string qrLink = string.Empty;
        protected string qrh5Link = string.Empty;

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
                    qrLink = Fetch.GetQrCode(info.Field1, 180);
                    qrh5Link = Fetch.GetQrCode(info.Field4, 180);
                }
            }
        }

        
    }
}