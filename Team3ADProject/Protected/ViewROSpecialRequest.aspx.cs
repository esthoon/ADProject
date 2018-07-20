using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;
using Team3ADProject.Code;
using System.Drawing;

namespace Team3ADProject.Protected
{
    public partial class ViewROSpecialRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_SortingSearch_Click(object sender, EventArgs e)
        {
            gv_ViewRO.DataSource = BusinessLogic.GetRODetailsByROId(txt_searchByRO.Text);
            gv_ViewRO.DataBind();
            Label_ViewRO.Text = txt_searchByRO.Text;
        }

        protected void btn_readyForCollect_Click(object sender, EventArgs e)
        {
            List<CollectionListItem> clList = new List<CollectionListItem>();
            foreach (GridViewRow gvr in gv_ViewRO.Rows)
            {
                string itemNumber = gvr.Cells[0].Text;

                TextBox tb = (TextBox)gvr.FindControl("txt_QtyPrepared");
                int qtyPrepped = Convert.ToInt32(tb.Text);

                CollectionListItem c = new CollectionListItem(itemNumber, qtyPrepped);
                clList.Add(c);
            }


            // (1) insert collection_details + (2) add RO Ids & their collection_id to requisition_disbursement_detail [table]
            string dptId = (Label_ViewRO.Text).Substring(0, 4);
            int placeId = BusinessLogic.GetPlaceIdFromDptId(dptId);
            DateTime collectionDate = DateTime.Parse(TextBox_Collect_Date.Text);
            string collectionStatus = "Pending";
            string ro_id = Label_ViewRO.Text;
            BusinessLogic.SpecialRequestReadyUpdates(placeId, collectionDate, collectionStatus, ro_id);


            // (3) change amounts in requisition_order_detail table
            foreach (var item in clList)
            {
                requisition_order_detail rodFromDB = BusinessLogic.GetRODetailByROIdAndItemNum(ro_id, item.itemNum);

                if (rodFromDB != null)
                {
                    rodFromDB.item_distributed_quantity += item.qtyPrepared;
                    rodFromDB.item_pending_quantity -= item.qtyPrepared;
                    BusinessLogic.UpdateRODetails(rodFromDB);
                }
            }

            // (4) deduct from inventory
            BusinessLogic.DeductFromInventory(clList);

            // (5) send email

            Response.Redirect("~/Protected/ViewROSpecialRequest.aspx");

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
    }
}