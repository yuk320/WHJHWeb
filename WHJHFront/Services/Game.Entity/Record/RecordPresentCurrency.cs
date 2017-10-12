/*
 * 版本：4.0
 * 时间：2017/9/22
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
	/// <summary>
	/// 实体类 RecordPresentCurrency。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordPresentCurrency
	{
		#region 常量

	    /// <summary>
	    /// 表名
	    /// </summary>
	    public const string TableName = "RecordPresentCurrency";

	    /// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 赠送者ID
		/// </summary>
		public const string _SourceUserID = "SourceUserID" ;

		/// <summary>
		/// 赠送者钻石数
		/// </summary>
		public const string _SourceDiamond = "SourceDiamond" ;

		/// <summary>
		/// 接收者ID
		/// </summary>
		public const string _TargetUserID = "TargetUserID" ;

		/// <summary>
		/// 接收者钻石数
		/// </summary>
		public const string _TargetDiamond = "TargetDiamond" ;

		/// <summary>
		/// 目标账号代理级别（非代理则为0）
		/// </summary>
		public const string _TargetAgentLevel = "TargetAgentLevel" ;

		/// <summary>
		/// 赠送钻石
		/// </summary>
		public const string _PresentDiamond = "PresentDiamond" ;

		/// <summary>
		/// 赠送地址
		/// </summary>
		public const string _ClientIP = "ClientIP" ;

		/// <summary>
		/// 赠送时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;

		/// <summary>
		/// 赠送备注
		/// </summary>
		public const string _CollectNote = "CollectNote" ;
		#endregion

		#region 私有变量
		private int m_recordID;					//记录标识
		private int m_sourceUserID;				//赠送者ID
		private long m_sourceDiamond;			//赠送者钻石数
		private int m_targetUserID;				//接收者ID
		private long m_targetDiamond;			//接收者钻石数
		private byte m_targetAgentLevel;		//目标账号代理级别（非代理则为0）
		private int m_presentDiamond;			//赠送钻石
		private string m_clientIP;				//赠送地址
		private DateTime m_collectDate;			//赠送时间
		private string m_collectNote;			//赠送备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordPresentCurrency
		/// </summary>
		public RecordPresentCurrency()
		{
			m_recordID=0;
			m_sourceUserID=0;
			m_sourceDiamond=0;
			m_targetUserID=0;
			m_targetDiamond=0;
			m_targetAgentLevel=0;
			m_presentDiamond=0;
			m_clientIP="";
			m_collectDate=DateTime.Now;
			m_collectNote="";
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
		/// 赠送者ID
		/// </summary>
		public int SourceUserID
		{
			get { return m_sourceUserID; }
			set { m_sourceUserID = value; }
		}

		/// <summary>
		/// 赠送者钻石数
		/// </summary>
		public long SourceDiamond
		{
			get { return m_sourceDiamond; }
			set { m_sourceDiamond = value; }
		}

		/// <summary>
		/// 接收者ID
		/// </summary>
		public int TargetUserID
		{
			get { return m_targetUserID; }
			set { m_targetUserID = value; }
		}

		/// <summary>
		/// 接收者钻石数
		/// </summary>
		public long TargetDiamond
		{
			get { return m_targetDiamond; }
			set { m_targetDiamond = value; }
		}

		/// <summary>
		/// 目标账号代理级别（非代理则为0）
		/// </summary>
		public byte TargetAgentLevel
		{
			get { return m_targetAgentLevel; }
			set { m_targetAgentLevel = value; }
		}

		/// <summary>
		/// 赠送钻石
		/// </summary>
		public int PresentDiamond
		{
			get { return m_presentDiamond; }
			set { m_presentDiamond = value; }
		}

		/// <summary>
		/// 赠送地址
		/// </summary>
		public string ClientIP
		{
			get { return m_clientIP; }
			set { m_clientIP = value; }
		}

		/// <summary>
		/// 赠送时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}

		/// <summary>
		/// 赠送备注
		/// </summary>
		public string CollectNote
		{
			get { return m_collectNote; }
			set { m_collectNote = value; }
		}
		#endregion
	}
}
