using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class Change_Collection_Point : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label2.Visible = false;
                DropDownList1.DataSource = BusinessLogic.GetCollection();
                DropDownList1.DataTextField = "collection_place";
                DropDownList1.DataValueField = "place_id";
                DropDownList1.DataBind();
            }
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);
            var q = BusinessLogic.getdepartmentcollection(dept).ToList();
            GridView1.DataSource = q;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);//to get the department
            int i = Convert.ToInt32(DropDownList1.SelectedValue);
            BusinessLogic.updatecollectionlocation(dept, i);
            Label2.Visible = true;
            string messagebody = "The following location has been selected as new location for collection of stationery \n \n" + DropDownList1.SelectedItem.Text.ToString();
            //sending the email to store on location change
            BusinessLogic.sendMail("pssruthi123@gmail.com", "Location Change", messagebody);

        }
    }
}