using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;


namespace Team3ADProject.Protected
{
    public partial class DisbursementSorting : System.Web.UI.Page
    {
        //JOEL START
        //This page allows users to know how to allocate collected items according to department. To be used in sorting centre.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox_Collect_Date.Visible = false;
                Calendar_Collect_Date.Visible = false;
                btn_ReadyForCollection.Visible = false;
                Label2.Visible = false;

                BindRadioButtonList();

            }
        }

        //Gets list of departments with items to collect.
        protected void BindRadioButtonList()
        {
            List<string> dptList = new List<string>();
            dptList = BusinessLogic.DisplayListofDepartmentsForCollection();

            DataTable dt = new DataTable();
            dt.Columns.Add("DepartmentName", typeof(string));

            for (int i = 0; i < dptList.Count; i++)
            {
                string dptName = dptList[i].ToString();
                dt.Rows.Add(dptName);
            }

            RadioButtonList_Dpt.DataSource = dt;
            RadioButtonList_Dpt.DataTextField = "DepartmentName";
            RadioButtonList_Dpt.DataValueField = "DepartmentName";
            RadioButtonList_Dpt.DataBind();
            if (dptList.Count > 0)
            {
                RadioButtonList_Dpt.Items[0].Selected = true;
            }

            if (dptList.Count <= 0)
            {
                Label_noDptWarning.Visible = true;
                btn_SortingSearch.Enabled = false;
            }
        }

        //The search button takes the selected value in the radiobutton list and retrieves the list of items that have been collected for the dpt. 
        protected void btn_SortingSearch_Click(object sender, EventArgs e)
        {
            DisplayDepartmentSortingTable();
            NoRowDetail();
        }

        //When 'Ready for Collection' button is clicked
        protected void btn_ReadyForCollection_Click(object sender, EventArgs e)
        {
            //(1) create new collection_detail table row. 
            string dpt_Id = GetDepartmentId();

            int placeId = BusinessLogic.GetPlaceIdFromDptId(dpt_Id);
            string s = TextBox_Collect_Date.Text;
            DateTime dt = DateTime.ParseExact(s, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            String x = dt.ToString("yyyy-MMMM-dd");
            DateTime collectionDate = Convert.ToDateTime(x);

            BusinessLogic.InsertCollectionDetailsRow(placeId, collectionDate, dpt_Id);

            //(2) add RO IDs to requisition_disbursement_detail table w/ newest collection list id. sends email to dpt rep for collection
            BusinessLogic.InsertDisbursementListROId(dpt_Id);
                                  
            Response.Redirect(Request.RawUrl);
        }

        //Get dpt id from selected dpt name. 
        protected string GetDepartmentId()
        {
            string selectedDptName = RadioButtonList_Dpt.SelectedItem.Value;
            return BusinessLogic.GetDptIdFromDptName(selectedDptName);
        }

        //displays list of items to allocate to the particular dpt.
        protected void DisplayDepartmentSortingTable()
        {
            //string dpt_Id = GetDepartmentId();
            string selectedDptName = RadioButtonList_Dpt.SelectedItem.Value;
            gridview_DptSort.DataSource = BusinessLogic.GetSortingListByDepartment(selectedDptName);
            gridview_DptSort.DataBind();
        }

        //populates column in gridview which shows how much has been collected. 
        protected void gridview_DptSort_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gridview_DptSort.Rows)
            {
                Label lb = (Label)gvr.FindControl("Label1");
                string itemcode = gvr.Cells[0].Text;
                int qty = 0;
                foreach (spGetFullCollectionROIDList_Result a in BusinessLogic.GetFullCollectionROIDList())
                {
                    if (a.item_number.Trim().ToLower() == itemcode.Trim().ToLower())
                    {
                        qty += a.item_distributed_quantity;
                    }
                }
                lb.Text = qty.ToString();
            }
        }

        //disables dates on datepicker before today
        protected void Calendar_Collect_Date_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date <= DateTime.Now)
            {
                e.Cell.BackColor = ColorTranslator.FromHtml("#a9a9a9");
                e.Day.IsSelectable = false;
            }
        }

        //when user chooses a diff date on the datepicker, this will be reflected in textbox
        protected void Calendar_Collect_Date_SelectionChanged(object sender, EventArgs e)
        {
            TextBox_Collect_Date.Text = Calendar_Collect_Date.SelectedDate.ToString("dd-MM-yyyy");
        }

        //for when user wants to reallocate items (items are sorted / allocated by system during the collection process). this provides a manual override for reallocation
        protected void btn_reallocate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            HiddenField hidden1 = (HiddenField)btn.FindControl("HiddenField1");
            HiddenField hidden2 = (HiddenField)btn.FindControl("HiddenField2");

            Response.Redirect("Reallocate.aspx?itemNum=" + hidden1.Value + "&description=" + hidden2.Value);

        }

        //if there are no departments that need to collect items, this hides some of the controls. 
        protected void NoRowDetail()
        {
            if (gridview_DptSort.Rows.Count <= 0)
            {
                TextBox_Collect_Date.Visible = false;
                Calendar_Collect_Date.Visible = false;
                btn_ReadyForCollection.Visible = false;
                Label2.Visible = false;
            }
            else
            {
                TextBox_Collect_Date.Visible = true;
                Calendar_Collect_Date.Visible = true;
                btn_ReadyForCollection.Visible = true;
                Label2.Visible = true;
            }
        }

       

    }
}



