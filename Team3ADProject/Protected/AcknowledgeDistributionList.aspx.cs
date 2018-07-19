using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class AcknowledgeDistributionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DepartmentNameLabel.Text = Session["DepartmentName"].ToString();
                DepartmentRepresentativeLabel.Text = Session["EmployeeName"].ToString();
                DateLabel.Text = Session["CollectionDate"].ToString();
                LocationLabel.Text = Session["CollectionLocation"].ToString();
                TimeLabel.Text = Session["CollectionTime"].ToString();
                int disbursement_list_id = Convert.ToInt32(Session["disbursement_list_id"]);
                myPageLoad(disbursement_list_id);
            }
            catch (Exception ex)
            {
                
                Response.Write("Exception " + ex);
            }
        }

        protected void myPageLoad(int disbursement_list_id)
        {
            gridview1.DataSource = BusinessLogic.ViewAcknowledgementList(disbursement_list_id);
            gridview1.DataBind();
        }
        

        protected void AcknowledgeButtonClick(object sender,EventArgs e)
        {
           if(PinCorrect())
            {
                Response.Write("\nPin entered is correct");
                if(!CheckIfQuantityIsGreater())
                {
                    //update ROD and Inventory
                }
                else
                {
                    //Say that you cannot enter bigger quantity
                }
            }
           else
            {
                Response.Write("\nPin is incorrect");
            }
        }

        protected bool CheckIfQuantityIsGreater()
        {
            //Quantity cannot be greater
            foreach(GridViewRow row in gridview1.Rows)
            {

            }
            return true;
        }




        protected Boolean PinCorrect()
        {
            try
            {
                string deptname = Session["DepartmentName"].ToString();
                int EnteredPin = Convert.ToInt32(PinTextBox.Text);
                int ActualPin = BusinessLogic.GetDepartmentPin(deptname);
                if (EnteredPin == ActualPin) return true;
            }
            catch(FormatException e)
            {
                System.FormatException ex = e;
                Response.Write("\nPin is numeric");
                return false;
            }
            catch(Exception e)
            {
                Response.Write("\nError occured : "+e);
            }
            return false;
        }
    }
}