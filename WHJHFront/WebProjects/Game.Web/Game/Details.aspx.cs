using Game.Facade;
using Game.Utils;
using Game.Entity.NativeWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Game
{
    public partial class Details : UCPageBase
    {
        //公用属性
        protected string kindName = string.Empty;
        protected string content = string.Empty;
        protected string intro = string.Empty;

        /// <summary>
        /// 加载页面标签
        /// </summary>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            //获取游戏规则详情
            int kindid = GameRequest.GetQueryInt("id", 0);
            if(kindid > 0)
            {
                GameRule rule = FacadeManage.aideNativeWebFacade.GetGameRuleInfo(kindid);
                if(rule != null)
                {
                    kindName = rule.KindName;
                    content = rule.KindRule;
                    intro = rule.KindIntro;
                }
            }
            //设置页面标签
            AddMetaTitle(string.IsNullOrEmpty(kindName) ? "游戏规则" : kindName);
            AddMetaTag("keywords", AppConfig.PageKey);
            AddMetaTag("description", string.IsNullOrEmpty(intro) ? AppConfig.PageDescript : intro);
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}