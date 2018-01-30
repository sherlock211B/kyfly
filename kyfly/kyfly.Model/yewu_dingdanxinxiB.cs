/**  版本信息模板在安装目录下，可自行修改。
* yewu_dingdanxinxiB.cs
* model
* 功 能： N/A
* 类 名： yewu_dingdanxinxiB
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
	/// yewu_dingdanxinxiB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class yewu_dingdanxinxiB
	{
		public yewu_dingdanxinxiB()
		{}
		#region Model
		private int _Id;
		private string _姓名;
		private string _收件人手机号;
        private string _物品类型;
        private string _照片;
        private string _配送地址;
		private string _状态;
		private DateTime _接单时间;
		private string _取件号;
		private string _货架号;
		private string _货物贵重否;
		private string _重量类别;
        private string _配送金额;
		private string _配送人手机号;
		private DateTime _配送时间;
		private string _审核人手机号;
		private string _订单修正;
		private DateTime _修正时间;
		private string _支付单号;
        private string _微信订单号;
        private string _评价;
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
		public string 物品类型
        {
            set { _物品类型 = value; }
            get { return _物品类型; }
        }
        /// <summary>
		///
		/// <summary>
		public string 照片
        {
            set { _照片 = value; }
            get { return _照片; }
        }
        /// <summary>
        ///
        /// <summary>
        public string 配送地址
		{
			set{ _配送地址=value;}
			get{return _配送地址;}
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
		public DateTime 接单时间
		{
			set{ _接单时间=value;}
			get{return _接单时间;}
		}
		/// <summary>
		///
		/// <summary>
		public string 取件号
		{
			set{ _取件号=value;}
			get{return _取件号;}
		}
		/// <summary>
		///
		/// <summary>
		public string 货架号
		{
			set{ _货架号=value;}
			get{return _货架号;}
		}
		/// <summary>
		///
		/// <summary>
		public string 货物贵重否
		{
			set{ _货物贵重否=value;}
			get{return _货物贵重否;}
		}
		/// <summary>
		///
		/// <summary>
		public string 重量类别
		{
			set{ _重量类别=value;}
			get{return _重量类别;}
		}

        public string 配送金额
        {
            set { _配送金额 = value; }
            get { return _配送金额; }
        }
        /// <summary>
        ///
        /// <summary>
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
		public string 订单修正
		{
			set{ _订单修正=value;}
			get{return _订单修正;}
		}
		/// <summary>
		///
		/// <summary>
		public DateTime 修正时间
		{
			set{ _修正时间=value;}
			get{return _修正时间;}
		}
		/// <summary>
		///
		/// <summary>
		public string 支付单号
		{
			set{ _支付单号=value;}
			get{return _支付单号;}
		}
        /// <summary>
		///
		/// <summary>
		public string 微信订单号
        {
            set { _微信订单号 = value; }
            get { return _微信订单号; }
        }
        /// <summary>
		///
		/// <summary>
		public string 评价
        {
            set { _评价 = value; }
            get { return _评价; }
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

