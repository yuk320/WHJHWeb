using Game.Facade;
using Game.Kernel;
using System;

namespace Game.Web.News
{
    public partial class Index : UCPageBase
    {
        /// <summary>
        /// 加载页面标签
        /// </summary>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            AddMetaTitle("新闻公告");
            AddMetaTag("keywords", AppConfig.PageKey);
            AddMetaTag("description", AppConfig.PageDescript);
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //绑定新闻公告数据
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetNewPageList(PageIndex, anpPage.PageSize);
            anpPage.RecordCount = pagerSet.RecordCount;
            rpNew.DataSource = pagerSet.PageSet;
            rpNew.DataBind();
        }
    }
}