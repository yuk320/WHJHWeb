using Game.Entity.Record;
using Game.Facade;
using Game.Utils;
using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Game.Web.UI;

namespace Game.Web.Module.DataStatistics
{
    public partial class WealthDistribute : AdminPage
    {
        //公用属性
        protected string score;
        protected string insure;
        protected string gjson;

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //获取分布数据
                DataSet ds = FacadeManage.aideTreasureFacade.GetGoldDistribute();
                
                //获取金币分布
                IList<StatisticsWealth> list = DataHelper.ConvertDataTableToObjects<StatisticsWealth>(ds.Tables[0]);
                if(list!=null)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    gjson = js.Serialize(list);
                }
                //获取金币总数
                score = ds.Tables[1].Rows[0]["Score"].ToString();
                insure = ds.Tables[1].Rows[0]["InsureScore"].ToString();
            }
        }
    }
}