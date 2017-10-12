using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Entity.Record;
using Game.Facade;
using Game.Entity.Treasure;
using Game.Entity.Accounts;
using Game.Web.UI;
using Game.Entity.Enum;
using Game.Utils;
using Game.Kernel;

namespace Game.Web.Module.AgentManager
{
    public partial class AgentGrantDiamond : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AuthUserOperationPermission(Permission.GrantDiamond);
            }
        }
        /// <summary>
        /// 页面保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strReason = CtrlHelper.GetText(txtReason);
            int diamond = CtrlHelper.GetInt(txtDiamond, 0);
            bool flag = cbPull.Checked;
            if(diamond <= 0)
            {
                MessageBox("赠送钻石数必须大于零！");
                return;
            }
            if(string.IsNullOrEmpty(strReason))
            {
                MessageBox("赠送备注不能为空");
                return;
            }
            string ip = GameRequest.GetUserIP();
            RecordGrantDiamond rgd = new RecordGrantDiamond();
            rgd.MasterID = userExt.UserID;
            rgd.UserID = IntParam;
            rgd.TypeID = 0;
            rgd.AddDiamond = diamond;
            rgd.ClientIP = ip;
            rgd.CollectNote = strReason;

            Message msg = FacadeManage.aideTreasureFacade.GrantDiamond(rgd);
            if(msg.Success)
            {
                if(flag)
                {
                    AccountsUmeng umeng = FacadeManage.aideAccountsFacade.GetAccountsUmeng(IntParam);
                    if(umeng != null && !string.IsNullOrEmpty(umeng.DeviceToken))
                    {
                        string content = "系统管理员" + userExt.UserName + "已赠送您" + diamond.ToString() + "钻石";
                        DateTime start = DateTime.Now.AddMinutes(1);
                        DateTime end = start.AddHours(5);
                        bool result = Umeng.SendMessage(umeng.DeviceType, content, "unicast", start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), umeng.DeviceToken);
                        if(!result)
                        {
                            MessageBox("赠送成功，但推送消息失败，请前往友盟后台绑定系统后台ip");
                            return;
                        }
                        RecordAccountsUmeng record = new RecordAccountsUmeng();
                        record.MasterID = rgd.MasterID;
                        record.UserID = rgd.UserID;
                        record.PushType = umeng.DeviceType;
                        record.PushTime = DateTime.Now;
                        record.PushIP = ip;
                        record.PushContent = content;
                        int rows = FacadeManage.aideRecordFacade.AddRecordAccountsUmeng(record);
                        MessageBox(rows > 0 ? "赠送成功" : "赠送成功，但推送记录写入失败");
                    }
                    else
                    {
                        MessageBox("赠送成功，但推送用户未绑定设备，无法推送");
                    }
                }
                else
                {
                    MessageBox("赠送成功");
                }
            }
            else
            {
                MessageBox("赠送失败");
            }
        }
    }
}