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

            //(2) sort goods according to req_date - write to the distri / pending
            BusinessLogic.SortCollectedGoods(allDptCollectionList);

            //(3) deduct from inventory
            BusinessLogic.DeductFromInventory(allDptCollectionList);

            LoadCollectionList();
            if (gv_CollectionList.Rows.Count == 0)
            {
                //(4) Redirect to Sorting Page
                Response.Redirect("~/Protected/DisbursementSorting.aspx");

            }

        }


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


        protected void gv_CollectionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_CollectionList.PageIndex = e.NewPageIndex;
            this.LoadCollectionList();
        }

        protected void btn_Adjustment_Click(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            HiddenField hd = (HiddenField)lb.FindControl("HiddenField1");
            string itemcode = hd.Value;
            Session["itemcode"] = itemcode;
            string url = "AdjustmentForm1.aspx?itemcode=" + itemcode;
            //Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
        }
    }
}