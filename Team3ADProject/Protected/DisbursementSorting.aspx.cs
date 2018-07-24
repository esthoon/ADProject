using System;
using System.Collections.Generic;
using System.Drawing;
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
        static List<CollectionListItem> allDptCollectionList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRadioButtonList();
                allDptCollectionList = new List<CollectionListItem>();

                if (Session["allDptCollectionList"] != null)
                {
                    allDptCollectionList = (List<CollectionListItem>)Session["allDptCollectionList"];
                }
            }
        }

        protected void btn_SortingSearch_Click(object sender, EventArgs e)
        {
            DisplayDepartmentSortingTable();
        }

        protected void BindRadioButtonList()
        {
            RadioButtonList_Dpt.DataSource = BusinessLogic.GetDepartmentList();
            RadioButtonList_Dpt.DataTextField = "department_name";
            RadioButtonList_Dpt.DataValueField = "department_name";
            RadioButtonList_Dpt.DataBind();
        }

        protected void btn_ReadyForCollection_Click(object sender, EventArgs e)
        {
            //recommended distribution qty must be smaller than required qty or collected qty available(from session), whichever is smaller (validator - front end)

            string dpt_Id = GetDepartmentId();


            //(3) create new collection_detail table row. 
            int placeId = BusinessLogic.GetPlaceIdFromDptId(dpt_Id);
            DateTime collectionDate = DateTime.Parse(TextBox_Collect_Date.Text);
            string collectionStatus = "Pending";

            BusinessLogic.InsertCollectionDetailsRow(placeId, collectionDate, collectionStatus);


            //(4) add RO IDs to requisition_disbursement_detail table w/ newest collection list id
            BusinessLogic.InsertDisbursementListROId(dpt_Id);


            //(5) send email to dpt rep

            ////if department count = 1, leftover items give back to inventory
            //List<spGetUndisbursedROList_Result> roCount = BusinessLogic.GetUndisbursedROList();
            //if (roCount.Count == 1)
            //{
            //    BusinessLogic.DeductFromInventory(allDptCollectionList);
            //}


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

        protected int ReturnIndex(string item_code)
        {
            int value = -1;
            for (int i = 0; i < allDptCollectionList.Count; i++)
            {
                if (allDptCollectionList[i].itemNum.Trim().ToLower() == item_code.Trim().ToLower())
                {
                    value = i;
                }
            }

            return value;
        }

        protected void gridview_DptSort_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gridview_DptSort.Rows)
            {
                Label lb = (Label)gvr.FindControl("Label1");
                string itemcode = gvr.Cells[0].Text;
                int qty = 0;
                foreach (CollectionListItem a in allDptCollectionList)
                {
                    if (a.itemNum.Trim().ToLower() == itemcode.Trim().ToLower())
                    {
                        qty = a.qtyPrepared;
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
            TextBox_Collect_Date.Text = Calendar_Collect_Date.SelectedDate.ToString("dd/MM/yyyy");
        }

        protected void btn_reallocate_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            HiddenField hidden1 = (HiddenField)btn.FindControl("HiddenField1");
            HiddenField hidden2 = (HiddenField)btn.FindControl("HiddenField2");

            Response.Redirect("Reallocate.aspx?itemNum=" + hidden1.Value + "&description=" + hidden2.Value);

        }
    }
}