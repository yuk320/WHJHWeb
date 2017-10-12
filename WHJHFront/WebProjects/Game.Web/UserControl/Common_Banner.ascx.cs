using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;

namespace Game.Web.UserControl
{
    // ReSharper disable once InconsistentNaming
    public partial class Common_Banner : System.Web.UI.UserControl
    {
        //公用属性
        public int TypeID = 1;
        protected string linkImg = "<a href=\"javascript:;\"><img src=\"/image/news-banner.png\" title=\"广告图\"/></a>";

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                IList<Ads> list = Fetch.GetAdsList();
                if(list != null)
                {
                    foreach(var item in list)
                    {
                        if(item.Type == TypeID)
                        {
                            linkImg = string.Format("<a href=\"{0}\"><img src=\"{1}\" title=\"{2}\"/></a>",
                            string.IsNullOrEmpty(item.LinkURL) ? "javascript:;" : item.LinkURL, 
                            Fetch.GetUploadFileUrl(item.ResourceURL), item.Title);
                            break;
                        }
                    }
                }
            }
        }
    }
}