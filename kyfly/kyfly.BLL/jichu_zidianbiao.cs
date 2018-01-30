﻿/**  版本信息模板在安装目录下，可自行修改。
* jichu_zidianbiao.cs
* bll
* 功 能： N/A
* 类 名： jichu_zidianbiao
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/12/25   N/A    初版
*
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波大学科技学院小鹿工作室 　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using kyfly.Model;
using System.Web;

namespace kyfly.BLL
{
	/// <summary>
	/// jichu_zidianbiao
	/// </summary>
    public partial class jichu_zidianbiao : System.Web.UI.Page
    {
        private readonly kyfly.DAL.jichu_zidianbiao dal = new kyfly.DAL.jichu_zidianbiao();
 
        private string strifofpage = "";
        HttpContext thiscontext;
        public jichu_zidianbiao()
        {

        }
        public jichu_zidianbiao(HttpContext context)
        {
            thiscontext = context;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["jichu_zidianbiao"] != null)
                strifofpage = Session["jichu_zidianbiao"].ToString();
        }
        #region  add update del

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add( string ziduanmingcheng, string ziduanzhi, string leibie, string fenleibie)
        {
            kyfly.Model.jichu_zidianbiao model = new kyfly.Model.jichu_zidianbiao();
                    model.字段名称 = ziduanmingcheng;
                    model.字段值 = ziduanzhi;
                    model.类别 = leibie;
                    model.分类别 = fenleibie;

            return dal.Add(model);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add1(kyfly.Model.jichu_zidianbiao model)
        {
             
            return dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(string Id, string ziduanmingcheng, string ziduanzhi, string leibie, string fenleibie)
        {
            

            kyfly.Model.jichu_zidianbiao model = new kyfly.Model.jichu_zidianbiao();
            model = dal.GetModel(int.Parse(Id));
            //对于传入的model如果其中属性已经更改，则更改，否则保留原数据库中的数据。
                    model.字段名称 = ziduanmingcheng;
                    model.字段值 = ziduanzhi;
                    model.类别 = leibie;
                    model.分类别 = fenleibie;


            return dal.Update(model);
        }
        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update1(kyfly.Model.jichu_zidianbiao model)
        {

            return dal.Update(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

                
        #endregion add update del              

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public kyfly.Model.jichu_zidianbiao GetModel(int Id)
        {
            return dal.GetModel(Id);
        }
        /// <summary>
        /// 得到一个对象值系列
        /// </summary>
        public string Geteditdata(string Id)
        {
            kyfly.Model.jichu_zidianbiao model = new kyfly.Model.jichu_zidianbiao();
            model = dal.GetModel(int.Parse( Id));
            string retstr = "";

            retstr +=model.Id+"|";
            retstr +=model.字段名称+"|";
            retstr +=model.字段值+"|";
            retstr +=model.类别+"|";
            retstr +=model.分类别+"|";

            return retstr;
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public kyfly.Model.jichu_zidianbiao GetModelByCache(int Id)
        {

            string CacheKey = "jichu_zidianbiaoModel-" + Id;
            object objModel = kyfly.Common.DbHelperMySQL.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = kyfly.Common.DbHelperMySQL.GetConfigInt("ModelCache");
                        kyfly.Common.DbHelperMySQL.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (kyfly.Model.jichu_zidianbiao)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            if (strWhere != "" && strifofpage!="")
                strWhere = "(" + strWhere + " and " + strifofpage+") ";
            return dal.GetList(strWhere);
        }

        /// <summary>
                ///根据要读取的字段列表和条件 获得数据列表
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄 </param>
        /// <param name="strWhere">条件 ，如：年龄>12 and 性别='女'   </param>
        /// <param name="strWhere"> 排序，如：   Id desc 或  </param>   
        /// <returns></returns>
        public DataSet GetList(string columslist,string strWhere,string orderstr)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";
            if (orderstr != "")
                strWhere +=" order by "+  orderstr;
            return dal.GetList(columslist,strWhere);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top">前几行，如：10</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <returns></returns>
        public DataSet GetList(int Top, string strWhere, string orderstr)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";
 
            return dal.GetList(Top, strWhere, orderstr);
        }

                
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top">前几行，如：10</param>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <returns></returns>
        public DataSet GetList(int Top, string columslist, string strWhere, string orderstr)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            return dal.GetList(Top, columslist, strWhere, orderstr);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<kyfly.Model.jichu_zidianbiao> GetModelList(string strWhere)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<kyfly.Model.jichu_zidianbiao> DataTableToList(DataTable dt)
        {
            List<kyfly.Model.jichu_zidianbiao> modelList = new List<kyfly.Model.jichu_zidianbiao>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                kyfly.Model.jichu_zidianbiao model;
                for (int n = 0; n < rowsCount; n++)
                {
                    DataRow row = dt.Rows[n];
                    model = dal.DataRowToModel(row);

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            string strWhere = "";
            if (strifofpage != "")
                strWhere = " (" + strifofpage + ") ";

            return GetList(strWhere);
        }

        
        /// <summary>
        /// 根据条件获得表中的某一个值
        /// </summary>
        /// <param name="columnname">字段，如：姓名</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <returns>如果读取不到值，则返回 空字符串</returns>
        public string GetValue(string columnname, string strWhere)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            return dal.GetValue(columnname, strWhere);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            return dal.GetRecordCount(strWhere);
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
        public DataSet GetListByPageColumns(string columslist, string strWhere, string orderstr, int PageSize, int PageIndex)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            return dal.GetListByPageColumns(columslist, strWhere, orderstr, PageSize, PageIndex);
        }

        /// <summary>
        /// 根据字段列表，条件，排序，分页读取记录
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <returns>返回组装好的json字符串</returns>
        public string GetListByPageColumns_tojson(string columslist, string strWhere, string orderstr,int state)
        {
            int PageIndex = 1;
            int PageSize = 20;
            if (thiscontext != null)
            {
                PageIndex = thiscontext.Request.Form["page"] != "" ? Convert.ToInt32(thiscontext.Request.Form["page"]) : 0;
                PageSize = thiscontext.Request.Form["rows"] != "" ? Convert.ToInt32(thiscontext.Request.Form["rows"]) : 0;
                string sort = thiscontext.Request.Form["sort"] != "" ? thiscontext.Request.Form["sort"] : "";
                string order = thiscontext.Request.Form["order"] != "" ? thiscontext.Request.Form["order"] : "";
                if (PageIndex < 1) PageIndex = 1;
                if (PageSize < 1) PageSize = 20;

                string strif1 = kyfly.Common.DbHelperMySQL.GetWhere(thiscontext);
                if (strif1 != "" && strif1 != null)
                {
                    if (strWhere.Trim() != "")
                        strWhere = strWhere + " and " + strif1;
                    else
                        strWhere = strif1;
                }
            }
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            return GetListByPageColumns_tojson(columslist, strWhere, orderstr, PageSize, PageIndex,state);
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
        public string GetListByPageColumns_tojson(string columslist, string strWhere, string orderstr, int PageSize, int PageIndex,int state)
        {
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            return dal.GetListByPageColumns_tojson(columslist, strWhere, orderstr, PageSize, PageIndex,state);
        }
        /// <summary>
        /// 根据字段列表，条件，排序，
        /// </summary>
        /// <param name="columslist">字段列表，如：Id,姓名,性别,年龄</param>
        /// <param name="strWhere">条件，如：年龄>12 and 性别='女'</param>
        /// <param name="filedOrder">排序，如： Id desc </param>
        /// <returns>返回组装好的json字符串</returns>
        public string GetListByColumn_tojson(string columslist, string strWhere, string orderstr)
        {
            if (thiscontext != null)
            {
                string strif1 = kyfly.Common.DbHelperMySQL.GetWhere(thiscontext);
                if (strif1 != "" && strif1 != null)
                {
                    if (strWhere.Trim() != "")
                        strWhere = strWhere + " and " + strif1;
                    else
                        strWhere = strif1;
                }
            }
            if (strWhere != "" && strifofpage != "")
                strWhere = "(" + strWhere + " and " + strifofpage + ") ";

            return dal.GetListByColumns_tojson(columslist, strWhere, orderstr);
        }
        /// <summary>
        /// 根据sqlstr 语句，页号和每页记录数 返回表格中一页的数据
        /// </summary>
        /// <param name="strsql">sql语句</param>
        /// <param name="PageSize">每页的记录数</param>
        /// <param name="PageIndex">页号，第一页是1</param>
        /// <returns>读取结果的数据集</returns>
        public DataSet GetListByPageofSql(string strsql, int PageSize, int PageIndex)
        {
            return dal.GetListByPageofSql(strsql, PageSize, PageIndex);
        }
    }
}
