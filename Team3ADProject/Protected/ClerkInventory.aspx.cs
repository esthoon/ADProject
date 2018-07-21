using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;
using System.Drawing;

//Esther
namespace Team3ADProject.Protected
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static List<cInventory> cList = new List<cInventory>();
        private static List<cInventory> cCatList = new List<cInventory>();
        private static List<cInventory> lowinstock = new List<cInventory>();
        private static employee user;
        protected void loadGrid(List<cInventory> list)
        {
            gvInventoryList.DataSource = list;
            gvInventoryList.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] != null)
                {
                    user = (employee)Session["user"];
                }
                else
                {
                    //hardcoded
                    user = BusinessLogic.GetEmployeeById(10);
                    Session["user"] = user;
                    //redirect to login homepage
                }
                cList = getCInventoryList(BusinessLogic.GetActiveInventories());
                loadGrid(cList);
                List<string> categories = new List<string>();
                categories.Add("All Categories");
                foreach (string a in BusinessLogic.GetCategories())
                {
                    categories.Add(a);
                }
                ddlCategory.DataSource = categories;
                ddlCategory.DataBind();
                Label1.Visible = false;
                btnAllPO.Visible = false;
            }

        }

        //convert inventory to cInventory
        protected List<cInventory> getCInventoryList(List<inventory> list)
        {
            List<cInventory> returnlist = new List<cInventory>();
            foreach (inventory a in list)
            {
                returnlist.Add(new cInventory(a));
            }
            return returnlist;
        }

        //select all or low-in-stock inventories
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
            ddlCategory.SelectedIndex = 0;
            List<cInventory> list = new List<cInventory>();
            if (RadioButtonList1.SelectedItem.Value.Equals("1"))
            {
                list = getCInventoryList(BusinessLogic.GetActiveInventories());
                btnAllPO.Visible = false;
                btnAllPO.Enabled = false;
                CheckBox1.Enabled = true;
                CheckBox1.Visible = true;
            }
            else if (RadioButtonList1.SelectedItem.Value.Equals("2"))
            {
                lowinstock = GetLowInStockItems();
                list = lowinstock;
                btnAllPO.Visible = true;
                btnAllPO.Enabled = true;
                CheckBox1.Enabled = false;
                CheckBox1.Visible = false;
            }
            loadGrid(list);
        }

        //show obsolete items
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedItem.Value.Equals("1"))
            {
                string category = ddlCategory.SelectedItem.Text.ToLower().Trim();
                List<cInventory> list = getList();
                string search = TextBox1.Text.ToLower().Trim();
                if (search != null)
                {
                    list = SearchResult(search);
                    if (category != "all categories")
                    {
                        list = list.Where(x => x.Inventory.category.ToLower().Trim() == category).ToList();
                    }
                }
                loadGrid(list);
                showNoResult(list);
            }
        }
        //get all active inventories
        protected List<cInventory> getList()
        {
            if (CheckBox1.Checked)
            {
                return getCInventoryList(BusinessLogic.GetAllInventories());
            }
            else
            {
                return getCInventoryList(BusinessLogic.GetActiveInventories());
            }
        }

        //category change event handler
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Text = string.Empty;
            loadGrid(filterCat());
            showNoResult(filterCat());
        }

        //display label
        protected void showNoResult(List<cInventory> list)
        {
            if (list.Count() > 0)
            {
                Label1.Visible = false;
            }
            else
            {
                Label1.Text = "No results found";
                Label1.Visible = true;
            }
        }

        //method for filtering category
        private List<cInventory> filterCat()
        {
            string category = ddlCategory.SelectedItem.Text.ToLower().Trim();
            List<cInventory> list = new List<cInventory>();
            if (RadioButtonList1.SelectedItem.Value.Equals("1"))
            {
                list = getList();
            }
            else if (RadioButtonList1.SelectedItem.Value.Equals("2"))
            {
                lowinstock = GetLowInStockItems();
                list = lowinstock;
            }
            if (category != "all categories")
            {
                return list.Where(x => x.Inventory.category.ToLower().Trim() == category).ToList();
            }
            else
            {
                return list;
            }
        }

        //styling the grid, grid databound event handler
        protected void gvInventoryList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string OrStatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Inventory.item_status"));
                if (OrStatus.ToLower().Trim() == "obsolete")
                {
                    //You can use whatever you want to play with rows
                    //e.Row.Cells[0].Font.Bold = true;
                    //e.Row.Cells[2].CssClass = "gridcss";
                    //e.Row.ForeColor = System.Drawing.Color.LightGray;
                    e.Row.BackColor = System.Drawing.Color.DarkGray;
                    (e.Row.FindControl("Button1") as Button).Enabled = false;
                    (e.Row.FindControl("Button1") as Button).BackColor = System.Drawing.Color.DarkSlateGray;
                    (e.Row.FindControl("Button2") as Button).Enabled = false;
                    (e.Row.FindControl("Button2") as Button).BackColor = System.Drawing.Color.DarkSlateGray;
                }
            }
        }

        //Search button event handler
        protected void Button1_Click(object sender, EventArgs e)
        {
            string search = TextBox1.Text.ToLower();
            List<cInventory> list = new List<cInventory>();
            if (search != null)
            {
                list = SearchResult(search);
            }
            showNoResult(list);

            loadGrid(list);
        }

        //search result method
        protected List<cInventory> SearchResult(string search)
        {
            return filterCat().Where(x => x.Inventory.description.Trim().ToLower().Contains(search)).ToList();
        }

        //adjustmentform button event handler
        protected void Button2_Click(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            HiddenField hd = (HiddenField)lb.FindControl("HiddenField1");
            string itemcode = hd.Value;
            Session["itemcode"] = itemcode;
            string url = "AdjustmentForm.aspx?itemcode=" + itemcode;
            Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
            //Response.Redirect(url);
        }

        //place po for all low-in-stock items event handler
        protected void btnAllPO_Click(object sender, EventArgs e)
        {
            //lowinstock = getCInventoryList(BusinessLogic.GetLowInStockInventories());
            List<POStaging> purchaseorderlist = new List<POStaging>();
            List<POStaging> StagingList = new List<POStaging>();
            if (Session["StagingList"] != null)
            {
                StagingList = (List<POStaging>)Session["StagingList"];
            }
            try
            {
                purchaseorderlist = ConvertListToPOStaging(lowinstock);
                foreach (POStaging a in purchaseorderlist)
                {
                    StagingList = BusinessLogic.AddToStaging(StagingList, a);
                }
                Session["StagingList"] = StagingList;
                Response.Redirect("POStagingSummary.aspx");
                //Label2.Text = purchaseorderlist[2].Supplier.supplier_id;
            }
            catch (Exception ex)
            {
                Label2.Text = "1 " + ex.Message;
            }

        }

        //convert cInventory list to POStaging List
        protected List<POStaging> ConvertListToPOStaging(List<cInventory> blist)
        {
            List<POStaging> alist = new List<POStaging>();
            string requiredDate = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd");
            employee user = new employee();
            if (Session["user"] != null)
            {
                user = (employee)Session["user"];
            }
            else
            {
                user = BusinessLogic.GetEmployeeById(10);
            }
            foreach (cInventory a in blist)
            {
                inventory item = a.Inventory;
                supplier_itemdetail supplieritem = BusinessLogic.GetPrioritySupplierItemDetail(item.item_number);
                alist.Add(new POStaging(item, supplieritem.supplier_id, a.reorder_quantity, supplieritem.unit_price, DateTime.ParseExact(requiredDate, "yyyy-MM-dd", null), user));
            }
            return alist;
        }

        //POStagingSummary button eventhandler
        protected void Button2_Click1(object sender, EventArgs e)
        {
            Response.Redirect("POStagingSummary.aspx");
        }

        //Logic for low-in-stock items
        protected List<cInventory> GetLowInStockItems()
        {
            return cList.Where(x => ((x.Inventory.current_quantity + x.OrderedQty + x.PendingApprovalQty) < x.Inventory.reorder_level)).ToList();
        }

        //direct to PO page
        protected void Button1_Click1(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            HiddenField hd = (HiddenField)lb.FindControl("HiddenField1");
            string itemid = hd.Value;
            Session["itemid"] = itemid;
            string url = "PlacePurchaseOrderForm.aspx?itemid=" + itemid;
            Response.Redirect(url);
        }
    }
}