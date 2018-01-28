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

namespace Game.Web.Module.WebManager
{
    public partial class UMessagePushAll : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtTime.Text = DateTime.Now.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss");

                gameid.Visible = false;
                txtGameID.Text = "0";
                time.Visible = false;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                logincount.Visible = false;
                txtNoLoginDay.Text = "0";
            }

        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Edit);
            try
            {
                int typeID = Convert.ToInt32(ddlType.SelectedValue);
                string content = CtrlHelper.GetText(txtContent);
                DateTime time = Convert.ToDateTime(txtTime.Text);

                if(string.IsNullOrEmpty(content))
                {
                    MessageBox("消息内容不能为空");
                    return;
                }
                if(DateTime.Now > time)
                {
                    MessageBox("推送时间不能小于或等于当前时间");
                    return;
                }
                DateTime endTime = time.AddHours(5);
                IList<AccountsUmeng> list = new List<AccountsUmeng>();
                if(typeID == 0)
                {
                    bool flag = false;
                    flag = Umeng.SendMessage(0, content, "broadcast", time.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"), "");
                    if(!flag)
                    {
                        MessageBox("推送消息失败，请前往友盟后台绑定系统后台ip");
                        return;
                    }
                    flag = Umeng.SendMessage(1, content, "broadcast", time.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"), "");
                    if(!flag)
                    {
                        MessageBox("推送消息失败，请前往友盟后台绑定系统后台ip");
                        return;
                    }
                    list = FacadeManage.aideAccountsFacade.GetAccountsUmengList("");
                }
                else
                {
                    bool flag = false;
                    string where = string.Empty;
                    switch(typeID)
                    {
                        case 1:
                            int gameid = CtrlHelper.GetInt(txtGameID, 0);
                            if(gameid != 0)
                            {
                                AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByGameId(gameid);
                                if(info == null)
                                {
                                    MessageBox("推送消息失败，代理商游戏id不存在");
                                    return;
                                }
                                if(info.AgentID <= 0)
                                {
                                    MessageBox("推送消息失败，游戏id为非代理商");
                                    return;
                                }
                                where = "WHERE UserID IN(SELECT UserID FROM AccountsInfo WHERE SpreaderID=" + info.UserID.ToString() + ")";
                            }
                            else
                            {
                                where = "WHERE UserID IN(SELECT UserID FROM AccountsInfo WHERE AgentID>0)";
                            }
                            break;
                        case 2:
                            where = "WHERE UserID IN(SELECT UserID FROM AccountsInfo WHERE AgentID=0)";
                            break;
                        case 3:
                            where = "WHERE DeviceType=0";
                            break;
                        case 4:
                            where = "WHERE DeviceType=1";
                            break;
                        case 5:
                            string start = CtrlHelper.GetText(txtStartDate);
                            string end = CtrlHelper.GetText(txtEndDate);
                            if(!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                            {
                                where = "WHERE UserID IN(SELECT UserID FROM AccountsInfo WHERE RegisterDate BETWEEN '" + start + "' AND '" + end + "')";
                            }
                            else
                            {
                                where = "WHERE 1=1";
                            }
                            break;
                        case 6:
                            int nologin = CtrlHelper.GetInt(txtNoLoginDay, 0);
                            if(nologin > 0)
                            {
                                where = "WHERE DATEDIFF(DAY,UpdateTime,GETDATE())>=" + nologin.ToString();
                            }
                            else
                            {
                                where = "WHERE 1=1";
                            }
                            break;
                        default:
                            break;
                    }
                    list = FacadeManage.aideAccountsFacade.GetAccountsUmengList(where);
                    if(list == null || list.Count <= 0)
                    {
                        MessageBox("推送用户未绑定设备，无法推送");
                        return;
                    }

                    //获取安卓用户
                    IList<AccountsUmeng> android = list.Where(a => a.DeviceType == 0).ToList<AccountsUmeng>();
                    if(android != null && android.Count > 0)
                    {
                        StringBuilder android_sb = new StringBuilder();
                        int i = 1, j = 1;
                        string android_tokens = string.Empty;
                        foreach(AccountsUmeng item in android)
                        {
                            if(!string.IsNullOrEmpty(item.DeviceToken))
                            {
                                android_sb.AppendFormat("{0},", item.DeviceToken);
                            }

                            if(i == 400 || j == android.Count)
                            {
                                android_tokens = android_sb.ToString();
                                android_tokens = android_tokens.Substring(0, (android_tokens.Length - 1));
                                flag = Umeng.SendMessage(0, content, "listcast", time.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"), android_tokens);
                                if(!flag)
                                {
                                    MessageBox("推送消息失败，请前往友盟后台绑定系统后台ip");
                                    return;
                                }
                                i = 0;
                                android_sb = new StringBuilder();
                                android_tokens = string.Empty;
                            }
                            i++;
                            j++;
                        }
                    }

                    //获取苹果用户
                    IList<AccountsUmeng> iphone = list.Where(a => a.DeviceType == 1).ToList<AccountsUmeng>();
                    if(iphone != null & iphone.Count > 0)
                    {
                        StringBuilder iphone_sb = new StringBuilder();
                        int i = 1, j = 1;
                        string iphone_tokens = string.Empty;
                        foreach(AccountsUmeng item in iphone)
                        {
                            if(!string.IsNullOrEmpty(item.DeviceToken))
                            {
                                iphone_sb.AppendFormat("{0},", item.DeviceToken);
                            }

                            if(i == 500 || j == iphone.Count)
                            {
                                iphone_tokens = iphone_sb.ToString();
                                iphone_tokens = iphone_tokens.Substring(0, (iphone_tokens.Length - 1));
                                flag = Umeng.SendMessage(1, content, "listcast", time.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"), iphone_tokens);
                                if(!flag)
                                {
                                    MessageBox("推送消息失败，请前往友盟后台绑定系统后台ip");
                                    return;
                                }
                                i = 0;
                                iphone_sb = new StringBuilder();
                                iphone_tokens = string.Empty;
                            }
                            i++;
                            j++;
                        }
                    }
                }

                //批量写入记录
                DataTable table = new DataTable();
                table.Columns.AddRange(new DataColumn[] {
                        new DataColumn("RecordID",typeof(int)),
                        new DataColumn("MasterID",typeof(int)),
                        new DataColumn("UserID",typeof(int)),
                        new DataColumn("PushType",typeof(byte)),
                        new DataColumn("PushContent",typeof(string)),
                        new DataColumn("PushTime",typeof(DateTime)),
                        new DataColumn("PushIP",typeof(string))
                    });
                int masterid = userExt.UserID;
                DateTime addTime = DateTime.Now;
                string ip = GameRequest.GetUserIP();
                string connStr = ApplicationSettings.Get("DBRecord");
                for(int i = 0; i < list.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = 0;
                    dr[1] = masterid;
                    dr[2] = list[i].UserID;
                    dr[3] = list[i].DeviceType;
                    dr[4] = content;
                    dr[5] = addTime;
                    dr[6] = ip;
                    table.Rows.Add(dr);
                }
                int result = FacadeManage.aideRecordFacade.AddRecordAccountsUmeng(table, connStr);
                if(result > 0)
                {
                    MessageBox("推送消息成功");
                }
                else
                {
                    MessageBox("推送消息成功，但推送记录写入失败");
                }
            }
            catch(Exception)
            {
                MessageBox("推送消息异常，请稍后重试");
            }
        }
        /// <summary>
        /// 选择类型
        /// </summary>
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeID = Convert.ToInt32(ddlType.SelectedValue);
            if(typeID == 1)
            {
                gameid.Visible = true;
                time.Visible = false;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                logincount.Visible = false;
                txtNoLoginDay.Text = "0";
            }
            else if(typeID == 5)
            {
                gameid.Visible = false;
                txtGameID.Text = "0";
                time.Visible = true;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                logincount.Visible = false;
                txtNoLoginDay.Text = "0";
            }
            else if(typeID == 6)
            {
                gameid.Visible = false;
                txtGameID.Text = "0";
                time.Visible = false;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                logincount.Visible = true;
                txtNoLoginDay.Text = "0";
            }
            else
            {
                gameid.Visible = false;
                txtGameID.Text = "0";
                time.Visible = false;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                logincount.Visible = false;
                txtNoLoginDay.Text = "0";
            }
        }
    }
}
