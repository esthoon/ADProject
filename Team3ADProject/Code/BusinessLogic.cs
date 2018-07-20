using System;
using System.Collections.Generic;
using System.Linq;
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
        public static void AddNewRequisitionOrder(string id, int emp, DateTime d)
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
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == "Pending").ToList<requisition_order>();
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

        //Cancel requisition order status to cancel
        public static void Cancelrequisition(string id)
        {
            requisition_order r = context.requisition_order.Where(x => x.requisition_id == id).First();
            r.requisition_status = "Cancel";
            context.SaveChanges();
        }

        //Save changes in edit request to requisition order
        public static void UpdateRequisitionOrderDetail(string id, List<getRequisitionOrderDetails_Result> order)
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

        //return all PO - with total PO item count
        public static List<getAllViewPOHistorytotalcount_Result> viewpohistorytotal()
        {
            return context.getAllViewPOHistorytotalcount().ToList();
        }

        //return list of supplier for dropdown in ViewPoHistory
        public static List<supplier> getSupplierNames()
        {
            return context.suppliers.ToList();
        }

        //return supplier code based on supplier name
        public static supplier getSupplierCode(string text)
        {
            return context.suppliers.Where(x => x.supplier_name == text).FirstOrDefault();
        }

        //Return list of PO status for dropdown in ViewPoHistory
        public static List<purchase_order> getPOStatus()
        {
            return context.purchase_order.ToList();
        }

        //return PO by status- with total PO item count
        public static List<getViewPOHistorytotalcountByStatus_Result> ViewPOHistorytotalcountByStatus(string s)
        {
            return context.getViewPOHistorytotalcountByStatus(s).ToList();
        }

        //return  PO by Po number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPO_Result> viewPOHistorytotalcountbyPO(int po)
        {
            return context.getViewPOHistorytotalcountbyPO(po).ToList();
        }

        //return  PO by Po number and status - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandstatus_Result> ViewPOHistorytotalcountbyPOandstatus(int po, string status)
        {
            return context.getViewPOHistorytotalcountbyPOandstatus(po, status).ToList();
        }

        //return  PO by supplier - with total PO item count
        public static List<getViewPOHistorytotalcountbySupplier_Result> viewPOHistorytotalcountbySupplier(string supplier)
        {
            return context.getViewPOHistorytotalcountbySupplier(supplier).ToList();
        }

        //return  PO by supplier and PO number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandSupplier_Result> viewPOHistorytotalcountbyPOandSupplier(int po, string supplier)
        {
            return context.getViewPOHistorytotalcountbyPOandSupplier(po, supplier).ToList();
        }

        //return  PO by supplier and status - with total PO item count
        public static List<getViewPOHistorytotalcountbysupandstatus_Result> viewPOHistorytotalcountbysupandstatus(string supplier, string status)
        {
            return context.getViewPOHistorytotalcountbysupandstatus(supplier, status).ToList();
        }

        //return  PO by supplier and status and Po number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandstatusandSupplier_Result> viewPOHistorytotalcountbyPOandstatusandSupplier(string supplier, int po, string status)
        {
            return context.getViewPOHistorytotalcountbyPOandstatusandSupplier(supplier, po, status).ToList();
        }

        //return all PO - with total PO item count
        public static List<getAllViewPOHistorypendingcount_Result> viewPOHistorypendingcount()
        {
            return context.getAllViewPOHistorypendingcount().ToList();
        }

        //return PO detail based on PO ID
        public static List<purchase_order_detail> getpurchaseorderdetail(int id)
        {
            return context.purchase_order_detail.Where(x => x.purchase_order_number == id).ToList();
        }

        //return supplier for PO by PO id
        public static supplier getSupplierNameforPurchaseorder(int id)
        {
            purchase_order p = getpurchaseorder(id);
            string sid = p.suppler_id;
            return context.suppliers.Where(x => x.supplier_id == sid).FirstOrDefault();
        }

        //return po for id
        public static purchase_order getpurchaseorder(int id)
        {
            return context.purchase_order.Where(x => x.purchase_order_number == id).FirstOrDefault();
        }

        //return employee name
        public static employee GetEmployee(int id)
        {
            return context.employees.Where(x => x.employee_id == id).FirstOrDefault();
        }

        //accept item from supplier
        public static void acceptitemfromsupplier(int po, string item, int quantity, string remark)
        {
            purchase_order_detail pod = context.purchase_order_detail.Where(x => x.purchase_order_number == po && x.item_number == item).FirstOrDefault();
            pod.item_accept_quantity = quantity;
            pod.purchase_order_item_remark = remark;
            pod.item_purchase_order_status = "Accepted";
            pod.item_accept_date = DateTime.Now.Date;
            context.SaveChanges();
        }
    }
}