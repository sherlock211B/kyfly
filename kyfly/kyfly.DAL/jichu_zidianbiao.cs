/**  版本信息模板在安装目录下，可自行修改。
* jichu_zidianbiao.cs
* dal
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
using System.Data;
using System.Text;
using kyfly.Common;//Please add references
using MySql.Data.MySqlClient;

namespace kyfly.DAL
{
	/// <summary>
	/// 数据访问类:jichu_zidianbiao
	/// </summary>
	public partial class jichu_zidianbiao
	{
        public jichu_zidianbiao()
		{}

		#region  add update del

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(kyfly.Model.jichu_zidianbiao model)
		{
			StringBuilder strSql=new StringBuilder();
         
			strSql.Append("insert into 基础_字典表(");
			strSql.Append("字段名称,字段值,类别,分类别)  values (");
			strSql.Append("@字段名称,@字段值,@类别,@分类别)");
			strSql.Append(";");
			MySqlParameter[] parameters = {
					new MySqlParameter("@字段名称", MySqlDbType.VarChar,30),
					new MySqlParameter("@字段值", MySqlDbType.VarChar,60),
					new MySqlParameter("@类别", MySqlDbType.VarChar,30),
					new MySqlParameter("@分类别", MySqlDbType.VarChar,30)};
			parameters[0].Value = model.字段名称;
			parameters[1].Value = model.字段值;
			parameters[2].Value = model.类别;
			parameters[3].Value = model.分类别;


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
        public bool Update(kyfly.Model.jichu_zidianbiao model)
		{
			StringBuilder strSql=new StringBuilder();

            strSql.Append("update 基础_字典表 set ");
            strSql.Append("字段名称=@字段名称,");
            strSql.Append("字段值=@字段值,");
            strSql.Append("类别=@类别,");
            strSql.Append("分类别=@分类别");
            strSql.Append(" where Id=@Id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@字段名称", MySqlDbType.VarChar,30),
					new MySqlParameter("@字段值", MySqlDbType.VarChar,60),
					new MySqlParameter("@类别", MySqlDbType.VarChar,30),
					new MySqlParameter("@分类别", MySqlDbType.VarChar,30),
					new MySqlParameter("@Id", MySqlDbType.Int16,6)};
			parameters[0].Value = model.字段名称;
			parameters[1].Value = model.字段值;
			parameters[2].Value = model.类别;
			parameters[3].Value = model.分类别;
			parameters[4].Value = model.Id;

			 

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
            strSql.Append("delete from 基础_字典表 ");
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
            strSql.Append("delete from 基础_字典表 ");
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
        public kyfly.Model.jichu_zidianbiao GetModel(int Id)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select   * from 基础_字典表 ");
            strSql.Append(" where Id=@Id LIMIT 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySql.Data.MySqlClient.MySqlDbType.Int16,11)
			};
            parameters[0].Value = Id;

            kyfly.Model.jichu_zidianbiao model = new kyfly.Model.jichu_zidianbiao();
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
		public kyfly.Model.jichu_zidianbiao DataRowToModel(DataRow row)
		{
			kyfly.Model.jichu_zidianbiao model=new kyfly.Model.jichu_zidianbiao();
			if (row != null)
			{
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["字段名称"] != null )
                {
                    model.字段名称 = row["字段名称"].ToString();
                }
                if (row["字段值"] != null )
                {
                    model.字段值 = row["字段值"].ToString();
                }
                if (row["类别"] != null )
                {
                    model.类别 = row["类别"].ToString();
                }
                if (row["分类别"] != null )
                {
                    model.分类别 = row["分类别"].ToString();
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
            strSql.Append(" FROM 基础_字典表 ");
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
            strSql.Append(" FROM 基础_字典表 ");
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
			strSql.Append(" FROM 基础_字典表 ");
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
            strSql.Append(" FROM 基础_字典表 ");
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
            strSql.Append(" FROM 基础_字典表 ");
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
			strSql.Append("select count(1) FROM 基础_字典表 ");
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
			strSql.Append(")AS Row, T.*  from 基础_字典表 T ");
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
            string str = DbHelperMySQL.GetListByPageColumns_tojson(columslist, strWhere, orderby, PageSize, PageIndex, "基础_字典表",state);

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
            string str = DbHelperMySQL.GetListByColumns_tojson(columslist, strWhere, orderby, "基础_字典表");

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
            string strsql = DbHelperMySQL.GetListByPageColumns(columslist, strWhere, orderby, PageSize, PageIndex, "基础_字典表");
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

