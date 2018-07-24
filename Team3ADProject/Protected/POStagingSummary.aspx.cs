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
    public partial class POStagingSummaryaspx : System.Web.UI.Page
    {
        static List<POStaging> list;
        static employee user;
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonPOApproval.Enabled = true;
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
            list = new List<POStaging>();
            if (Session["StagingList"] != null)
            {
                list = (List<POStaging>)Session["StagingList"];
                GridViewPOStagingSummary.DataSource = list.GroupBy(x => x.Supplier.supplier_name.Trim().ToUpper()).Select(x => new { SupplierID = x.Key, ItemCount = x.Count() }).ToList();
                GridViewPOStagingSummary.DataBind();
                LabelNoResult.Visible = false;

            }
            if (list.Count == 0)
            {
                LabelNoResult.Visible = true;
                ButtonPOApproval.Visible = false;
                ButtonClear.Visible = false;
            }
        }

        protected List<supplier> GetSupplierList()
        {
            return list.Select(x => x.Supplier).Distinct().ToList();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            HiddenField hd = (HiddenField)lb.FindControl("HiddenField1");
            string PODetailsSup = hd.Value;
            Session["PODetailsSup"] = PODetailsSup;
            Label1.Text = PODetailsSup;
            Response.Redirect("POStagingDetails.aspx?PODetailsSup=" + PODetailsSup);

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            HiddenField hf = (HiddenField)btn.FindControl("HiddenField1");
            string sup = hf.Value;
            List<int> indexes = ReturnIndexSupplierMatch(sup);
            using (TransactionScope tx = new TransactionScope())
            {
                for(int i = indexes.Count - 1; i >= 0; i--)
                {
                    list.RemoveAt(indexes[i]);
                }
                tx.Complete();
            }
            Session["StagingList"] = list;
            loadGrid();
        }

        protected List<int> ReturnIndexSupplierMatch(string supname)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Supplier.supplier_name.Trim().ToLower().Equals(supname.ToLower().Trim()))
                {
                    indexes.Add(i);
                }
            }
            return indexes;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkInventory.aspx");
        }

        protected void ButtonPOApproval_Click(object sender, EventArgs e)
        {
            ButtonPOApproval.Enabled = false;
            List<supplier> supplierlist = GetSupplierList();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                using (TransactionScope tx = new TransactionScope())
                {
                    foreach (supplier sup in supplierlist)
                    {
                        string supcode = sup.supplier_id.Trim().ToLower();
                        purchase_order po = new purchase_order()
                        {
                            purchase_order_date = DateTime.ParseExact(date, "yyyy-MM-dd", null),
                            suppler_id = sup.supplier_id,
                            employee_id = user.employee_id,
                            purchase_order_status = "Awaiting approval",
                            manager_remark = null,
                        };
                        BusinessLogic.CreatePO(po);
                        int NewPOnum = po.purchase_order_number;
                        for (int i = 0; i < list.Count; i++)
                        {
                            string DateRequired = list[i].DateRequired.ToString("yyyy-MM-dd");
                            if (list[i].Supplier.supplier_id.ToLower().Trim().Equals(supcode))
                            {
                                purchase_order_detail poDetails = new purchase_order_detail()
                                {
                                    purchase_order_number = NewPOnum,
                                    item_number = list[i].Inventory.item_number,
                                    item_purchase_order_quantity = list[i].OrderedQty,
                                    item_accept_quantity = 0,
                                    item_purchase_order_price = list[i].OrderedQty * list[i].UnitPrice,
                                    purchase_order_item_remark = null,
                                    item_purchase_order_status = "Pending",
                                    item_accept_date = null,
                                    item_required_date = DateTime.ParseExact(DateRequired, "yyyy-MM-dd", null),
                                };
                                BusinessLogic.CreatePOdetails(poDetails);
                            }
                        }
                    }
                    list.Clear();
                    BusinessLogic.sendMail("e0283990@u.nus.edu", "New PO awaiting for approval", user.employee_name + " has submitted a new PO for approval.");
                    tx.Complete();
                    Session["StagingList"] = list;
                    loadGrid();
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }


        }

        protected void GridViewPOStagingSummary_DataBound(object sender, EventArgs e)
        {
            int i = 1;
            foreach (GridViewRow gvr in GridViewPOStagingSummary.Rows)
            {
                gvr.Cells[0].Text = i.ToString();
                HiddenField hf=(HiddenField) gvr.FindControl("HiddenField1");
                i++;
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            list.Clear();
            Session["StagingList"] = list;
            loadGrid();
        }
    }
}