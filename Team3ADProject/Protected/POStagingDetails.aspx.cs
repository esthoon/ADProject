using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;
using Team3ADProject.Code;
using System.Transactions;

//esther
namespace Team3ADProject.Protected
{
    public partial class POStagingDetails : System.Web.UI.Page
    {
        static List<POStaging> polist = new List<POStaging>();
        static string param_supname;
        static string supplierid;
        static employee user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Employee"] != null)
                {
                    int employeeid = (int)Session["Employee"];
                    user = BusinessLogic.GetEmployeeById(employeeid);
                }
                else
                {
                    //hardcoded
                    Session["Employee"] = 10;
                    user = BusinessLogic.GetEmployeeById(10);
                    //redirect to login homepage
                }
                loadGrid();
            }
        }

        protected void loadGrid()
        {
            param_supname = Request.QueryString["PODetailsSup"];
            supplierid = BusinessLogic.GetSupplierID(param_supname);
            List<POStaging> display = new List<POStaging>();
            if (Session["StagingList"] != null)
            {
                polist = (List<POStaging>)Session["StagingList"];
                GridViewPODetails.DataSource = polist.Where(x => x.Supplier.supplier_name.ToLower().Trim() == param_supname.ToLower().Trim());
                GridViewPODetails.DataBind();
                Label1.Visible = false;
                LabelSupplier.Text = param_supname;
            }
            if (polist.Count == 0)
            {
                Label1.Visible = true;
                Button3.Enabled = false;
                Button3.Visible = false;
                LabelSupplier.Visible = false;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            HiddenField hf1 = (HiddenField)tb.FindControl("HiddenField1");
            int index = Int32.Parse(hf1.Value);
            polist[index].OrderedQty = Int32.Parse(tb.Text);
            Session["StagingList"] = polist;
            loadGrid();
        }

        protected void GridViewPODetails_DataBound(object sender, EventArgs e)
        {
            UpdateTotalCostGrid();
        }

        protected void UpdateTotalCostGrid()
        {
            int i = 1;
            foreach (GridViewRow gvr in GridViewPODetails.Rows)
            {
                TextBox tb = (TextBox)gvr.FindControl("TextBox1");
                int orderedqty = Int32.Parse(tb.Text);
                HiddenField hf3 = (HiddenField)gvr.FindControl("HiddenField3");
                gvr.Cells[6].Text = String.Format("{0:c}", (Double.Parse(hf3.Value) * orderedqty));
                HiddenField hf1 = (HiddenField)gvr.FindControl("HiddenField1");
                string index = polist.FindIndex(x => x.Inventory.item_number.Trim().ToLower() == gvr.Cells[3].Text.ToLower().Trim() && x.Supplier.supplier_id.Trim().ToLower() == supplierid.Trim().ToLower()).ToString();
                hf1.Value = index;
                gvr.Cells[0].Text = i.ToString();
                i++;
            }
        }

        protected static int ReturnIndex(List<POStaging> StagingList, string itemcode, string suppliercode)
        {
            int value = -1;
            if (StagingList != null)
            {
                for (int i = 0; i < StagingList.Count(); i++)
                {
                    string aItemCode = StagingList[i].Inventory.item_number.Trim().ToLower();
                    string aSupplier = StagingList[i].Supplier.supplier_id.Trim().ToLower();
                    if (itemcode.Equals(aItemCode) && suppliercode.Equals(aSupplier))
                    {
                        value = i;
                    }
                }
            }
            return value;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Button btn = (Button)sender;
                TextBox tb = (TextBox)btn.FindControl("TextBox1");
                HiddenField hf = (HiddenField)btn.FindControl("HiddenField1");
                int qty = Int32.Parse(tb.Text);
                int index = Int32.Parse(hf.Value);
                polist[index].OrderedQty = qty;
                Session["StagingList"] = polist;
                loadGrid();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            HiddenField hf = (HiddenField)btn.FindControl("HiddenField1");
            int index = Int32.Parse(hf.Value);
            polist.RemoveAt(index);
            Session["StagingList"] = polist;
            loadGrid();

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Button3.Enabled = false;
            if (user != null && supplierid != null)
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                List<int> indexes = new List<int>();
                for (int i = 0; i < polist.Count; i++)
                {
                    if (polist[i].Supplier.supplier_id.Trim().ToLower() == supplierid.Trim().ToLower())
                    {
                        indexes.Add(i);
                    }
                }
                using (TransactionScope tx = new TransactionScope())
                {
                    try
                    {
                        //create purchase order
                        purchase_order po = new purchase_order()
                        {
                            purchase_order_date = DateTime.ParseExact(date, "yyyy-MM-dd", null),
                            suppler_id = supplierid,
                            employee_id = user.employee_id,
                            purchase_order_status = "Awaiting approval",
                            manager_remark = null,
                        };
                        try
                        {
                            BusinessLogic.CreatePO(po);
                            int NewPOnum = po.purchase_order_number;

                            foreach (int index in indexes)
                            {
                                string DateRequired = polist[index].DateRequired.ToString("yyyy-MM-dd");

                                try
                                {
                                    //create purchase order details
                                    purchase_order_detail poDetails = new purchase_order_detail()
                                    {
                                        purchase_order_number = NewPOnum,
                                        item_number = polist[index].Inventory.item_number,
                                        item_purchase_order_quantity = polist[index].OrderedQty,
                                        item_accept_quantity = 0,
                                        item_purchase_order_price = polist[index].OrderedQty * polist[index].UnitPrice,
                                        purchase_order_item_remark = null,
                                        item_purchase_order_status = "Pending",
                                        item_accept_date = null,
                                        item_required_date = DateTime.ParseExact(DateRequired, "yyyy-MM-dd", null),
                                    };
                                    BusinessLogic.CreatePOdetails(poDetails);
                                }
                                catch (Exception ex)
                                {
                                    Label3.Text = "4" + ex.Message;
                                }
                            }
                            try
                            {
                                for(int k = indexes.Count - 1; k >= 0; k--)
                                {
                                    polist.RemoveAt(indexes[k]);
                                }

                                BusinessLogic.sendMail("e0283990@u.nus.edu", "New PO awaiting for approval", user.employee_name + " has submitted a new PO for approval.");
                                tx.Complete();
                                Session["StagingList"] = polist;
                                Response.Redirect("POStagingSummary.aspx");
                            }
                            catch (Exception ex)
                            {
                                Label3.Text = "3" + ex.Message;
                            }
                        }
                        catch (Exception ex)
                        {
                            Label3.Text = "2" + ex.Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        Label3.Text = "1" + ex.Message;
                    }
                }

            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("POStagingSummary.aspx");
        }
    }
}