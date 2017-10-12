using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Kernel;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AccountManager
{
    public partial class ConfineMachineInfo : AdminPage
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
            ConfineMachine machine = new ConfineMachine();
            if(!string.IsNullOrEmpty(StrParam))
            {
                AuthUserOperationPermission(Permission.Edit);
                machine = FacadeManage.aideAccountsFacade.GetConfineMachineBySerial(StrParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                machine.MachineSerial = CtrlHelper.GetText(txtString);
            }

            machine.EnjoinLogon = ckbEnjoinLogon.Checked;
            machine.EnjoinRegister = ckbEnjoinRegister.Checked;
            machine.EnjoinOverDate = string.IsNullOrEmpty(CtrlHelper.GetText(txtEnjoinOverDate)) ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(CtrlHelper.GetText(txtEnjoinOverDate));
            machine.CollectNote = TextFilter.FilterAll(CtrlHelper.GetText(txtCollectNote));
            int result = 0;

            if(string.IsNullOrEmpty(StrParam))
            {
                ConfineMachine model = FacadeManage.aideAccountsFacade.GetConfineMachineBySerial(machine.MachineSerial);
                if(model != null)
                {
                    ShowError("该限制机器码已经存在");
                    return;
                }
                result = FacadeManage.aideAccountsFacade.InsertConfineMachine(machine);
            }
            else
            {
                result = FacadeManage.aideAccountsFacade.UpdateConfineMachine(machine);
            }
            if(result>0)
            {
                ShowInfo("操作成功", "ConfineMachineList.aspx", 1200);
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
                ConfineMachine model = FacadeManage.aideAccountsFacade.GetConfineMachineBySerial(StrParam);
                if(model != null)
                {
                    CtrlHelper.SetText(ltString, model.MachineSerial);
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