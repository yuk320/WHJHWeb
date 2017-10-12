using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Entity.NativeWeb;

namespace Game.Web.UserControl
{
    public partial class Common_Contact : System.Web.UI.UserControl
    {
        //公用属性
        protected string qq = string.Empty;
        protected string phone = string.Empty;

        /// <summary>
        /// 控件加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ConfigInfo info = Fetch.GetCustomerService();
                if(info != null)
                {
                    qq = info.Field3;
                    phone = info.Field1;
                }
            }
        }
    }
}