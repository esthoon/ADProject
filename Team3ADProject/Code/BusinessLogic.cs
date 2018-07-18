using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Team3ADProject.Model;

namespace Team3ADProject.Code
{
    
    public class BusinessLogic
    {
    static LogicUniversityEntities ctx= new LogicUniversityEntities();


        //List all adjustment form
        public static List<adjustment> GetAdjustment()
        {

            return ctx.adjustments.Where(x => x.adjustment_status == "pending" && x.adjustment_price <= 250).ToList();
            

        }

        public static void Updateadj(int id, string comment)
        {
            adjustment adj = ctx.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Approved";
            adj.manager_remark = comment;
            ctx.SaveChanges();

        }

        public static void rejectAdj(int id, string comment)
        {
            adjustment adj = ctx.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Rejected";
            adj.manager_remark = comment;
            ctx.SaveChanges();

        }

        public static List<adjustment> Search(DateTime date)
        {
            return ctx.adjustments.Where(x => x.adjustment_date == date).ToList<adjustment>();
        }

        
        public static List<purchase_order> GetPurchaseOrders()
        {
            return ctx.purchase_order.OrderBy(x=>x.suppler_id).Where(x => x.purchase_order_status == "awaiting approval").ToList();

        }

       

        public static inventory GetInventory(string id)
        {
            LogicUniversityEntities model = new LogicUniversityEntities();
            return model.inventories.Where(i => i.item_number == id).ToList<inventory>()[0];
        }
        public static List<supplier_itemdetail> GetSupplier(string id)
        {
            LogicUniversityEntities model = new LogicUniversityEntities();
            return model.supplier_itemdetail.Where(i => i.item_number == id).OrderBy(i => i.priority).ToList<supplier_itemdetail>();
        }

        public static List<inventory> GetActiveInventories()
        {
            LogicUniversityEntities ctx = new LogicUniversityEntities();
            return ctx.inventories.Where(x => x.item_status.ToLower() == "active").ToList();
        }

        public static List<inventory> GetAllInventories()
        {
            LogicUniversityEntities ctx = new LogicUniversityEntities();
            return ctx.inventories.ToList();
        }

        public static List<supplier> GetActiveSuppliers()
        {
            using(LogicUniversityEntities ctx=new LogicUniversityEntities())
            {
                return ctx.suppliers.Distinct().Where(s => s.supplier_status.ToLower() == "active").ToList();
            }
        }

        public static List<string> GetCategories()
        {
            using (LogicUniversityEntities ctx = new LogicUniversityEntities())
            {
                return ctx.inventories.OrderBy(x=>x.category).Select(x=> x.category).Distinct().ToList();
            }
        }

        public static int ReturnPendingPOqtyByStatus(inventory item, string status)
        {
            LogicUniversityEntities ctx = new LogicUniversityEntities();
            var q = ctx.purchase_order_detail.Where(x => x.item_purchase_order_status.ToLower().Trim() == "pending" 
            && x.purchase_order.purchase_order_status.ToLower().Trim() == status);
            int qty = 0;
            foreach(var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.item_number.ToLower().Trim()))
                {
                    qty += a.item_purchase_order_quantity;
                }
            }
            return qty;
            
        }

        public static int ReturnPendingAdjustmentQty(inventory item)
        {
            LogicUniversityEntities ctx = new LogicUniversityEntities();
            var q = ctx.adjustments.Where(x=>x.adjustment_status.ToLower().Trim() == "pending");
            int qty = 0;
            foreach(var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.item_number.ToLower().Trim()))
                {
                    qty += a.adjustment_quantity;
                }
            }
            return qty;
        }

        public static List<inventory> GetInventoriesByCategory(string category)
        {
            LogicUniversityEntities ctx = new LogicUniversityEntities();
            return ctx.inventories.Where(x => x.category.Trim().ToLower() == category.Trim().ToLower()).ToList();
        }
    }
}