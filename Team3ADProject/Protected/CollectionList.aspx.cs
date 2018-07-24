using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
    public partial class CollectionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadCollectionList();
            }
        }

        protected void LoadCollectionList()
        {
            gv_CollectionList.DataSource = BusinessLogic.GetCollectionList();
            gv_CollectionList.DataBind();
        }

        protected void btn_submitCollectionList_Click(object sender, EventArgs e)
        {
            //(1) tracking qty collected per item for all dpts
            List<CollectionListItem> allDptCollectionList = new List<CollectionListItem>();

            foreach (GridViewRow gvr in gv_CollectionList.Rows)
            {
                string itemNumber = gvr.Cells[0].Text; //if BoundField

                TextBox tb = (TextBox)gvr.FindControl("txt_QtyPrepared"); //if TemplateField
                int qtyPrepped = Convert.ToInt32(tb.Text);

                CollectionListItem c = new CollectionListItem(itemNumber, qtyPrepped);
                allDptCollectionList.Add(c);
            }


            //(2) sort goods according to req_date - write to the distri / pending
            List<spGetFullCollectionROIDList_Result> list = BusinessLogic.GetFullCollectionROIDList();

            foreach (var collectedItem in allDptCollectionList)
            {
                int store = collectedItem.qtyPrepared;
                foreach (var roidListItem in list)
                {
                    {
                        if (collectedItem.itemNum.Trim() == roidListItem.item_number.Trim())
                        {
                            if (collectedItem.qtyPrepared >= roidListItem.item_pending_quantity)
                            {
                                roidListItem.item_distributed_quantity += roidListItem.item_pending_quantity;
                                collectedItem.qtyPrepared -= roidListItem.item_pending_quantity;
                                roidListItem.item_pending_quantity = 0;

                                requisition_order_detail rod = new requisition_order_detail();
                                rod.requisition_id = roidListItem.requisition_id;
                                rod.item_number = roidListItem.item_number;
                                rod.item_distributed_quantity = roidListItem.item_distributed_quantity;
                                rod.item_pending_quantity = roidListItem.item_pending_quantity;
                                BusinessLogic.UpdateRODetails(rod);
                            }
                            else
                            {
                                roidListItem.item_distributed_quantity += collectedItem.qtyPrepared;
                                roidListItem.item_pending_quantity -= collectedItem.qtyPrepared;
                                collectedItem.qtyPrepared = 0;

                                requisition_order_detail rod = new requisition_order_detail();
                                rod.requisition_id = roidListItem.requisition_id;
                                rod.item_number = roidListItem.item_number;
                                rod.item_distributed_quantity = roidListItem.item_distributed_quantity;
                                rod.item_pending_quantity = roidListItem.item_pending_quantity;
                                BusinessLogic.UpdateRODetails(rod);
                                BusinessLogic.UpdateRODetails(rod);
                            }
                        }
                    }
                }

                collectedItem.qtyPrepared = store;
            }



            //(3) deduct from inventory
            BusinessLogic.DeductFromInventory(allDptCollectionList);

            // store in session, pass to next page.
            Session["allDptCollectionList"] = allDptCollectionList;

            //(4) Redirect to Sorting Page
            Response.Redirect("~/Protected/DisbursementSorting.aspx");
        }

        protected void gv_CollectionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CollectionList.PageIndex = e.NewPageIndex;
            this.LoadCollectionList();
        }
        protected void btn_Adjustment_Click(object sender, EventArgs e)
        {
            //Add link to redirect to adjustment form
            //Response.Redirect("~/Protected/DisbursementSorting.aspx");

        }

    }
}