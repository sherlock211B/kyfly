using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.SessionState;
using System.Data;

namespace kyfly.ashx
{
    /// <summary>
    /// UsersHandler 的摘要说明
    /// </summary>
    public class UsersHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ljxpower.BLL.com_user bll = new ljxpower.BLL.com_user();
            if (context.Request.QueryString["type"] == "pass")//修改密码
            {
                userinfo userobj = (userinfo)context.Session["userobj"];
                string Userid = userobj.userid;
                string pass = context.Request.QueryString["pass"];

                ljxpower.Model.com_user model = new ljxpower.Model.com_user();
                string myid = ljxpower.Common.DbHelperMySQL.getvalue("select id from com_user where userid='" + Userid + "'");
                    if (myid == "")
                        myid = "0";
                    model = bll.GetModel(int.Parse(myid));
                    model.password = ljxpower.Common.DESEncrypt.Encrypt(pass);
                    if (bll.Update1(model))
                    {
                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                 

            }
            if (context.Request.QueryString["type"] == "login")//修改密码
            {
                string Userid = context.Request.QueryString["userid"];
                string pass = context.Request.QueryString["pass"];

                context.Session.RemoveAll();
                string name = Userid.Replace("'", "").Replace(" ", "");
                pass = pass.Replace("'", "").Replace(" ", "");
                //string strwhere = "";
                //string username = "";
                pass = ljxpower.Common.DESEncrypt.Encrypt(pass);

                #region  login ....


                #endregion login....

            }
            if (context.Request.QueryString["type"] == "edit")//获取要编辑的用户信息
            {
                string Userid = context.Request.QueryString["Id"];
                DataSet ds = ljxpower.Common.DbHelperMySQL.Query("select RolesId,UserId from tb_rolesadduser where UserId = '" + Userid + "'");
                string IdList = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (IdList != "")
                        IdList += ",";
                    IdList += dr["RolesId"].ToString();
                }
                context.Response.Write(IdList);
            }
            else if (context.Request.QueryString["type"] == "role")//
            {

                string Userid = context.Request.QueryString["Userid"];
                string role = context.Request.QueryString["role"];
                if (Userid != null && Userid != "null" && Userid != "undefined")//
                    saveRole(Userid, role);
            }
            else if (Convert.ToString(context.Request.Form["action"]) == "query" || context.Request.Form["OrgId"] != null)
            {
                int row = int.Parse(context.Request["rows"].ToString());
                int page = int.Parse(context.Request["page"].ToString());
                string strorgid="";
                string strWhere = "";
                if (context.Request["OrgId"] != null)
                {
                    strorgid = kyfly.Common.DbHelperMySQL.getvalue("select OrgId from com_organization where   id=" + context.Request["OrgId"].ToString());
                    strorgid = strorgid.Replace("0"," ").TrimEnd();
                    strorgid = strorgid.Replace(" ","0");
                    strWhere = " orgid like '" + strorgid + "%'";
                }
                
                string retstr = "";
                retstr = bll.GetListByPageColumns_tojson("*", strWhere, "userid desc", row, page);

                context.Response.Write(retstr);
            }
        }

        public StringBuilder GetId(int ParentId, StringBuilder sb, List<ljxpower.Model.com_organization> list)
        {
            foreach (ljxpower.Model.com_organization item in list)
            {
                if (item.ParentId == ParentId)
                {
                    if (sb.Length != 0)
                        sb.Append(",");
                    sb.Append("'" + item.orgid + "'");
                    sb = GetId(item.Id, sb, list);
                }
            }
            return sb;
        }

        public bool saveRole(string Userid, string role)
        {
            List<string> list = new List<string>();
            string sql2 = "delete tb_rolesadduser where UserId='" + Userid + "'";
            list.Add(sql2);
            if (role != null && role != "")
            {
                string[] str = role.Split(',');
                foreach (string s in str)
                {
                    string sql = "insert into tb_rolesadduser(RolesId,UserId) values(" + s + ",'" + Userid + "')";
                    list.Add(sql);
                }
            }
            if (ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(list) > 0)
                return true;
            else
                return false;
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