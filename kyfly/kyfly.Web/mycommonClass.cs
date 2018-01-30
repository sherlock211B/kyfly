using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.Net;

namespace kyfly
{
    public class mycommonClass : System.Web.UI.Page
    {



        public static string ToJson_static(DataSet ds, int mycount, string dateflag)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder jsonString = new StringBuilder();
            //
            //TODO:total表示记录的总数
            //
            jsonString.Append("{\"total\":" + mycount.ToString() + ",\"rows\":[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    if (type.Name.ToString().ToLower() == "datetime" && strValue != "" && dateflag == "date")
                        strValue = Convert.ToDateTime(strValue).ToString("yyyy-MM-dd");

                    jsonString.Append("\"" + strKey + "\":");
                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        //jsonString.Append("\"" + strValue + "\",");
                        jsonString.Append("\"" + strValue.Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'") + "\",");
                    }
                    else
                    {
                        //jsonString.Append("\"" + strValue + "\"");
                        jsonString.Append("\"" + strValue.Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'") + "\"");
                    }

                }
                jsonString.Append("},");

            }
            if (drc.Count > 0)
                jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]}");
            //string jsonStringret = stringToJson(jsonString.ToString());
            // string jsonStringret = RemoveIllegalCharacterForJson(jsonString.ToString());
            return jsonString.ToString();
        }

        public static string ToJson_combox_static(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder jsonString = new StringBuilder();
            //
            //TODO:total表示记录的总数
            //
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    if (type.Name.ToString().ToLower() == "datetime" && strValue != "")
                        strValue = Convert.ToDateTime(strValue).ToString("yyyy-MM-dd");

                    jsonString.Append("\"" + strKey + "\":");
                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + strValue + "\",");
                    }
                    else
                    {
                        jsonString.Append("\"" + strValue + "\"");
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            if (jsonString.Length == 1)         //周翼 2014-11-23-20:03
                jsonString.Remove(jsonString.Length - 1, 1);
            return jsonString.ToString();
        }

        public string ToJson(DataSet ds, int mycount, string dateflag)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder jsonString = new StringBuilder();
            //
            //TODO:total表示记录的总数
            //
            jsonString.Append("{\"total\":" + mycount.ToString() + ",\"rows\":[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    if (type.Name.ToString().ToLower() == "datetime" && strValue != "" && dateflag == "date")
                        strValue = Convert.ToDateTime(strValue).ToString("yyyy-MM-dd");

                    jsonString.Append("\"" + strKey + "\":");
                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        //jsonString.Append("\"" + strValue + "\",");
                        jsonString.Append("\"" + strValue.Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'") + "\",");
                    }
                    else
                    {
                        //jsonString.Append("\"" + strValue + "\"");
                        jsonString.Append("\"" + strValue.Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'") + "\"");
                    }
 
                }
                jsonString.Append("},");
            
            }
            if (drc.Count > 0)
                jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]}");
            //string jsonStringret = stringToJson(jsonString.ToString());
           // string jsonStringret = RemoveIllegalCharacterForJson(jsonString.ToString());
            return jsonString.ToString();
        }

        public string ToJson_combox(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            StringBuilder jsonString = new StringBuilder();
            //
            //TODO:total表示记录的总数
            //
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    if (type.Name.ToString().ToLower() == "datetime" && strValue != "")
                        strValue = Convert.ToDateTime(strValue).ToString("yyyy-MM-dd");

                    jsonString.Append("\"" + strKey + "\":");
                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append("\"" + strValue + "\",");
                    }
                    else
                    {
                        jsonString.Append("\"" + strValue + "\"");
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            if (jsonString.Length == 1)         //周翼 2014-11-23-20:03
                jsonString.Remove(jsonString.Length - 1, 1);
            return jsonString.ToString();
        }

 

        protected string RemoveIllegalCharacterForJson(string json)
        {
            StringBuilder stb = new StringBuilder(json);
            return stb.Replace("\r", "\\r").Replace("\n", "\\n").Replace("'", "\"").ToString();
        }
     

        #region 将一个数据表转换成一个JSON字符串，在客户端可以直接转换成二维数组。
        /// <summary>
        /// 将一个数据表转换成一个JSON字符串，在客户端可以直接转换成二维数组。
        /// </summary>
        /// <param name="source">需要转换的表。</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable source)
        {
            if (source.Rows.Count == 0)
                return "";
            StringBuilder sb = new StringBuilder("[");
            foreach (DataRow row in source.Rows)
            {
                sb.Append("[");
                for (int i = 0; i < source.Columns.Count; i++)
                {
                    sb.Append('"' + row[i].ToString().Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'") + "\",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("],");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }


        /// <summary>
        /// 返回JSON数据到前台
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>JSON字符串</returns>
        public   string DataTableToJsonParam(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'") + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>").Replace("\"", "'") + "\"");
                        }
                    }
                    /**/
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return ""; ;
            }
        }
        #endregion

 
 

        public static int getpagestartindex(int pageSize, int pageindex)
        {
            if (pageSize == 0)
                pageSize = 10;
            return ((pageindex - 1) * pageSize) + 1;
        }
        public static int getpageendindex(int pageSize, int pageindex)
        {
            if (pageSize == 0)
                pageSize = 10;
            return (pageindex * pageSize);
        }

    
    }

    public class userinfo
    {
        public string logincount;//登录帐户名   
        public string userid;//登录帐户编名 
        public string username;//登录用户姓名
        public string usertype;//用户类别
        public string orgid;//部门编号
        public DateTime logintime;//登录时间
         
    }
}