using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Web.SessionState;//第一步：导入此命名空间

namespace kyfly.ashx
{
    public class baseashxClass: IHttpHandler, IRequiresSessionState//第二步：实现接口
    {
        public string pagestrif = "";   //不同用户对应本页面读取或更新信息时对应的内容权限级别，如： (orgid like '101%')
        //mycommonClass mycommonClassobj = new mycommonClass();
        public virtual void ProcessRequest(HttpContext context)
        {
            //context.Request.RawUrl:/ashx/loginout.ashx
            //context.Request.UrlReferrer.Query:?time=2015/8/28%207:39:44
            //设置功能权限，如果有某些功能权限，显示隐藏某些按钮

            //如果没有权限，则直接退出。 pageload
            //if (context.Request.QueryString["pageload"] == null)
            //    return;
 
            //设置内容权限，读取内容权限，保留在本页中。
            string str1 = context.Request.UrlReferrer.LocalPath;
            if (str1.Length > 3)
                str1 = str1.Substring(1);
            pagestrif = getstrif(str1, context);
            

        }

        private string getstrif(string pagename,HttpContext context)
        {

            string retstr = "";
            Dictionary<string, string> pageif;
            if (context.Session["pageif"] == null)
                pageif = new Dictionary<string, string>();
            else
            {
                pageif = (Dictionary<string, string>)context.Session["pageif"];
            }
            foreach (KeyValuePair<string, string> kvp in pageif)//如果已经存在，则读出页面的条件
            {
                if (kvp.Key == pagename)
                {
                    retstr = kvp.Value;
                    return retstr;
                }
            }
            if(retstr == "")//如果不存在，则读取条件，并加入到session中。
            {
                userinfo userobj = (userinfo)context.Session["userobj"];
                DataSet ds0 = new DataSet();
                ds0 = ljxpower.Common.DbHelperMySQL.Query("select distinct  BtnCode   from V_Tb_RolesAndNavigation  where (RolesId in (select RolesId from Tb_RolesAddUser where UserId='" + userobj.userid + "')) and  LinkAddress like '" + pagename + "%'  and ( BtnCode not like 'ljxfun%')  order by BtnCode desc");
                if (userobj.logincount == "admin")//表示admin权限，条件永远为真。
                    retstr = "";
                else if (ds0.Tables[0].Rows.Count <= 0)//表示没有权限，条件永远为假。
                    retstr = "(1>2)";
                else
                {
                    //编号说明：共10位，采用2-2-3-3，第一层从11开始，第二层从01，第三层从001，第四层001；编号从左到右编，不足在右边补0.
                    //
                    if (ds0.Tables[0].Rows[0]["BtnCode"].ToString().Trim() == "0" )//设置为全部读的权限。
                        retstr = "";
                    else if (ds0.Tables[0].Rows[0]["BtnCode"].ToString().Trim() == "1")
                        retstr = " (orgid like " + userobj.userid.Substring(0, 2) + "%') ";
                    else if (ds0.Tables[0].Rows[0]["BtnCode"].ToString().Trim() == "2")
                        retstr = " (orgid like " + userobj.userid.Substring(0, 4) + "%') ";
                    else if (ds0.Tables[0].Rows[0]["BtnCode"].ToString().Trim() == "3")
                        retstr = " (orgid like " + userobj.userid.Substring(0, 7) + "%') ";
                    else if (ds0.Tables[0].Rows[0]["BtnCode"].ToString().Trim() == "4")
                        retstr = " (orgid like " + userobj.userid + "%') ";
                }
                pageif.Add(pagename, retstr);
                context.Session["pageif"] = pageif;
            }
             
            return retstr;
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