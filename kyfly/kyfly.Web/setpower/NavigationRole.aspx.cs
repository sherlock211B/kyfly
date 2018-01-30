using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace kyfly
{
    public partial class NavigationRole : System.Web.UI.Page
    {
        protected string strMap = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try{
                    string RoleId = Request.QueryString["RoleId"];
                    ljxpower.BLL.tb_roles bll = new ljxpower.BLL.tb_roles();
                    ljxpower.Model.tb_roles model = bll.GetModel(int.Parse(RoleId));
                    lblRole.Text = model.RolesName;
                    HdId.Value = RoleId;
                    BindDataList();
                }catch{
                    Response.Redirect("RolesList.html");
                }
            }
        }
        private void BindDataList()
        {
            //根据OrderId,NodeCode asc 排序获得的数据记录集
            List<ljxpower.Model.tb_navigation> list = new List<ljxpower.Model.tb_navigation>();
            ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
            List<ljxpower.Model.tb_navigation> list1 = bll.GetModelList(" ParentId=0 and  IsShow=0 order by id");
            //对原有的数据进行重新排序
            foreach (ljxpower.Model.tb_navigation model in list1)
            {
                if (strMap == "")
                    strMap = "0";
                else strMap += ",0";
                model.MenuName = "<a title='" + getfunstr(model.Id) + "' href='#'>" + model.MenuName + " </a>";
                list.Add(model);
                int i = list.Count;
                List<ljxpower.Model.tb_navigation> list2 = bll.GetModelList(" ParentId=" + model.Id + " and  IsShow=0 order by id");
                foreach (ljxpower.Model.tb_navigation item in list2)
                {
                    item.MenuName = "<a title='" + getfunstr(item.Id) + "' href='#'>" + item.MenuName + " </a>";
                    strMap += "," + i;
                    list.Add(item);
                }
            }
            rptList.DataSource = list;
            rptList.DataBind();
        }

        private string getfunstr(int myid)
        {
            string retstr = "";
            DataSet ds3 = ljxpower.Common.DbHelperMySQL.Query("select ButtonId,buttonstr,ButtonName from V_Com_NavigationAndButton where NavigationId=" + myid + "  order by sort ");
 
            foreach (DataRow dr in ds3.Tables[0].Rows)
            {
                if (dr["ButtonName"].ToString().IndexOf("功能")>=0)
                    retstr += dr["ButtonName"] + ":" + dr["buttonstr"] + ",  ";
            }

            return retstr;
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblShow = e.Item.FindControl("lblShow") as Label;
            if (lblShow != null)
            {
                if (lblShow.Text == "0")
                {
                    lblShow.Text = "<img src='images/checked1.gif' alt='' />";
                }
                else
                {
                    lblShow.Text = "<img src='images/checked2.gif' alt='' />";
                }
            }
        }
        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            int Id = int.Parse(e.CommandArgument.ToString());
            ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
            ljxpower.Model.tb_navigation model = bll.GetModel(Id);
            string sql = "update tb_navigation set Sort=Sort-1 where ParentId=" + model.ParentId + " and Sort>" + model.Sort;
            ljxpower.Common.DbHelperMySQL.ExecuteSql(sql);
            if (bll.Delete(Id))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>msgShow('系统提示', '菜单删除成功！', 'info');window.location.href='NavigationList.aspx';</script>");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>msgShow('系统提示', '菜单删除失败，请稍后重试！', 'error');</script>");
            }
        }
    }
}