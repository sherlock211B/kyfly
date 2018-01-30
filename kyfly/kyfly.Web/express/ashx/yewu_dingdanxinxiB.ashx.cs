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
    /// 文件说明：业务_订单信息表
    /// 开发者：
    /// 最后编辑时间：
    /// 编辑说明：
    /// </summary>
    public class yewu_dingdanxinxiB : baseashxClass
    {
        
        public override void ProcessRequest(HttpContext context)
        {
            kyfly.BLL.yewu_dingdanxinxiB bll= new kyfly.BLL.yewu_dingdanxinxiB(context); 
            base.ProcessRequest(context);
            context.Response.ContentType = "text/plain";

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
                    string shoujianrenshouJH = context.Request.QueryString["shoujianrenshouJH"];
                    string wupinleixing = context.Request.QueryString["wupinleixing"];
                    string zhanpian = context.Request.QueryString["zhanpian"];
                    string peisongdizhi = context.Request.QueryString["peisongdizhi"];
                    string zhuangtai = context.Request.QueryString["zhuangtai"];
                    string jiedanshijian = context.Request.QueryString["jiedanshijian"];
                    string qujianhao = context.Request.QueryString["qujianhao"];
                    string huojiahao = context.Request.QueryString["huojiahao"];
                    string huowuguizhongF = context.Request.QueryString["huowuguizhongF"];
                    string zhongliangleibie = context.Request.QueryString["zhongliangleibie"];
                    string peisongjine = context.Request.QueryString["peisongjine"];
                    string peisongrenshouJH = context.Request.QueryString["peisongrenshouJH"];
                    string peisongshijian = context.Request.QueryString["peisongshijian"];
                    string shenherenshouJH = context.Request.QueryString["shenherenshouJH"];
                    string dingdanxiuzheng = context.Request.QueryString["dingdanxiuzheng"];
                    string xiuzhengshijian = context.Request.QueryString["xiuzhengshijian"];
                    string zhifudanhao = context.Request.QueryString["zhifudanhao"];
                    string weixindingdh = context.Request.QueryString["weixindingdh"];
                    string pingjia = context.Request.QueryString["pingjia"];
                    string beizhu = context.Request.QueryString["beizhu"];

                
                if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                {
                    bll.Update(context.Request.QueryString["Id"].ToString(),xingming,shoujianrenshouJH,wupinleixing,zhanpian,peisongdizhi,zhuangtai,jiedanshijian,qujianhao,huojiahao,huowuguizhongF,zhongliangleibie,peisongjine,peisongrenshouJH,peisongshijian,shenherenshouJH,dingdanxiuzheng,xiuzhengshijian,zhifudanhao,weixindingdh,pingjia,beizhu);
                }
                else
                {
                    bll.Add(xingming,shoujianrenshouJH,wupinleixing,zhanpian,peisongdizhi,zhuangtai,jiedanshijian,qujianhao,huojiahao,huowuguizhongF,zhongliangleibie,peisongjine,peisongrenshouJH,peisongshijian,shenherenshouJH,dingdanxiuzheng,xiuzhengshijian,zhifudanhao,weixindingdh,pingjia, beizhu);
                }

                context.Response.Write("true");
            }
            else if (Convert.ToString(context.Request.Form["action"]) == "query")
            {
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
                string strret = bll.GetListByPageColumns_tojson("Id,姓名,收件人手机号,物品类型,照片,配送地址,状态,接单时间,取件号,货架号,货物贵重否,重量类别,配送金额,配送人手机号,配送时间,审核人手机号,订单修正,修正时间,支付单号,微信订单号,评价,备注", pagestrif, "Id",0);
                //writeFile("S");
               // writeFile(strret);
                context.Response.Write(strret);
                return;
            }
            else if(context.Request.QueryString["action"] == "query")
            {
                string strret = bll.GetListByPageColumns_tojson("Id,姓名,收件人手机号,物品类型,照片,配送地址,状态,接单时间,取件号,货架号,货物贵重否,重量类别,配送金额,配送人手机号,配送时间,审核人手机号,订单修正,修正时间,支付单号,微信订单号,评价,备注", pagestrif, "Id",1);
                writeFile("S");
                writeFile(strret);
                context.Response.Write(strret);
            }

        }
        private void writeFile(string str)
        {
           // StreamWriter sw = new StreamWriter("C: \\Users\\dell\\Desktop\\日志.txt", true);
            StreamWriter sw = new StreamWriter("C:\\temp\\trytest.txt", true);
            sw.Write(str + "\r\n");
            sw.Close();
        }
    }
}