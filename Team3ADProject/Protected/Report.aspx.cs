using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;
using Team3ADProject.Code;
using System.Globalization;

namespace Team3ADProject.Protected
{
    public partial class Report1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String print = "";


            /*
            // Debugger test
            string format = "dd-mm-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime start = DateTime.ParseExact("01-01-2000", format, provider);
            DateTime end = DateTime.ParseExact("24-07-2018", format, provider);

            var context = new LogicUniversityEntities();
            var query = from po in context.purchase_order
                        join pod in context.purchase_order_detail on po.purchase_order_number equals pod.purchase_order_number
                        join inv in context.inventories on pod.item_number equals inv.item_number
                        where (pod.item_purchase_order_status.Trim() == "Completed" || pod.item_purchase_order_status.Trim() == "Pending")
                        && (po.purchase_order_date.CompareTo(start) >= 0 && po.purchase_order_date.CompareTo(end) <= 0)
                        select new { po, pod, inv };

            foreach (var i in query.ToList())
            {
                print = print + "{" + i.po.purchase_order_detail + ", " + i.inv.item_number + ", " + "}";

                //wcfList.Add(new WCF_PurchaseQuantityByItemCategory(i.Category.Trim(), i.PurchaseQuantity));
            }

            var result = query.GroupBy(cat => cat.inv.category)
                .Select(g => new
                {
                    Category = g.Key.Trim(),
                    PurchaseQuantity = g.Sum(x => x.pod.item_purchase_order_quantity)
                });


            foreach (var i in result.ToList())
            {
                print = print + "{" + i.Category.Trim() + ", " + i.PurchaseQuantity + "}";

                //wcfList.Add(new WCF_PurchaseQuantityByItemCategory(i.Category.Trim(), i.PurchaseQuantity));
            }
            message.InnerText = print;
            */
        }

        protected void ChartList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // message.InnerText = ChartList.SelectedValue;
            // Enable start and end date only if the chart allows it.
            if (ChartList.SelectedValue.Equals(Constants.CHART_REQUISITION_ITEM_QUANTITY_BY_DEPARTMENT)
                || ChartList.SelectedValue.Equals(Constants.CHART_STATIONARIES_ORDERED_BY_CATEGORY)
                )
            {

                String today = DateTime.Now.ToString("dd-MM-yyyy");
                startDate.Value = DateTime.Now.AddMonths(-2).ToString("dd-MM-yyyy");
                endDate.Value = today;

                startDate.Disabled = false;
                endDate.Disabled = false;

                StartDateValidator.Enabled = true;
                EndDateValidator.Enabled = true;
            }

            else
            {
                startDate.Value = "-";
                endDate.Value = "-";

                startDate.Disabled = true;
                endDate.Disabled = true;

                StartDateValidator.Enabled = false;
                EndDateValidator.Enabled = false;
            }
        }
    }
}