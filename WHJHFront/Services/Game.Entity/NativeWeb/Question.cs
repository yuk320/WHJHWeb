/*
 * 版本：4.0
 * 时间：2018/1/25
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
// ReSharper disable InconsistentNaming

namespace Game.Entity.NativeWeb
{
    /// <summary>
    /// 实体类 Question。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Question
    {
        #region 常量

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "Question";

        /// <summary>
        /// 问答标识
        /// </summary>
        public const string _ID = "ID";

        /// <summary>
        /// 问题
        /// </summary>
        public const string _QuestionTitle = "QuestionTitle";

        /// <summary>
        /// 答案
        /// </summary>
        public const string _Answer = "Answer";

        /// <summary>
        /// 
        /// </summary>
        public const string _SortID = "SortID";

        /// <summary>
        /// 更新时间
        /// </summary>
        public const string _UpdateAt = "UpdateAt";
        #endregion

        #region 私有变量
        private int m_iD;                   //问答标识
        private string m_questionTitle;         //问题
        private string m_answer;            //答案
        private int m_sortID;               //
        private DateTime m_updateAt;        //更新时间
        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化Question
        /// </summary>
        public Question()
        {
            m_iD = 0;
            m_questionTitle = "";
            m_answer = "";
            m_sortID = 0;
            m_updateAt = DateTime.Now;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 问答标识
        /// </summary>
        public int ID
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        /// <summary>
        /// 问题
        /// </summary>
        public string QuestionTitle
        {
            get { return m_questionTitle; }
            set { m_questionTitle = value; }
        }

        /// <summary>
        /// 答案
        /// </summary>
        public string Answer
        {
            get { return m_answer; }
            set { m_answer = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SortID
        {
            get { return m_sortID; }
            set { m_sortID = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt
        {
            get { return m_updateAt; }
            set { m_updateAt = value; }
        }
        #endregion
    }

}
