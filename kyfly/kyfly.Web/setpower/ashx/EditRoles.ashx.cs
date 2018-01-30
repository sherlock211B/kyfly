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
    /// EditRoles 的摘要说明
    /// </summary>
    public class EditRoles : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string RoleId = "0";
            if (context.Request.QueryString["type"] == "edit")//获取角色信息
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                ljxpower.BLL.tb_roles bll = new ljxpower.BLL.tb_roles();
                ljxpower.Model.tb_roles model= bll.GetModel(Id);
                StringBuilder sb = new StringBuilder();
                sb.Append(model.RolesName+",");
                sb.Append(model.Remark+",");
                ljxpower.BLL.com_user ubll = new ljxpower.BLL.com_user();
                List<ljxpower.Model.com_user> list = ubll.GetModelList(" Userid in(select UserId from tb_rolesadduser where RolesId="+Id+")");
                foreach(ljxpower.Model.com_user item in list)
                {
                    sb.Append("<div onclick='ss(" + item.userid + ")' height='23px'><input type='hidden' value='" + item.userid + "' />" + item.password + "|" + item.username + "</div>");
                }
                context.Response.Write(sb.ToString());
            }
            else if (context.Request.QueryString["type"] == "getUser")//获取未绑定到当前角色的用户
            {
                StringBuilder sb = new StringBuilder();
                ljxpower.BLL.com_user ubll = new ljxpower.BLL.com_user();
                List<ljxpower.Model.com_user> list = new List<ljxpower.Model.com_user>();
                //if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                //{
                //    int Id = int.Parse(context.Request.QueryString["Id"]);
                //    list = ubll.GetModelList(" Userid not in(select UserId from tb_rolesadduser where RolesId=" + Id + ")");
                //}
                //else
                //{
                    list = ubll.GetModelList("");
               // }
                foreach (ljxpower.Model.com_user item in list)
                {
                    sb.Append("<div><input name=\"chkItem\" value=\"<div onclick='ss(" + item.userid + ")' height='23px'><input type='hidden' value='" + item.userid + "'  />" + item.password + "|" + item.username + "</div>\" type=\"checkbox\" /> ");
                    sb.Append(item.password + "|" + item.username + "</div>");
                }
                context.Response.Write(sb.ToString());
            }
            else if (context.Request.QueryString["type"] == "save")//保存角色信息
            {
                string name = context.Request.QueryString["name"];
                string remark = context.Request.QueryString["remark"];
                int Id = int.Parse(context.Request.QueryString["Id"]);
                ljxpower.BLL.tb_roles bll = new ljxpower.BLL.tb_roles();
                ljxpower.Model.tb_roles model = bll.GetModel(Id);
                model.Remark = remark;
                model.RolesName = name;
                context.Response.Write(bll.Update1(model));
            }
            else if (context.Request.QueryString["type"] == "add")//添加
            {
                string name = context.Request.QueryString["name"];
                string remark = context.Request.QueryString["remark"];
                ljxpower.Model.tb_roles model = new ljxpower.Model.tb_roles();
                ljxpower.BLL.tb_roles bll = new ljxpower.BLL.tb_roles();
                model.Remark = remark;
                model.RolesName = name;
                if(bll.Add1(model)>0)
                    context.Response.Write("true");
                else
                    context.Response.Write("false");

            }
            else if (context.Request.QueryString["type"] == "delRole")//删除角色
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                ljxpower.BLL.tb_roles bll = new ljxpower.BLL.tb_roles();
               
                ljxpower.BLL.tb_rolesadduser rbll = new ljxpower.BLL.tb_rolesadduser();
                rbll.Delete(Id);
                context.Response.Write( bll.Delete(Id));
            }
            else if (context.Request.QueryString["type"] == "Distri")//获取已分配的权限
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                ljxpower.BLL.tb_rolesandnavigation  bll = new ljxpower.BLL.tb_rolesandnavigation();
                List<ljxpower.Model.tb_rolesandnavigation> list = new List<ljxpower.Model.tb_rolesandnavigation>();
                list = bll.GetModelList(" RolesId=" + Id);
                StringBuilder sb = new StringBuilder();
                foreach (ljxpower.Model.tb_rolesandnavigation model in list)
                {
                    sb.Append(model.NavigationId + ",");
                }
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                context.Response.Write(sb.ToString());
            }
            else if (context.Request.QueryString["type"] == "saveDistri")
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                string nav = context.Request.QueryString["nav"];
                string[] str = nav.Split(',');
                List<string> list = new List<string>();
                string sql = "delete tb_rolesandnavigation where RolesId=" + Id;
                foreach (string ss in str)
                {
                    ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
                    ljxpower.Model.tb_navigation model = bll.GetModel(int.Parse(ss));
                    if (model.ParentId != 0)
                    {
                        string sql3 = "delete tb_rolesandnavigation where RolesId=" + Id + " and NavigationId=" + model.ParentId;
                        string sql1 = "insert into tb_rolesandnavigation (RolesId,NavigationId) values(" + Id + "," + model.ParentId + ")";
                        list.Add(sql3);
                        list.Add(sql1);
                    }
                    string sql2 = "insert into tb_rolesandnavigation (RolesId,NavigationId) values(" + Id + "," + ss + ")";
                    list.Add(sql2);
                }
                ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(list);
            }
            else if (context.Request.QueryString["type"] == "savenav")
            {
                RoleId = context.Request["RoleId"].ToString();
                string NavId = context.Request["NavId"].ToString();
                List<string> list = new List<string>();
                list.Add("delete from tb_rolesandnavigation where RolesId=" + RoleId);
                string[] str = NavId.Split('|');
                for (int i = 0; i < str.Length - 1; i++)
                {
                    string[] str2 = str[i].Split(',');
                    if (str2.Length == 2)
                        list.Add(" insert into tb_rolesandnavigation(RolesId,NavigationId,ButtonId) values(" + RoleId + "," + str2[0] + ",0)");
                    else
                    {
                        for (int j = 1; j < str2.Length - 1; j++)
                        {
                            list.Add(" insert into tb_rolesandnavigation(RolesId,NavigationId,ButtonId) values(" + RoleId + "," + str2[0] + "," + str2[j] + ")");
                        }
                    }
                }
                ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(list);
                context.Response.Write("Hello World");
            }
            else if (context.Request.QueryString["type"] == "query")
            {
                ljxpower.BLL.tb_roles bll = new ljxpower.BLL.tb_roles();
                List<ljxpower.Model.tb_roles> list = bll.GetModelList("");
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (ljxpower.Model.tb_roles item in list)
                {
                    sb.Append("{\"Id\":" + item.Id + ",");
                    sb.Append("\"Name\":\"" + item.RolesName + "\",");
                    sb.Append("\"Remark\":\"" + item.Remark + "\"},");
                }
                if (sb.Length > 1)
                    sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
                context.Response.Write(sb.ToString());
            }

           /////////////////////////////////////

            else if (context.Request["type"].ToString() == "ljxquery")
            {
                RoleId = context.Request["RoleId"].ToString();
                getdata(RoleId, context);
            }
            else if (context.Request["type"] == "pageload")
            {
                //设置内容权限，读取内容权限，保留在本页中。
                string str1 = context.Request.UrlReferrer.LocalPath;
                if (str1.Length > 3)
                    str1 = str1.Substring(1);
                string pageljxfun = getstrif(str1, context);
                context.Response.Write(pageljxfun);
                //context.Response.Write("OK");
                //return;
            }
            else if (context.Request["type"] == "ljxsave")
            {
                //设置内容权限，读取内容权限，保留在本页中。
                RoleId = context.Request["RoleId"].ToString();
                string NavId = context.Request["NavId"].ToString();
                List<string> list = new List<string>();
                list.Add("delete from tb_rolesandnavigation where RolesId=" + RoleId);
                string[] str = NavId.Split('|');
                for (int i = 0; i < str.Length - 1; i++)
                {
                    string[] str2 = str[i].Split(',');
                    if (str2.Length == 2)
                        list.Add(" insert into tb_rolesandnavigation(RolesId,NavigationId,ButtonId) values(" + RoleId + "," + str2[0] + ",0)");
                    else
                    {
                        for (int j = 1; j < str2.Length - 1; j++)
                        {
                            list.Add(" insert into tb_rolesandnavigation(RolesId,NavigationId,ButtonId) values(" + RoleId + "," + str2[0] + "," + str2[j] + ")");
                        }
                    }
                }
                ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(list);
                context.Response.Write("OK");
                //return;
            }
        }

        private void getdata(string RoleId, HttpContext context)
        {
            ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
            DataSet ds = bll.GetList("  IsShow=0 order by id");
            DataSet ds2 = ljxpower.Common.DbHelperMySQL.Query("select Id,ButtonName from Com_ButtonGroup order by Sort");
            DataSet ds4 = ljxpower.Common.DbHelperMySQL.Query("select RolesId,NavigationId,ButtonId  FROM V_tb_rolesandnavigation where RolesId=" + RoleId + " and  IsShow=0  order by NavigationId");
            DataSet ds3 = ljxpower.Common.DbHelperMySQL.Query("select NavigationId,ButtonId from Com_NavigationAndButton order by NavigationId ");
            //0菜单没有按钮,1菜单有按钮角色没按钮，2菜单有按钮并且角色也有按钮
            StringBuilder sb = new StringBuilder();
            string strId = "";
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                sb.Append(dr["ButtonName"] + ",");
                strId += dr["Id"] + ",";
            }
            sb.Append("|");
            sb.Append(strId + "|");
            foreach (DataRow dr in ds4.Tables[0].Rows)
            {
                sb.Append(dr["NavigationId"] + ",");
            }
            sb.Append("|");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = " ParentId=0";
                dv.Sort = "id";
                //dv.Sort = "Sort";
                for (int i = 0; i < dv.Count; i++)
                {
                    //DataView dv5 = new DataView(ds4.Tables[0]);
                    //dv5.RowFilter = " NavigationId=" + dv[i]["Id"];
                    //if (dv5.Count > 0) 
                    //    sb.Append("0,");
                    for (int k = 0; k < ds2.Tables[0].Rows.Count; k++)
                    {
                        sb.Append("0,");
                    }
                    sb.Append("|");
                    DataView dv2 = new DataView(ds.Tables[0]);
                    dv2.RowFilter = " ParentId=" + dv[i]["Id"];
                    dv2.Sort = " id";
                    if (dv2.Count > 0)
                    {
                        for (int j = 0; j < dv2.Count; j++)
                        {
                            //DataView dv6 = new DataView(ds4.Tables[0]);
                            //dv6.RowFilter = " NavigationId=" + dv2[j]["Id"];
                            //if (dv6.Count > 0)
                            //    sb.Append("1,");
                            //else
                            //    sb.Append("0,");
                            foreach (DataRow dr in ds2.Tables[0].Rows)
                            {
                                DataView dv3 = new DataView(ds3.Tables[0]);
                                dv3.RowFilter = " NavigationId=" + dv2[j]["Id"] + " and ButtonId=" + dr["Id"];
                                if (dv3.Count > 0)
                                {
                                    DataView dv4 = new DataView(ds4.Tables[0]);
                                    dv4.RowFilter = " NavigationId=" + dv2[j]["Id"] + " and ButtonId=" + dr["Id"];
                                    if (dv4.Count > 0)
                                        sb.Append("2,");
                                    else
                                        sb.Append("1,");
                                }
                                else
                                    sb.Append("0,");
                            }
                            sb.Append("|");
                        }
                    }

                }
            }
            context.Response.Write(sb.ToString());
        }

        private string getstrif(string pagename, HttpContext context)
        {

            string retstr = "";
            Dictionary<string, string> ljxfunpower;
            if (context.Session["ljxfunpower"] == null)
                ljxfunpower = new Dictionary<string, string>();
            else
            {
                ljxfunpower = (Dictionary<string, string>)context.Session["ljxfunpower"];
            }
            foreach (KeyValuePair<string, string> kvp in ljxfunpower)//如果已经存在，则读出页面的条件
            {
                if (kvp.Key == pagename)
                {
                    retstr = kvp.Value;
                    return retstr;
                }
            }
            if (retstr == "")//如果不存在，则读取条件，并加入到session中。
            {
                userinfo userobj = (userinfo)context.Session["userobj"];
                DataSet ds0 = new DataSet();
                ds0 = ljxpower.Common.DbHelperMySQL.Query("select distinct  BtnCode   from V_Tb_RolesAndNavigation  where (RolesId in (select RolesId from Tb_RolesAddUser where UserId='" + userobj.userid + "')) and  LinkAddress like '" + pagename + "%'  and ( BtnCode  like 'ljxfun%')  order by BtnCode desc");
                if (userobj.logincount == "admin")//设置为全部读的权限。
                    retstr = "ljxfun1|ljxfun2|ljxfun3|ljxfun4|ljxfun5|ljxfun6";
                else if (ds0.Tables[0].Rows.Count <= 0)//表示没有权限， 。
                    retstr = "";
                else
                {
                    // 
                    for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    {
                        retstr += ds0.Tables[0].Rows[i]["BtnCode"].ToString().Trim() + "|";

                    }
                }
                ljxfunpower.Add(pagename, retstr);
                context.Session["ljxfunpower"] = ljxfunpower;
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