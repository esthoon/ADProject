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
    public partial class ViewPOHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GenerateGrid();
                DropdonwAddPOStatus();
                DropdonwAddSupplier();
            }
        }

        protected void GenerateGrid()
        {
            List<getAllViewPOHistorytotalcount_Result> total_list = BusinessLogic.viewpohistorytotal();
            GridView1.DataSource = total_list;
            GridView1.DataBind();
        }

        protected void DropdonwAddSupplier()
        {
            List<supplier> list = BusinessLogic.getSupplierNames();
            DropDownList1.Items.Add("All");
            foreach (supplier x in list)
            {
                DropDownList1.Items.Add(x.supplier_name);
            }
        }

        protected void DropdonwAddPOStatus()
        {
            List<purchase_order> list = BusinessLogic.getPOStatus();
            List<string> name = new List<string>();
            foreach (purchase_order x in list)
            {
                string s = x.purchase_order_status.Trim();
                int index = isExisting(s, name);
                if (index == -1)
                {
                    name.Add(s);
                }
                else
                {
                }
            }
            DropDownList2.Items.Add("All");
            foreach (string x in name)
            {
                DropDownList2.Items.Add(x);
            }
        }
        protected int isExisting(string id, List<string> name)
        {
            for (int i = 0; i < name.Count; i++)
            {
                if (name[i] == id)
                    return i;
            }
            return -1;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string supplier;
            if (DropDownList1.SelectedValue == "All")
            {
                supplier = DropDownList1.SelectedValue;
            }
            else
            {
                supplier s = BusinessLogic.getSupplierCode(DropDownList1.SelectedValue);
                supplier = s.supplier_id;
            }
            string status = DropDownList2.SelectedValue;
            int po;
            bool flag;
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                flag = false;
            }
            else
            {
                flag = true;

            }

            if (supplier == "All" && flag == false && status != "All")
            {
                GridView1.DataSource = BusinessLogic.ViewPOHistorytotalcountByStatus(status);
                GridView1.DataBind();
            }

            else if (supplier == "All" && flag == true && status == "All")
            {
                po = Convert.ToInt32(TextBox1.Text);
                GridView1.DataSource = BusinessLogic.viewPOHistorytotalcountbyPO(po);
                GridView1.DataBind();
            }

            else if (supplier == "All" && flag == true && status != "All")
            {
                po = Convert.ToInt32(TextBox1.Text);
                GridView1.DataSource = BusinessLogic.ViewPOHistorytotalcountbyPOandstatus(po, status);
                GridView1.DataBind();
            }

            else if (supplier != "All" && flag == false && status == "All")
            {
                GridView1.DataSource = BusinessLogic.viewPOHistorytotalcountbySupplier(supplier);
                GridView1.DataBind();
            }

            else if (supplier != "All" && flag == true && status == "All")
            {
                po = Convert.ToInt32(TextBox1.Text);
                GridView1.DataSource = BusinessLogic.viewPOHistorytotalcountbyPOandSupplier(po, supplier);
                GridView1.DataBind();
            }

            else if (supplier != "All" && flag == false && status != "All")
            {
                GridView1.DataSource = BusinessLogic.viewPOHistorytotalcountbysupandstatus(supplier, status);
                GridView1.DataBind();
            }

            else if (supplier != "All" && flag == true && status != "All")
            {
                po = Convert.ToInt32(TextBox1.Text);
                GridView1.DataSource = BusinessLogic.viewPOHistorytotalcountbyPOandstatusandSupplier(supplier, po, status);
                GridView1.DataBind();

            }
            else
            {
                GenerateGrid();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           List<getAllViewPOHistorypendingcount_Result> list = BusinessLogic.viewPOHistorypendingcount();
           int r = GridView1.Rows.Count;
           for (int i = 0; i < r; i++)
           {
              Label le = (Label)GridView1.Rows[i].FindControl("pendingcount");
              HiddenField hd = (HiddenField)GridView1.Rows[i].FindControl("HiddenField1");
              int po = Convert.ToInt32(hd.Value);
              bool found = false;
                for (int j = 0; j < list.Count; j++)
                {
                  if (po == list[j].purchase_order_number)
                  {
                    le.Text = Convert.ToString(list[j].outstanding_item_count);
                    found = true;
                    break;
                  }
                }
                if (found == false)
                {
                        le.Text = "0";
                }
           }
        }

        protected void viewpo_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd1 = (HiddenField)b.FindControl("HiddenFieldPO");
            HiddenField hd2 = (HiddenField)b.FindControl("HiddenFieldstatus");
            int po = Convert.ToInt32(hd1.Value);
            string status = hd2.Value;
            if(status == "Pending")
            {
                Response.Redirect("~/protected/PendingPODetails.aspx?Id=" + po);
            }
            else
            {
                Response.Redirect("~/protected/OtherPODetails.aspx?Id=" + po);
            }

        }
    }
}