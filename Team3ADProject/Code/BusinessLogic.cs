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
        static LogicUniversityEntities context = new LogicUniversityEntities();

        public static List<spGetCollectionList_Result> GetCollectionList()
        {
            List<spGetCollectionList_Result> list = new List<spGetCollectionList_Result>();
            return list = context.spGetCollectionList().ToList();
        }

        public static void DeductFromInventory(List<CollectionListItem> list)
        {
            foreach (var item in list)
            {
                inventory i = context.inventories.Where(x => x.item_number == item.itemNum).First();
                i.current_quantity -= item.qtyPrepared;
                context.SaveChanges();
            }
        }

        public static List<spGetUndisbursedROList_Result> GetUndisbursedROList()
        {
            List<spGetUndisbursedROList_Result> list = new List<spGetUndisbursedROList_Result>();
            return list = context.spGetUndisbursedROList().ToList();
        }

        public static requisition_order_detail GetRODetailByROIdAndItemNum(string roId, string itemNum)
        {
            requisition_order_detail rod = new requisition_order_detail();
            return rod = context.requisition_order_detail.Where(x => (x.requisition_id == roId) && (x.item_number == itemNum)).FirstOrDefault();
        }


        public static void UpdateRODetails(requisition_order_detail rod)
        {
            requisition_order_detail rodUpdate = context.requisition_order_detail.Where(x => (x.requisition_id == rod.requisition_id) && (x.item_number == rod.item_number)).First();
            rodUpdate.item_distributed_quantity = rod.item_distributed_quantity;
            rodUpdate.item_pending_quantity = rod.item_pending_quantity;
            context.SaveChanges();
        }

        public static List<spGetDepartmentList_Result> GetDepartmentList()
        {
            List<spGetDepartmentList_Result> list = new List<spGetDepartmentList_Result>();
            return list = context.spGetDepartmentList().ToList();
        }

        public static List<spGetRODetailsByROId_Result> GetRODetailsByROId(string roId)
        {
            List<spGetRODetailsByROId_Result> list = new List<spGetRODetailsByROId_Result>();
            return list = context.spGetRODetailsByROId(roId).ToList();
        }

        public static List<spViewCollectionList_Result> ViewCollectionList()
        {
            List<spViewCollectionList_Result> list = new List<spViewCollectionList_Result>();
            return list = context.spViewCollectionList().ToList();
        }

        public static int GetDepartmentPin(string departmentname)
        {
            return (int)context.spGetDepartmentPin(departmentname).ToList().Single();
        }


        //List all adjustment form
        public static List<adjustment> GetAdjustment()
        {
            return context.adjustments.Where(x => x.adjustment_status == "pending" && x.adjustment_price <= 250).ToList();
        }
        //Update adjustment form
        public static void Updateadj(int id, string comment)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Approved";
            adj.manager_remark = comment;
            context.SaveChanges();
        }
        public static void rejectAdj(int id, string comment)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Rejected";
            adj.manager_remark = comment;
            context.SaveChanges();
        }
        public static List<adjustment> Search(DateTime date)
        {
            return context.adjustments.Where(x => x.adjustment_date == date).ToList<adjustment>();
        }       
        //List purchase order
        public static List<purchase_order> GetPurchaseOrders()
        {
            return context.purchase_order.OrderBy(x=>x.suppler_id).Where(x => x.purchase_order_status == "awaiting approval").ToList();
        }
        public static inventory GetInventory(string id)
        {
            return context.inventories.Where(i => i.item_number == id).ToList<inventory>()[0];
        }

        public static System.Collections.IEnumerable GetSupplier(string id)
       //     public static List<(string supplier_name, double unit_price)> GetSupplier(string id)
        {
            var nestedQuery = from s in context.suppliers
                              from sid in s.supplier_itemdetail
                              from i in context.inventories
                              where (sid.item_number == id && i.item_number ==id)
                              orderby (sid.priority)
                              select new { s.supplier_name, sid.unit_price , i.description};
            return nestedQuery.ToList();                
            //return context.supplier_itemdetail.Where(i => i.item_number == id).OrderBy(i => i.priority).ToList<supplier_itemdetail>();
        }
        public static List<inventory> GetActiveInventories()
        {
            return context.inventories.Where(x => x.item_status.ToLower() == "active").ToList();
        }
        public static List<inventory> GetAllInventories()
        {
            return context.inventories.ToList();
        }
        public static List<supplier> GetActiveSuppliers()
        {
            return context.suppliers.Distinct().Where(s => s.supplier_status.ToLower() == "active").ToList();
        }
        public static List<string> GetCategories()
        {
            return context.inventories.OrderBy(x=>x.category).Select(x=> x.category).Distinct().ToList();
        }
        public static int ReturnPendingPOqtyByStatus(inventory item, string status)
        {
            var q = context.purchase_order_detail.Where(x => x.item_purchase_order_status.ToLower().Trim() == "pending" 
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
            var q = context.adjustments.Where(x=>x.adjustment_status.ToLower().Trim() == "pending");
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
            return context.inventories.Where(x => x.category.Trim().ToLower() == category.Trim().ToLower()).ToList();
        }

        // Returns a suggested reorder quantity when give an item code
        // Returns zero if there are no purchase order in the past.
        public static int GetSuggestedReorderQuantity(string itemCode)
        {

            var context = new LogicUniversityEntities();
            var result = context.getRequestedItemQuantityLastYear(itemCode).ToList();
            if (result.Count == 1)
            {
                // Formula: Quantity requested every month
                int quantity = (int)result.First().quantity_requested;
                quantity = quantity / 12;
                return quantity;
            }

            return 0;
        }

        //ViewRO
        public static int GetPlaceIdFromDptId(string dptId)
        {
            spGetPlaceIdFromDptId_Result result = context.spGetPlaceIdFromDptId(dptId).FirstOrDefault();
            return (int)result.place_id;
        }

        //ViewRO
        public static void SpecialRequestReadyUpdates(int placeId, DateTime collectionDate, string collectionStatus, string ro_id)
        {
            context.spSpecialRequestReady(placeId, collectionDate, collectionStatus, ro_id);
        }

    }
}