using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Contact
{
    public partial class Index : UCPageBase
    {
        //公用属性
        protected string contactIntro = string.Empty;
        protected string contactPhone = string.Empty;
        protected string contactEmail = string.Empty;
        protected string contactWeChat = string.Empty;
        protected string contactQQ = string.Empty;
        protected string contactAddress = string.Empty;
        protected string baiduAddress = string.Empty;

        /// <summary>
        /// 加载页面标签
        /// </summary>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            AddMetaTitle("联系我们");
            AddMetaTag("keywords", AppConfig.PageKey);
            AddMetaTag("description", AppConfig.PageDescript);
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ConfigInfo info = Fetch.GetCustomerService();
                if(info != null)
                {
                    contactIntro = info.Field8;
                    contactEmail = info.Field5;
                    contactAddress = info.Field6;
                    contactPhone = info.Field1;
                    contactQQ = info.Field3;
                    contactWeChat = info.Field2;
                    baiduAddress = info.Field7;
                }
            }
        }
    }
}