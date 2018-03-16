using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.Card.DataStruct
{
    public class AgentInfo
    {
        public int UserID { get; set; }
        public int GameID { get; set; }
        public int AgentID { get; set; }
        public string NickName { get; set; }
        public string Compellation { get; set; }
        public string AgentLevel { get; set; }
        public string AgentDomain { get; set; }
        public int MyAgent { get; set; }
        public long MyPlayer { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string QQAccount { get; set; }
        public string WCNickName { get; set; }
        public long CurDiamond { get; set; }
        public long PresentToday { get; set; }
        public long PresentMonth { get; set; }
        public long PresentTotal { get; set; }

        public bool IsHasPassword { get; set; }
    }
}
