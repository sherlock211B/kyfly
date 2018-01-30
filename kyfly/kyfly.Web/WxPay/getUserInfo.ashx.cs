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

namespace WxPayAPI
{
    /// <summary>
    /// getUserInfo 的摘要说明
    /// </summary>
    public class getUserInfo : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            writeFile("getinfor");
            //string phone = "";
            string key = "";
            //string iv = "";
            //string encryptedData = "";
            string threedsession = "";
            string value = "";
            string openid = "";
            string[] arr = new string[10];
            try
            {
               // phone = HttpContext.Current.Request.QueryString["phone"].ToString();
               // key = HttpContext.Current.Request.QueryString["key"].ToString();
                //iv = HttpContext.Current.Request.QueryString["iv"].ToString();
                //encryptedData = HttpContext.Current.Request.QueryString["encryptedData"].ToString();
                threedsession = HttpContext.Current.Request.QueryString["threedsession"].ToString();
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
            //writeFile(phone);
           // writeFile(key);
            //writeFile(iv);
            //writeFile(encryptedData);
           // writeFile(threedsession);
            if (!string.IsNullOrEmpty(threedsession))
            {
                //writeFile("1");
                GetUsersHelper GetUsersHelper = new GetUsersHelper();
                //用户数据解密  

                //Session存储
                //writeFile("cooming");
               // SetSession(threedsession, "enter" + "|" + "will");
             
                if (GetSession(threedsession) != null)
                {
                    value = GetSession(threedsession).ToString();
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

                //value = threedsession.ToString();
                //writeFile(value);
                //arr = value.Split('|');
                //openid = arr[0];
                //writeFile(openid);
                //value = arr[1];
                //writeFile(value);

                //GetUsersHelper.AesIV = iv;
                //GetUsersHelper.AesKey = value;
                //writeFile(value);
                //string result = GetUsersHelper.AESDecrypt(encryptedData);
                //writeFile(result);

                //存储用户数据  
                //JObject _usrInfo = (JObject)JsonConvert.DeserializeObject(result);

                //userInfo userInfo = new userInfo();
                //userInfo.openId = _usrInfo["openId"].ToString();

                //try //部分验证返回值中没有unionId  
                //{
                //    userInfo.unionId = _usrInfo["unionId"].ToString();
                //}
                //catch (Exception)
                //{
                //    userInfo.unionId = "unionId";
                //}

                //userInfo.nickName = _usrInfo["nickName"].ToString();
                //userInfo.gender = _usrInfo["gender"].ToString();
                //userInfo.city = _usrInfo["city"].ToString();
                //userInfo.province = _usrInfo["province"].ToString();
                //userInfo.country = _usrInfo["country"].ToString();
                //userInfo.avatarUrl = _usrInfo["avatarUrl"].ToString();

                //object watermark = _usrInfo["watermark"].ToString();
                //object appid = _usrInfo["watermark"]["appid"].ToString();
                //object timestamp = _usrInfo["watermark"]["timestamp"].ToString();


                //writeFile(userInfo.nickName);
                //writeFile(userInfo.city);
                //writeFile(userInfo.province);
                //writeFile(userInfo.country);

                //context.Response.Write(result);
            }
            else
            {
                writeFile("threesession null");
                context.Response.Write("threesession null");
            }
        }
        private void writeFile(string str)
        {
            StreamWriter sw = new StreamWriter("C: \\Users\\dell\\Desktop\\日志.txt", true);
           // StreamWriter sw = new StreamWriter("C:\\temp\\trytest.txt", true);
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