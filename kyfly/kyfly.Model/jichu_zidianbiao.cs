/**  版本信息模板在安装目录下，可自行修改。
* jichu_zidianbiao.cs
* model
* 功 能： N/A
* 类 名： jichu_zidianbiao
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
	/// jichu_zidianbiao:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class jichu_zidianbiao
	{
		public jichu_zidianbiao()
		{}
		#region Model
		private int _Id;
		private string _字段名称;
		private string _字段值;
		private string _类别;
		private string _分类别;


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
		public string 字段名称
		{
			set{ _字段名称=value;}
			get{return _字段名称;}
		}
		/// <summary>
		///
		/// <summary>
		public string 字段值
		{
			set{ _字段值=value;}
			get{return _字段值;}
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
		public string 分类别
		{
			set{ _分类别=value;}
			get{return _分类别;}
		}

 
		#endregion Model

	}
}

