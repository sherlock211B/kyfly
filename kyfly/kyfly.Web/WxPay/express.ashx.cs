using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace kyfly.WxPay
{
    /// <summary>
    /// express 的摘要说明
    /// </summary>
    public class express : IHttpHandler
    {

        public  void ProcessRequest(HttpContext context)
        {
            kyfly.BLL.yewu_dingdanxinxiB bll = new kyfly.BLL.yewu_dingdanxinxiB(context);
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
                writeFile("going");
                string xingming = context.Request.QueryString["xingming"];
                string shoujianrenshouJH = context.Request.QueryString["shoujianrenshouJH"];
                string wupinleixing = context.Request.QueryString["wupinleixing"];
                string zhanpian = context.Request.QueryString["zhanpian"];
                string peisongdizhi = context.Request.QueryString["peisongdizhi"];
                string zhuangtai = context.Request.QueryString["zhuangtai"];
                string jiedanshijian = DateTime.Now.Date.ToString();
                string qujianhao = context.Request.QueryString["qujianhao"];
                string huojiahao = context.Request.QueryString["huojiahao"];
                string huowuguizhongF = context.Request.QueryString["huowuguizhongF"];
                string zhongliangleibie = context.Request.QueryString["zhongliangleibie"];
                string peisongjine = context.Request.QueryString["peisongjine"];
                string peisongrenshouJH = context.Request.QueryString["peisongrenshouJH"];
                string peisongshijian = DateTime.Now.Date.ToString();
                string shenherenshouJH = context.Request.QueryString["shenherenshouJH"];
                string dingdanxiuzheng = context.Request.QueryString["dingdanxiuzheng"];
                string xiuzhengshijian = DateTime.Now.Date.ToString();
                string zhifudanhao = context.Request.QueryString["zhifudanhao"];
                string weixindingdh = context.Request.QueryString["weixindingdh"];
                string pingjia = context.Request.QueryString["pingjia"];
                string beizhu = context.Request.QueryString["beizhu"];
                writeFile(xingming);
                writeFile(shoujianrenshouJH);
                writeFile(peisongdizhi);
                writeFile(zhuangtai);
                writeFile(qujianhao);
                writeFile(huowuguizhongF);
                writeFile(zhongliangleibie);

                dd = bllzd.GetList("字段名称='" + wupinleixing + "'");
                zhanpian = dd.Tables[0].Rows[0]["字段值"].ToString();

                dd = bllzd.GetList("字段名称='"+ zhongliangleibie+"'");
                dt = bllzd.GetList("字段名称='" + huowuguizhongF+"'");
                string str = dd.Tables[0].Rows[0]["字段值"].ToString();
                float pricef = float.Parse(dd.Tables[0].Rows[0]["字段值"].ToString().Trim());
                float prices = float.Parse(dt.Tables[0].Rows[0]["字段值"].ToString().Trim());
                writeFile(pricef.ToString());
                writeFile(prices.ToString());
                peisongjine = (pricef * prices).ToString();
                writeFile(peisongjine);

                if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                {
                    bll.Update(context.Request.QueryString["Id"].ToString(), xingming, shoujianrenshouJH, wupinleixing, zhanpian, peisongdizhi, zhuangtai, jiedanshijian, qujianhao, huojiahao, huowuguizhongF, zhongliangleibie, peisongjine, peisongrenshouJH, peisongshijian, shenherenshouJH, dingdanxiuzheng, xiuzhengshijian, zhifudanhao, weixindingdh, pingjia, beizhu);
                }
                else
                {
                    bll.Add(xingming, shoujianrenshouJH, wupinleixing, zhanpian, peisongdizhi, zhuangtai, jiedanshijian, qujianhao, huojiahao, huowuguizhongF, zhongliangleibie, peisongjine, peisongrenshouJH, peisongshijian, shenherenshouJH, dingdanxiuzheng, xiuzhengshijian, zhifudanhao, weixindingdh, pingjia, beizhu);
                }

                context.Response.Write(peisongjine);
            }
            else if (Convert.ToString(context.Request.Form["action"]) == "query")
            {
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
                string strret = bll.GetListByPageColumns_tojson("Id,姓名,收件人手机号,物品类型,照片,配送地址,状态,接单时间,取件号,货架号,货物贵重否,重量类别,配送金额,配送人手机号,配送时间,审核人手机号,订单修正,修正时间,支付单号,微信订单号,评价,备注", "", "Id",0);
                writeFile("S");
                writeFile(strret);
                context.Response.Write(strret);
                return;
            }
            else if (context.Request.QueryString["action"] == "query")
            {
                string phone = "";
                string status = "";
                string strret= "";
                string statust = "";
                string pagestr = "";
                int Id = 0;
                if (context.Request.QueryString["phone"] != null && context.Request.QueryString["status"] != null)
                {
                    phone = context.Request.QueryString["phone"].ToString();
                    status = context.Request.QueryString["status"].ToString();
                    writeFile(phone);
                    writeFile(status);
                }
                if(status=="0")
                {
                    pagestr = "收件人手机号='" + phone + "'";
                    strret = bll.GetListByPageColumns_tojson("Id,姓名,收件人手机号,物品类型,照片,配送地址,状态,接单时间,取件号,货架号,货物贵重否,重量类别,配送金额,配送人手机号,配送时间,审核人手机号,订单修正,修正时间,支付单号,微信订单号,评价,备注", pagestr, "Id", 1);
                    strret = Regex.Replace(strret, "(?<=\"ZT\":\")[^\",]*", status);
                }
                else 
                {
                    if(context.Request.QueryString["Id"] != null)
                    {
                        Id = int.Parse(context.Request.QueryString["Id"].ToString());
                        pagestr = "Id=" + Id;

                        dd = bll.GetList(pagestr);
                        statust= dd.Tables[0].Rows[0]["状态"].ToString();

                        dt= bllzd.GetList("字段名称='" + statust + "'" + "and " + "类别='" + "状态" + "'");
                        status = dt.Tables[0].Rows[0]["字段值"].ToString();
                        strret = bll.GetListByPageColumns_tojson("Id,姓名,收件人手机号,物品类型,照片,配送地址,状态,接单时间,取件号,货架号,货物贵重否,重量类别,配送金额,配送人手机号,配送时间,审核人手机号,订单修正,修正时间,支付单号,微信订单号,评价,备注", pagestr, "Id", 1);
                        strret = Regex.Replace(strret, "(?<=\"ZT\":\")[^\",]*", status);
                    }
                    else
                    {
                        dd = bllzd.GetList("字段值='" + status + "'" + "and " + "类别='" + "状态" + "'");
                        statust = dd.Tables[0].Rows[0]["字段名称"].ToString();
                        writeFile(statust);
                        pagestr = "收件人手机号='" + phone + "'" + "and " + "状态='" + statust + "'";
                         writeFile(pagestr);
                        strret = bll.GetListByPageColumns_tojson("Id,姓名,收件人手机号,物品类型,照片,配送地址,状态,接单时间,取件号,货架号,货物贵重否,重量类别,配送金额,配送人手机号,配送时间,审核人手机号,订单修正,修正时间,支付单号,微信订单号,评价,备注", pagestr, "Id", 1);
                        strret = Regex.Replace(strret, "(?<=\"ZT\":\")[^\",]*", status);
                        writeFile(strret);
                    }

                }
                
               writeFile("S");
               writeFile(strret);
               context.Response.Write(strret);
            }
            else if (context.Request.QueryString["type"] == "compute")
            {
                string huowuguizhongF = context.Request.QueryString["huowuguizhongF"];
                string zhongliangleibie = context.Request.QueryString["zhongliangleibie"];

                dd = bllzd.GetList("字段名称='" + zhongliangleibie + "'");
                dt = bllzd.GetList("字段名称='" + huowuguizhongF + "'");
              
                float pricef = float.Parse(dd.Tables[0].Rows[0]["字段值"].ToString().Trim());
                float prices = float.Parse(dt.Tables[0].Rows[0]["字段值"].ToString().Trim());
                writeFile(pricef.ToString());
                writeFile(prices.ToString());
                string peisongjine = (pricef * prices).ToString();
                context.Response.Write(peisongjine);
            }

        }
        private void writeFile(string str)
        {
            StreamWriter sw = new StreamWriter("C: \\Users\\dell\\Desktop\\日志.txt", true);
            //StreamWriter sw = new StreamWriter("C:\\temp\\trytest.txt", true);
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