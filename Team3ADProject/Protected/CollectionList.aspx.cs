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
    //JOEL START
    public partial class CollectionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.LoadCollectionList();
            }
        }

        // Loads the list of items to collect from warehouse for all departments
        protected void LoadCollectionList()
        {
            gv_CollectionList.DataSource = BusinessLogic.GetCollectionList();
            gv_CollectionList.DataBind();
        }

        //Submits the items that have been collected. 
        protected void btn_submitCollectionList_Click(object sender, EventArgs e)
        {
            //validate if textbox value is less than ordered & inventory
            if (ValidatePreparedQty() < 0)
            {
                return;
            }

            //(1) tracking qty collected per item for all dpts, store in a list of Collection objs
            List<CollectionListItem> allDptCollectionList = new List<CollectionListItem>();

            foreach (GridViewRow gvr in gv_CollectionList.Rows)
            {
                string itemNumber = gvr.Cells[0].Text;

                TextBox tb = (TextBox)gvr.FindControl("txt_QtyPrepared");
                int qtyPrepped = Convert.ToInt32(tb.Text);

                CollectionListItem c = new CollectionListItem(itemNumber, qtyPrepped);
                allDptCollectionList.Add(c);
            }

            //(2) sort goods according to req_date - write to the distri / pending tables
            BusinessLogic.SortCollectedGoods(allDptCollectionList);

            //(3) deduct collected items from inventory
            BusinessLogic.DeductFromInventory(allDptCollectionList);

            LoadCollectionList();
            if (gv_CollectionList.Rows.Count == 0)
            {
                //(4) Redirect to Sorting Page if gridview has no more rows (no more items to collect)
                Response.Redirect("~/Protected/DisbursementSorting.aspx");

            }

        }

        //validates that qty collected is not more than either the qty availabe in inventory, or the qty orderd by dpts 
        protected int ValidatePreparedQty()
        {
            bool flag = false;
            foreach (GridViewRow gvr in gv_CollectionList.Rows)
            {
                int qtyOrder = Convert.ToInt32(gvr.Cells[3].Text);
                int qtyAvail = Convert.ToInt32(gvr.Cells[4].Text);

                TextBox tb = (TextBox)gvr.FindControl("txt_QtyPrepared");
                int qtyToPrep = Convert.ToInt32(tb.Text);

                Label validator = (Label)gvr.FindControl("Label1");
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

        //allows pagination for gridview.
        protected void gv_CollectionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CollectionList.PageIndex = e.NewPageIndex;
            this.LoadCollectionList();
        }

        //allows user to perform adjustment if they find broken / missing items during collection
        protected void btn_Adjustment_Click(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            HiddenField hd = (HiddenField)lb.FindControl("HiddenField1");
            string itemcode = hd.Value;
            Session["itemcode"] = itemcode;
            string url = "AdjustmentForm1.aspx?itemcode=" + itemcode;
            //Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>"); // decided to open on html side.
        }
    }
}