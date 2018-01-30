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
    /// 文件说明：业务_退费理赔改价表
    /// 开发者：
    /// 最后编辑时间：
    /// 编辑说明：
    /// </summary>
    public class yewu_tuifeilipeiGJB : baseashxClass
    {
        
        public override void ProcessRequest(HttpContext context)
        {
            kyfly.BLL.yewu_tuifeilipeiGJB bll = new kyfly.BLL.yewu_tuifeilipeiGJB(context); 
            base.ProcessRequest(context);
            context.Response.ContentType = "text/plain";

            mycommonClass mycommonClassobj = new mycommonClass();
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            if (context.Request.QueryString["type"] == "edit")//获取编辑信息
            {
                if (context.Request.QueryString["Id"] != null)
                {
                    string strret = bll.Geteditdata(context.Request.QueryString["Id"].ToString());//aa1
                    //writeFile("f");
                    //writeFile(strret);
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
                        if (context.Request.QueryString["comboxname"] == "dingdanbiaoId")
                        {
                            kyfly.BLL.yewu_dingdanxinxiB bllzd = new kyfly.BLL.yewu_dingdanxinxiB();
                            string strret = bllzd.GetListByColumn_tojson("Id","1=1","");
                            context.Response.Write(strret);
                        }


            }
             
            else if (context.Request.QueryString["type"] == "save")//保存修改或添加 
            {
                    string xingming = context.Request.QueryString["xingming"];
                    string shoujianrenshouJH = context.Request.QueryString["shoujianrenshouJH"];
                    string OpenId = context.Request.QueryString["OpenId"];
                    string dizhi = context.Request.QueryString["dizhi"];
                    string zhuangtai = context.Request.QueryString["zhuangtai"];
                    string peisongrenshouJH = context.Request.QueryString["peisongrenshouJH"];
                    string peisongshijian = context.Request.QueryString["peisongshijian"];
                    string shenherenshouJH = context.Request.QueryString["shenherenshouJH"];
                    string shenhejieguo = context.Request.QueryString["shenhejieguo"];
                    string jine = context.Request.QueryString["jine"];
                    string tuikuanjine = context.Request.QueryString["tuikuanjine"];
                    string leibie = context.Request.QueryString["leibie"];
                    string dingdanbiaoId = context.Request.QueryString["dingdanbiaoId"];
                    string zhifudanhao = context.Request.QueryString["zhifudanhao"];
                    string weixindingdh = context.Request.QueryString["weixindingdh"];
                    string tuikuandanhao = context.Request.QueryString["tuikuandanhao"];
                    string beizhu = context.Request.QueryString["beizhu"];

                
                if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                {
                    bll.Update(context.Request.QueryString["Id"].ToString(),xingming,shoujianrenshouJH,OpenId,dizhi,zhuangtai,peisongrenshouJH,peisongshijian,shenherenshouJH,shenhejieguo,jine,tuikuanjine,leibie,dingdanbiaoId,zhifudanhao,weixindingdh,tuikuandanhao, beizhu);
                }
                else
                {
                    bll.Add(xingming,shoujianrenshouJH,OpenId,dizhi,zhuangtai,peisongrenshouJH,peisongshijian,shenherenshouJH,shenhejieguo,jine,tuikuanjine,leibie,dingdanbiaoId,zhifudanhao,weixindingdh,tuikuandanhao,beizhu);
                }

                context.Response.Write("true");
            }
            else if (Convert.ToString(context.Request.Form["action"]) == "query")
            {
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
                string strret = bll.GetListByPageColumns_tojson("Id,姓名,收件人手机号,OpenId,地址,状态,配送人手机号,配送时间,审核人手机号,审核结果,金额,退款金额,类别,订单表Id,支付单号,微信订单号,退款单号,备注", pagestrif, "Id",0);
                //writeFile("f");
                //writeFile(strret);
                context.Response.Write(strret);
                return;
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