using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;
using Team3ADProject.Code;
using System.Globalization;

namespace Team3ADProject.Protected
{
    public partial class Report1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = (string)Session["Department"];
        
        }

        protected void ChartList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // message.InnerText = ChartList.SelectedValue;
            // Enable start and end date only if the chart allows it.
            if (ChartList.SelectedValue.Equals(Constants.CHART_REQUISITION_ITEM_QUANTITY_BY_DEPARTMENT)
                || ChartList.SelectedValue.Equals(Constants.CHART_STATIONARIES_ORDERED_BY_CATEGORY)
                )
            {

                String today = DateTime.Now.ToString("dd-MM-yyyy");
                startDate.Value = DateTime.Now.AddMonths(-2).ToString("dd-MM-yyyy");
                endDate.Value = today;

                startDate.Disabled = false;
                endDate.Disabled = false;

                StartDateValidator.Enabled = true;
                EndDateValidator.Enabled = true;
            }

            else
            {
                startDate.Value = "-";
                endDate.Value = "-";

                startDate.Disabled = true;
                endDate.Disabled = true;

                StartDateValidator.Enabled = false;
                EndDateValidator.Enabled = false;
            }
        }
    }
}