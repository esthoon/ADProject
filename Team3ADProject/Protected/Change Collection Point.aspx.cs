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
			DropDownList1.DataSource = BusinessLogic.GetCollection();
			DropDownList1.DataTextField = "collection_place";
			DropDownList1.DataValueField = "place_id";
			DropDownList1.DataBind();
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			string dept= DepartmentPinChange.getmethoddepartment();
			int i = Convert.ToInt32(DropDownList1.SelectedValue);

		}
	}
}