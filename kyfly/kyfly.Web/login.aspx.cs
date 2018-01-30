using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace kyfly
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            string name = txtName.Text.Replace("'", "").Replace(" ", "");
            string pass = txtPass.Text.Replace("'", "").Replace(" ", "");
            string strwhere = "";
            string username = "";
            pass = ljxpower.Common.DESEncrypt.Encrypt(pass);

            #region  login ....


            ljxpower.BLL.com_user bll = new ljxpower.BLL.com_user();
            strwhere = "(userid='" + name + "'  or logincount='" + name + "'    ) and password='" + pass + "'";
                Session["schoolnum"] = "";

                DataSet ds = ljxpower.Common.DbHelperMySQL.Query("select * from  com_user where " + strwhere);

                if (ds.Tables[0].Rows.Count <= 0)
                {
                    lblName.Text = "用户名或密码错误";
                }
                else
                {
                    ljxpower.Model.com_user usermodel = new ljxpower.Model.com_user();
                    usermodel = bll.GetModel(int.Parse(ds.Tables[0].Rows[0]["Id"].ToString()));
                    ljxpower.BLL.com_loginlog lbll = new ljxpower.BLL.com_loginlog();
                    ljxpower.Model.com_loginlog lmodel = new ljxpower.Model.com_loginlog();
                    lmodel.LoginDate = DateTime.Now;
                    lmodel.LoginIP = Page.Request.UserHostAddress;
                    lmodel.Status = "0";
                    lmodel.Userid = name;

                    if (ds.Tables[0].Rows.Count > 0)
                        username = ds.Tables[0].Rows[0]["username"].ToString(); //登录用户姓名
                    lmodel.username = username;
                    lbll.Add1(lmodel);

                    userinfo userobj = new userinfo();
                    userobj.logincount = ds.Tables[0].Rows[0]["logincount"].ToString();//登录帐户编名 
                    userobj.userid = ds.Tables[0].Rows[0]["userid"].ToString();//登录帐户编名 
                    userobj.username = ds.Tables[0].Rows[0]["username"].ToString();//登录用户名
                    userobj.usertype = ds.Tables[0].Rows[0]["usertype"].ToString();//用户类别
                    userobj.orgid = ds.Tables[0].Rows[0]["orgid"].ToString();//部门编号 
                    userobj.logintime = DateTime.Now;//登录时间 
 
                    Session["userobj"] = userobj;
                    Response.Redirect("index.html?username="+ds.Tables[0].Rows[0]["username"].ToString()+"&time=" + DateTime.Now.ToUniversalTime());

                }

              
 
            #endregion login....
        }
    }
}