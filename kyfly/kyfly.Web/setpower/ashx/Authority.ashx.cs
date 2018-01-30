using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace kyfly.ashx
{
    /// <summary>
    /// Authority 的摘要说明
    /// </summary>
    public class Authority : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //StringBuilder sb = new StringBuilder();
            if (context.Request.QueryString["type"] == "update")//保存修改的菜单
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                string Name = context.Request.QueryString["Name"];
                string Code = context.Request.QueryString["Code"];
                string Icon = context.Request.QueryString["Icon"];
                int sort = int.Parse(context.Request.QueryString["sort"]);
                string remark = context.Request.QueryString["remark"];
                ljxpower.BLL.com_buttongroup bll = new ljxpower.BLL.com_buttongroup();
                ljxpower.Model.com_buttongroup model = bll.GetModel(Id);
                model.ButtonName = Name;
                model.BtnCode = Code;
                model.Remark = remark;
                int maxSort = ljxpower.Common.DbHelperMySQL.GetMaxID("Id", "com_buttongroup") - 1;// bll.GetMaxId() - 1;//得到最大的排序
                string sql = "";
                if (model.Sort > sort)
                {
                    sql = "update Com_ButtonGroup set Sort=Sort+1 where Sort<" + model.Sort + " and Sort>=" + sort;
                }
                else if (model.Sort < sort)
                {
                    sql = "update Com_ButtonGroup set Sort=Sort-1 where Sort>" + model.Sort + " and Sort<=" + sort;
                }
                if (sql != "")
                    ljxpower.Common.DbHelperMySQL.ExecuteSql(sql);
                model.Icon = Icon;
                if (sort > maxSort)
                {
                    model.Sort = maxSort;
                }
                else
                {
                    model.Sort = sort;
                }
                bll.Update1(model);
            }
            else if (context.Request.QueryString["type"] == "add")//添加菜单
            {
                string Name = context.Request.QueryString["Name"];
                string Code = context.Request.QueryString["Code"];
                string Icon = context.Request.QueryString["Icon"];
                int sort = int.Parse(context.Request.QueryString["sort"]);
                string remark = context.Request.QueryString["remark"];
                ljxpower.BLL.com_buttongroup bll = new ljxpower.BLL.com_buttongroup();
                ljxpower.Model.com_buttongroup model = new ljxpower.Model.com_buttongroup();
                model.ButtonName = Name;
                model.BtnCode = Code;
                model.Remark = remark;
                int maxSort = ljxpower.Common.DbHelperMySQL.GetMaxID("Id", "com_buttongroup") - 1;//得到最大的排序
                string sql = "update Com_ButtonGroup set Sort=Sort+1 where Sort>=" + sort;
                ljxpower.Common.DbHelperMySQL.ExecuteSql(sql);
                model.Icon = Icon;
                if (sort > maxSort)
                {
                    model.Sort = maxSort;
                }
                else
                {
                    model.Sort = sort;
                }
                bll.Add1(model);
            }
            else if (context.Request.QueryString["type"] == "delete")// 
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                int sort = int.Parse(context.Request.QueryString["sort"]);
                ljxpower.BLL.com_buttongroup bll = new ljxpower.BLL.com_buttongroup();
                string sql = "update Com_ButtonGroup set Sort=Sort-1 where Sort>" + sort;
                ljxpower.Common.DbHelperMySQL.ExecuteSql(sql);
                bll.Delete(Id);
            }



            ///////////////////
            if (context.Request.QueryString["type"] == "save")
            {
                int Id = int.Parse(context.Request.QueryString["Id"]);
                string Autid = context.Request.QueryString["Autid"];
                string tishistr = Convert.ToString(context.Request.QueryString["tishi"]);
                if (tishistr == "")
                    tishistr = "添加,修改,删除,浏览,查询";
                ljxpower.BLL.com_navigationandbutton bll = new ljxpower.BLL.com_navigationandbutton();
                List<string> list = new List<string>();
                list.Add("delete from Com_NavigationAndButton where NavigationId="+Id);
                string[] str = Autid.Split(',');
                string[] arrtishi = tishistr.Split(',');
 
                    for (int i = 0; i < str.Length;i++ )
                    {
                        list.Add("insert into Com_NavigationAndButton(NavigationId,ButtonId) values(" + Id + "," + str[i] +  ")");
                    }

                int k=0;
                    for (int i = 0; i < arrtishi.Length; i++)
                    {
                        k++;
                        list.Add("update Com_NavigationAndButton set buttonstr='" + arrtishi[i] + "' where NavigationId=" + Id + " and ButtonId=" + ljxpower.Common.DbHelperMySQL.getvalue("select id from Com_ButtonGroup where sort=" + k.ToString()));
                    }

                    ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(list);
            }
            else if (context.Request.QueryString["type"] == "auth") {
                string NavigaId = "0";
                if (context.Request["NavigaId"] != null)
                {
                    NavigaId = context.Request["NavigaId"].ToString();
                }
                DataSet ds = ljxpower.Common.DbHelperMySQL.Query("select ButtonId,buttonstr,BtnCode from V_Com_NavigationAndButton where  NavigationId=" + NavigaId);
                string str = "";
                foreach (DataRow dr in ds.Tables[0].Rows) {
                    if (str != "")
                        str += ",";
                    str += dr["ButtonId"];
                }
                string str2 = "";
                string tempstr = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (str2 != "")
                        str2 += ",";
                    str2 += dr["BtnCode"] + "_" + dr["buttonstr"];
                    tempstr += dr["buttonstr"];
                }
                if (tempstr == "")
                {
                    DataSet ds2 = ljxpower.Common.DbHelperMySQL.Query("select  BtnCode,Remark from Com_ButtonGroup where  BtnCode like 'ljxfun%' order by sort");
                    str2 = "";
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        if (str2 != "")
                            str2 += ",";
                        str2 += dr["BtnCode"] + "_" + dr["Remark"];
                    }
                }
                str = str + "|" + str2;
                context.Response.Write(str);
            }
            else
            {
               
                // ljxpower.BLL.Com_NavigationAndButton Nbll = new ljxpower.BLL.Com_NavigationAndButton();
                // DataSet ds = Nbll.GetList(" NavigationId=" + NavigaId);
                
                ljxpower.BLL.com_buttongroup bll = new ljxpower.BLL.com_buttongroup();
                List<ljxpower.Model.com_buttongroup> list = bll.GetModelList(" 1=1 order by Sort");
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (ljxpower.Model.com_buttongroup item in list)
                {
                    sb.Append("{\"Id\":" + item.Id + ",");
                    sb.Append("\"ButtonName\":\"" + item.ButtonName + "\",");
                    sb.Append("\"BtnCode\":\"" + item.BtnCode + "\",");
                    sb.Append("\"Icon\":\"" + item.Icon + "\",");
                    sb.Append("\"Sort\":\"" + item.Sort + "\",");
                    sb.Append("\"Remark\":\"" + item.Remark + "\"");
                    //DataView dv = new DataView(ds.Tables[0]);
                    // dv.RowFilter = " ButtonId=" + item.Id;
                    //if (dv.Count > 0)
                    //  sb.Append(",\"checked\":true");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
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