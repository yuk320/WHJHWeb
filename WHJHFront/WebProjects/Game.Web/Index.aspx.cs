using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using System.Text;

namespace Game.Web
{
    public partial class Index : UCPageBase
    {
        //公用属性
        protected string bannerAds = string.Empty;
        protected string bannerNews = string.Empty;
        protected string title = string.Empty;
        protected string time = string.Empty;
        protected string content = string.Empty;

        /// <summary>
        /// 加载页面标签
        /// </summary>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);

            AddMetaTitle("首页");
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
                //获取网站站点配置
                ConfigInfo info = Fetch.GetWebSiteConfig();
                string imgDomain = info != null ? info.Field2 : "";

                //获取置顶新闻图片
                bannerNews = Fetch.GetUploadFileUrl(imgDomain, "/Site/toplogo.png");

                //获取广告图
                IList <Ads> list = Fetch.GetAdsList();
                if(list != null)
                {
                    foreach(var item in list)
                    {
                        if(item.Type == 0)
                        {
                            bannerAds = bannerAds + string.Format("<div class=\"slider-item\"><a href=\"{0}\"><img src=\"{1}\" title=\"{2}\"></a></div>",
                            string.IsNullOrEmpty(item.LinkURL) ? "javascript:;" : item.LinkURL, Fetch.GetUploadFileUrl(imgDomain, item.ResourceURL), item.Title);
                        }
                    }
                }
                bannerAds = bannerAds == null ? "<div class=\"slider-item\"><a href=\"javascript:;\"><img src=\"/image/banner.png\" title=\"网站首页\"/></a></div>" : bannerAds;

                //绑定新闻公告
                IList <SystemNotice> snList = FacadeManage.aideNativeWebFacade.GetHomePageNews();
                if(snList != null)
                {
                    SystemNotice notice = snList[0];
                    if(notice!=null&&notice.IsTop)
                    {
                        snList.RemoveAt(0);
                        //显示置顶新闻公告
                        title = notice.NoticeTitle.Length > 20 ? (notice.NoticeTitle.Substring(0, 20) + "...") : notice.NoticeTitle;
                        time = notice.PublisherTime.ToString("yyyy-MM-dd");
                        content = notice.MoblieContent.Length > 140 ? (notice.MoblieContent.Substring(0, 140) + "...") : notice.MoblieContent;
                        content = !string.IsNullOrEmpty(content) ? (content + string.Format("&emsp;<a href=\"/News/Details.aspx?id={0}\">详情>></a>", notice.NoticeID)) : "";
                    }
                    if(snList.Count > 12)
                    {
                        snList.RemoveAt(12);
                    }

                    //显示新闻信息列表
                    rpNews.DataSource = snList;
                    rpNews.DataBind();
                }
            }
        }
    }
}