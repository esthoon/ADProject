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

        //List All Active Inventory
        public static List<inventory> GetActiveInventory()
        {
            return context.inventories.Where(p => p.item_status == "Active").ToList<inventory>();
        }

        //Return inventory which matches description
        public static List<inventory> SearchActiveInventory(string search)
        {
            return context.inventories.Where(P => P.item_status == "Active" && P.description.Contains(search)).ToList<inventory>();
        }

        //Return selected inventory item
        public static inventory GetInventoryById(string Id)
        {
            return context.inventories.Where(P => P.item_number == Id).FirstOrDefault();
        }

        //Return selected inventory unit price
        public static double GetItemUnitPrice(string Id)
        {
            List<supplier_itemdetail> l = new List<supplier_itemdetail>();
            l = context.supplier_itemdetail.Where(P => P.item_number == Id).ToList<supplier_itemdetail>();
            double s = 0;
            s = l[0].unit_price;
            return s;
        }

        //Add new entry to Requisition order table
        public static void AddNewRequisitionOrder(string id, int emp, DateTime d )
        {

            requisition_order r = new requisition_order();
            r.requisition_id = id;
            r.employee_id = emp;
            r.requisition_date = d;
            r.requisition_status = "Pending";
            context.requisition_order.Add(r);
            context.SaveChanges();
        }

        //Add new entry to Requisition order detail table
        public static void AddRequisitionOrderDetail(cart c, string id)
        {
            requisition_order_detail rod = new requisition_order_detail();
            rod.requisition_id = id;
            rod.item_number = c.Inventory.item_number;
            rod.item_requisition_quantity = c.Quantity;
            rod.item_distributed_quantity = 0;
            rod.item_pending_quantity = c.Quantity;
            rod.item_requisition_price = c.Itemprice;
            context.requisition_order_detail.Add(rod);
            context.SaveChanges();
        }

        //return detail of requisition order by Id (For Request Confirmation)
        public static requisition_order GetRequisitionOrderById(string Id)
        {
            return context.requisition_order.Where(x => x.requisition_id == Id).First();
        }

        //Return status of requisition orders for dropdownlist
        public static List<GetRequisitionStatus_Result> GetRequisitionStatus()
        {
            return context.GetRequisitionStatus().ToList();
        }

        //Return Pending requisition order by employee
        public static List<requisition_order> GetPendingRequisitionByEmployee(int id)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status=="Pending").ToList<requisition_order>();
        }

        //Return all requisition by employee
        public static List<requisition_order> GetAllRequisitionByEmployee(int id)
        {
            return context.requisition_order.Where(x => x.employee_id == id).ToList<requisition_order>();
        }

        //Return requisition by employee with date for all status
        public static List<requisition_order> GetRequisitionByEmployeeSearchDateAllStatus(int i, DateTime d)
        {
            return context.requisition_order.Where(x => x.employee_id == i && x.requisition_date == d).ToList();
        }

        //return requisition by employee with date for status
        public static List<requisition_order> GetRequisitionByEmployeeSearchDate(int id, DateTime d, string status)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == status && x.requisition_date == d).ToList();
        }

        //return requisition by employee with status
        public static List<requisition_order> GetRequisitionByEmployeeSearchStatus(int id, string status)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == status).ToList();
        }

        //Return requisition order detail by id
        public static List<getRequisitionOrderDetails_Result> GetRequisitionorderDetail(string id)
        {
            List<getRequisitionOrderDetails_Result> l = new List<getRequisitionOrderDetails_Result>();
            return l = context.getRequisitionOrderDetails(id).ToList();
        }

        public static void Cancelrequisition(string id)
        {
            requisition_order r = context.requisition_order.Where(x => x.requisition_id == id).First();
            r.requisition_status = "Cancel";
            context.SaveChanges();
        }

        public static void UpdateRequisitionOrderDetail(string id, List<getRequisitionOrderDetails_Result> order )
        {
            if (order.Count > 0)
            {
                List<requisition_order_detail> rod = new List<requisition_order_detail>();
                rod = context.requisition_order_detail.Where(x => x.requisition_id == id).ToList();
                for (int i = 0; i < rod.Count; i++)
                {
                    bool found = false;
                    for (int j = 0; j < order.Count; j++)
                    {
                        if (rod[i].item_number == order[j].item_number)
                        {
                            found = true;
                            string item = order[j].item_number;
                            requisition_order_detail r = context.requisition_order_detail.Where(x => x.requisition_id == id && x.item_number == item).First<requisition_order_detail>();
                            r.item_requisition_quantity = order[j].item_requisition_quantity;
                            r.item_distributed_quantity = 0;
                            r.item_pending_quantity = order[j].item_requisition_quantity;
                            r.item_requisition_price = order[j].item_requisition_price;
                            context.SaveChanges();
                        }
                    }
                    if (!found)
                    {
                        string it = rod[i].item_number;
                        requisition_order_detail re = context.requisition_order_detail.Where(x => x.requisition_id == id && x.item_number == it).First<requisition_order_detail>();
                        context.requisition_order_detail.Remove(re);
                    }
                }
            }
            else
            {
                Cancelrequisition(id);
            }
        }

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

        public static List<spAcknowledgeDistributionList_Result> ViewAcknowledgementList(int disbursement_list_id)
        {
            List<spAcknowledgeDistributionList_Result> list = new List<spAcknowledgeDistributionList_Result>();
            return list = context.spAcknowledgeDistributionList(disbursement_list_id).ToList();
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
        public static List<supplier_itemdetail> GetSupplier(string id)
        {
            return context.supplier_itemdetail.Where(i => i.item_number == id).OrderBy(i => i.priority).ToList<supplier_itemdetail>();
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

    }
}