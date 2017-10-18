using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using System;

namespace Game.Web.News
{
    public partial class Details : UCPageBase
    {
        //公用属性
        protected string Resource = string.Empty;
        protected string Time = string.Empty;
        protected string Content = string.Empty;
        protected string Intro = string.Empty;
        protected string NewsTitle;
        /// <summary>
        /// 加载页面标签
        /// </summary>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            //获取新闻公告详情
            int noticeid = GameRequest.GetQueryInt("id", 1);
            if(noticeid > 0)
            {
                SystemNotice notice = FacadeManage.aideNativeWebFacade.GetWebNewsInfo(noticeid);
                if(notice != null)
                {
                    NewsTitle = notice.NoticeTitle;
                    Resource = notice.Publisher;
                    Time = notice.PublisherTime.ToString("yyyy-MM-dd");
                    Content = notice.WebContent;
                    Intro = notice.MoblieContent.Length>100? notice.MoblieContent.Substring(0,100): notice.MoblieContent;
                }
            }
            //设置页面标签
            AddMetaTitle(string.IsNullOrEmpty(NewsTitle) ? "新闻公告" : NewsTitle);
            AddMetaTag("keywords", AppConfig.PageKey);
            AddMetaTag("description", string.IsNullOrEmpty(Intro) ? AppConfig.PageDescript : Intro);
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}