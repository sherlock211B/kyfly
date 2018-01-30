using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace kyfly.WxPay
{
    /// <summary>
    /// address 的摘要说明
    /// </summary>
    public class address : IHttpHandler
    {

        public  void ProcessRequest(HttpContext context)
        {
            kyfly.BLL.jichu_renyuanxinxiB bll = new kyfly.BLL.jichu_renyuanxinxiB(context);
            
            context.Response.ContentType = "text/plain";

            // writeFile("begin");
            mycommonClass mycommonClassobj = new mycommonClass();
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            if (context.Request.QueryString["type"] == "edit")//获取编辑信息
            {
                if (context.Request.QueryString["Id"] != null)
                {
                    string strret = bll.Geteditdata(context.Request.QueryString["Id"].ToString());//aa1
                    writeFile("f");
                    writeFile(strret);
                    context.Response.Write(strret);
                }
            }
            else if (context.Request.QueryString["type"] == "del")//删除信息
            {
                if (context.Request.QueryString["Id"] == null)
                    return;
                string Id = context.Request.QueryString["Id"].ToString();
                bll.Delete(int.Parse(Id));
                context.Response.Write(1);
            }
            else if (context.Request.QueryString["type"] == "combox")//保存修改或添加 信息 aa2
            {

            }

            else if (context.Request.QueryString["type"] == "save")//保存修改或添加 
            {
                string xingming = context.Request.QueryString["xingming"];
                string shoujihao = context.Request.QueryString["shoujihao"];
                string OpenId = context.Request.QueryString["OpenId"];
                string mima = context.Request.QueryString["mima"];
                string dizhi = context.Request.QueryString["dizhi"];
                string xxdz = context.Request.QueryString["xxdz"];
                string zhuangtai = context.Request.QueryString["zhuangtai"];
                string leibie = context.Request.QueryString["leibie"];
                string quanxian = context.Request.QueryString["quanxian"];
                string huiyuanjibie = context.Request.QueryString["huiyuanjibie"];
                string peisongcishu = "0";
                string beizhu = context.Request.QueryString["beizhu"];
               writeFile(dizhi);
                dizhi = dizhi + "-" + xxdz;
                writeFile("S");
                writeFile(xingming);
                writeFile(shoujihao);
                writeFile(xxdz);
                writeFile(dizhi);
                if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                {
                    bll.Update(context.Request.QueryString["Id"].ToString(), xingming, shoujihao, OpenId, mima, dizhi, zhuangtai, leibie, quanxian, huiyuanjibie, peisongcishu, beizhu);
                }
                else
                {
                    bll.Add(xingming, shoujihao, OpenId, mima, dizhi, zhuangtai, leibie, quanxian, huiyuanjibie, peisongcishu, beizhu);
                }

                context.Response.Write("true");
            }
            else if (context.Request.Form["action"] != null && Convert.ToString(context.Request.Form["action"]) == "query")
            {
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
                string strret = bll.GetListByPageColumns_tojson("Id,姓名,手机号,OpenId,密码,地址,状态,类别,权限,会员级别,配送次数,备注", "", "Id", 0);
                //writeFile("S");
                //writeFile(strret);
                context.Response.Write(strret);
                return;
            }
            else if (context.Request.QueryString["action"] == "query")
            {
                string phone = "";
                string strret = "";
                if (context.Request.QueryString["phone"] != null)
                {
                    phone = context.Request.QueryString["phone"].ToString();
               
                    writeFile(phone);
    
                }
                string pagestr = "手机号='" + phone + "'";
                writeFile(pagestr);
                strret = bll.GetListByPageColumns_tojson("Id,姓名,手机号,OpenId,密码,地址,状态,类别,权限,会员级别,配送次数,备注", pagestr, "Id", 1);
                writeFile("S");
                writeFile(strret);
                context.Response.Write(strret);
             
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