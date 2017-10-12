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
    public partial class DiamondDistribute : AdminPage
    {
        //公用属性
        protected string diamond;
        protected string djson;

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //获取分布数据
                DataSet ds = FacadeManage.aideTreasureFacade.GetDiamondDistribute();

                //获取钻石分布
                IList<StatisticsWealth> list = DataHelper.ConvertDataTableToObjects<StatisticsWealth>(ds.Tables[0]);
                if(list != null)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    djson = js.Serialize(list);
                }
                //获取钻石总数
                diamond = ds.Tables[1].Rows[0]["Diamond"].ToString();
            }
        }
    }
}