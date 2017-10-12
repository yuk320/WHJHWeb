/*
 * 版本： 4.0
 * 日期：2017/8/7 10:51:24
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
    /// <summary>
    /// 实体类 RecordDrawScore  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class RecordDrawScore
    {
        #region 常量 

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "RecordDrawScore";

        #endregion 

        #region 私有变量

        private int p_drawid;
        private int p_userid;
        private int p_chairid;
        private Int64 p_score;
        private Int64 p_grade;
        private Int64 p_revenue;
        private int p_playtimecount;
        private int p_dbquestid;
        private int p_inoutindex;
        private DateTime p_inserttime;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 初始化RecordDrawScore
        /// </summary>
        public RecordDrawScore() 
        {
            p_drawid = 0;
            p_userid = 0;
            p_chairid = 0;
            p_score = 0;
            p_grade = 0;
            p_revenue = 0;
            p_playtimecount = 0;
            p_dbquestid = 0;
            p_inoutindex = 0;
            p_inserttime = DateTime.Now;
        }

        #endregion

        #region 公共属性 

        /// <summary>
        /// DrawID
        /// </summary>
        public int DrawID
        {
            set
            {
                p_drawid=value;
            }
            get
            {
                return p_drawid;
            }
        }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            set
            {
                p_userid=value;
            }
            get
            {
                return p_userid;
            }
        }

        /// <summary>
        /// ChairID
        /// </summary>
        public int ChairID
        {
            set
            {
                p_chairid=value;
            }
            get
            {
                return p_chairid;
            }
        }

        /// <summary>
        /// Score
        /// </summary>
        public Int64 Score
        {
            set
            {
                p_score=value;
            }
            get
            {
                return p_score;
            }
        }

        /// <summary>
        /// Grade
        /// </summary>
        public Int64 Grade
        {
            set
            {
                p_grade=value;
            }
            get
            {
                return p_grade;
            }
        }

        /// <summary>
        /// Revenue
        /// </summary>
        public Int64 Revenue
        {
            set
            {
                p_revenue=value;
            }
            get
            {
                return p_revenue;
            }
        }

        /// <summary>
        /// PlayTimeCount
        /// </summary>
        public int PlayTimeCount
        {
            set
            {
                p_playtimecount=value;
            }
            get
            {
                return p_playtimecount;
            }
        }

        /// <summary>
        /// DBQuestID
        /// </summary>
        public int DBQuestID
        {
            set
            {
                p_dbquestid=value;
            }
            get
            {
                return p_dbquestid;
            }
        }

        /// <summary>
        /// InoutIndex
        /// </summary>
        public int InoutIndex
        {
            set
            {
                p_inoutindex=value;
            }
            get
            {
                return p_inoutindex;
            }
        }

        /// <summary>
        /// InsertTime
        /// </summary>
        public DateTime InsertTime
        {
            set
            {
                p_inserttime=value;
            }
            get
            {
                return p_inserttime;
            }
        }

        #endregion
    }
}

