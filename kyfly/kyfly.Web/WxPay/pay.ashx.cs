using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace WxPayAPI
{
    /// <summary>
    /// pay 的摘要说明
    /// </summary>
    public class pay : IHttpHandler
    {

        //public WxPayData unifiedOrderResult { get; set; }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            writeFile("coming2");
            string openid = HttpContext.Current.Request.QueryString["openid"];
            string total_fee = HttpContext.Current.Request.QueryString["jiage"];
            string goods = HttpContext.Current.Request.QueryString["wupinglx"];
            string huowuguizhongF = HttpContext.Current.Request.QueryString["huowuguizhongF"];
            string qujianhao = HttpContext.Current.Request.QueryString["qujianhao"];
            string huojiahao = HttpContext.Current.Request.QueryString["huojiahao"];
            string zhongliangleibie = HttpContext.Current.Request.QueryString["zhongliangleibie"];
            string name = HttpContext.Current.Request.QueryString["shouhuoren"];
            string tel = HttpContext.Current.Request.QueryString["tel"];
            string kuaididizhi = HttpContext.Current.Request.QueryString["kuaididizhi"];

            JsApiPay jsApiPay = new JsApiPay(context);
            jsApiPay.openid = openid;
            writeFile(jsApiPay.openid);
            jsApiPay.goods = goods;
            writeFile(jsApiPay.goods.ToString());
            jsApiPay.total_fee = int.Parse(total_fee);
            double price = jsApiPay.total_fee / 100.0;


            writeFile(jsApiPay.total_fee.ToString());
            
            //统一下单
            WxPayData data = new WxPayData();
            data.SetValue("body", goods);
            writeFile(data.GetValue("body").ToString());
            data.SetValue("attach", goods);
            writeFile(data.GetValue("attach").ToString());
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());

            string out_trade_no = data.GetValue("out_trade_no").ToString();
            writeFile(data.GetValue("out_trade_no").ToString());
            data.SetValue("total_fee", total_fee);
            writeFile(data.GetValue("total_fee").ToString());
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            writeFile(data.GetValue("time_start").ToString());
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            writeFile(data.GetValue("time_expire").ToString());
            data.SetValue("goods_tag", goods);
            writeFile(data.GetValue("goods_tag").ToString());
            data.SetValue("trade_type", "JSAPI");
            writeFile(data.GetValue("trade_type").ToString());
            data.SetValue("openid", openid);
            writeFile(data.GetValue("openid").ToString());
            WxPayData result = UnifiedOrder(data);

            writeFile("over...");
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
                throw new WxPayException("UnifiedOrder response error!");
            }
            writeFile("overing");
            kyfly.BLL.yewu_dingdanxinxiB bll = new kyfly.BLL.yewu_dingdanxinxiB(context);
            string jiedanshijian = DateTime.Now.Date.ToString();
            string peisongshijian = DateTime.Now.Date.ToString();
            string xiuzhengshijian = DateTime.Now.Date.ToString();
          


            WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();//此处有问题！
            
            context.Response.Write(jsApiPay.GetJsApiParameters());//获取H5调起JS API参数  

            bll.Add(name, tel, goods, "", kuaididizhi, "待付款", jiedanshijian, qujianhao, huojiahao, huowuguizhongF, zhongliangleibie, price.ToString(), "", peisongshijian, "", "", xiuzhengshijian, out_trade_no, "", "", "");
        }
        public  WxPayData UnifiedOrder(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new WxPayException("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new WxPayException("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new WxPayException("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new WxPayException("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new WxPayException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new WxPayException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                writeFile("url");
                inputObj.SetValue("notify_url", WxPayConfig.NOTIFY_URL);//异步通知url
                writeFile(inputObj.GetValue("notify_url").ToString());
            }

            inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            writeFile(inputObj.GetValue("appid").ToString());
            inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            writeFile(inputObj.GetValue("mch_id").ToString());
            inputObj.SetValue("spbill_create_ip", WxPayConfig.IP);//终端ip	  
            writeFile(inputObj.GetValue("spbill_create_ip").ToString());
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            writeFile(inputObj.GetValue("nonce_str").ToString());
            //签名
            inputObj.SetValue("sign", inputObj.MakeSign());
            writeFile(inputObj.GetValue("sign").ToString());
            string xml = inputObj.ToXml();
            writeFile(xml);

            var start = DateTime.Now;

            Log.Debug("WxPayApi", "UnfiedOrder request : " + xml);
            string response = HttpService.Post(xml, url, false, timeOut);
            Log.Debug("WxPayApi", "UnfiedOrder response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);
            writeFile(response);
            WxPayData result = new WxPayData();
            result.FromXml(response);
            
            //ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }

        private void writeFile(string str)
        {
            StreamWriter sw = new StreamWriter("C:\\temp\\mytest.txt", true);
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