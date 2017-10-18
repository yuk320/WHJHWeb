using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;
using Game.Web.UI;
using Game.Entity.Accounts;
using Game.Kernel;
using Game.Entity.Record;
using System.Text;
using System.Data;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class GrantGameID : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.GrantGameID);
            if(!IsPostBack)
            {
                if(IntParam > 0)
                {
                    AccountsInfo model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(IntParam);
                    if(model != null)
                    {
                        CtrlHelper.SetText(ltNickName, model.NickName);
                        CtrlHelper.SetText(ltGameID, model.GameID.ToString());
                        DataBindGameID();
                    }
                }
            }
        }
        /// <summary>
        /// 刷新靓号
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataBindGameID();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string gameid = ddlGameID.SelectedValue;
            string strReason = CtrlHelper.GetText(txtReason);
            if(IntParam<=0 || string.IsNullOrEmpty(gameid))
            {
                MessageBox("赠送失败");
                return;
            }
            if(string.IsNullOrEmpty(strReason))
            {
                MessageBox("请填写赠送原因");
                return;
            }
            if(userExt == null)
            {
                MessageBox("登录已过期，请重新登录");
                return;
            }

            Message msg = FacadeManage.aideRecordFacade.GrantGameId(IntParam, Convert.ToInt32(gameid),
               userExt.UserID, strReason, GameRequest.GetUserIP());
            if(msg.Success)
            {
                MessageBox("赠送靓号成功");
            }
            else
            {
                MessageBox(msg.Content);
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBindGameID()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetReserveIdentifierList();
            ddlGameID.DataSource = ds.Tables[0];
            ddlGameID.DataTextField = "GameID";
            ddlGameID.DataValueField = "GameID";
            ddlGameID.DataBind();
        }
    }
}