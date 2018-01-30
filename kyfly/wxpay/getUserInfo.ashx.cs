using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using WxPayAPI.login;


namespace WxPayAPI
{
    /// <summary>
    /// getUserInfo 的摘要说明
    /// </summary>
    public class getUserInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            kyfly.BLL.jichu_renyuanxinxiB bll = new kyfly.BLL.jichu_renyuanxinxiB(context);
            context.Response.ContentType = "text/plain";
            writeFile("getinfor");
            string phone = "";
            string key = "";
            string iv = "";
            string encryptedData = "";
            string threedsession = "";
            string value = "";
            try
            {
                phone = HttpContext.Current.Request.QueryString["phone"].ToString();
                key = HttpContext.Current.Request.QueryString["key"].ToString();
                iv = HttpContext.Current.Request.QueryString["iv"].ToString();
                encryptedData = HttpContext.Current.Request.QueryString["encryptedData"].ToString();
                threedsession = HttpContext.Current.Request.QueryString["threedsession"].ToString();
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
            writeFile(phone);
            writeFile(key);
            writeFile(iv);
            writeFile(encryptedData);
            writeFile(threedsession);
            if (!string.IsNullOrEmpty(threedsession))
            {
                GetUsersHelper GetUsersHelper = new GetUsersHelper();
                //用户数据解密  
                if (HttpContext.Current.Request.Cookies["threedsession"] != null)
                {
                    value = HttpContext.Current.Request.Cookies["threedsession"].Value;

                }
                GetUsersHelper.AesIV = iv;
                GetUsersHelper.AesKey =value;

                string result = GetUsersHelper.AESDecrypt(encryptedData);


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

               bll.Add(userInfo.nickName,phone,userInfo.openId,key,"","","客户","","","","");
                //#region  


                ////创建连接池对象（与数据库服务器进行连接）  
                //SqlConnection conn = new SqlConnection("server=127.0.0.1;database=Test;uid=sa;pwd=1");
                ////打开连接池  
                //conn.Open();
                ////创建命令对象  
                //string Qrystr = "SELECT * FROM WeChatUsers WHERE openId='" + userInfo.openId + "'";
                //SqlCommand cmdQry = new SqlCommand(Qrystr, conn);
                //object obj = cmdQry.ExecuteScalar();
                //if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                //{
                //    string str = "INSERT INTO WeChatUsers ([UnionId] ,[OpenId],[NickName],[Gender],[City],[Province],[Country],[AvatarUrl],[Appid],[Timestamp],[Memo],[counts])VALUES('" + userInfo.unionId + "','" + userInfo.openId + "','" + userInfo.nickName + "','" + userInfo.gender + "','" + userInfo.city + "','" + userInfo.province + "','" + userInfo.country + "','" + userInfo.avatarUrl + "','" + appid.ToString() + "','" + timestamp.ToString() + "','来自微信小程序','1')";

                //    SqlCommand cmdUp = new SqlCommand(str, conn);
                //    // 执行操作  
                //    try
                //    {
                //        int row = cmdUp.ExecuteNonQuery();
                //    }
                //    catch (Exception ex)
                //    {
                //        context.Response.Write(ex.ToString());
                //    }
                //}
                //else
                //{
                //    //多次访问，记录访问次数counts   更新unionId是预防最初没有，后期关联后却仍未记录  
                //    string str = "UPDATE dbo.WeChatUsers SET counts = counts+1，UnionId = '" + userInfo.unionId + "' WHERE OpenId='" + userInfo.openId + "'";
                //    SqlCommand cmdUp = new SqlCommand(str, conn);
                //    int row = cmdUp.ExecuteNonQuery();
                //}

                ////关闭连接池  
                //conn.Close();
                //#endregion

                //返回解密后的用户数据  
                context.Response.Write(result);
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