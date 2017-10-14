using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Game.Utils;

namespace Game.Facade
{
    /// <summary>
    /// 页面基类
    /// </summary>
    public abstract class UCPageBase : Page
    {
        #region 参数属性
        /// <summary>
        /// 页码
        /// </summary>
        protected int PageIndex
        {
            get
            {
                return GameRequest.GetQueryInt("page", 1);
            }
        }
        #endregion

        /// <summary>
        /// 初始化验证访问设备
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(Fetch.GetTerminalType(Page.Request) != 0)
            {
                Response.Redirect("/Mobile/Index.aspx");
            }
        }

        #region 页面标签添加
        /// <summary>
        /// 添加标题标签
        /// </summary>
        /// <param name="titleName">页面title</param>
        protected virtual void AddMetaTitle(string titleName)
        {
            HtmlTitle title = new HtmlTitle();
            title.Text = titleName + " - " + AppConfig.PageTitle;
            Page.Header.Controls.Add(title);
        }
        /// <summary>
        /// 添加meta标签
        /// </summary>
        /// <param name="name">标签名称</param>
        /// <param name="value">标签值</param>
        protected virtual void AddMetaTag(string name, string value)
        {
            HtmlMeta meta = new HtmlMeta
            {
                Name = name,
                Content = value
            };
            Page.Header.Controls.Add(meta);
        }
        #endregion
    }
}
