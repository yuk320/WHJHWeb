using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ReSharper disable InconsistentNaming

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

        public string FaceUrl { get; set; }

        /// <summary>
        /// 虚拟属性
        /// </summary>
        public bool IsHasPassword { get; set; }
    }

    public class PayDiamondRecord
    {
        public string PayDate { get; set; }
        public long BeforeDiamond { get; set; }
        public long PayDiamond { get; set; }
        public decimal Amount { get; set; }
    }

    public class PresentDiamondRecord
    {
        public string  CollectDate { get; set; }
        public int GameID { get; set; }
        public long PresentDiamond { get; set; }
        public long SourceDiamond { get; set; }
        public string CollectNote { get; set; }
    }

    public class CostDiamondRecord
    {
        public string CreateDate { get; set; }
        public int RoomID { get; set; }
        public long CreateTableFee { get; set; }
        public string DissumeDate { get; set; }
    }

    public class ExchGoldRecord
    {
        public string CollectDate { get; set; }
        public long PresentGold { get; set; }
        public long ExchDiamond { get; set; }
        public long CurDiamond { get; set; }
    }

    public class SpreadRegsiterRecord
    {
        public string RegisterDate { get; set; }
        public int GameID { get; set; }
        public string RegisterOrigin { get; set; }
        public string AgentState { get; set; }
    }
}
