using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Entity.PlatformManager;
using Game.Entity;
using Game.Facade;
using Game.Utils;
using Game.Utils.Cache;

namespace Game.Web
{
    public partial class Right : System.Web.UI.Page
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        protected LoginUser userExt;
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            userExt = Fetch.GetLoginUser();
        }
    }
}