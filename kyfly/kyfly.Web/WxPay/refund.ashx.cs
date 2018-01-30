using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Text;

namespace WxPayAPI
{
    /// <summary>
    /// refund 的摘要说明
    /// </summary>
    public class refund : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string transaction_id = HttpContext.Current.Request.QueryString["transaction_id"];
            string out_trade_no = HttpContext.Current.Request.QueryString["out_trade_no"];
            string total_fee = HttpContext.Current.Request.QueryString["total_fee"];
            string refund_fee = HttpContext.Current.Request.QueryString["refund_fee"];
            Log.Info(this.GetType().ToString(), "transaction_id : " + transaction_id);
            string result = Run(transaction_id, out_trade_no, total_fee, refund_fee);



            context.Response.Write("true");

        }
        public  string Run(string transaction_id, string out_trade_no, string total_fee, string refund_fee)
        {
       
            Log.Info("Refund", "Refund is processing...");
            Log.Info(this.GetType().ToString(), "transaction_id : " + transaction_id);
            Log.Info(this.GetType().ToString(), "out_trade_no : " + out_trade_no);
            Log.Info(this.GetType().ToString(), "total_fee : " + total_fee);
            Log.Info(this.GetType().ToString(), "refund_fee : " + refund_fee);
            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", out_trade_no);
            }

            data.SetValue("total_fee", int.Parse(total_fee));//订单总金额
      
            data.SetValue("refund_fee", int.Parse(refund_fee));//退款金额
         
            data.SetValue("out_refund_no", WxPayApi.GenerateOutTradeNo());//随机生成商户退款单号
     
            data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号
           
            WxPayData result = Refund(data);//提交退款申请给API，接收返回数据
            
            Log.Info("Refund", "Refund process complete, result : " + result.ToXml());
            return result.ToPrintStr();
        }




        public  WxPayData Refund(WxPayData inputObj, int timeOut = 6)
        {
        
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("退款申请接口中，out_trade_no、transaction_id至少填一个！");
            }
            else if (!inputObj.IsSet("out_refund_no"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数out_refund_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("refund_fee"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数refund_fee！");
            }
            else if (!inputObj.IsSet("op_user_id"))
            {
                throw new WxPayException("退款申请接口中，缺少必填参数op_user_id！");
            }
            Log.Info(this.GetType().ToString(), "out_refund_no : " + inputObj.GetValue("out_refund_no"));
            Log.Info(this.GetType().ToString(), "transaction_id : " + inputObj.GetValue("transaction_id"));
            Log.Info(this.GetType().ToString(), "total_fee : " + inputObj.GetValue("total_fee"));
            Log.Info(this.GetType().ToString(), "refund_fee : " + inputObj.GetValue("refund_fee"));
            Log.Info(this.GetType().ToString(), "op_user_id : " + inputObj.GetValue("op_user_id"));
            inputObj.SetValue("appid", WxPayConfig.APPID);//公众账号ID
            inputObj.SetValue("mch_id", WxPayConfig.MCHID);//商户号
            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();
          
            var start = DateTime.Now;
         
            Log.Debug("WxPayApi", "Refund request : " + xml);
            string response = Post(xml, url, true, timeOut);//调用HTTP通信接口提交数据到API
            Log.Debug("WxPayApi", "Refund response : " + response);
           
            var end = DateTime.Now;
          
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            //ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }



        public  bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }
        public  string Post(string xml, string url, bool isUseCert, int timeout)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;

                //设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = "text/xml";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;
                
                //是否使用证书
                if (isUseCert)
                {
                    
                    string path = HttpContext.Current.Request.PhysicalApplicationPath;
                    
 
                    X509Certificate2 cert = new X509Certificate2(path + WxPayConfig.SSLCERT_PATH, WxPayConfig.SSLCERT_PASSWORD, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
                    
                    request.ClientCertificates.Add(cert);
                    Log.Debug("WxPayApi", "PostXml used cert");
                }

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                
                Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                Log.Error("Exception message: {0}", e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                
                Log.Error("HttpService", e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Log.Error("HttpService", "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new WxPayException(e.ToString());
            }
            catch (Exception e)
            {
               
                Log.Error("HttpService", e.ToString());
                throw new WxPayException(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
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