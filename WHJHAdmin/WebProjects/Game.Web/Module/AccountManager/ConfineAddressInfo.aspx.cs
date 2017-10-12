using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using System.Text.RegularExpressions;

using Game.Utils;
using Game.Entity.Accounts;
using Game.Kernel;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class ConfineAddressInfo : AdminPage
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
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ConfineAddress address = new ConfineAddress();
            if(!string.IsNullOrEmpty(StrParam))
            {
                AuthUserOperationPermission(Permission.Edit);
                address = FacadeManage.aideAccountsFacade.GetConfineAddressByAddress(StrParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                address.AddrString = CtrlHelper.GetText(txtString);
            }
            address.EnjoinLogon = ckbEnjoinLogon.Checked;
            address.EnjoinRegister = ckbEnjoinRegister.Checked;
            address.EnjoinOverDate = string.IsNullOrEmpty(CtrlHelper.GetText(txtEnjoinOverDate)) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(CtrlHelper.GetText(txtEnjoinOverDate));
            address.CollectNote = TextFilter.FilterAll(CtrlHelper.GetText(txtCollectNote));
            int result = 0;
            if(string.IsNullOrEmpty(StrParam))
            {
                if(!Utils.Validate.IsIP(address.AddrString))
                {
                    ShowError("该限制IP地址格式不正确");
                    return;
                }
                ConfineAddress model = FacadeManage.aideAccountsFacade.GetConfineAddressByAddress(address.AddrString);
                if(model != null)
                {
                    ShowError("该限制IP地址已经存在");
                    return;
                }
                result = FacadeManage.aideAccountsFacade.InsertConfineAddress(address);
            }
            else
            {
                result = FacadeManage.aideAccountsFacade.UpdateConfineAddress(address);
            }
            if(result>0)
            {
                ShowInfo("操作成功", "ConfineAddressList.aspx", 1200);
            }
            else
            {
                ShowError("操作失败");
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            if(!string.IsNullOrEmpty(StrParam))
            {
                ConfineAddress model = FacadeManage.aideAccountsFacade.GetConfineAddressByAddress(StrParam);
                if(model != null)
                {
                    CtrlHelper.SetText(ltString, model.AddrString);
                    txtString.Visible = false;
                    ckbEnjoinLogon.Checked = model.EnjoinLogon;
                    ckbEnjoinRegister.Checked = model.EnjoinRegister;
                    CtrlHelper.SetText(txtEnjoinOverDate, model.EnjoinOverDate == null ? "" : Convert.ToDateTime(model.EnjoinOverDate).ToString("yyyy-MM-dd"));
                    CtrlHelper.SetText(txtCollectNote, model.CollectNote);
                }
            }
        }
    }
}