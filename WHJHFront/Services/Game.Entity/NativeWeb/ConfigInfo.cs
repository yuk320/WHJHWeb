/*
 * 版本：4.0
 * 时间：2014-3-20
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.NativeWeb
{
	/// <summary>
	/// 实体类 ConfigInfo。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ConfigInfo  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ConfigInfo" ;

		/// <summary>
		/// 配置标识
		/// </summary>
		public const string _ConfigID = "ConfigID" ;

		/// <summary>
		/// 配置KEY
		/// </summary>
		public const string _ConfigKey = "ConfigKey" ;

		/// <summary>
		/// 配置名称
		/// </summary>
		public const string _ConfigName = "ConfigName" ;

		/// <summary>
		/// 配置说明
		/// </summary>
		public const string _ConfigString = "ConfigString" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field1 = "Field1" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field2 = "Field2" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field3 = "Field3" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field4 = "Field4" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field5 = "Field5" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field6 = "Field6" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field7 = "Field7" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Field8 = "Field8" ;

		/// <summary>
		/// 排序ID
		/// </summary>
		public const string _SortID = "SortID" ;
		#endregion

		#region 私有变量
		private int m_configID;					//配置标识
		private string m_configKey;				//配置KEY
		private string m_configName;			//配置名称
		private string m_configString;			//配置说明
		private string m_field1;				//
		private string m_field2;				//
		private string m_field3;				//
		private string m_field4;				//
		private string m_field5;				//
		private string m_field6;				//
		private string m_field7;				//
		private string m_field8;				//
		private int m_sortID;					//排序ID
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ConfigInfo
		/// </summary>
		public ConfigInfo()
		{
			m_configID=0;
			m_configKey="";
			m_configName="";
			m_configString="";
			m_field1="";
			m_field2="";
			m_field3="";
			m_field4="";
			m_field5="";
			m_field6="";
			m_field7="";
			m_field8="";
			m_sortID=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 配置标识
		/// </summary>
		public int ConfigID
		{
			get { return m_configID; }
			set { m_configID = value; }
		}

		/// <summary>
		/// 配置KEY
		/// </summary>
		public string ConfigKey
		{
			get { return m_configKey; }
			set { m_configKey = value; }
		}

		/// <summary>
		/// 配置名称
		/// </summary>
		public string ConfigName
		{
			get { return m_configName; }
			set { m_configName = value; }
		}

		/// <summary>
		/// 配置说明
		/// </summary>
		public string ConfigString
		{
			get { return m_configString; }
			set { m_configString = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field1
		{
			get { return m_field1; }
			set { m_field1 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field2
		{
			get { return m_field2; }
			set { m_field2 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field3
		{
			get { return m_field3; }
			set { m_field3 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field4
		{
			get { return m_field4; }
			set { m_field4 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field5
		{
			get { return m_field5; }
			set { m_field5 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field6
		{
			get { return m_field6; }
			set { m_field6 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field7
		{
			get { return m_field7; }
			set { m_field7 = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Field8
		{
			get { return m_field8; }
			set { m_field8 = value; }
		}

		/// <summary>
		/// 排序ID
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}
		#endregion
	}
}
