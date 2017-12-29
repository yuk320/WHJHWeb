/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:43
 *
 * 描述：实体类
 *
 */

using System;
// ReSharper disable InconsistentNaming

namespace Game.Entity.Platform
{
    /// <summary>
    /// 实体类 GameProperty  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class GameProperty
    {
        #region 常量

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "GameProperty";

        #endregion

        #region 私有变量

        private int p_id;
        private string p_name;
        private int p_kind;
        private int p_exchangeratio;
        // private int p_exchangegoldratio;
        private byte p_usearea;
        private byte p_servicearea;
        private long p_buyresultsgold;
        private long p_sendloveliness;
        private long p_recvloveliness;
        private long p_useresultsgold;
        private int p_useresultsvalidtime;
        private int p_useresultsvalidtimescoremultiple;
        private int p_useresultsgiftpackage;
        private string p_regulationsinfo;
        private byte p_recommend;
        private int p_sortid;
        private byte p_nullity;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化GameProperty
        /// </summary>
        public GameProperty()
        {
            p_id = 0;
            p_name = string.Empty;
            p_kind = 0;
            p_exchangeratio = 0;
            // p_exchangegoldratio = 0;
            p_usearea = 0;
            p_servicearea = 0;
            p_buyresultsgold = 0;
            p_sendloveliness = 0;
            p_recvloveliness = 0;
            p_useresultsgold = 0;
            p_useresultsvalidtime = 0;
            p_useresultsvalidtimescoremultiple = 0;
            p_useresultsgiftpackage = 0;
            p_regulationsinfo = string.Empty;
            p_recommend = 0;
            p_sortid = 0;
            p_nullity = 0;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set
            {
                p_id=value;
            }
            get
            {
                return p_id;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            set
            {
                p_name=value;
            }
            get
            {
                return p_name;
            }
        }

        /// <summary>
        /// Kind
        /// </summary>
        public int Kind
        {
            set
            {
                p_kind=value;
            }
            get
            {
                return p_kind;
            }
        }

        /// <summary>
        /// ExchangeDiamondRatio
        /// </summary>
        public int ExchangeRatio
        {
            set
            {
                p_exchangeratio=value;
            }
            get
            {
                return p_exchangeratio;
            }
        }

        /// <summary>
        /// ExchangeGoldRatio
        /// </summary>
        // public int ExchangeGoldRatio
        // {
        //     set
        //     {
        //         p_exchangegoldratio = value;
        //     }
        //     get
        //     {
        //         return p_exchangegoldratio;
        //     }
        // }

        /// <summary>
        /// UseArea
        /// </summary>
        public byte UseArea
        {
            set
            {
                p_usearea=value;
            }
            get
            {
                return p_usearea;
            }
        }

        /// <summary>
        /// ServiceArea
        /// </summary>
        public byte ServiceArea
        {
            set
            {
                p_servicearea=value;
            }
            get
            {
                return p_servicearea;
            }
        }

        /// <summary>
        /// BuyResultsGold
        /// </summary>
        public long BuyResultsGold
        {
            set
            {
                p_buyresultsgold=value;
            }
            get
            {
                return p_buyresultsgold;
            }
        }

        /// <summary>
        /// SendLoveLiness
        /// </summary>
        public long SendLoveLiness
        {
            set
            {
                p_sendloveliness=value;
            }
            get
            {
                return p_sendloveliness;
            }
        }

        /// <summary>
        /// RecvLoveLiness
        /// </summary>
        public long RecvLoveLiness
        {
            set
            {
                p_recvloveliness=value;
            }
            get
            {
                return p_recvloveliness;
            }
        }

        /// <summary>
        /// UseResultsGold
        /// </summary>
        public long UseResultsGold
        {
            set
            {
                p_useresultsgold=value;
            }
            get
            {
                return p_useresultsgold;
            }
        }

        /// <summary>
        /// UseResultsValidTime
        /// </summary>
        public int UseResultsValidTime
        {
            set
            {
                p_useresultsvalidtime=value;
            }
            get
            {
                return p_useresultsvalidtime;
            }
        }

        /// <summary>
        /// UseResultsValidTimeScoreMultiple
        /// </summary>
        public int UseResultsValidTimeScoreMultiple
        {
            set
            {
                p_useresultsvalidtimescoremultiple=value;
            }
            get
            {
                return p_useresultsvalidtimescoremultiple;
            }
        }

        /// <summary>
        /// UseResultsGiftPackage
        /// </summary>
        public int UseResultsGiftPackage
        {
            set
            {
                p_useresultsgiftpackage=value;
            }
            get
            {
                return p_useresultsgiftpackage;
            }
        }

        /// <summary>
        /// RegulationsInfo
        /// </summary>
        public string RegulationsInfo
        {
            set
            {
                p_regulationsinfo=value;
            }
            get
            {
                return p_regulationsinfo;
            }
        }

        /// <summary>
        /// Recommend
        /// </summary>
        public byte Recommend
        {
            set
            {
                p_recommend=value;
            }
            get
            {
                return p_recommend;
            }
        }

        /// <summary>
        /// SortID
        /// </summary>
        public int SortID
        {
            set
            {
                p_sortid=value;
            }
            get
            {
                return p_sortid;
            }
        }

        /// <summary>
        /// Nullity
        /// </summary>
        public byte Nullity
        {
            set
            {
                p_nullity=value;
            }
            get
            {
                return p_nullity;
            }
        }

        #endregion
    }
}

