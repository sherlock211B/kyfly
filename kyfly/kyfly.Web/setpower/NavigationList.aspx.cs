using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kyfly
{
    public partial class NavigationList : System.Web.UI.Page
    {
        public static string top = "";
        protected string strMap = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                BindDataList();
            }
        }
        private void BindDataList()
        {
            List<ljxpower.Model.tb_navigation> list = new List<ljxpower.Model.tb_navigation>();
            ljxpower.BLL.tb_navigation bll = new ljxpower.BLL.tb_navigation();
            List<ljxpower.Model.tb_navigation> list1 = bll.GetModelList(" ParentId=0");
            //对原有的数据进行重新排序
            foreach (ljxpower.Model.tb_navigation model in list1)
            {
                if (strMap == "")
                    strMap = "0";
                else strMap += ",0";
                list.Add(model);
                int i = list.Count;
                List<ljxpower.Model.tb_navigation> list2 = bll.GetModelList(" ParentId=" + model.Id);
                foreach (ljxpower.Model.tb_navigation item in list2)
                {
                    strMap += "," + i;
                    list.Add(item);
                }
            }
            rptList.DataSource = list;
            rptList.DataBind();
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

        public string gettop() {
            return top;
        }
    }
}