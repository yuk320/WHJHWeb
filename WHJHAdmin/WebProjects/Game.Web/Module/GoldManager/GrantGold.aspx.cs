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


namespace Game.Web.Module.GoldManager
{
    public partial class GrantGold : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AuthUserOperationPermission(Permission.GrantTreasure);
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strReason = CtrlHelper.GetText(txtReason);
            int gold = CtrlHelper.GetInt(txtGold, 0);
            bool flag = cbPull.Checked;

            if(string.IsNullOrEmpty(strReason))
            {
                MessageBox("赠送备注不能为空");
                return;
            }
            string ip = GameRequest.GetUserIP();
            RecordGrantTreasure rgt = new RecordGrantTreasure();
            rgt.MasterID = userExt.UserID;
            rgt.UserID = IntParam;
            rgt.AddGold = gold;
            rgt.ClientIP = ip;
            rgt.Reason = strReason;

            Message msg = FacadeManage.aideTreasureFacade.GrantTreasure(rgt);
            if(msg.Success)
            {
                if(flag)
                {
                    AccountsUmeng umeng = FacadeManage.aideAccountsFacade.GetAccountsUmeng(IntParam);
                    if(umeng != null && !string.IsNullOrEmpty(umeng.DeviceToken))
                    {
                        string content = "系统管理员" + userExt.UserName + "已" + (gold < 0 ? "扣除" : "赠送") + "您" + gold.ToString() + "金币";
                        DateTime start = DateTime.Now.AddMinutes(1);
                        DateTime end = start.AddHours(5);
                        bool result = Umeng.SendMessage(umeng.DeviceType, content, "unicast", start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), umeng.DeviceToken);
                        if(!result)
                        {
                            MessageBox("赠送成功，但推送消息失败，请前往友盟后台绑定系统后台ip");
                            return;
                        }
                        RecordAccountsUmeng record = new RecordAccountsUmeng();
                        record.MasterID = rgt.MasterID;
                        record.UserID = rgt.UserID;
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