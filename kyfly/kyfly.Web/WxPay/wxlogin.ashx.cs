using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using WxPayAPI.login;
using System.Web.SessionState;


namespace WxPayAPI
{
    /// <summary>
    /// wxlogin 的摘要说明
    /// </summary>
    public class wxlogin : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            writeFile("enter");
            string code = "";
            string threedsession = "";
            string[] arr = new string[100];
            string openid = "";
            try
            {
                code = HttpContext.Current.Request.QueryString["code"].ToString();
                writeFile("nice");
                writeFile(code);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
            string Appid = "wx2e67c9b918d371a4";
            string Secret = "84060efaf50e9fa85a62963a36dbb4b2";
            string grant_type = "authorization_code";

            //向微信服务端 使用登录凭证 code 获取 session_key 和 openid   
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + Appid + "&secret=" + Secret + "&js_code=" + code + "&grant_type=" + grant_type;
            string type = "utf-8";
            writeFile(url);
            GetUsersHelper GetUsersHelper = new GetUsersHelper();
            string j = GetUsersHelper.GetUrltoHtml(url, type);//获取微信服务器返回字符串  


            //将字符串转换为json格式  
            JObject jo = (JObject)JsonConvert.DeserializeObject(j);
            writeFile(jo.ToString());

            result res = new result();
            try
            {
                //微信服务器验证成功  
                res.openid = jo["openid"].ToString();
                res.session_key = jo["session_key"].ToString();
                threedsession = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
                writeFile(res.openid);
                writeFile(res.session_key);
                writeFile(threedsession);

                //测试用
                // string threedsession= "425f364ba60247b5";
                //string openid = "obrIH0QPYFOYAREo9qZwpq69DxbQ";
                //string session_key = "UTokwQsvVFTQ7adPuwG1ow==";


                //Session存储
                // SetSession(threedsession, openid + "|" + session_key);
               // HttpContext.Current.Session["threedsession"] = threedsession;
                SetSession(threedsession, res.openid + "|" + res.session_key);
                if (GetSession(threedsession).ToString() != null)
                {
                    string value = GetSession(threedsession).ToString();
                    writeFile(value);
                    arr = value.Split('|');
                    openid = arr[0];
                    writeFile(openid);
                    value = arr[1];
                    writeFile(value);
                }
                else
                {
                    writeFile("以Session得openid fail");
                }
                context.Response.Write(threedsession);
            }
            catch (Exception)
            {
                //微信服务器验证失败  
                res.errcode = jo["errcode"].ToString();
                res.errmsg = jo["errmsg"].ToString();
            }
        }

      
        private void writeFile(string str)
        {
            StreamWriter sw = new StreamWriter("C: \\Users\\dell\\Desktop\\日志.txt", true);
            //StreamWriter sw = new StreamWriter("C:\\temp\\trytest.txt", true);
            sw.Write(str + "\r\n");
            sw.Close();
        }

        /// <summary>
        /// 根据session名获取session对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }
        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="name">session 名</param>
        /// <param name="val">session 值</param>
        public void SetSession(string name, object val)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, val);
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