using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace kyfly.ashx
{
    /// <summary>
    /// OrglistHandler 的摘要说明
    /// </summary>
    public class OrglistHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ljxpower.BLL.com_organization bll = new ljxpower.BLL.com_organization();
            ljxpower.Model.com_organization model = null;
            if (context.Request.QueryString["type"] == "edit")//获取部门信息
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);

                model = bll.GetModel(Id);
                if (model != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(model.Agency + ",");
                    //ljxpower.Model.com_organization pmodel = bll.GetModel(model.ParentId);
                    //if (pmodel != null)
                    //{
                    //    sb.Append(pmodel.Agency);
                    //}
                    sb.Append(model.ParentId + ",");
                    sb.Append(model.Sort + ",");
                    sb.Append(model.Person + ",");
                    sb.Append(model.Remark + ",");
                    sb.Append(model.orgid);
                    context.Response.Write(sb.ToString());
                }
            }
            else if (context.Request.QueryString["type"] == "del")//删除部门信息
            {
                string Id = context.Request.QueryString["Id"];
                //ljxpower.BLL.com_organization bll = new ljxpower.BLL.com_organization();
                string[] str = Id.Split(',');
                int oId = int.Parse(str[str.Length - 1]);
                model = bll.GetModel(oId);
                List<string> listSql = new List<string>();
                if (model != null)
                {
                    string sql = "update com_organization set Sort=Sort-1 where ParentId=" + model.ParentId + " and Sort>" + model.Sort;
                    listSql.Add(sql);
                }
                listSql.Add(" delete  from  com_organization where Id in(" + Id + ")");
                listSql.Add(" update Com_OrgAddUser set OrgId=(select   Id from com_organization where ParentId=0 limit 1,1) where OrgId=" + Id);
                if (ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(listSql) > 0)
                {
                    context.Response.Write("true");
                }
            }
            else if (context.Request.QueryString["type"] == "save")//保存修改或添加部门信息
            {
                //ljxpower.BLL.com_organization bll = new ljxpower.BLL.com_organization();

                string name = context.Request.QueryString["name"];
                string remark = context.Request.QueryString["remark"];
                string person = context.Request.QueryString["person"];
                int sort = int.Parse(context.Request.QueryString["sort"]);
                int parent = 0;
                if (context.Request.QueryString["parentId"] != null && context.Request.QueryString["parentId"] != "")
                {
                    parent = int.Parse(context.Request.QueryString["parentId"]);
                }
                string orgid = getnewbianhao(Convert.ToString(context.Request.QueryString["parentId"]));

                List<ljxpower.Model.com_organization> list = bll.GetModelList(" ParentId=" + parent);
                List<string> listSql = new List<string>();
                if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                {
                    int Id = int.Parse(context.Request.QueryString["Id"]);
                    model = bll.GetModel(Id);
                    if (model.ParentId == parent)
                    {
                        if (sort > list.Count)
                        {
                            sort = list.Count;
                        }
                        if (model.Sort > sort)
                        {
                            string sql = "update com_organization set Sort=Sort+1 where ParentId=" + parent + " and Sort>=" + sort + " and Sort<" + model.Sort;
                            listSql.Add(sql);
                        }
                        else if (model.Sort < sort)
                        {
                            string sql = "update com_organization set Sort=Sort-1 where ParentId=" + parent + " and Sort<=" + sort + " and Sort>" + model.Sort;
                            listSql.Add(sql);
                        }
                    }
                    else
                    {
                        if (sort > list.Count + 1)
                        {
                            sort = list.Count + 1;
                        }
                        else
                        {
                            string sql = "update com_organization set Sort=Sort+1 where ParentId=" + parent + " and Sort>=" + sort;
                            listSql.Add(sql);
                            string sql2 = "update com_organization set Sort=Sort-1 where ParentId=" + model.ParentId + " and Sort>" + model.Sort;
                            listSql.Add(sql2);
                        }
                    }
                    if (listSql.Count > 0)
                    {
                        ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(listSql);
                    }
                    model.Agency = name;
                    model.Person = person;
                    model.Remark = remark;
                    model.Sort = sort;
                    if (model.ParentId != parent)
                    {
                        model.ParentId = parent;
                        model.orgid = orgid;
                    }
                    bll.Update1(model);
                }
                else
                {
                    model = new ljxpower.Model.com_organization();
                    model.Agency = name;
                    model.Person = person;
                    model.Remark = remark;
                    model.orgid = orgid;
                    if (sort > list.Count + 1)
                    {
                        sort = list.Count + 1;
                    }
                    else
                    {
                        string sql = "update com_organization set Sort=Sort+1 where ParentId=" + parent + " and Sort>=" + sort;
                        listSql.Add(sql);
                        string sql2 = "update com_organization set Sort=Sort-1 where ParentId=" + model.ParentId + " and Sort>" + model.Sort;
                        listSql.Add(sql2);
                        ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(listSql);
                    }
                    model.Sort = sort;
                    model.ParentId = parent;
                    bll.Add1(model);
                }

                context.Response.Write("true");
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                //ljxpower.BLL.Com_UserInfos ubll = new ljxpower.BLL.Com_UserInfos();
                //ljxpower.Model.Com_UserInfos user = null;
                DataSet ds = new DataSet();
                if (context.Request["Id"] != null)
                {
                    ds = bll.GetList(" Id!=" + context.Request.QueryString["Id"]);
                }
                else
                {
                    ds = bll.GetAllList();
                }
                if (ds.Tables.Count > 0)
                {
                    sb.Append("[");
                    DataView dv = new DataView(ds.Tables[0]);
                    dv.RowFilter = "ParentId=0";
                    dv.Sort = " Sort ";
                    for (int i = 0; i < dv.Count; i++)
                    {
                        sb.Append("{");
                        sb.Append("\"id\":" + dv[i]["Id"] + ",");
                        sb.Append("\"text\":\"" + dv[i]["Agency"] + "\",");
                        sb.Append("\"Sort\":\"" + dv[i]["Sort"] + "\"");
                        //sb.Append("\"Person\":\"" + dv[i]["Person"] + "\",");
                        //sb.Append("\"Remark\":\"" + dv[i]["Remark"] + "\"");
                        DataView dv2 = new DataView(ds.Tables[0]);
                        dv2.RowFilter = "ParentId=" + dv[i]["Id"];
                        dv2.Sort = " Sort ";
                        if (dv2.Count > 0)
                        {
                            sb.Append(GetChlid(dv2, ds));
                        }
                        sb.Append("},");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("]");
                }
                context.Response.Write(sb.ToString());
            }
        }

        private string getnewbianhao(string parentbhold)
        {
            //编号说明：共10位，采用2-2-3-3，第一层从11开始，第二层从01，第三层从001，第四层001；编号从左到右编，不足在右边补0.
            string newbh = "";
            string strtemp = "";
            int nownum = 0;//当前父类所拥有的子类数量。
            //找出父类编号，去掉后面的0，再找出父类下一级的类数，确定新类编号
            if (parentbhold == "")
                parentbhold = "0";
            parentbhold = ljxpower.Common.DbHelperMySQL.getvalue("select orgid from com_organization  where id= " + parentbhold + "");

            string parentbh = parentbhold.TrimEnd('0');
            if (parentbh == "")
                strtemp = ljxpower.Common.DbHelperMySQL.getvalue("select count(*) from com_organization  where orgid like '%00000000'");
            else
                strtemp = ljxpower.Common.DbHelperMySQL.getvalue("select count(*) from com_organization  where orgid like '" + parentbh + "%'");//包括自身已经加1 ，所以下面不用再加1
            if (parentbh.Length == 0)//第0层 1200000000
            {
                //if (strtemp == "0")
                //    strtemp = "10";
                nownum = int.Parse(strtemp) + 11;
                nownum = nownum * 100000000;
                newbh = nownum.ToString();
            }
            else if (parentbh.Length == 2)//第一层  1201000000
            {
                nownum = int.Parse(strtemp);
                nownum = int.Parse(parentbhold) + nownum * 1000000;
                newbh = nownum.ToString();
            }
            else if (parentbh.Length == 4)//第2层   1201001000
            {
                nownum = int.Parse(strtemp);
                nownum = int.Parse(parentbhold) + nownum * 1000;
                newbh = nownum.ToString();
            }
            else if (parentbh.Length == 7)//第3层
            {
                nownum = int.Parse(strtemp);
                nownum = int.Parse(parentbhold) + nownum;
                newbh = nownum.ToString();
            }
            else if (parentbh.Length == 10)//第4层,最后一层，不允许再分，返回空字符串。
                newbh = "";
            return newbh;

        }

        protected StringBuilder GetChlid(DataView dv, DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            //ljxpower.BLL.Com_UserInfos ubll = new ljxpower.BLL.Com_UserInfos();
            //ljxpower.Model.Com_UserInfos user = null;
            sb.Append(",\"children\":[");

            for (int i = 0; i < dv.Count; i++)
            {
                sb.Append("{");
                sb.Append("\"id\":" + dv[i]["Id"] + ",");
                sb.Append("\"text\":\"" + dv[i]["Agency"] + "\",");
                sb.Append("\"Sort\":\"" + dv[i]["Sort"] + "\",");
                //user = new ljxpower.Model.Com_UserInfos();
                //user = ubll.GetModel(dv[i]["Person"].ToString());
                //if (user != null)
                //{
                //    sb.Append("\"Person\":\"" + user.UserRealName + "\",");
                //}
                //else
                //{
                sb.Append("\"Person\":\"" + dv[i]["Person"] + "\",");
                //}
                sb.Append("\"Remark\":\"" + dv[i]["Remark"] + "\"");
                DataView dv2 = new DataView(ds.Tables[0]);
                dv2.RowFilter = "ParentId=" + dv[i]["Id"];
                dv2.Sort = " Sort ";
                if (dv2.Count > 0)
                {
                    sb.Append(GetChlid(dv2, ds));
                }
                sb.Append("},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb;
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