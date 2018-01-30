using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Web.SessionState;
using System.IO;

namespace kyfly.ashx
{
    /// <summary>
    /// 生成代码时间： ljxtime
    /// 文件说明：基础_人员信息表
    /// 开发者：
    /// 最后编辑时间：
    /// 编辑说明：
    /// </summary>
    public class jichu_renyuanxinxiB : baseashxClass
    {
       
        public override void ProcessRequest(HttpContext context)
        {
            kyfly.BLL.jichu_renyuanxinxiB bll = new kyfly.BLL.jichu_renyuanxinxiB(context); 
            base.ProcessRequest(context);
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
                string zhuangtai = context.Request.QueryString["zhuangtai"];
                string leibie = context.Request.QueryString["leibie"];
                string quanxian = context.Request.QueryString["quanxian"];
                string huiyuanjibie = context.Request.QueryString["huiyuanjibie"];
                string peisongcishu = context.Request.QueryString["peisongcishu"];
                string beizhu = context.Request.QueryString["beizhu"];


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
            else if (context.Request.Form["action"] != null&& Convert.ToString(context.Request.Form["action"]) == "query")
            {
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
                string strret = bll.GetListByPageColumns_tojson("Id,姓名,手机号,OpenId,密码,地址,状态,类别,权限,会员级别,配送次数,备注", pagestrif, "Id",0);
                //writeFile("S");
                //writeFile(strret);
                context.Response.Write(strret);
                return;
            }
            else if(context.Request.QueryString["type"]=="read")
            {
                string strret = bll.GetListByPageColumns_tojson("Id,姓名,手机号,OpenId,密码,地址,状态,类别,权限,会员级别,配送次数,备注", pagestrif, "Id",0);
                //writeFile("S");
               // writeFile(strret);
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
    }
}