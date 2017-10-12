using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Game
{
    public partial class Index : UCPageBase
    {
        protected string imgDomain = string.Empty;

        /// <summary>
        /// 加载页面标签
        /// </summary>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            AddMetaTitle("游戏下载");
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
                //获取图片域名
                ConfigInfo info = Fetch.GetWebSiteConfig();
                imgDomain = info != null ? info.Field2 : "";

                //绑定游戏列表
                PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetGamePageList(PageIndex, anpPage.PageSize);
                anpPage.RecordCount = pagerSet.RecordCount;
                rpGame.DataSource = pagerSet.PageSet;
                rpGame.DataBind();
            }
        }
    }
}