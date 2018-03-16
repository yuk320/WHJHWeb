/*
 * 版本：4.0
 * 时间：2018/3/16
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
	/// 实体类 AgentTokenInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class AgentTokenInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AgentTokenInfo" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 代理标识
		/// </summary>
		public const string _AgentID = "AgentID" ;

		/// <summary>
		/// 认证串（SHA256）
		/// </summary>
		public const string _Token = "Token" ;

		/// <summary>
		/// 过期时间
		/// </summary>
		public const string _ExpirtAt = "ExpirtAt" ;
		#endregion

		#region 私有变量
		private int m_userID;				//用户标识
		private int m_agentID;				//代理标识
		private string m_token;				//认证串（SHA256）
		private DateTime m_expirtAt;		//过期时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AgentTokenInfo
		/// </summary>
		public AgentTokenInfo()
		{
			m_userID=0;
			m_agentID=0;
			m_token="";
			m_expirtAt=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 代理标识
		/// </summary>
		public int AgentID
		{
			get { return m_agentID; }
			set { m_agentID = value; }
		}

		/// <summary>
		/// 认证串（SHA256）
		/// </summary>
		public string Token
		{
			get { return m_token; }
			set { m_token = value; }
		}

		/// <summary>
		/// 过期时间
		/// </summary>
		public DateTime ExpirtAt
		{
			get { return m_expirtAt; }
			set { m_expirtAt = value; }
		}
		#endregion
	}
}
