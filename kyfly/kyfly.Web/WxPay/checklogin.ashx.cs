using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace WxPayAPI
{
    /// <summary>
    /// checklogin 的摘要说明
    /// </summary>
    public class checklogin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string openid = "";
            try
            {
                openid = HttpContext.Current.Request.QueryString["openid"].ToString();
                        
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }

            //创建连接池对象（与数据库服务器进行连接）  
            SqlConnection conn = new SqlConnection("User ID=kyfly;Database=kyfly; Password=kyfly; server=106.14.173.39; Port= 3306;Protocol=TCP; Compress=false; Pooling=true; Min Pool Size=0;Max Pool Size=100; Connection Lifetime=0;Charset=utf8 ");
            //打开连接池  
            conn.Open();
            //创建命令对象  
            string Qrystr = "SELECT * FROM 基础_人员信息表 WHERE openId='" + openid + "'";
            SqlCommand cmdQry = new SqlCommand(Qrystr, conn);
            object obj = cmdQry.ExecuteScalar();
            writeFile(obj.ToString ());
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
            
                // 执行操作  
                try
                {
                    writeFile("1");
                    context.Response.Write(1);
                    
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.ToString());
                }
            }
            else
            {
                writeFile("2");
                context.Response.Write(0);
            }

            //关闭连接池  
            conn.Close();
        }
        private void writeFile(string str)
        {
            StreamWriter sw = new StreamWriter("C:\\temp\\trytest.txt", true);
            sw.Write(str + "\r\n");
            sw.Close();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}