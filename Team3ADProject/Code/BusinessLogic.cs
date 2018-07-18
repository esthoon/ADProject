using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;

namespace Team3ADProject.Code
{
    public static class BusinessLogic
    {
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
    }
}