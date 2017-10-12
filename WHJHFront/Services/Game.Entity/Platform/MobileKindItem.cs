/*
 * 版本：4.0
 * 时间：2011-8-1
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
    /// <summary>
    /// 实体类 MobileKindItem。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class MobileKindItem
    {
        #region 常量

        /// <summary>
        /// 表名
        /// </summary>
        public const string Tablename = "MobileKindItem";

        /// <summary>
        /// 游戏标识
        /// </summary>
        public const string _KindID = "KindID";

        /// <summary>
        /// 游戏名称
        /// </summary>
        public const string _KindName = "KindName";

        /// <summary>
        /// 类型标识
        /// </summary>
        public const string _TypeID = "TypeID";

        /// <summary>
        /// 模块名称
        /// </summary>
        public const string _ModuleName = "ModuleName";

        /// <summary>
        /// 模块版本号
        /// </summary>
        public const string _ClientVersion = "ClientVersion";

        /// <summary>
        /// 资源版本号
        /// </summary>
        public const string _ResVersion = "ResVersion";

        /// <summary>
        /// 排序标识
        /// </summary>
        public const string _SortID = "SortID";

        /// <summary>
        /// 掩码值
        /// </summary>
        public const string _KindMark = "KindMark";

        /// <summary>
        /// 无效标志
        /// </summary>
        public const string _Nullity = "Nullity";
        #endregion

        #region 私有变量
        private int m_kindID;					//类型标识
        private string m_kindName;				//游戏名称
        private int m_typeID;                   //类型标识
        private string m_moduleName;            //模块名称
        private int m_clientVersion;			//模块版本号
        private int m_resVersion;               //资源版本号
        private int m_sortID;					//排序标识
        private int m_kindMark;				    //掩码值
        private byte m_nullity;					//无效标志
        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化MobileKindItem
        /// </summary>
        public MobileKindItem()
        {
            m_kindID = 0;
            m_kindName = "";
            m_typeID = 0;
            m_moduleName = "";
            m_clientVersion = 0;
            m_resVersion = 0;
            m_sortID = 0;
            m_kindMark = 0;
            m_nullity = 0;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 类型标识
        /// </summary>
        public int KindID
        {
            get { return m_kindID; }
            set { m_kindID = value; }
        }

        /// <summary>
        /// 游戏名称
        /// </summary>
        public string KindName
        {
            get { return m_kindName; }
            set { m_kindName = value; }
        }

        /// <summary>
        /// 类型标识
        /// </summary>
        public int TypeID
        {
            get { return m_typeID; }
            set { m_typeID = value; }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get { return m_moduleName; }
            set { m_moduleName = value; }
        }

        /// <summary>
        /// 模版版本号
        /// </summary>
        public int ClientVersion
        {
            get { return m_clientVersion; }
            set { m_clientVersion = value; }
        }

        /// <summary>
        /// 资源版本号
        /// </summary>
        public int ResVersion
        {
            get { return m_resVersion; }
            set { m_resVersion = value; }
        }

        /// <summary>
        /// 排序标识
        /// </summary>
        public int SortID
        {
            get { return m_sortID; }
            set { m_sortID = value; }
        }

        /// <summary>
        /// 掩码
        /// </summary>
        public int KindMark
        {
            get { return m_kindMark; }
            set { m_kindMark = value; }
        }

        /// <summary>
        /// 无效标志
        /// </summary>
        public byte Nullity
        {
            get { return m_nullity; }
            set { m_nullity = value; }
        }
        #endregion
    }
}
