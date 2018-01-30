using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using WxPayAPI.login;

namespace WxPayAPI
{
    /// <summary>
    /// register 的摘要说明
    /// </summary>
    public class register : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            writeFile("enter");
            string code = "";
            string iv = "";
            string encryptedData = "";
            string threedsession = "";
            try
            {
                code = HttpContext.Current.Request.QueryString["code"].ToString();
                writeFile("nice");
                writeFile(code);
                iv = HttpContext.Current.Request.QueryString["iv"].ToString();
                encryptedData = HttpContext.Current.Request.QueryString["encryptedData"].ToString();
                //threedsession = HttpContext.Current.Request.QueryString["threedsession"].ToString();         
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
            writeFile(code);
            string Appid = "wx2e67c9b918d371a4";
            string Secret = "84060efaf50e9fa85a62963a36dbb4b2";
            string grant_type = "authorization_code";

            //向微信服务端 使用登录凭证 code 获取 session_key 和 openid   
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + Appid + "&secret=" + Secret + "&js_code=" + code + "&grant_type=" + grant_type;
            string type = "utf-8";
            writeFile(url);
            GetUsersHelper GetUsersHelper = new GetUsersHelper();
            string j = GetUsersHelper.GetUrltoHtml(url, type);//获取微信服务器返回字符串  
            writeFile(j);

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
                //GetUsersHelper GetUsers = new GetUsersHelper();
                // GetUsers.SetSession(threedsession, res.openid + "|" + res.session_key);
                SetCookie(threedsession, res.openid + "|" + res.session_key, 1);  //写code 保存到cookies
                //context.Response.Write(res.openid);
                //context.Response.Write("fail");
               // context.Response.Write(threedsession);
            }
            catch (Exception)
            {
                //微信服务器验证失败  
                res.errcode = jo["errcode"].ToString();
                res.errmsg = jo["errmsg"].ToString();
            }
            if (!string.IsNullOrEmpty(res.openid))
            {
                //用户数据解密  
                GetUsersHelper.AesIV = iv;
                GetUsersHelper.AesKey = res.session_key;

                string result = GetUsersHelper.AESDecrypt(encryptedData);
                writeFile(result);

                //存储用户数据  
                JObject _usrInfo = (JObject)JsonConvert.DeserializeObject(result);

                userInfo userInfo = new userInfo();
                userInfo.openId = _usrInfo["openId"].ToString();

                try //部分验证返回值中没有unionId  
                {
                    userInfo.unionId = _usrInfo["unionId"].ToString();
                }
                catch (Exception)
                {
                    userInfo.unionId = "unionId";
                }

                userInfo.nickName = _usrInfo["nickName"].ToString();
                userInfo.gender = _usrInfo["gender"].ToString();
                userInfo.city = _usrInfo["city"].ToString();
                userInfo.province = _usrInfo["province"].ToString();
                userInfo.country = _usrInfo["country"].ToString();
                userInfo.avatarUrl = _usrInfo["avatarUrl"].ToString();

                object watermark = _usrInfo["watermark"].ToString();
                object appid = _usrInfo["watermark"]["appid"].ToString();
                object timestamp = _usrInfo["watermark"]["timestamp"].ToString();
                writeFile(userInfo.nickName);
                writeFile(userInfo.city);
                writeFile(userInfo.province);
                writeFile(userInfo.country);
                context.Response.Write(result);
                

            }
            else
            {
                context.Response.Write(j);
            }
        }
        /// <summary>
        /// 设置cookies
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static void SetCookie(string name, string value, int time)
        {
            HttpCookie cookies = new HttpCookie(name);
            cookies.Name = name;
            cookies.Value = value;
            cookies.Expires = DateTime.Now.AddDays(time);
            HttpContext.Current.Response.Cookies.Add(cookies);

        }


        /// <summary>
        /// 删除cookies
        /// </summary>
        /// <param name="name"></param>
        public static void delCookies(string name)
        {
            foreach (string cookiename in HttpContext.Current.Request.Cookies.AllKeys)
            {
                HttpCookie cookies = HttpContext.Current.Request.Cookies[name];
                if (cookies != null)
                {
                    cookies.Expires = DateTime.Today.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookies);
                    HttpContext.Current.Request.Cookies.Remove(name);
                }
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