//OLD METHOD TO GET FULL LIST OF DPTS
//RadioButtonList_Dpt.DataSource = BusinessLogic.GetDepartmentList();
//RadioButtonList_Dpt.DataTextField = "department_name";
//RadioButtonList_Dpt.DataValueField = "department_name";
//RadioButtonList_Dpt.DataBind();
//RadioButtonList_Dpt.Items[0].Selected = true;
//if (BusinessLogic.GetDepartmentList() == null)
//{
//    Label_noDptWarning.Visible = true;
//    Label_noDptWarning.Text = "There are no departments for disbursement";
//    btn_SortingSearch.Enabled = false;
//}


//protected int ReturnIndex(string item_code)
//{
//    int value = -1;
//    for (int i = 0; i < allDptCollectionList.Count; i++)
//    {
//        if (allDptCollectionList[i].itemNum.Trim().ToLower() == item_code.Trim().ToLower())
//        {
//            value = i;
//        }
//    }

//    return value;
//}


//TO UNPACK LIST<COLLECTIONLISTITEM> FROM SESSION VARIABLE
//static List<CollectionListItem> allDptCollectionList;
//allDptCollectionList = new List<CollectionListItem>();

//if (Session["allDptCollectionList"] != null)
//{
//    allDptCollectionList = (List<CollectionListItem>)Session["allDptCollectionList"];
//}

//OLD METHOD FOR VALIDATION OF FIELDS IN GRIDVIEW
//protected int ValidatePreparedQty()
//{
//    bool flag = false;
//    foreach (GridViewRow gvr in gridview_DptSort.Rows)
//    {
//        Label lb = (Label)gvr.FindControl("Label1");

//        int qtyOrder = Convert.ToInt32(gvr.Cells[2].Text);
//        int qtyAvail = Convert.ToInt32(lb.Text);

//        TextBox tb = (TextBox)gvr.FindControl("txt_QtyToSupply");
//        int qtyToPrep = Convert.ToInt32(tb.Text);

//        Label validator = (Label)gvr.FindControl("Label2");
//        validator.Visible = false;

//        if (qtyToPrep > qtyOrder)
//        {
//            validator.Visible = true;
//            validator.Text = "Amount is more than Ordered Qty";
//            flag = true;
//            break;
//        }
//        if (qtyToPrep > qtyAvail)
//        {
//            validator.Visible = true;
//            validator.Text = "Insufficient inventory";
//            flag = true;
//            break;
//        }
//    }
//    if (flag == true)
//        return -1;

//    else
//        return 1;
//}