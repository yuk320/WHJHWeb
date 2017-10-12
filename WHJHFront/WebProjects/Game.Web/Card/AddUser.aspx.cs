using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Card
{
    public partial class AddUser : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 0;
        }

        protected void btnBind_Click(object sender, EventArgs e)
        {
            int gameid = CtrlHelper.GetInt(txtGameID, 0);
            if(gameid <= 0)
            {
                ShowInfo("抱歉，添加下线游戏ID不正确");
                return;
            }

            Message msg = FacadeManage.aideAccountsFacade.InsertAgentSpread(userTicket.UserID, gameid);
            if(msg.Success)
            {
                Response.Redirect("/Card/Success.aspx?t=1005");
            }
            else
            {
                ShowInfo(msg.Content);
            }
        }
    }
}