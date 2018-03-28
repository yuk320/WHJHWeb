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
using Game.Entity.Accounts;
using Game.Entity;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AccountsDataBind();
            }
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            AccountsDataBind();
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string query = CtrlHelper.GetTextAndFilter(txtSearch);
            int type = Convert.ToInt32(ddlSearchType.SelectedValue);

            StringBuilder condition = new StringBuilder(" WHERE 1=1");

            if (!string.IsNullOrEmpty(query))
            {
                if (type != 2)
                {
                    if (!Utils.Validate.IsPositiveInt(query))
                    {
                        ShowError("输入查询格式不正确");
                        return;
                    }
                    if (type == 1)
                    {
                        condition.AppendFormat(" AND GameID={0}", query);
                    }
                    else
                    {
                        condition.AppendFormat(" AND SpreaderID={0}", GetUserIDByGameID(Convert.ToInt32(query)));
                    }
                }
                else
                {
                    condition.AppendFormat(" AND NickName='{0}'", query);
                }
            }
            ViewState["SearchItems"] = condition.ToString();
            AccountsDataBind();
        }

        /// <summary>
        /// 批量冻结玩家
        /// </summary>
        protected void btnDongjie_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aideAccountsFacade.NullityAccountInfo(StrCIdList, 1);
            if (result > 0)
            {
                ShowInfo("冻结成功");
                AccountsDataBind();
            }
            else
            {
                ShowError("冻结失败");
            }
        }

        /// <summary>
        /// 批量解冻玩家
        /// </summary>
        protected void btnJiedong_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Enable);
            int result = FacadeManage.aideAccountsFacade.NullityAccountInfo(StrCIdList, 0);
            if (result > 0)
            {
                ShowInfo("解冻成功");
                AccountsDataBind();
            }
            else
            {
                ShowError("解冻失败");
            }
        }

        /// <summary>
        /// 批量设置转账权限
        /// </summary>
        protected void btnSetSingle_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.TransferPower);
            int result = FacadeManage.aideAccountsFacade.TransferPowerAccounts(StrCIdList, 64);
            if (result > 0)
            {
                ShowInfo("设置成功");
                AccountsDataBind();
            }
            else
            {
                ShowError("设置失败");
            }
        }

        /// <summary>
        /// 批量取消转账权限
        /// </summary>
        protected void benCancleSingle_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.TransferPower);
            int result = FacadeManage.aideAccountsFacade.TransferPowerAccounts(StrCIdList, 64, "^");
            if (result > 0)
            {
                ShowInfo("取消成功");
                AccountsDataBind();
            }
            else
            {
                ShowError("取消失败");
            }
        }

        /// <summary>
        /// 设置所有人转账权限
        /// </summary>
        protected void btnSetting_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.TransferPower);
            int result = FacadeManage.aideAccountsFacade.TransferPowerAccounts(64);
            if (result > 0)
            {
                ShowInfo("设置成功");
                AccountsDataBind();
            }
            else
            {
                ShowError("设置失败");
            }
        }

        /// <summary>
        /// 取消所有人转账权限
        /// </summary>
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.TransferPower);
            int result = FacadeManage.aideAccountsFacade.TransferPowerAccounts(64, "^");
            if (result > 0)
            {
                ShowInfo("取消成功");
                AccountsDataBind();
            }
            else
            {
                ShowError("取消失败");
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void AccountsDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideAccountsFacade.GetList(AccountsInfo.Tablename,
                anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
            anpPage.RecordCount = pagerSet.RecordCount;
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
                if (ViewState["SearchItems"] == null)
                {
                    ViewState["SearchItems"] = "WHERE 1=1";
                }
                return (string) ViewState["SearchItems"];
            }
            set { ViewState["SearchItems"] = value; }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if (ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY UserID DESC";
                }
                return (string) ViewState["Orderby"];
            }
            set { ViewState["Orderby"] = value; }
        }
    }
}
