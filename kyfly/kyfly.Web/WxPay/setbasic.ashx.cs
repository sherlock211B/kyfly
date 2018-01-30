using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace kyfly.WxPay
{
    /// <summary>
    /// setbasic 的摘要说明
    /// </summary>
    public class setbasic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            kyfly.BLL.jichu_zidianbiao bll = new kyfly.BLL.jichu_zidianbiao(context);
            kyfly.BLL.jichu_zidianbiao bllzd = new kyfly.BLL.jichu_zidianbiao();
            DataSet dd = new DataSet();
            DataSet dt = new DataSet();
            context.Response.ContentType = "text/plain";

            mycommonClass mycommonClassobj = new mycommonClass();
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            if (context.Request.QueryString["type"] == "edit")//获取编辑信息
            {
                if (context.Request.QueryString["Id"] != null)
                {
                    string strret = bll.Geteditdata(context.Request.QueryString["Id"].ToString());//aa1
                                                                                                  
                    context.Response.Write(strret);
                }
            }
            else if (context.Request.QueryString["type"] == "del")//删除信息
            {
                if (context.Request.QueryString["Id"] == null)
                    return;
                string Id = context.Request.QueryString["Id"].ToString();
                bll.Delete(int.Parse(Id));
            }
            else if (context.Request.QueryString["type"] == "combox")//保存修改或添加 信息 aa2
            {

            }

            else if (context.Request.QueryString["type"] == "save")//保存修改或添加 
            {
                string ziduanmingcheng = context.Request.QueryString["ziduanmingcheng"];
                string ziduanzhi = context.Request.QueryString["ziduanzhi"];
                string leibie = context.Request.QueryString["leibie"];
                string fenleibie = context.Request.QueryString["fenleibie"];


                if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                {
                    bll.Update(context.Request.QueryString["Id"].ToString(), ziduanmingcheng, ziduanzhi, leibie, fenleibie);
                }
                else
                {
                    bll.Add(ziduanmingcheng, ziduanzhi, leibie, fenleibie);
                }

                context.Response.Write("true");
            }
            else if (Convert.ToString(context.Request.Form["action"]) == "query")
            {
            
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
                string strret = bll.GetListByPageColumns_tojson("Id,字段名称,字段值,类别,分类别", "", "Id", 1);
                writeFile("S");
                writeFile(strret);
                context.Response.Write(strret);
                return;
            }
            else if (context.Request.QueryString["action"] == "query")
            {
                string type = "";
                string pagestr = "";
                if (context.Request.QueryString["fenleibie"] != null)
                {
                    type = context.Request.QueryString["fenleibie"].ToString();
                    pagestr= "分类别='" + type + "'";
                    string strret = bll.GetListByPageColumns_tojson("Id,字段名称,字段值,类别,分类别", pagestr, "Id", 1);
                    writeFile("S");
                    writeFile(strret);
                    context.Response.Write(strret);
                    return;
                }
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
            }
            else
            {

            }
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