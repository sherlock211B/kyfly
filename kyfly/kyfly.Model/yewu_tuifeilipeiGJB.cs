/**  版本信息模板在安装目录下，可自行修改。
* yewu_tuifeilipeiGJB.cs
* model
* 功 能： N/A
* 类 名： yewu_tuifeilipeiGJB
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
	/// yewu_tuifeilipeiGJB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class yewu_tuifeilipeiGJB
	{
		public yewu_tuifeilipeiGJB()
		{}
		#region Model
		private int _Id;
		private string _姓名;
		private string _收件人手机号;
		private string _OpenId;
		private string _地址;
		private string _状态;
		private string _配送人手机号;
		private DateTime _配送时间;
		private string _审核人手机号;
		private string _审核结果;
        private string _金额;
        private string _退款金额;
        private string _类别;
		private int _订单表Id;
		private string _支付单号;
        private string _微信订单号;
        private string _退款单号;
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
		public string 收件人手机号
		{
			set{ _收件人手机号=value;}
			get{return _收件人手机号;}
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
		public string 配送人手机号
		{
			set{ _配送人手机号=value;}
			get{return _配送人手机号;}
		}
		/// <summary>
		///
		/// <summary>
		public DateTime 配送时间
		{
			set{ _配送时间=value;}
			get{return _配送时间;}
		}
		/// <summary>
		///
		/// <summary>
		public string 审核人手机号
		{
			set{ _审核人手机号=value;}
			get{return _审核人手机号;}
		}
		/// <summary>
		///
		/// <summary>
		public string 审核结果
		{
			set{ _审核结果=value;}
			get{return _审核结果;}
		}
        public string 金额
        {
            set { _金额 = value; }
            get { return _金额; }
        }
        /// <summary>
        ///
        /// <summary>
         public string 退款金额
        {
            set { _退款金额 = value; }
            get { return _退款金额; }
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
		public int 订单表Id
		{
			set{ _订单表Id=value;}
			get{return _订单表Id;}
		}
		/// <summary>
		///
		/// <summary>
		public string 支付单号
		{
			set{ _支付单号=value;}
			get{return _支付单号;}
		}

        public string 微信订单号
        {
            set { _微信订单号 = value; }
            get { return _微信订单号; }
        }

        public string 退款单号
        {
            set { _退款单号 = value; }
            get { return _退款单号; }
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

