using System;
// ReSharper disable InconsistentNaming

namespace Game.Facade.DataStruct
{
    public class MobileSystemConfig
    {
        public int IsOpenMall { get; set; }
        public int IsPayBindSpread { get; set; }
        public int BindSpreadPresent { get; set; }
        public int RankingListType { get; set; }
        public int PayChannel { get; set; }
        public int DiamondBuyPropCount { get; set; }
        public int RealNameAuthentPresent { get; set; }
        public int EffectiveFriendGame { get; set; }
        public int IOSNotStorePaySwitch { get; set; }
        public int GoldBuyPropCount { get; set; }
        public int EnjoinInsure { get; set; }
        public int TransferStauts { get; set; }
    }

    public class MobileCustomerService
    {
        public string Phone { get; set; }
        public string WeiXin { get; set; }
        public string QQ { get; set; }
        public string Link { get; set; }
    }

    public class AdsMobile
    {
        public string ResourceURL { get; set; }
        public string LinkURL { get; set; }
        public int SortID { get; set; }
    }

    public class NoticeMobile
    {
        public int NoticeID { get; set; }
        public string NoticeTitle { get; set; }
        public string MoblieContent { get; set; }
        public DateTime PublisherTime { get; set; }
    }

    public class SpreadConfigMobile
    {
        public int ConfigID { get; set; }
        public int SpreadNum { get; set; }
        public int PresentDiamond { get; set; }
        public int PresentPropID { get; set; }
        public string PresentPropName { get; set; }
        public int PresentPropNum { get; set; }
        public bool Flag { get; set; }
    }

    public class RankingRecevieMobile
    {
        public int DateID { get; set; }
        public int UserID { get; set; }
        public int GameID { get; set; }
        public string NickName { get; set; }
        public int SystemFaceID { get; set; }
        public string FaceUrl { get; set; }
        public int TypeID { get; set; }
        public int RankID { get; set; }
        public long RankValue { get; set; }
        public int Diamond { get; set; }
    }

    public class AppPayConfigMoile
    {
        public int ConfigID { get; set; }
        public string AppleID { get; set; }
        public string PayName { get; set; }
        public int PayType { get; set; }
        public decimal PayPrice { get; set; }
        public int PayIdentity { get; set; }
        public int ImageType { get; set; }
        public int SortID { get; set; }
        public int Diamond { get; set; }
        public decimal PresentScale { get; set; } = 0;
        public int PresentDiamond { get; set; }
    }

    public class TreasureStream
    {
        public string SerialNumber { get; set; }
        public string SerialTime { get; set; }
        public long BeforeGold { get; set; }
        public int ChangeGold { get; set; }
        public long AfterGold { get; set; }
        public string TypeName { get; set; }
    }

    public class DiamondStream
    {
        public string SerialNumber { get; set; }
        public string SerialTime { get; set; }
        public long BeforeDiamond { get; set; }
        public int ChangeDiamond { get; set; }
        public long AfterDiamond { get; set; }
        public string TypeName { get; set; }
    }

    public class DiamondExchRecord
    {
        /// <summary>
        /// 兑换后钻石
        /// </summary>
        public long AfterDiamond { get; set; }
        /// <summary>
        /// 兑换后银行金币
        /// </summary>
        public long AfterInsureScore { get; set; }
        /// <summary>
        /// 兑换后身上金币
        /// </summary>
        public long AfterScore { get; set; }
        /// <summary>
        /// 消耗钻石
        /// </summary>
        public long ExchDiamond { get; set; }
        /// <summary>
        /// 兑换金币
        /// </summary>
        public long PresentGold { get; set; }
    }

    /// <summary>
    /// 手机游戏玩法结构体
    /// </summary>
    public class MobileGameRule
    {
        /// <summary>
        /// 游戏标识
        /// </summary>
        public int KindID { get; set; }

        /// <summary>
        /// 游戏名称
        /// </summary>
        public string KindName { get; set; }

        /// <summary>
        /// 游戏简介
        /// </summary>
        public string Content { get; set; }
    }
}
