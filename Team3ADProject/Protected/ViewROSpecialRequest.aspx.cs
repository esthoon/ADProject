using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;
using Team3ADProject.Code;
using System.Drawing;
using System.Globalization;

namespace Team3ADProject.Protected
{
    public partial class ViewROSpecialRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label_ViewRO.Visible = false;
                TextBox_Collect_Date.Visible = false;
                Calendar_Collect_Date.Visible = false;
                btn_readyForCollect.Visible = false;
                Label1.Visible = false;
                RequiredFieldValidator1.Visible = false;
                Label2.Visible = false;
            }
        }

        protected void btn_SortingSearch_Click(object sender, EventArgs e)
        {
            gv_ViewRO.DataSource = BusinessLogic.GetRODetailsByROId(txt_searchByRO.Text.Trim());
            gv_ViewRO.DataBind();
            Label_ViewRO.Text = txt_searchByRO.Text.ToUpper();
            NoRowDetail();
        }

        protected void btn_readyForCollect_Click(object sender, EventArgs e)
        {

            if (ValidatePreparedQty() < 0)
            {
                return;
            }

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

            /**
              String s = TextBox2.Text;
                DateTime dt = DateTime.ParseExact(s, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                String x = dt.ToString("yyyy-MMMM-dd");
                DateTime search = Convert.ToDateTime(x);
             */
            string s = TextBox_Collect_Date.Text;
            DateTime dt = DateTime.ParseExact(s, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            String x = dt.ToString("yyyy-MMMM-dd");
            DateTime collectionDate = Convert.ToDateTime(x);
            //DateTime collectionDate = DateTime.Parse(TextBox_Collect_Date.Text); //joel
            string ro_id = Label_ViewRO.Text.ToUpper();
            BusinessLogic.SpecialRequestReadyUpdatesCDRDD(placeId, collectionDate, ro_id, dptId);

            // (3) change amounts in requisition_order_detail table

            BusinessLogic.ViewROSpecialRequestUpdateRODTable(clList, ro_id);
           

            // (4) deduct from inventory
            BusinessLogic.DeductFromInventory(clList);

            // (5) send email

            string emailAdd = BusinessLogic.GetDptRepEmailAddFromDptID(dptId);
            string subj = "Your ordered stationery is ready for collection";
            string body = "Dear Department Rep, your stationery order is ready for collection. Please procede to your usual collection point at the correct time.";

            BusinessLogic.sendMail(emailAdd, subj, body);

            Response.Redirect("~/Protected/ViewROSpecialRequest.aspx");

        }

        protected void NoRowDetail()
        {
            if (gv_ViewRO.Rows.Count <= 0)
            {
                Label_ViewRO.Visible = false;
                TextBox_Collect_Date.Visible = false;
                Calendar_Collect_Date.Visible = false;
                btn_readyForCollect.Visible = false;
                Label1.Visible = false;
                RequiredFieldValidator1.Visible = false;
                Label2.Visible = false;

            }
            else
            {
                Label_ViewRO.Visible = true;
                TextBox_Collect_Date.Visible = true;
                Calendar_Collect_Date.Visible = true;
                btn_readyForCollect.Visible = true;
                Label1.Visible = true;
                RequiredFieldValidator1.Visible = true;
                Label2.Visible = true;
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

        protected void btn_Adjustment_Click(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            HiddenField hd = (HiddenField)lb.FindControl("HiddenField1");
            string itemcode = hd.Value;
            Session["itemcode"] = itemcode;
            string url = "AdjustmentForm1.aspx?itemcode=" + itemcode;
            Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
        }

        protected int ValidatePreparedQty()
        {
            bool flag = false;
            foreach (GridViewRow gvr in gv_ViewRO.Rows)
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
    }
}