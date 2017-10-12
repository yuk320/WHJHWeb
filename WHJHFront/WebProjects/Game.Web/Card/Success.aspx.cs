using Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.Card
{
    public partial class Success : System.Web.UI.Page
    {
        protected string TipInfo = string.Empty;
        protected string LinkInfo = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int typeid = GameRequest.GetQueryInt("t", 0);
                switch(typeid)
                {
                    case 1001:
                        TipInfo = "恭喜您，代理信息新增成功";
                        LinkInfo = "<a href=\"/Card/AgentInfo.aspx\">返回首页</a>&nbsp;<a href=\"/Card/AddAgent.aspx\">继续新增</a>";
                        break;
                    case 1002:
                        TipInfo = "恭喜您，代理信息修改成功";
                        LinkInfo = "<a href=\"/Card/AgentInfo.aspx\">返回首页</a>&nbsp;<a href=\"/Card/UpdateAgent.aspx\">继续修改</a>";
                        break;
                    case 1003:
                        TipInfo = "恭喜您，代理密码修改成功";
                        LinkInfo = "<a href=\"/Card/AgentInfo.aspx\">返回首页</a>&nbsp;<a href=\"/Card/UpdatePass.aspx\">继续修改</a>";
                        break;
                    case 1004:
                        TipInfo = "恭喜您，赠送钻石成功";
                        LinkInfo = "<a href=\"/Card/AgentInfo.aspx\">返回首页</a>&nbsp;<a href=\"/Card/Present.aspx\">继续赠送</a>";
                        break;
                    case 1005:
                        TipInfo = "恭喜您，代理下线新增成功";
                        LinkInfo = "<a href=\"/Card/AgentInfo.aspx\">返回首页</a>&nbsp;<a href=\"/Card/AddUser.aspx\">继续新增</a>";
                        break;
                    default:
                        TipInfo = "恭喜您，操作成功！";
                        break;
                }
            }
        }
    }
}