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
using Game.Entity.Platform;
using Game.Entity.Enum;
using System.Collections;
using Game.Entity.Record;
using Game.Facade;
using System.Text;

namespace Game.Web.Module.AccountManager
{
    public partial class AccountsBaseInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(IntParam > 0)
                {
                    AccountsInfo model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(IntParam);
                    if(model != null)
                    {
                        int logonTimes = model.WebLogonTimes + model.GameLogonTimes;
                        CtrlHelper.SetText(ltSpread, model.SpreaderID > 0 ? GetGameID(model.SpreaderID) : "无推广人");
                        CtrlHelper.SetText(ltAgent, model.AgentID > 0 ? "代理商" : "非代理商");
                        CtrlHelper.SetText(ltGameID, model.GameID.ToString());
                        CtrlHelper.SetText(ltGameLogonTimes, model.GameLogonTimes.ToString());
                        CtrlHelper.SetText(ltLastLogonDate, logonTimes > 0 ? model.LastLogonDate.ToString("yyyy-MM-dd HH:mm:ss") : "");
                        CtrlHelper.SetText(ltLastLogonIP, logonTimes > 0 ? model.LastLogonIP : "");
                        CtrlHelper.SetText(ltLastLogonMachine, model.LastLogonMachine);
                        CtrlHelper.SetText(ltNickName, model.NickName);
                        CtrlHelper.SetText(ltOnLineTimeCount, Fetch.ConverTimeToDHMS(model.OnLineTimeCount));
                        CtrlHelper.SetText(ltPlayTimeCount, Fetch.ConverTimeToDHMS(model.PlayTimeCount));
                        CtrlHelper.SetText(ltRegisterDate, model.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        CtrlHelper.SetText(ltRegisterIP, model.RegisterIP);
                        CtrlHelper.SetText(ltRegisterMachine, model.RegisterMachine);
                        CtrlHelper.SetText(ltRegisterOrigin, GetRegisterOrigin(model.RegisterOrigin));
                        CtrlHelper.SetText(ltWebLogonTimes, model.WebLogonTimes.ToString());
                        CtrlHelper.SetText(ltLogonSpacingTime, logonTimes > 0 ? (Fetch.GetTimeSpan(Convert.ToDateTime(model.LastLogonDate), DateTime.Now) + "前") : "");
                        CtrlHelper.SetText(ltRegSpacingTime, Fetch.GetTimeSpan(Convert.ToDateTime(model.RegisterDate), DateTime.Now) +"前");
                        CtrlHelper.SetText(ltSex, model.Gender == 1 ? "男" : "女");

                        CtrlHelper.SetText(txtRealName, model.Compellation);
                        CtrlHelper.SetText(txtCardNum, model.PassPortID);
                        CtrlHelper.SetText(txtUnderWrite, model.UnderWrite);

                        ckbNullity.Checked = model.Nullity == 1 ? true : false;
                        ckbLock.Checked = model.MoorMachine == 1 ? true : false;

                        imgFace.ImageUrl = FacadeManage.aideAccountsFacade.GetAccountsFaceById(model.CustomID);
                    }
                }
            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //权限验证
            AuthUserOperationPermission(Permission.Edit);

            if(IntParam > 0)
            {
                //获取保存数值
                string realname = CtrlHelper.GetText(txtRealName);
                string cardnum = CtrlHelper.GetText(txtCardNum);
                string underwrite = CtrlHelper.GetText(txtUnderWrite);

                //输入验证
                AccountsInfo model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(IntParam);
                if(model != null && model.Compellation.Length > 0 && string.IsNullOrEmpty(realname))
                {
                    MessageBox("已实名认证信息保存不能为空");
                    return;
                }
                if(model != null && model.PassPortID.Length > 0 && string.IsNullOrEmpty(cardnum))
                {
                    MessageBox("已实名认证信息保存不能为空");
                    return;
                }
                if(!string.IsNullOrEmpty(cardnum)&&!Utils.Validate.IsIDCard(cardnum))
                {
                    MessageBox("身份证格式错误");
                    return;
                }

                int isNullity = ckbNullity.Checked ? 1 : 0;
                int isLock = ckbLock.Checked ? 1 : 0;

                AccountsInfo info = new AccountsInfo();
                info.UserID = IntParam;
                info.Compellation = realname;
                info.PassPortID = cardnum;
                info.UnderWrite = underwrite;
                info.Nullity = (byte)isNullity;
                info.MoorMachine = (byte)isLock;

                int result = FacadeManage.aideAccountsFacade.UpdateAccountsBaseInfo(info);
                MessageBox(result > 0 ? "修改资料成功" : "修改资料失败");
            }
        }
    }
}