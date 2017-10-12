using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.UserControl
{
    public partial class Common_Footer : System.Web.UI.UserControl
    {
        //公用属性
        protected string footer = string.Empty;

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
                    footer = info.Field8;
                }
            }
        }
    }
}