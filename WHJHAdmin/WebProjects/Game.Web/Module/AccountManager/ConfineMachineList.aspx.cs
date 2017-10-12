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
using Game.Entity.Enum;
using Game.Facade;
using Game.Entity.Accounts;

namespace Game.Web.Module.AccountManager
{
    public partial class ConfineMachineList : AdminPage
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
        /// 数据分页
        /// </summary>
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            string where = string.Empty;
            if(!string.IsNullOrEmpty(query))
            {
                where = ckbIsLike.Checked ? string.Format(" WHERE MachineSerial LIKE '%{0}%' ", query) : string.Format(" WHERE MachineSerial='{0}'", query);
            }
            ViewState["SearchItems"] = where;
            BindData();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Delete);
            StringBuilder sbValue = new StringBuilder();
            string Checkbox_Value = GameRequest.GetFormString("cid");
            string[] arrValue = Checkbox_Value.Split(',');
            if(arrValue.Length > 0)
            {
                for(int i = 0; i < arrValue.Length; i++)
                {
                    sbValue.AppendFormat("'{0}',", arrValue[i]);
                }

            }
            string strQuery = "WHERE MachineSerial in (" + sbValue.ToString().TrimEnd(new char[] { ',' }) + ")";
            int result = FacadeManage.aideAccountsFacade.DeleteConfineMachine(strQuery);
            if(result > 0)
            {
                ShowInfo("删除成功", "ConfineMachineList.aspx", 1200);
                BindData();
            }
            else
            {
                ShowError("删除失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(ConfineMachine.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, SearchItems, Orderby);
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if(ViewState["SearchItems"] == null)
                {
                    ViewState["SearchItems"] = " WHERE 1=1";
                }
                return (string)ViewState["SearchItems"];
            }
            set
            {
                ViewState["SearchItems"] = value;
            }
        }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if(ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY CollectDate DESC";
                }
                return (string)ViewState["Orderby"];
            }
            set
            {
                ViewState["Orderby"] = value;
            }
        }
    }
}