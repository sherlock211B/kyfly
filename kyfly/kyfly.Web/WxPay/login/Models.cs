using System;
using System.Collections.Generic;
using System.Web;

namespace WxPayAPI.login
{
    public class Models
    {
    }
    public class userInfo
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string unionId { get; set; }
        
        public string phoneNumber { get; set; }
        public string purePhoneNumber { get; set; }
        public string countryCode { get; set; }
        public data watermark { get; set; }
    }
     
    /// <summary>  
    /// 微信用户数据水印  
    /// </summary>  
    public class data
    {
        public string appid { get; set; }
        public string timestamp { get; set; }
    }
    /// <summary>  
    /// 微信小程序验证返回结果  
    /// </summary>  
    public class result
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
    
}