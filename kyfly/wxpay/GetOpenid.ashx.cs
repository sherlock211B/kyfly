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
            //writeFile("coming");
            context.Response.Write("coming");
            //context.Response.Write("Hello World");
            string code = HttpContext.Current.Request.QueryString["code"];
            writeFile(code);
            WxPayData data = new WxPayData();
            data.SetValue("appid", WxPayConfig.APPID);
            data.SetValue("secret", WxPayConfig.APPSECRET);
            data.SetValue("code", code);
            data.SetValue("grant_type", "authorization_code");
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrl();
            writeFile(url);
            //请求url以获取数据  
            string result = HttpService.Get(url);
            writeFile(result);
            Log.Debug(this.GetType().ToString(), "GetOpenidAndAccessTokenFromCode response : " + result);

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