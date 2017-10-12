using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Entity.Treasure;

namespace Game.Web.Card
{
    public partial class AgentChildList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 0;
            if(!IsPostBack)
            {
                
            }
        }
    }
}