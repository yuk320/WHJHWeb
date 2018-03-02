using System;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Entity.Treasure;

namespace Game.Web.Card
{
    public partial class Present : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 3;
            if(!IsPostBack)
            {
                //获取登录信息
                UserTicketInfo user = userTicket;
                //身上钻石数
                UserCurrency currency = FacadeManage.aideTreasureFacade.GetUserCurrency(user.UserID);
                lbDiamond.Text = currency?.Diamond.ToString() ?? "0";
                //获取今日赠送钻石
                lbPresentDiamond.Text = FacadeManage.aideRecordFacade.GetTodayAgentPresentDiamond(user.UserID).ToString();
            }
        }

        /// <summary>
        /// 确定赠送
        /// </summary>
        protected void btnPresent_Click(object sender, EventArgs e)
        {
            UserTicketInfo user = userTicket;
            //验证是否设置过安全密码
            AccountsAgentInfo agent = FacadeManage.aideAccountsFacade.GetAccountsAgentInfoByAgentID(user.AgentID);
            if(agent == null)
            {
                ShowInfo("抱歉，你的账号不属于代理账号");
                return;
            }
            if(string.IsNullOrEmpty(agent.Password))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message",
                    "<script type='text/javascript'>layer.open({type: 1,content: '<form action=\"/Card/SetPassword.aspx\" method=\"post\" class=\"popForm\"><h3>为了您的账号安全，请先设置安全密码！</h3><p><span>安全密码：</span><em><input type=\"password\" id=\"password\" name=\"password\" /></em></p><p><span></span><em><input type=\"submit\" id=\"btnsave\" value=\"设置密码\" /></em></p></form>',anim: 'up',style: 'position:fixed; bottom:0; left:0; width: 100%; height: 200px; padding:10px 0; border:none;'});</script>");
                return;
            }

            //获取赠送数据
            int gameID = CtrlHelper.GetInt(txtGameID, 0);
            int presentCount = CtrlHelper.GetInt(txtPresentCount, 0);
            string password = CtrlHelper.GetText(txtPassword);
            string note = CtrlHelper.GetText(txtNote);

            //数据验证
            if(gameID <= 0)
            {
                ShowInfo("抱歉，赠送对象无效");
                return;
            }
            if(presentCount <= 0)
            {
                ShowInfo("抱歉，赠送钻石数需大于或等于零");
                return;
            }
            if(string.IsNullOrEmpty(password))
            {
                ShowInfo("抱歉，安全密码不能为空");
                return;
            }

            //执行赠送操作
            Message msg = FacadeManage.aideTreasureFacade.AgentPresentDiamond(user.UserID, TextEncrypt.EncryptPassword(password), presentCount, gameID, GameRequest.GetUserIP(), note);
            if(msg.Success)
            {
                Response.Redirect("/Card/Success.aspx?t=1004");
            }
            else
            {
                txtGameID.Text = "";
                ShowInfo(msg.Content);
            }
        }
    }
}