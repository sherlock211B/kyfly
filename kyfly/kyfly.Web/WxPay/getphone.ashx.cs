using kyfly;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using WxPayAPI.login;
using System.Web.SessionState;
using kyfly.ashx;

namespace kyfly.WxPay
{
    /// <summary>
    /// getphone 的摘要说明
    /// </summary>
    public class getphone : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string iv = "";
            string encryptedData = "";
            string threedsession = "";
            string value = "";
            string openid = "";
            string[] arr = new string[10];
            try
            {

                iv = HttpContext.Current.Request.QueryString["iv"].ToString();
                encryptedData = HttpContext.Current.Request.QueryString["encryptedData"].ToString();
                threedsession = HttpContext.Current.Request.QueryString["threedsession"].ToString();
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }

            writeFile(iv);
            writeFile(encryptedData);
            writeFile(threedsession);
            if (!string.IsNullOrEmpty(threedsession))
            {
                writeFile("1");
                GetUsersHelper GetUsersHelper = new GetUsersHelper();

                writeFile("cooming");

                value = threedsession.ToString();
                writeFile(value);
                arr = value.Split('|');
                openid = arr[0];
                writeFile(openid);
                value = arr[1];
                writeFile(value);

                GetUsersHelper.AesIV = iv;
                GetUsersHelper.AesKey = value;
                writeFile(value);
                string result = GetUsersHelper.AESDecrypt(encryptedData);
                writeFile(result);

                //存储用户数据  
                JObject _usrInfo = (JObject)JsonConvert.DeserializeObject(result);

                userInfo userInfo = new userInfo();
                userInfo.phoneNumber = _usrInfo["phoneNumber"].ToString();   //用户绑定的手机号
                userInfo.purePhoneNumber = _usrInfo["purePhoneNumber"].ToString(); //没有区号的手机号
                userInfo.countryCode = _usrInfo["countryCode"].ToString();  //区号

                object watermark = _usrInfo["watermark"].ToString();
                object appid = _usrInfo["watermark"]["appid"].ToString();
                object timestamp = _usrInfo["watermark"]["timestamp"].ToString();

                writeFile(userInfo.phoneNumber);
                writeFile(userInfo.purePhoneNumber);
                writeFile(userInfo.countryCode);
                

                context.Response.Write(result);
            }
            else
            {
                writeFile("threesession null");
                context.Response.Write("threesession null");
            }
        
        }
       private void writeFile(string str)
       {
             StreamWriter sw = new StreamWriter("C:\\temp\\trytest.txt", true);
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