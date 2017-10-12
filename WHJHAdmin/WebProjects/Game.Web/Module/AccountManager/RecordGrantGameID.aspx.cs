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

namespace Game.Web.Module.AccountManager
{
    public partial class RecordGrantGameID : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PagerSet pagerSet = FacadeManage.aideRecordFacade.GetList(Entity.Record.RecordGrantGameID.Tablename, 
                    1, 100, string.Format("WHERE UserID={0}", IntParam), "ORDER BY RecordID DESC");
                litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
                rptDataList.DataSource = pagerSet.PageSet;
                rptDataList.DataBind();
            }
        }
    }
}