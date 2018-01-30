/**  版本信息模板在安装目录下，可自行修改。
* yewu_tuifeilipeiGJB.cs
* dal
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
using System.Data;
using System.Text;
using kyfly.Common;//Please add references
using MySql.Data.MySqlClient;

namespace kyfly.DAL
{
	/// <summary>
	/// 数据访问类:yewu_tuifeilipeiGJB
	/// </summary>
	public partial class yewu_tuifeilipeiGJB
	{
        public yewu_tuifeilipeiGJB()
		{}

		#region  add update del

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(kyfly.Model.yewu_tuifeilipeiGJB model)
		{
			StringBuilder strSql=new StringBuilder();
         
			strSql.Append("insert into 业务_退费理赔改价表(");
			strSql.Append("姓名,收件人手机号,OpenId,地址,状态,配送人手机号,配送时间,审核人手机号,审核结果,金额,退款金额,类别,订单表Id,支付单号,微信订单号,退款单号,备注)  values (");
			strSql.Append("@姓名,@收件人手机号,@OpenId,@地址,@状态,@配送人手机号,@配送时间,@审核人手机号,@审核结果,@金额,@退款金额,@类别,@订单表Id,@支付单号,@微信订单号,@退款单号,@备注)");
			strSql.Append(";");
			MySqlParameter[] parameters = {
					new MySqlParameter("@姓名", MySqlDbType.VarChar,30),
					new MySqlParameter("@收件人手机号", MySqlDbType.VarChar,11),
					new MySqlParameter("@OpenId", MySqlDbType.VarChar,30),
					new MySqlParameter("@地址", MySqlDbType.VarChar,100),
					new MySqlParameter("@状态", MySqlDbType.VarChar,20),
					new MySqlParameter("@配送人手机号", MySqlDbType.VarChar,11),
					new MySqlParameter("@配送时间", MySqlDbType.DateTime),
					new MySqlParameter("@审核人手机号", MySqlDbType.VarChar,11),
					new MySqlParameter("@审核结果", MySqlDbType.VarChar,20),
                    new MySqlParameter("@金额", MySqlDbType.Double,0),
                    new MySqlParameter("@退款金额", MySqlDbType.Double,0),
                    new MySqlParameter("@类别", MySqlDbType.VarChar,20),
					new MySqlParameter("@订单表Id", MySqlDbType.Int32,11),
					new MySqlParameter("@支付单号", MySqlDbType.VarChar,30),
                    new MySqlParameter("@微信订单号", MySqlDbType.VarChar,50),
                    new MySqlParameter("@退款单号", MySqlDbType.VarChar,50),
                    new MySqlParameter("@备注", MySqlDbType.VarChar,30)};
			parameters[0].Value = model.姓名;
			parameters[1].Value = model.收件人手机号;
			parameters[2].Value = model.OpenId;
			parameters[3].Value = model.地址;
			parameters[4].Value = model.状态;
			parameters[5].Value = model.配送人手机号;
			parameters[6].Value = model.配送时间;
			parameters[7].Value = model.审核人手机号;
			parameters[8].Value = model.审核结果;
			parameters[9].Value = model.金额;
            parameters[10].Value = model.退款金额;
            parameters[11].Value = model.类别;
			parameters[12].Value = model.订单表Id;
			parameters[13].Value = model.支付单号;
            parameters[14].Value = model.微信订单号;
            parameters[15].Value = model.退款单号;
            parameters[16].Value = model.备注;


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
        public bool Update(kyfly.Model.yewu_tuifeilipeiGJB model)
		{
			StringBuilder strSql=new StringBuilder();

            strSql.Append("update 业务_退费理赔改价表 set ");
            strSql.Append("姓名=@姓名,");
            strSql.Append("收件人手机号=@收件人手机号,");
            strSql.Append("OpenId=@OpenId,");
            strSql.Append("地址=@地址,");
            strSql.Append("状态=@状态,");
            strSql.Append("配送人手机号=@配送人手机号,");
            strSql.Append("配送时间=@配送时间,");
            strSql.Append("审核人手机号=@审核人手机号,");
            strSql.Append("审核结果=@审核结果,");
            strSql.Append("金额=@金额,");
            strSql.Append("退款金额=@退款金额,");
            strSql.Append("类别=@类别,");
            strSql.Append("订单表Id=@订单表Id,");
            strSql.Append("支付单号=@支付单号,");
            strSql.Append("微信订单号=@微信订单号,");
            strSql.Append("退款单号=@退款单号,");
            strSql.Append("备注=@备注");
            strSql.Append(" where Id=@Id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@姓名", MySqlDbType.VarChar,30),
                    new MySqlParameter("@收件人手机号", MySqlDbType.VarChar,11),
                    new MySqlParameter("@OpenId", MySqlDbType.VarChar,30),
                    new MySqlParameter("@地址", MySqlDbType.VarChar,100),
                    new MySqlParameter("@状态", MySqlDbType.VarChar,20),
                    new MySqlParameter("@配送人手机号", MySqlDbType.VarChar,11),
                    new MySqlParameter("@配送时间", MySqlDbType.DateTime),
                    new MySqlParameter("@审核人手机号", MySqlDbType.VarChar,11),
                    new MySqlParameter("@审核结果", MySqlDbType.VarChar,20),
                    new MySqlParameter("@金额", MySqlDbType.Double,0),
                    new MySqlParameter("@退款金额", MySqlDbType.Double,0),
                    new MySqlParameter("@类别", MySqlDbType.VarChar,20),
                    new MySqlParameter("@订单表Id", MySqlDbType.Int32,11),
                    new MySqlParameter("@支付单号", MySqlDbType.VarChar,30),
                    new MySqlParameter("@微信订单号", MySqlDbType.VarChar,50),
                    new MySqlParameter("@退款单号", MySqlDbType.VarChar,50),
                    new MySqlParameter("@备注", MySqlDbType.VarChar,30),
                    new MySqlParameter("@Id", MySqlDbType.Int16,6)};
            parameters[0].Value = model.姓名;
            parameters[1].Value = model.收件人手机号;
            parameters[2].Value = model.OpenId;
            parameters[3].Value = model.地址;
            parameters[4].Value = model.状态;
            parameters[5].Value = model.配送人手机号;
            parameters[6].Value = model.配送时间;
            parameters[7].Value = model.审核人手机号;
            parameters[8].Value = model.审核结果;
            parameters[9].Value = model.金额;
            parameters[10].Value = model.退款金额;
            parameters[11].Value = model.类别;
            parameters[12].Value = model.订单表Id;
            parameters[13].Value = model.支付单号;
            parameters[14].Value = model.微信订单号;
            parameters[15].Value = model.退款单号;
            parameters[16].Value = model.备注;
            parameters[17].Value = model.Id;

			 

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from 业务_退费理赔改价表 ");
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
            strSql.Append("delete from 业务_退费理赔改价表 ");
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
        public kyfly.Model.yewu_tuifeilipeiGJB GetModel(int Id)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select   * from 业务_退费理赔改价表 ");
            strSql.Append(" where Id=@Id LIMIT 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySql.Data.MySqlClient.MySqlDbType.Int16,11)
			};
            parameters[0].Value = Id;

            kyfly.Model.yewu_tuifeilipeiGJB model = new kyfly.Model.yewu_tuifeilipeiGJB();
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


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public kyfly.Model.yewu_tuifeilipeiGJB DataRowToModel(DataRow row)
		{
			kyfly.Model.yewu_tuifeilipeiGJB model=new kyfly.Model.yewu_tuifeilipeiGJB();
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
                if (row["OpenId"] != null )
                {
                    model.OpenId = row["OpenId"].ToString();
                }
                if (row["地址"] != null )
                {
                    model.地址 = row["地址"].ToString();
                }
                if (row["状态"] != null )
                {
                    model.状态 = row["状态"].ToString();
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
                if (row["审核结果"] != null )
                {
                    model.审核结果 = row["审核结果"].ToString();
                }
                if (row["金额"] != null)
                {
                    model.金额 = row["金额"].ToString();
                }
                if (row["退款金额"] != null)
                {
                    model.退款金额 = row["退款金额"].ToString();
                }
                if (row["类别"] != null )
                {
                    model.类别 = row["类别"].ToString();
                }
                if (row["订单表Id"] != null && row["订单表Id"].ToString() != "")
                {
                    model.订单表Id = int.Parse(row["订单表Id"].ToString());
                }
                if (row["支付单号"] != null )
                {
                    model.支付单号 = row["支付单号"].ToString();
                }
                if (row["微信订单号"] != null)
                {
                    model.微信订单号 = row["微信订单号"].ToString();
                }
                if (row["退款单号"] != null)
                {
                    model.退款单号 = row["退款单号"].ToString();
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
            strSql.Append(" FROM 业务_退费理赔改价表 ");
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
            strSql.Append(" FROM 业务_退费理赔改价表 ");
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
			strSql.Append(" FROM 业务_退费理赔改价表 ");
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
            strSql.Append(" FROM 业务_退费理赔改价表 ");
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
            strSql.Append(" FROM 业务_退费理赔改价表 ");
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
			strSql.Append("select count(1) FROM 业务_退费理赔改价表 ");
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
			strSql.Append(")AS Row, T.*  from 业务_退费理赔改价表 T ");
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
            string str = DbHelperMySQL.GetListByPageColumns_tojson(columslist, strWhere, orderby, PageSize, PageIndex, "业务_退费理赔改价表", state);

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
            string str = DbHelperMySQL.GetListByColumns_tojson(columslist, strWhere, orderby, "业务_退费理赔改价表");

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
            string strsql = DbHelperMySQL.GetListByPageColumns(columslist, strWhere, orderby, PageSize, PageIndex, "业务_退费理赔改价表");
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

