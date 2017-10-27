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
            if (IsPostBack) return;
            if (IntParam <= 0) return;
            AccountsInfo model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(IntParam);
            if (model == null) return;
            CtrlHelper.SetText(ltNickName, model.NickName);
            CtrlHelper.SetText(ltGameID, model.GameID.ToString());
            DataBindGameId();
        }
        /// <summary>
        /// 刷新靓号
        /// </summary>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DataBindGameId();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int gameid = CtrlHelper.GetInt(txtGameID, 0);
            string strReason = CtrlHelper.GetText(txtReason);
            if(IntParam<=0 || gameid<=0)
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

            Message msg = FacadeManage.aideRecordFacade.GrantGameId(IntParam, gameid,
               userExt.UserID, strReason, GameRequest.GetUserIP());
            MessageBox(msg.Success ? "赠送靓号成功" : msg.Content);
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBindGameId()
        {
            DataSet ds = FacadeManage.aideAccountsFacade.GetReserveIdentifierList();
            txtGameID.Focus();
            txtGameID.Text = ds.Tables[0].Rows[0]["GameID"].ToString();
        }
    }
}