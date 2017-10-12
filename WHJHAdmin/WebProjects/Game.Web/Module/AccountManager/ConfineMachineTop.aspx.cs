using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using System.Data;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class ConfineMachineTop : AdminPage
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
        /// 批量禁用机器码
        /// </summary>
        protected void DisableMachine(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Add);
            try
            {
                string machinelist = GameRequest.GetFormString("cid");
                FacadeManage.aideAccountsFacade.BatchInsertConfineMachine(machinelist);
                MessageBox("操作成功", "ConfineMachineTop.aspx");
            }
            catch(Exception)
            {
                MessageBox("操作失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetMachineRegisterTop100();
            litNoData.Visible = ds.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = ds;
            rptDataList.DataBind();
        }
    }
}
