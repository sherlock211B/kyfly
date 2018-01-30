/**  版本信息模板在安装目录下，可自行修改。
* jichu_renyuanxinxiB.cs
* model
* 功 能： N/A
* 类 名： jichu_renyuanxinxiB
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/12/25   N/A    初版
*
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波大学科技学院 　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace kyfly.Model
{
	/// <summary>
	/// jichu_renyuanxinxiB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class jichu_renyuanxinxiB
	{
		public jichu_renyuanxinxiB()
		{}
		#region Model
		private int _Id;
		private string _姓名;
		private string _手机号;
		private string _OpenId;
		private string _密码;
		private string _地址;
		private string _状态;
		private string _类别;
		private string _权限;
		private string _会员级别;
		private int _配送次数;
		private string _备注;


		/// <summary>
		///
		/// <summary>
		public int Id
		{
			set{ _Id=value;}
			get{return _Id;}
		}
		/// <summary>
		///
		/// <summary>
		public string 姓名
		{
			set{ _姓名=value;}
			get{return _姓名;}
		}
		/// <summary>
		///
		/// <summary>
		public string 手机号
		{
			set{ _手机号=value;}
			get{return _手机号;}
		}
		/// <summary>
		///
		/// <summary>
		public string OpenId
		{
			set{ _OpenId=value;}
			get{return _OpenId;}
		}
		/// <summary>
		///
		/// <summary>
		public string 密码
		{
			set{ _密码=value;}
			get{return _密码;}
		}
		/// <summary>
		///
		/// <summary>
		public string 地址
		{
			set{ _地址=value;}
			get{return _地址;}
		}
		/// <summary>
		///
		/// <summary>
		public string 状态
		{
			set{ _状态=value;}
			get{return _状态;}
		}
		/// <summary>
		///
		/// <summary>
		public string 类别
		{
			set{ _类别=value;}
			get{return _类别;}
		}
		/// <summary>
		///
		/// <summary>
		public string 权限
		{
			set{ _权限=value;}
			get{return _权限;}
		}
		/// <summary>
		///
		/// <summary>
		public string 会员级别
		{
			set{ _会员级别=value;}
			get{return _会员级别;}
		}
		/// <summary>
		///
		/// <summary>
		public int 配送次数
		{
			set{ _配送次数=value;}
			get{return _配送次数;}
		}
		/// <summary>
		///
		/// <summary>
		public string 备注
		{
			set{ _备注=value;}
			get{return _备注;}
		}

 
		#endregion Model

	}
}

