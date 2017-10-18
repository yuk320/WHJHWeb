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
    public partial class ConfineAddressTop : AdminPage
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
        /// 批量禁用地址
        /// </summary>
        protected void DisableIP(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Add);
            try
            {
                string ipList = GameRequest.GetFormString("cid");
                FacadeManage.aideAccountsFacade.BatchInsertConfineAddress(ipList);
                MessageBox("操作成功", "ConfineAddressTop.aspx");
            }
            catch(Exception)
            {
                MessageBox("操作失败");
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetIpRegisterTop100();
            litNoData.Visible = ds.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = ds;
            rptDataList.DataBind();
        }
    }
}
