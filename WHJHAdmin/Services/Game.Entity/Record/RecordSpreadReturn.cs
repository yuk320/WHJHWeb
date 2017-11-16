/*
 * 版本：4.0
 * 时间：2017/11/14
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;

namespace Game.Entity.Record
{
	/// <summary>
	/// 实体类 RecordSpreadReturn。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordSpreadReturn  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordSpreadReturn" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 充值对象
		/// </summary>
		public const string _SourceUserID = "SourceUserID" ;

		/// <summary>
		/// 返利对象
		/// </summary>
		public const string _TargetUserID = "TargetUserID" ;

		/// <summary>
		/// 充值所得钻石
		/// </summary>
		public const string _SourceDiamond = "SourceDiamond" ;

		/// <summary>
		/// 返利前数值（根据ReturnType 0：金币 1：钻石）
		/// </summary>
		public const string _TargetBefore = "TargetBefore" ;

		/// <summary>
		/// 返利配置推广级别（目前仅支持3级）
		/// </summary>
		public const string _SpreadLevel = "SpreadLevel" ;

		/// <summary>
		/// 返利比例
		/// </summary>
		public const string _ReturnScale = "ReturnScale" ;

		/// <summary>
		/// 返利数值 （根据ReturnType 0：金币 1：钻石）
		/// </summary>
		public const string _ReturnNum = "ReturnNum" ;

		/// <summary>
		/// 返利类型（0：金币、1：钻石）
		/// </summary>
		public const string _ReturnType = "ReturnType" ;

		/// <summary>
		/// 记录时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;				//记录标识
		private int m_sourceUserID;			//充值对象
		private int m_targetUserID;			//返利对象
		private int m_sourceDiamond;		//充值所得钻石
		private long m_targetBefore;		//返利前数值（根据ReturnType 0：金币 1：钻石）
		private int m_spreadLevel;			//返利配置推广级别（目前仅支持3级）
		private decimal m_returnScale;		//返利比例
		private int m_returnNum;			//返利数值 （根据ReturnType 0：金币 1：钻石）
		private byte m_returnType;			//返利类型（0：金币、1：钻石）
		private DateTime m_collectDate;		//记录时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordSpreadReturn
		/// </summary>
		public RecordSpreadReturn()
		{
			m_recordID=0;
			m_sourceUserID=0;
			m_targetUserID=0;
			m_sourceDiamond=0;
			m_targetBefore=0;
			m_spreadLevel=0;
			m_returnScale=0;
			m_returnNum=0;
			m_returnType=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 记录标识
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
		}

		/// <summary>
		/// 充值对象
		/// </summary>
		public int SourceUserID
		{
			get { return m_sourceUserID; }
			set { m_sourceUserID = value; }
		}

		/// <summary>
		/// 返利对象
		/// </summary>
		public int TargetUserID
		{
			get { return m_targetUserID; }
			set { m_targetUserID = value; }
		}

		/// <summary>
		/// 充值所得钻石
		/// </summary>
		public int SourceDiamond
		{
			get { return m_sourceDiamond; }
			set { m_sourceDiamond = value; }
		}

		/// <summary>
		/// 返利前数值（根据ReturnType 0：金币 1：钻石）
		/// </summary>
		public long TargetBefore
		{
			get { return m_targetBefore; }
			set { m_targetBefore = value; }
		}

		/// <summary>
		/// 返利配置推广级别（目前仅支持3级）
		/// </summary>
		public int SpreadLevel
		{
			get { return m_spreadLevel; }
			set { m_spreadLevel = value; }
		}

		/// <summary>
		/// 返利比例
		/// </summary>
		public decimal ReturnScale
		{
			get { return m_returnScale; }
			set { m_returnScale = value; }
		}

		/// <summary>
		/// 返利数值 （根据ReturnType 0：金币 1：钻石）
		/// </summary>
		public int ReturnNum
		{
			get { return m_returnNum; }
			set { m_returnNum = value; }
		}

		/// <summary>
		/// 返利类型（0：金币、1：钻石）
		/// </summary>
		public byte ReturnType
		{
			get { return m_returnType; }
			set { m_returnType = value; }
		}

		/// <summary>
		/// 记录时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
