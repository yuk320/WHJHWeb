/*
 * 版本：4.0
 * 时间：2017/11/15
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;

namespace Game.Entity.Record
{
	/// <summary>
	/// 实体类 RecordSpreadReturnReceive。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordSpreadReturnReceive  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordSpreadReturnReceive" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 领取类型（0：金币、1：钻石）
		/// </summary>
		public const string _ReceiveType = "ReceiveType" ;

		/// <summary>
		/// 领取数值（根据ReceiveType 0：金币 1：钻石）
		/// </summary>
		public const string _ReceiveNum = "ReceiveNum" ;

		/// <summary>
		/// 领取前数值（根据ReceiveType 0：金币 1：钻石）
		/// </summary>
		public const string _ReceiveBefore = "ReceiveBefore" ;

		/// <summary>
		/// 领取地址
		/// </summary>
		public const string _ReceiveAddress = "ReceiveAddress" ;

		/// <summary>
		/// 记录时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;					//记录标识
		private int m_userID;					//用户标识
		private byte m_receiveType;				//领取类型（0：金币、1：钻石）
		private int m_receiveNum;				//领取数值（根据ReceiveType 0：金币 1：钻石）
		private long m_receiveBefore;			//领取前数值（根据ReceiveType 0：金币 1：钻石）
		private string m_receiveAddress;		//领取地址
		private DateTime m_collectDate;			//记录时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordSpreadReturnReceive
		/// </summary>
		public RecordSpreadReturnReceive()
		{
			m_recordID=0;
			m_userID=0;
			m_receiveType=0;
			m_receiveNum=0;
			m_receiveBefore=0;
			m_receiveAddress="";
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
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 领取类型（0：金币、1：钻石）
		/// </summary>
		public byte ReceiveType
		{
			get { return m_receiveType; }
			set { m_receiveType = value; }
		}

		/// <summary>
		/// 领取数值（根据ReceiveType 0：金币 1：钻石）
		/// </summary>
		public int ReceiveNum
		{
			get { return m_receiveNum; }
			set { m_receiveNum = value; }
		}

		/// <summary>
		/// 领取前数值（根据ReceiveType 0：金币 1：钻石）
		/// </summary>
		public long ReceiveBefore
		{
			get { return m_receiveBefore; }
			set { m_receiveBefore = value; }
		}

		/// <summary>
		/// 领取地址
		/// </summary>
		public string ReceiveAddress
		{
			get { return m_receiveAddress; }
			set { m_receiveAddress = value; }
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
