using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Utils;

namespace Game.Web.Card
{
    public partial class Under : AdminPage
    {
        public string Type = GameRequest.GetQueryString("type");

        protected void Page_Load(object sender, EventArgs e)
        {
            Card.Site.Menu = 0;
        }
    }
}