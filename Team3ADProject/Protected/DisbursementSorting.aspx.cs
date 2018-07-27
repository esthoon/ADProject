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

        protected void BindRadioButtonList()
        {
            //DISPLAY ONLY DEPARTMENTS WITH ITEMS TO COLLECT
            List<string> dptList = new List<string>();
            dptList = BusinessLogic.DisplayListofDepartmentsForCollection();

            DataTable dt = new DataTable();
            dt.Columns.Add("DepartmentName", typeof(string));
            //DataRow dr;

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


        protected void btn_SortingSearch_Click(object sender, EventArgs e)
        {
            DisplayDepartmentSortingTable();
            NoRowDetail();
        }

        protected void btn_ReadyForCollection_Click(object sender, EventArgs e)
        {
            //(1) recommended distribution qty must be smaller than required qty or collected qty available(from session), whichever is smaller (validator - front end)
            if (ValidatePreparedQty() < 0)
            {
                return;
            }

            //(2) create new collection_detail table row. 
            string dpt_Id = GetDepartmentId();

            int placeId = BusinessLogic.GetPlaceIdFromDptId(dpt_Id);
            string s = TextBox_Collect_Date.Text;
            DateTime dt = DateTime.ParseExact(s, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            String x = dt.ToString("yyyy-MMMM-dd");
            DateTime collectionDate = Convert.ToDateTime(x);

            BusinessLogic.InsertCollectionDetailsRow(placeId, collectionDate, dpt_Id);

            //(3) add RO IDs to requisition_disbursement_detail table w/ newest collection list id
            BusinessLogic.InsertDisbursementListROId(dpt_Id);


            //(4) send email to dpt rep
            string emailAdd = "joelfong@gmail.com";            //NEED TO UPDATE TO DPT REP EMAIL
            string subj = "Your ordered stationery is ready for collection";
            string body = "Your order is ready for collection. Please procede to your usual collection point at the correct time.";

            BusinessLogic.sendMail(emailAdd, subj, body);

            Response.Redirect(Request.RawUrl);
        }

        protected string GetDepartmentId()
        {
            string selectedDptName = RadioButtonList_Dpt.SelectedItem.Value;
            return BusinessLogic.GetDptIdFromDptName(selectedDptName);
        }

        protected void DisplayDepartmentSortingTable()
        {
            string dpt_Id = GetDepartmentId();
            gridview_DptSort.DataSource = BusinessLogic.GetSortingListByDepartment(dpt_Id);
            gridview_DptSort.DataBind();
        }

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


        protected void Calendar_Collect_Date_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date <= DateTime.Now)
            {
                e.Cell.BackColor = ColorTranslator.FromHtml("#a9a9a9");
                e.Day.IsSelectable = false;
            }
        }

        protected void Calendar_Collect_Date_SelectionChanged(object sender, EventArgs e)
        {
            TextBox_Collect_Date.Text = Calendar_Collect_Date.SelectedDate.ToString("dd-MM-yyyy");
        }

        protected void btn_reallocate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            HiddenField hidden1 = (HiddenField)btn.FindControl("HiddenField1");
            HiddenField hidden2 = (HiddenField)btn.FindControl("HiddenField2");

            Response.Redirect("Reallocate.aspx?itemNum=" + hidden1.Value + "&description=" + hidden2.Value);

        }

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

        protected int ValidatePreparedQty()
        {
            bool flag = false;
            foreach (GridViewRow gvr in gridview_DptSort.Rows)
            {
                Label lb = (Label)gvr.FindControl("Label1");

                int qtyOrder = Convert.ToInt32(gvr.Cells[2].Text);
                int qtyAvail = Convert.ToInt32(lb.Text);

                TextBox tb = (TextBox)gvr.FindControl("txt_QtyToSupply");
                int qtyToPrep = Convert.ToInt32(tb.Text);

                Label validator = (Label)gvr.FindControl("Label2");
                validator.Visible = false;

                if (qtyToPrep > qtyOrder)
                {
                    validator.Visible = true;
                    validator.Text = "Amount is more than Ordered Qty";
                    flag = true;
                    break;
                }
                if (qtyToPrep > qtyAvail)
                {
                    validator.Visible = true;
                    validator.Text = "Insufficient inventory";
                    flag = true;
                    break;
                }
            }
            if (flag == true)
                return -1;

            else
                return 1;
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