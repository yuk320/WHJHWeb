/*
 * 版本：4.0
 * 时间：2014-2-11
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
	/// 实体类 Ads。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Ads  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "Ads" ;

		/// <summary>
		/// 广告标识
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 图片标题
		/// </summary>
		public const string _Title = "Title" ;

		/// <summary>
		/// 资源路径
		/// </summary>
		public const string _ResourceURL = "ResourceURL" ;

		/// <summary>
		/// 链接地址
		/// </summary>
		public const string _LinkURL = "LinkURL" ;

		/// <summary>
		/// 广告图片类型 0:网站首页轮换广告 1:大厅广告
		/// </summary>
		public const string _Type = "Type" ;

		/// <summary>
		/// 排序
		/// </summary>
		public const string _SortID = "SortID" ;

		/// <summary>
		/// 备注信息
		/// </summary>
		public const string _Remark = "Remark" ;
		#endregion

		#region 私有变量
		private int m_iD;						//广告标识
		private string m_title;					//图片标题
		private string m_resourceURL;			//资源路径
		private string m_linkURL;				//链接地址
		private byte m_type;					//广告图片类型 0:网站首页轮换广告 1:大厅广告
		private int m_sortID;					//排序
		private string m_remark;				//备注信息
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化Ads
		/// </summary>
		public Ads()
		{
			m_iD=0;
			m_title="";
			m_resourceURL="";
			m_linkURL="";
			m_type=0;
			m_sortID=0;
			m_remark="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 广告标识
		/// </summary>
		public int ID
		{
			get { return m_iD; }
			set { m_iD = value; }
		}

		/// <summary>
		/// 图片标题
		/// </summary>
		public string Title
		{
			get { return m_title; }
			set { m_title = value; }
		}

		/// <summary>
		/// 资源路径
		/// </summary>
		public string ResourceURL
		{
			get { return m_resourceURL; }
			set { m_resourceURL = value; }
		}

		/// <summary>
		/// 链接地址
		/// </summary>
		public string LinkURL
		{
			get { return m_linkURL; }
			set { m_linkURL = value; }
		}

		/// <summary>
		/// 广告图片类型 0:网站首页轮换广告 1:大厅广告
		/// </summary>
		public byte Type
		{
			get { return m_type; }
			set { m_type = value; }
		}

		/// <summary>
		/// 排序
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}

		/// <summary>
		/// 备注信息
		/// </summary>
		public string Remark
		{
			get { return m_remark; }
			set { m_remark = value; }
		}
		#endregion
	}
}
