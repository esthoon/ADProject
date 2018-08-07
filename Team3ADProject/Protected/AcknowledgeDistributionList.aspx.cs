using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using System.Drawing;

//Rohit
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
                    //these sessions are read from the previous page (ViewCollectionInformation)
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

            //When the department rep is collecting items, he :
            // 1) Cannot collect if he enters more than what he requested for. (In Collected_Qty textbox)
            // 2) Can collect same amount that he requested for.
            // 3) Can collect lesser than what he requested. (Maybe some items are damaged or he just doesn't need so many anymore)
            // 4) Not allow any other invalid input.(Like Negative numbers or strings)
            
            //Solution:
            // 1) Prompt user to enter lesser.
            // 2) Update the collection as completed. No changes to inventory.
            // 3) Update the collection as completed. The difference has to be updated back to the inventory.
            // 4) Have appropriate mechanisms to encounter exceptions.


            try
            {
                int collection_id = Convert.ToInt32(Session["collection_id"]);
                bool GreaterThan = false;

                for (int i = 0; i < gridview1.Rows.Count; i++)
                {   
                    int UserInput = Convert.ToInt32(((TextBox)gridview1.Rows[i].FindControl("TextBox1")).Text);
                    string ItemCode = gridview1.Rows[i].Cells[0].Text;
                    int ActualSupplyQuantityValue = BusinessLogic.getActualSupplyQuantityValue(collection_id, ItemCode);
                    
                    //Logic for 1
                    if (UserInput > ActualSupplyQuantityValue)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You cannot enter more than the requested supply quantity')", true);
                        GreaterThan = true;
                        break;
                    }

                    //Logic for 4(the try-catch takes care of other invalid inputs)
                    if (UserInput < 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No negative values allowed. Please try again')", true);
                        GreaterThan = true;
                        break;
                    }

                }

                if (GreaterThan == false)
                {
                    //Th number of items Dept head has collected is <= Supplied
                    for (int i = 0; i < gridview1.Rows.Count; i++)
                    {
                        int UserInput = Convert.ToInt32(((TextBox)gridview1.Rows[i].FindControl("TextBox1")).Text);
                        string ItemCode = gridview1.Rows[i].Cells[0].Text;
                        int ActualSupplyQuantityValue = BusinessLogic.getActualSupplyQuantityValue(collection_id, ItemCode);

                        //logic lesser userInput < supply qty 
                        BusinessLogic.AcknowledgeDL(collection_id, ItemCode, ActualSupplyQuantityValue, UserInput);
                    }
                    //update status as collected(for collected <= Supplied)
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