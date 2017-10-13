using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Facade;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentUserUnder : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetAgentUserUnderRegister(IntParam);
            lbTotal.Text = ds.Tables[0].Rows.Count.ToString();
            litNoData.Visible = ds.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = ds;
            rptDataList.DataBind();
        }
    }
}