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
            //keeping track of goods in hand
            List<CollectionListItem> cli = new List<CollectionListItem>();
            foreach (GridViewRow gvr in gv_CollectionList.Rows)
            {
                string itemNumber = gvr.Cells[0].Text; //if BoundField

                TextBox tb = (TextBox)gvr.FindControl("txt_QtyPrepared"); //if TemplateField
                int qtyPrepped = Convert.ToInt32(tb.Text);

                CollectionListItem c = new CollectionListItem(itemNumber, qtyPrepped);
                cli.Add(c);
            }

            //deduct from inventory
            BusinessLogic.DeductFromInventory(cli);

            //Deduct from [TABLE]requisition_order_detail  [column]item_pending_quantity, add to [column]item_distributed_quantity
            CollectionListItem[] tracking = new CollectionListItem[cli.Count];
            cli.CopyTo(tracking);

            List<spGetUndisbursedROList_Result> uro = BusinessLogic.GetUndisbursedROList();
            foreach (var roId in uro)
            {
                foreach (var track in tracking)
                {
                    requisition_order_detail rodFromDB = BusinessLogic.GetRODetailByROIdAndItemNum(roId.requisition_id, track.itemNum);

                    if (rodFromDB != null)
                    {
                        if (track.qtyPrepared >= rodFromDB.item_pending_quantity)
                        {
                            rodFromDB.item_distributed_quantity += rodFromDB.item_pending_quantity;
                            track.qtyPrepared -= rodFromDB.item_pending_quantity;
                            rodFromDB.item_pending_quantity = 0;
                            BusinessLogic.UpdateRODetails(rodFromDB);
                        }
                        else
                        {
                            rodFromDB.item_distributed_quantity += track.qtyPrepared;
                            rodFromDB.item_pending_quantity -= track.qtyPrepared;
                            track.qtyPrepared = 0;
                            BusinessLogic.UpdateRODetails(rodFromDB);
                        }
                    }
                }
            }

            //add to session
            Session["CollectionList"] = cli;

            //Redirect to Sorting Page
            Response.Redirect("~/Protected/DisbursementSorting.aspx");
        }

        protected void gv_CollectionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CollectionList.PageIndex = e.NewPageIndex;
            this.LoadCollectionList();
        }
    }
}