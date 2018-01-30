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
    /// 生成代码时间： ljxtime
    /// 文件说明：com_user
    /// 开发者：
    /// 最后编辑时间：
    /// 编辑说明：
    /// </summary>
    public class com_user : baseashxClass
    {
        
        public override void ProcessRequest(HttpContext context)
        {
            ljxpower.BLL.com_user bll = new ljxpower.BLL.com_user(context); 
            base.ProcessRequest(context);
            context.Response.ContentType = "text/plain";

            mycommonClass mycommonClassobj = new mycommonClass();
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            if (context.Request.QueryString["type"] == "edit")//获取编辑信息
            {
                if (context.Request.QueryString["Id"] != null)
                {
                    string strret = bll.Geteditdata(context.Request.QueryString["Id"].ToString());//aa1
                    context.Response.Write(strret);
                }
            }
            else if (context.Request.QueryString["type"] == "del")//删除信息
            {
                if (context.Request.QueryString["Id"] == null)
                    return;
                string Id = context.Request.QueryString["Id"].ToString();                
                bll.Delete(int.Parse(Id));
                kyfly.Common.DbHelperMySQL.ExecuteSql("delete from  Com_OrgAddUser where UserId=" + Id);
            }
            else if (context.Request.QueryString["type"] == "combox")//保存修改或添加 信息 aa2
            {
                        if (context.Request.QueryString["comboxname"] == "status")
                        {
                            ljxpower.BLL.com_zidian bllzd = new ljxpower.BLL.com_zidian();
                            string strret = bllzd.GetListByColumn_tojson("Id,xianshizhi", "leibie='人员状态'", "");
                            context.Response.Write(strret);
                            //context.Response.Write("");
                        }

                        if (context.Request.QueryString["comboxname"] == "orgid")
                        {
                            ljxpower.BLL.com_organization bllzd = new ljxpower.BLL.com_organization();
                            string strret = bllzd.GetListByColumn_tojson("Id,Agency,orgid,Person","1=1","");
                            context.Response.Write(strret);
                        }


            }
            else if (context.Request.QueryString["type"] == "comboxtree")
            {
                //string str = "";
                //str =  ljxpower.Common.DbHelperMySQL.getvalue("select mycontent from temp1");
                //context.Response.Write(str);
                //return;

                StringBuilder sb = new StringBuilder();

                ljxpower.BLL.com_organization orgbll = new ljxpower.BLL.com_organization();
 
                DataSet ds = new DataSet();
                ds = orgbll.GetAllList();
                if (ds.Tables.Count > 0)
                {
                    sb.Append("[");
                    DataView dv = new DataView(ds.Tables[0]);
                    dv.RowFilter = "ParentId=0";
                    dv.Sort = " Sort ";
                    for (int i = 0; i < dv.Count; i++)
                    {
                        sb.Append("{");
                        sb.Append("\"id\":" + dv[i]["orgid"] + ",");
                        sb.Append("\"text\":\"" + dv[i]["Agency"] + "\"");
              
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

            else if (context.Request.QueryString["type"] == "powersave")//保存修改或添加 
            {
                string userstr = context.Request.QueryString["idlist"].ToString();
                string roleid = context.Request.QueryString["roleid"].ToString();
                string[] userid;
                if (userstr != "" && roleid != "")
                {
                    userid = userstr.Split(':');
                    for (int i = 0; i < userid.Length; i++)
                    {
                        if (userid[i] == "")
                            continue ;
                        saveRole(userid[i], roleid);
                    }
                }
            }

            else if (context.Request.QueryString["type"] == "save")//保存修改或添加 
            {
                string userid = context.Request.QueryString["userid"];
                string logincount = context.Request.QueryString["logincount"];
                string username = context.Request.QueryString["username"];
                string orgid = context.Request.QueryString["orgid"];
                string password = context.Request.QueryString["password"];
                string usertype = context.Request.QueryString["usertype"];
                string status = context.Request.QueryString["status"];
                string gongsibianhao = context.Request.QueryString["gongsibianhao"];
                string orgname = context.Request.QueryString["orgname"];
                if (password.Trim() != "")
                    password = ljxpower.Common.DESEncrypt.Encrypt(password);
                else
                    password = kyfly.Common.DbHelperMySQL.getvalue("select password from com_user where   UserId=" + userid);
                if (context.Request.QueryString["Id"] != null && context.Request.QueryString["Id"] != "")
                {
                    if (kyfly.Common.DbHelperMySQL.getvalue("select id from Com_OrgAddUser where   UserId=" + userid) == "")
                        kyfly.Common.DbHelperMySQL.ExecuteSql("insert into  Com_OrgAddUser(orgid,UserId) values('" + orgid + "','" + userid + "')");
                    else
                    {
                        bll.Update(context.Request.QueryString["Id"].ToString(), userid, logincount, username, orgid, password, usertype, status, gongsibianhao, orgname);
                        kyfly.Common.DbHelperMySQL.ExecuteSql("update  Com_OrgAddUser set orgid='" + orgid + "' where UserId=" + userid);
                    }
                }//(string Id, string userid, string logincount, string username, string orgid, string password, string usertype, string status, string gongsibianhao)
                else
                {
                    bll.Add(userid, logincount, username, orgid, password, usertype, status, gongsibianhao, username);
                }

                context.Response.Write("true");
            }
            else if (Convert.ToString(context.Request.Form["action"]) == "query")
            {
                //string strret = bll.GetListByPageColumns_tojson("id,产品名称,产品数量,产品规格,备注", "1=1", "产品数量"); 加权限, 1=1 改为:部门编号 like '1010%'
                string strret = bll.GetListByPageColumns_tojson("Id,userid,logincount,username,orgid,password,usertype,status,gongsibianhao,orgname", pagestrif, "Id");
                context.Response.Write(strret);
                return;
            }
            else
            {

            }
        }
        public bool saveRole(string Userid, string role)
        {
            List<string> list = new List<string>();
            string sql2 = "delete from tb_rolesadduser where UserId='" + Userid + "' and RolesId='" + role + "'";
            list.Add(sql2);
            if (role != null && role != "")
            {
                string sql = "insert into tb_rolesadduser(RolesId,UserId) values(" + role + ",'" + Userid + "')";
                list.Add(sql);
            }
            if (ljxpower.Common.DbHelperMySQL.ExecuteSqlTran(list) > 0)
                return true;
            else
                return false;
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
                sb.Append("\"id\":" + dv[i]["orgid"] + ",");
                sb.Append("\"text\":\"" + dv[i]["Agency"] + "\"");
 
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
    }
}