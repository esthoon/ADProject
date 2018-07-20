using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
	public partial class depheadpendingm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				string dept = DepartmentPinChange.getmethoddepartment();
				var q = BusinessLogic.ViewPendingRequests(dept); // to get the pending the requests for that department
				GridView1.DataSource = q.ToList();
				GridView1.DataBind();
			}
			catch (Exception x)
			{
				Response.Write(x.Message);
			}

		}


		//To redirect to pendingdetails page
		public void button_click(object sender, EventArgs e)
		{
			Button b = (Button)sender;
			HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
			string s = hd.Value;
			Response.Redirect("~/protected/pendingdetails?Id=" + s.TrimEnd());

		}
	}
}