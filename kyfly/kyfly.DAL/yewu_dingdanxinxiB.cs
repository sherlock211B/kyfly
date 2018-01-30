/**  版本信息模板在安装目录下，可自行修改。
* yewu_dingdanxinxiB.cs
* dal
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
using System.Data;
using System.Text;
using kyfly.Common;//Please add references
using MySql.Data.MySqlClient;

namespace kyfly.DAL
{
	/// <summary>
	/// 数据访问类:yewu_dingdanxinxiB
	/// </summary>
	public partial class yewu_dingdanxinxiB
	{
        public yewu_dingdanxinxiB()
		{}

		#region  add update del

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(kyfly.Model.yewu_dingdanxinxiB model)
		{
			StringBuilder strSql=new StringBuilder();
         
			strSql.Append("insert into 业务_订单信息表(");
			strSql.Append("姓名,收件人手机号,物品类型,照片,配送地址,状态,接单时间,取件号,货架号,货物贵重否,重量类别,配送金额,配送人手机号,配送时间,审核人手机号,订单修正,修正时间,支付单号,微信订单号,评价,备注)  values (");
			strSql.Append("@姓名,@收件人手机号,@物品类型,@照片,@配送地址,@状态,@接单时间,@取件号,@货架号,@货物贵重否,@重量类别,@配送金额,@配送人手机号,@配送时间,@审核人手机号,@订单修正,@修正时间,@支付单号,@微信订单号,@评价,@备注)");
			strSql.Append(";");
			MySqlParameter[] parameters = {
					new MySqlParameter("@姓名", MySqlDbType.VarChar,30),
					new MySqlParameter("@收件人手机号", MySqlDbType.VarChar,11),
                    new MySqlParameter("@物品类型", MySqlDbType.VarChar,30),
                    new MySqlParameter("@照片", MySqlDbType.VarChar,100),
                    new MySqlParameter("@配送地址", MySqlDbType.VarChar,100),
					new MySqlParameter("@状态", MySqlDbType.VarChar,20),
					new MySqlParameter("@接单时间", MySqlDbType.DateTime),
					new MySqlParameter("@取件号", MySqlDbType.VarChar,20),
					new MySqlParameter("@货架号", MySqlDbType.VarChar,20),
					new MySqlParameter("@货物贵重否", MySqlDbType.VarChar,20),
					new MySqlParameter("@重量类别", MySqlDbType.VarChar,20),
                    new MySqlParameter("@配送金额", MySqlDbType.Double,0),
                    new MySqlParameter("@配送人手机号", MySqlDbType.VarChar,11),
					new MySqlParameter("@配送时间", MySqlDbType.DateTime),
					new MySqlParameter("@审核人手机号", MySqlDbType.VarChar,11),
					new MySqlParameter("@订单修正", MySqlDbType.VarChar,20),
					new MySqlParameter("@修正时间", MySqlDbType.DateTime),
					new MySqlParameter("@支付单号", MySqlDbType.VarChar,30),
                    new MySqlParameter("@微信订单号", MySqlDbType.VarChar,50),
                    new MySqlParameter("@评价", MySqlDbType.VarChar,100),
                    new MySqlParameter("@备注", MySqlDbType.VarChar,30)};
			parameters[0].Value = model.姓名;
			parameters[1].Value = model.收件人手机号;
            parameters[2].Value = model.物品类型;
            parameters[3].Value = model.照片;
            parameters[4].Value = model.配送地址;
			parameters[5].Value = model.状态;
			parameters[6].Value = model.接单时间;
			parameters[7].Value = model.取件号;
			parameters[8].Value = model.货架号;
			parameters[9].Value = model.货物贵重否;
			parameters[10].Value = model.重量类别;
			parameters[11].Value = model.配送金额;
			parameters[12].Value = model.配送人手机号;
			parameters[13].Value = model.配送时间;
			parameters[14].Value = model.审核人手机号;
			parameters[15].Value = model.订单修正;
			parameters[16].Value = model.修正时间;
			parameters[17].Value = model.支付单号;
            parameters[18].Value = model.微信订单号;
            parameters[19].Value = model.评价;
            parameters[20].Value = model.备注;


			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return rows;
			}
			else
			{
				return 0;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(kyfly.Model.yewu_dingdanxinxiB model)
		{
			StringBuilder strSql=new StringBuilder();

            strSql.Append("update 业务_订单信息表 set ");
            strSql.Append("姓名=@姓名,");
            strSql.Append("收件人手机号=@收件人手机号,");
            strSql.Append("物品类型=@物品类型,");
            strSql.Append("照片=@照片,");
            strSql.Append("配送地址=@配送地址,");
            strSql.Append("状态=@状态,");
            strSql.Append("接单时间=@接单时间,");
            strSql.Append("取件号=@取件号,");
            strSql.Append("货架号=@货架号,");
            strSql.Append("货物贵重否=@货物贵重否,");
            strSql.Append("重量类别=@重量类别,");
            strSql.Append("配送金额=@配送金额,");
            strSql.Append("配送人手机号=@配送人手机号,");
            strSql.Append("配送时间=@配送时间,");
            strSql.Append("审核人手机号=@审核人手机号,");
            strSql.Append("订单修正=@订单修正,");
            strSql.Append("修正时间=@修正时间,");
            strSql.Append("支付单号=@支付单号,");
            strSql.Append("微信订单号=@微信订单号,");
            strSql.Append("评价=@评价,");
            strSql.Append("备注=@备注");
            strSql.Append(" where Id=@Id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@姓名", MySqlDbType.VarChar,30),
                    new MySqlParameter("@收件人手机号", MySqlDbType.VarChar,11),
                    new MySqlParameter("@物品类型", MySqlDbType.VarChar,30),
                    new MySqlParameter("@照片", MySqlDbType.VarChar,100),
                    new MySqlParameter("@配送地址", MySqlDbType.VarChar,100),
                    new MySqlParameter("@状态", MySqlDbType.VarChar,20),
                    new MySqlParameter("@接单时间", MySqlDbType.DateTime),
                    new MySqlParameter("@取件号", MySqlDbType.VarChar,20),
                    new MySqlParameter("@货架号", MySqlDbType.VarChar,20),
                    new MySqlParameter("@货物贵重否", MySqlDbType.VarChar,20),
                    new MySqlParameter("@重量类别", MySqlDbType.VarChar,20),
                    new MySqlParameter("@配送金额", MySqlDbType.Double,0),
                    new MySqlParameter("@配送人手机号", MySqlDbType.VarChar,11),
                    new MySqlParameter("@配送时间", MySqlDbType.DateTime),
                    new MySqlParameter("@审核人手机号", MySqlDbType.VarChar,11),
                    new MySqlParameter("@订单修正", MySqlDbType.VarChar,20),
                    new MySqlParameter("@修正时间", MySqlDbType.DateTime),
                    new MySqlParameter("@支付单号", MySqlDbType.VarChar,30),
                    new MySqlParameter("@微信订单号", MySqlDbType.VarChar,50),
                    new MySqlParameter("@评价", MySqlDbType.VarChar,100),
                    new MySqlParameter("@备注", MySqlDbType.VarChar,30),
                    new MySqlParameter("@Id", MySqlDbType.Int16,6)};
            parameters[0].Value = model.姓名;
            parameters[1].Value = model.收件人手机号;
            parameters[2].Value = model.物品类型;
            parameters[3].Value = model.照片;
            parameters[4].Value = model.配送地址;
            parameters[5].Value = model.状态;
            parameters[6].Value = model.接单时间;
            parameters[7].Value = model.取件号;
            parameters[8].Value = model.货架号;
            parameters[9].Value = model.货物贵重否;
            parameters[10].Value = model.重量类别;
            parameters[11].Value = model.配送金额;
            parameters[12].Value = model.配送人手机号;
            parameters[13].Value = model.配送时间;
            parameters[14].Value = model.审核人手机号;
            parameters[15].Value = model.订单修正;
            parameters[16].Value = model.修正时间;
            parameters[17].Value = model.支付单号;
            parameters[18].Value = model.微信订单号;
            parameters[19].Value = model.评价;
            parameters[20].Value = model.备注;
            parameters[21].Value = model.Id;

			 

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}



        public bool Updatestate(kyfly.Model.yewu_dingdanxinxiB model)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update 业务_订单信息表 set ");
            strSql.Append("状态=@状态,");
            strSql.Append(" where 支付单号=@支付单号");
            MySqlParameter[] parameters = {
                 
                    new MySqlParameter("@状态", MySqlDbType.VarChar,20),
                  
                    new MySqlParameter("@支付单号",MySqlDbType.VarChar,30)};
         
            parameters[5].Value = model.状态;
            parameters[17].Value = model.支付单号;
    
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from 业务_订单信息表 ");
            strSql.Append(" where Id=@Id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySql.Data.MySqlClient.MySqlDbType.Int16,11)
			};
            parameters[0].Value = Id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from 业务_订单信息表 ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion add update del              

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
        public kyfly.Model.yewu_dingdanxinxiB GetModel(int Id)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * from 业务_订单信息表 ");
            strSql.Append(" where Id=@Id LIMIT 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySql.Data.MySqlClient.MySqlDbType.Int16,11)
			};
            parameters[0].Value = Id;

            kyfly.Model.yewu_dingdanxinxiB model = new kyfly.Model.yewu_dingdanxinxiB();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

        public kyfly.Model.yewu_dingdanxinxiB GetModelstate(string zhifudanhao)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from 业务_订单信息表 ");
            strSql.Append(" where zhifudanhao=@zhifudanhao LIMIT 1");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@zhifudanhao", MySql.Data.MySqlClient.MySqlDbType.Int16,11)
            };
            parameters[0].Value = zhifudanhao;

            kyfly.Model.yewu_dingdanxinxiB model = new kyfly.Model.yewu_dingdanxinxiB();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public kyfly.Model.yewu_dingdanxinxiB DataRowToModel(DataRow row)
		{
			kyfly.Model.yewu_dingdanxinxiB model=new kyfly.Model.yewu_dingdanxinxiB();
			if (row != null)
			{
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["姓名"] != null )
                {
                    model.姓名 = row["姓名"].ToString();
                }
                if (row["收件人手机号"] != null )
                {
                    model.收件人手机号 = row["收件人手机号"].ToString();
                }
                if (row["物品类型"] != null)
                {
                    model.物品类型 = row["物品类型"].ToString();
                }
                if (row["照片"] != null)
                {
                    model.照片 = row["照片"].ToString();
                }
                if (row["配送地址"] != null )
                {
                    model.配送地址 = row["配送地址"].ToString();
                }
                if (row["状态"] != null )
                {
                    model.状态 = row["状态"].ToString();
                }
                if (row["接单时间"] != null && row["接单时间"].ToString() != "")
                {
                    model.接单时间 = DateTime.Parse(row["接单时间"].ToString());
                }
                if (row["取件号"] != null )
                {
                    model.取件号 = row["取件号"].ToString();
                }
                if (row["货架号"] != null )
                {
                    model.货架号 = row["货架号"].ToString();
                }
                if (row["货物贵重否"] != null )
                {
                    model.货物贵重否 = row["货物贵重否"].ToString();
                }
                if (row["重量类别"] != null )
                {
                    model.重量类别 = row["重量类别"].ToString();
                }
                if (row["配送金额"] != null)
                {
                    model.配送金额 = row["配送金额"].ToString();
                }
                if (row["配送人手机号"] != null )
                {
                    model.配送人手机号 = row["配送人手机号"].ToString();
                }
                if (row["配送时间"] != null && row["配送时间"].ToString() != "")
                {
                    model.配送时间 = DateTime.Parse(row["配送时间"].ToString());
                }
                if (row["审核人手机号"] != null )
                {
                    model.审核人手机号 = row["审核人手机号"].ToString();
                }
                if (row["订单修正"] != null )
                {
                    model.订单修正 = row["订单修正"].ToString();
                }
                if (row["修正时间"] != null && row["修正时间"].ToString() != "")
                {
                    model.修正时间 = DateTime.Parse(row["修正时间"].ToString());
                }
                if (row["支付单号"] != null )
                {
                    model.支付单号 = row["支付单号"].ToString();
                }
                if (row["微信订单号"] != null)
                {
                    model.微信订单号 = row["微信订单号"].ToString();
                }
                if (row["评价"] != null)
                {
                    model.评价 = row["评价"].ToString();
                }
                if (row["备注"] != null )
                {
                    model.备注 = row["备注"].ToString();
                }

				
			}
			return model;
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM 业务_订单信息表 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
                ///根据要读取的字段列表和条件 获得数据列表
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄 </param>
        /// <param name="strWhere">条件或排序，如：年龄>12 and 性别='女' order by Id desc </param>
        /// <returns></returns>
        public DataSet GetList(string columslist,string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + columslist  );
            strSql.Append(" FROM 业务_订单信息表 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top">前几行，如：10</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <returns></returns>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
 
			strSql.Append(" * ");
			strSql.Append(" FROM 业务_订单信息表 ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
            if (Top > 0)
            {
                strSql.Append("  LIMIT 0," + Top.ToString());
            }

			return DbHelperMySQL.Query(strSql.ToString());
		}
 
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top">前几行，如：10</param>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <returns></returns>
        public DataSet GetList(int Top, string columslist, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");

            strSql.Append(" " + columslist + " ");
            strSql.Append(" FROM 业务_订单信息表 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            if (Top > 0)
            {
                strSql.Append("  LIMIT 0," + Top.ToString());
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得表中的某一个值
        /// </summary>
        /// <param name="columnname">字段，如：姓名</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <returns>如果读取不到值，则返回 空字符串</returns>
        public string GetValue(string columnname, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
 
            strSql.Append(" " + columnname + " ");
            strSql.Append(" FROM 业务_订单信息表 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = new DataSet();
            ds =  DbHelperMySQL.Query(strSql.ToString());
            string tempstr = "";
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                tempstr = ds.Tables[0].Rows[0][0].ToString();

            return tempstr;
        }

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM 业务_订单信息表 ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperMySQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}


        /// <summary>
        /// 根据条件和排序，开始位置和结束位置 分页获取数据列表
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private DataSet GetListByPage(string columslist, string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("SELECT " + columslist + " FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from 业务_订单信息表 T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 根据字段列表，条件，排序，每页记录数，页码（从1开始），分页读取记录
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <param name="PageSize">每页记录数，如：30</param>
        /// <param name="PageIndex">页码，如：2</param>
        /// <returns>返回组装好的json字符串</returns>
        public string GetListByPageColumns_tojson(string columslist, string strWhere, string orderby, int PageSize, int PageIndex,int state)
        {
            string str = DbHelperMySQL.GetListByPageColumns_tojson(columslist, strWhere, orderby, PageSize, PageIndex, "业务_订单信息表",state);

            return str;
        }

        /// <summary>
        /// 根据字段列表，条件，排序，读取记录
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <returns>返回组装好的json字符串</returns>
        public string GetListByColumns_tojson(string columslist, string strWhere, string orderby)
        {
            string str = DbHelperMySQL.GetListByColumns_tojson(columslist, strWhere, orderby, "业务_订单信息表");

            return str;
        }


        /// <summary>
        /// 根据字段列表，条件，排序，每页记录数，页码（从1开始），分页读取记录
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <param name="PageSize">每页记录数，如：30</param>
        /// <param name="PageIndex">页码，如：2</param>
        /// <returns></returns>
        public DataSet GetListByPageColumns(string columslist, string strWhere, string orderby, int PageSize, int PageIndex)
        {
            string strsql = DbHelperMySQL.GetListByPageColumns(columslist, strWhere, orderby, PageSize, PageIndex, "业务_订单信息表");
            return DbHelperMySQL.Query(strsql);
        }

        #region 根据sql语句分页函数


        /// <summary>
        /// 根据sqlstr 语句，页号和每页记录数 返回表格中一页的数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="PageSize">每页的记录数</param>
        /// <param name="PageIndex">页号，第一页是1</param>
        /// <returns>读取结果的数据集</returns>
        public DataSet GetListByPageofSql(string sql, int PageSize, int PageIndex)
        {

            int pagenum = PageIndex;
            int pagerecord = PageSize;

            DataSet ds = new DataSet();
 
            string strsql = sql;
            strsql = DbHelperMySQL.createsql(sql, pagenum, pagerecord);
            ds = DbHelperMySQL.Query(strsql);
 
            return ds;
        }
        #endregion
	}
}

