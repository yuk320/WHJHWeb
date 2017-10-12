using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using System.Data;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Entity.Treasure;

namespace Game.Web.Module.Diamond
{
    public partial class TopOutDiamond : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                rptDataList.DataSource = FacadeManage.aideRecordFacade.GetTradeOutDiamondRank();
                rptDataList.DataBind();
            }
        }
    }
}