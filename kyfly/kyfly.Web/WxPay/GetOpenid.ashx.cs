using LitJson;
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
namespace WxPayAPI
{
    /// <summary>
    /// GetOpenid 的摘要说明
    /// </summary>
    public class GetOpenid : IHttpHandler
    {
        public string openid { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            writeFile("coming");

            string code = HttpContext.Current.Request.QueryString["code"];
            writeFile(code);

            WxPayData data = new WxPayData();


           
            data.SetValue("appid", WxPayConfig.APPID);
            writeFile(data.GetValue("appid").ToString());
            data.SetValue("secret", WxPayConfig.APPSECRET);
            writeFile(data.GetValue("secret").ToString());
            data.SetValue("js_code", code);
            writeFile(data.GetValue("js_code").ToString());
            data.SetValue("grant_type", "authorization_code");
            writeFile(data.GetValue("grant_type").ToString());
            string url = "https://api.weixin.qq.com/sns/jscode2session?" + data.ToUrl();
            writeFile(url);
            //请求url以获取数据  
            string result = HttpService.Get(url);

            Log.Debug(this.GetType().ToString(), "GetOpenidAndAccessTokenFromCode response : " + result);
            writeFile(result);
            //保存access_token，用于收货地址获取  
            JsonData jd = JsonMapper.ToObject(result);
            //access_token = (string)jd["access_token"];  

            //获取用户openid  
            openid = (string)jd["openid"];
            writeFile(openid);
            context.Response.Write(openid);//获取H5调起JS API参数  
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