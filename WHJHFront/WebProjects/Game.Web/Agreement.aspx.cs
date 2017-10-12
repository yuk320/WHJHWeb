using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web
{
    public partial class Agreement : UCPageBase
    {
        //公用属性
        protected string content = string.Empty;

        /// <summary>
        /// 加载页面标签
        /// </summary>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            AddMetaTitle("服务条款");
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
                IList<ConfigInfo> list = Fetch.GetConfigInfoList();
                if(list != null)
                {
                    foreach(var item in list)
                    {
                        if(item.ConfigKey == AppConfig.SiteConfigKey.WebAgreement.ToString())
                        {
                            content = item.Field8;
                            break;
                        }
                    }
                }
            }
        }
    }
}