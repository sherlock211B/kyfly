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
    /// wxlogin 的摘要说明
    /// </summary>
    public class wxlogin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            writeFile("enter");
            string code = "";
            //string iv = "";
            //string encryptedData = "";
            string threedsession = "";
            try
            {
                code = HttpContext.Current.Request.QueryString["code"].ToString();
                //iv = HttpContext.Current.Request.QueryString["iv"].ToString();
                //encryptedData = HttpContext.Current.Request.QueryString["encryptedData"].ToString();
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
                threedsession = WxPayApi.GenerateOutTradeNo();
                writeFile(res.openid);
                writeFile(res.session_key);
                writeFile(threedsession);
                SetCookie("threedsession", res.openid+ res.session_key,1);  //写code 保存到cookies
                //context.Response.Write(res.openid);
                context.Response.Write(threedsession);
            }
            catch (Exception)
            {
                //微信服务器验证失败  
                res.errcode = jo["errcode"].ToString();
                res.errmsg = jo["errmsg"].ToString();
            }
            //if (!string.IsNullOrEmpty(res.openid))
            //{
            //    //用户数据解密  
            //    GetUsersHelper.AesIV = iv;
            //    GetUsersHelper.AesKey = res.session_key;

            //    string result = GetUsersHelper.AESDecrypt(encryptedData);


            //    //存储用户数据  
            //    JObject _usrInfo = (JObject)JsonConvert.DeserializeObject(result);

            //    userInfo userInfo = new userInfo();
            //    userInfo.openId = _usrInfo["openId"].ToString();

            //    try //部分验证返回值中没有unionId  
            //    {
            //        userInfo.unionId = _usrInfo["unionId"].ToString();
            //    }
            //    catch (Exception)
            //    {
            //        userInfo.unionId = "unionId";
            //    }

            //    userInfo.nickName = _usrInfo["nickName"].ToString();
            //    userInfo.gender = _usrInfo["gender"].ToString();
            //    userInfo.city = _usrInfo["city"].ToString();
            //    userInfo.province = _usrInfo["province"].ToString();
            //    userInfo.country = _usrInfo["country"].ToString();
            //    userInfo.avatarUrl = _usrInfo["avatarUrl"].ToString();

            //    object watermark = _usrInfo["watermark"].ToString();
            //    object appid = _usrInfo["watermark"]["appid"].ToString();
            //    object timestamp = _usrInfo["watermark"]["timestamp"].ToString();


            //    #region  


            //    //创建连接池对象（与数据库服务器进行连接）  
            //    SqlConnection conn = new SqlConnection("server=127.0.0.1;database=Test;uid=sa;pwd=1");
            //    //打开连接池  
            //    conn.Open();
            //    //创建命令对象  
            //    string Qrystr = "SELECT * FROM WeChatUsers WHERE openId='" + userInfo.openId + "'";
            //    SqlCommand cmdQry = new SqlCommand(Qrystr, conn);
            //    object obj = cmdQry.ExecuteScalar();
            //    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            //    {
            //        string str = "INSERT INTO WeChatUsers ([UnionId] ,[OpenId],[NickName],[Gender],[City],[Province],[Country],[AvatarUrl],[Appid],[Timestamp],[Memo],[counts])VALUES('" + userInfo.unionId + "','" + userInfo.openId + "','" + userInfo.nickName + "','" + userInfo.gender + "','" + userInfo.city + "','" + userInfo.province + "','" + userInfo.country + "','" + userInfo.avatarUrl + "','" + appid.ToString() + "','" + timestamp.ToString() + "','来自微信小程序','1')";

            //        SqlCommand cmdUp = new SqlCommand(str, conn);
            //        // 执行操作  
            //        try
            //        {
            //            int row = cmdUp.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {
            //            context.Response.Write(ex.ToString());
            //        }
            //    }
            //    else
            //    {
            //        //多次访问，记录访问次数counts   更新unionId是预防最初没有，后期关联后却仍未记录  
            //        string str = "UPDATE dbo.WeChatUsers SET counts = counts+1，UnionId = '" + userInfo.unionId + "' WHERE OpenId='" + userInfo.openId + "'";
            //        SqlCommand cmdUp = new SqlCommand(str, conn);
            //        int row = cmdUp.ExecuteNonQuery();
            //    }

            //    //关闭连接池  
            //    conn.Close();
            //    #endregion

            //    //返回解密后的用户数据  
            //    context.Response.Write(result);
            //}
            //else
            //{
            //    context.Response.Write(j);
            //}
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