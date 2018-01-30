using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Web.SessionState;

namespace kyfly.ashx
{
    /// <summary>
    /// NavigationList 的摘要说明
    /// </summary>
    public class NavigationList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder sb = new StringBuilder();
            if (context.Request.QueryString["type"] == "edit")//获取要修改的菜单
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
                ljxpower.Model.tb_navigation model= bll.GetModel(Id);
                if (model != null)
                {
                    sb.Append(model.MenuName+",");
                    sb.Append(model.Pagelogo+",");
                    sb.Append(model.ParentId + ",");
                    sb.Append(model.LinkAddress+",");
                    sb.Append(model.Icon+",");
                    List<ljxpower.Model.tb_navigation> list = bll.GetModelList(" ParentId="+model.ParentId);
                    string str = "";
                    for (int i = 1; i <= list.Count; i++)
                    {
                        if (i == model.Sort)
                        {
                            str += "<option value='" + i + "'  selected='selected'>" + i + "</option>";
                        }
                        else
                        {
                            str += "<option value='" + i + "'>" + i + "</option>";
                        }
                    }
                    sb.Append(str + ",");
                    sb.Append(model.IsShow);
                }
                context.Response.Write(sb.ToString());
            }
            else if (context.Request.QueryString["type"] == "Parent")//获取所有根节点 并生成选项
            {
                ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
                List<ljxpower.Model.tb_navigation> list = bll.GetModelList(" ParentId=0");
                sb.Append("<option value='0'></option>");
                foreach (ljxpower.Model.tb_navigation model in list)
                {
                    sb.Append("<option value='" + model.Id + "'>" + model.MenuName+ "</option>");
                }
                context.Response.Write(sb.ToString());
            }
            else if (context.Request.QueryString["type"] == "child")//获取所有子节点数量并生成排序选项
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
                List<ljxpower.Model.tb_navigation> list = bll.GetModelList(" ParentId="+Id);
                int i = 1;
                for ( ; i <= list.Count; i++)
                {
                    sb.Append("<option value='" + i + "'>" + i + "</option>");
                }
                sb.Append("<option value='" + i + "' selected='selected'>" + i + "</option>");
                sb.Append(",<option value='" + i + "' selected='selected'>" + i + "</option>");
                context.Response.Write(sb.ToString());
            }
            else if (context.Request.QueryString["type"] == "save")//保存修改的菜单
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                string MenuName = context.Request.QueryString["Name"];
                string Logo = context.Request.QueryString["Name"];
                int Parent = int.Parse(context.Request.QueryString["Parent"]);
                string Link = context.Request.QueryString["Link"];
                string icon = context.Request.QueryString["icon"];
                int sort = int.Parse(context.Request.QueryString["sort"]);
                int Isshow = int.Parse(context.Request.QueryString["Isshow"]);
                ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
                ljxpower.Model.tb_navigation model = bll.GetModel(Id);
                model.MenuName = MenuName;
                model.Pagelogo = Logo;
                List<string> listSql = new List<string>();
                if (model.ParentId == Parent)
                {
                    if (model.Sort > sort)
                    {
                        string sql = "update tb_navigation set Sort=Sort+1 where ParentId=" + Parent + " and Sort>=" + sort + " and Sort<" + model.Sort;
                        listSql.Add(sql);
                    }
                    else if(model.Sort<sort)
                    {
                        string sql = "update tb_navigation set Sort=Sort-1 where ParentId=" + Parent + " and Sort<=" + sort + " and Sort>" + model.Sort;
                        listSql.Add(sql);
                    }
                }
                else
                {
                    string sql = "update tb_navigation set Sort=Sort+1 where ParentId=" + Parent + " and Sort>=" + sort;
                    listSql.Add(sql);
                    string sql2 = "update tb_navigation set Sort=Sort-1 where ParentId=" + model.ParentId + " and Sort>" + model.Sort;
                    listSql.Add(sql2);
                }
                if (listSql.Count>0)
                {
                    ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(listSql);
                }
                if (Parent == 0)
                {
                    model.LinkAddress = null;
                }
                else
                {
                    model.LinkAddress = Link;
                }
                model.ParentId = Parent;
                model.Icon = icon;
                List<ljxpower.Model.tb_navigation> list = bll.GetModelList(" ParentId=" + Parent);
                if (sort > list.Count + 1)
                {
                    model.Sort = list.Count + 1;
                }
                else
                {
                    model.Sort = sort;
                }
                model.IsShow = Isshow;
                bll.Update1(model);
            }
            else if (context.Request.QueryString["type"] == "add")//添加菜单
            {
                int n1=1,n2=1;
                string MenuName = context.Request.QueryString["Name"];
                string Logo = context.Request.QueryString["Name"];
                int Parent = int.Parse(context.Request.QueryString["Parent"]);
                string Link = context.Request.QueryString["Link"];
                string icon = context.Request.QueryString["icon"];
                if (context.Request.QueryString["sort"] != null)
                    n1 = int.Parse(context.Request.QueryString["sort"].ToString());
                if (context.Request.QueryString["sort"] != null)
                    n2 = int.Parse(context.Request.QueryString["Isshow"].ToString());

                int sort = n1;
                int Isshow = n2;
                ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
                ljxpower.Model.tb_navigation model =new ljxpower.Model.tb_navigation();
                model.MenuName = MenuName;
                model.Pagelogo = Logo;
                string sql = "update tb_navigation set Sort=Sort+1  where ParentId=" + Parent + " and Sort>=" + sort;
                ljxpower.Common.DbHelperMySQL.ExecuteSql(sql);
                if (Parent == 0)
                {
                    model.LinkAddress = null;
                }
                model.LinkAddress = Link;
                model.ParentId = Parent;
                model.Icon = icon;
                model.Sort = sort;
                model.IsShow = Isshow;
                bll.Add1(model);
            }
            else if (context.Request.QueryString["type"] == "ljxquery")//添加菜单
            {
                //StringBuilder sb = new StringBuilder();
                sb.Clear();
                ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
                List<ljxpower.Model.tb_navigation> list = new List<ljxpower.Model.tb_navigation>();

                userinfo userobj = (userinfo)context.Session["userobj"];
                string strWhere = null;
                if (userobj.logincount == "admin")
                {
                    strWhere = " IsShow=0";
                }
                else
                {
                    strWhere = "Id in(select NavigationId from tb_rolesandnavigation where RolesId in(select RolesId from tb_rolesadduser where UserId='" + userobj.userid + "')) and IsShow=0";
                }
                DataSet ds = bll.GetList(strWhere);
                sb.Append(" {\"menus\":[");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = new DataView(ds.Tables[0]);
                    dv.RowFilter = " ParentId=0";
                    dv.Sort = "Sort";
                    for (int i = 0; i < dv.Count; i++)
                    {
                        sb.Append("{\"menuid\":\"" + dv[i]["Id"] + "\",\"icon\":\"" + dv[i]["Icon"] + "\",\"menuname\":\"" + dv[i]["Pagelogo"] + "\",");
                        sb.Append("\"menus\":[");
                        //sb.Append("{" + dv[i]["Pagelogo"] + "," + dv[i]["Icon"] + ",");
                        //sb.Append("<ul>");
                        DataView dv2 = new DataView(ds.Tables[0]);
                        dv2.RowFilter = " ParentId=" + dv[i]["Id"];
                        dv2.Sort = " Sort";
                        for (int j = 0; j < dv2.Count; j++)
                        {
                            sb.Append("{\"menuid\":\"" + dv2[j]["Id"] + "\",\"menuname\":\"" + dv2[j]["Pagelogo"].ToString().Trim() + "\",\"icon\":\"" + dv2[j]["Icon"].ToString().ToString().Trim() + "\",\"url\":\"" + dv2[j]["LinkAddress"].ToString().Trim() + "\"},");
                            //sb.Append("<li><div><a ref=\"" + dv2[j]["Pagelogo"] + "\" href=\"javascript:void(0)\" rel=\"" + dv2[j]["LinkAddress"] + "\" ><span class=\"" + dv2[j]["Icon"].ToString() + "\" >&nbsp;</span><span class=\"nav\">" + dv2[j]["Pagelogo"] + "</span></a></div></li>");
                        }
                        if (dv2.Count > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }

                        sb.Append("]},");
                        // sb.Append("</ul>}");
                    }
                    //sb.Remove(0, 1);
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]}");
                }

                context.Response.Write(sb.ToString());
            }
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