using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using System.Drawing;

namespace Team3ADProject.Protected
{
    public partial class AcknowledgeDistributionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DepartmentNameLabel.Text = Session["DepartmentName"].ToString();
                    DepartmentRepresentativeLabel.Text = Session["EmployeeName"].ToString();
                    DateLabel.Text = Session["CollectionDate"].ToString();
                    LocationLabel.Text = Session["CollectionLocation"].ToString();
                    TimeLabel.Text = Session["CollectionTime"].ToString();
                    int collection_id = Convert.ToInt32(Session["collection_id"]);
                    AcknowledgeButton.Enabled = false;
                    AcknowledgeButton.BackColor = Color.Red;
                    myPageLoad(collection_id);
                }
                catch (Exception ex)
                {

                    Response.Write("Exception " + ex);
                }

            }
        }



        protected void myPageLoad(int collection_id)
        {
            gridview1.DataSource = BusinessLogic.ViewAcknowledgementList(collection_id);
            gridview1.DataBind();

        }


        protected void AcknowledgeButton_Click(object sender, EventArgs e)
        {

            try
            {
                int collection_id = Convert.ToInt32(Session["collection_id"]);
                bool GreaterThan = false;

                for (int i = 0; i < gridview1.Rows.Count; i++)
                {
                    int UserInput = Convert.ToInt32(((TextBox)gridview1.Rows[i].FindControl("TextBox1")).Text);
                    string ItemCode = gridview1.Rows[i].Cells[0].Text;
                    int ActualSupplyQuantityValue = BusinessLogic.getActualSupplyQuantityValue(collection_id, ItemCode);

                    if (UserInput > ActualSupplyQuantityValue)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot enter more than the requested supply quantity')", true);
                        GreaterThan = true;
                        break;
                    }

                    if (UserInput < 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No negative values allowed. Please try again')", true);
                        GreaterThan = true;
                        break;
                    }

                }

                if (GreaterThan == false)
                {
                    for (int i = 0; i < gridview1.Rows.Count; i++)
                    {
                        int UserInput = Convert.ToInt32(((TextBox)gridview1.Rows[i].FindControl("TextBox1")).Text);
                        string ItemCode = gridview1.Rows[i].Cells[0].Text;
                        int ActualSupplyQuantityValue = BusinessLogic.getActualSupplyQuantityValue(collection_id, ItemCode);

                        //logic lesser userInput < supply qty 
                        BusinessLogic.AcknowledgeDL(collection_id, ItemCode, ActualSupplyQuantityValue, UserInput);
                    }
                    //update status as collected
                    BusinessLogic.updateCollectionStatus(collection_id);
                    Response.Redirect("ViewCollectionInformation.aspx");
                }
            }

            catch (Exception ex)
            {
                Exception exx = ex;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Oops ! Something went wrong. Please try again !')", true);
            }


        }




        protected void VerifyPasswordButtonClick(object sender, EventArgs e)
        {

            if (PinCorrect())
            {
                // Response.Write("\nPin entered is correct");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Pin is incorrect')", true);
            }
        }



        protected Boolean PinCorrect()
        {
            try
            {
                string deptname = Session["DepartmentName"].ToString();
                int EnteredPin = Convert.ToInt32(PinTextBox.Text);
                int ActualPin = BusinessLogic.GetDepartmentPin(deptname);
                if (EnteredPin == ActualPin)
                {
                    Session["Pin"] = true;
                    AcknowledgeButton.Enabled = true;
                    AcknowledgeButton.BackColor = Color.Green;
                    return true;
                }
                else
                {
                    Session["Pin"] = false;
                    AcknowledgeButton.Enabled = false;
                    AcknowledgeButton.BackColor = Color.Red;
                }
            }
            catch (FormatException e)
            {
                System.FormatException ex = e;
                //Response.Write("\nPin has to be numeric");
                return false;
            }
            catch (Exception e)
            {
                Response.Write("\nError occured : " + e);
            }
            return false;
        }


    }
}