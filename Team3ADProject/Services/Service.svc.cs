using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Team3ADProject.Model;

namespace Team3ADProject.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {

        public List<WCF_RequisitionOrder> GetRequisitionOrders()
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.requisition_order select x;
            List<requisition_order> requisitionOrders = query.ToList();
            List<WCF_RequisitionOrder> wcf_requisitionOrders = new List<WCF_RequisitionOrder>();

            foreach (requisition_order ro in requisitionOrders)
            {
                WCF_RequisitionOrder wcf_ro = new WCF_RequisitionOrder(ro.requisition_id.Trim(), ro.employee_id, ro.requisition_status, ro.requisition_date);
                wcf_requisitionOrders.Add(wcf_ro);
            }

            return wcf_requisitionOrders;
        }

        public List<WCF_Department> GetDepartments()
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.departments select x;
            List<department> departments = query.ToList();
            List<WCF_Department> wcf_departments = new List<WCF_Department>();

            foreach (department d in departments)
            {
                WCF_Department wcf_d = new WCF_Department(d.department_id, d.head_id, d.temp_head_id, d.department_pin, d.department_name, d.contact_name, d.phone_number, d.fax_number);
                wcf_departments.Add(wcf_d);
            }

            return wcf_departments;
        }

        public List<WCF_Item> GetInventory()
        {

            var context = new LogicUniversityEntities();
            var query = from x in context.inventories select x;
            List<inventory> inventories = query.ToList();
            List <WCF_Item> wcf_items = new List<WCF_Item>();

            foreach (inventory i in inventories)
            {
                wcf_items.Add(new WCF_Item(i.item_number, i.description, i.category, i.unit_of_measurement, i.item_status));
            }

            return wcf_items;
        }

        public List<WCF_PurchaseOrder> GetPurchaseOrders()
        {
            var context = new LogicUniversityEntities();
            var query = from p in context.purchase_order
                        join d in context.purchase_order_detail on p.purchase_order_number equals d.purchase_order_number
                        join i in context.inventories on d.item_number equals i.item_number
                        select new { p, d, i };

            var purchaseOrders  = query.ToList();
            List<WCF_PurchaseOrder> wcf_purchaseOrders = new List<WCF_PurchaseOrder>();

            foreach (var i in purchaseOrders)
            {
                wcf_purchaseOrders.Add(new WCF_PurchaseOrder(i.p.purchase_order_number, i.i.item_number, i.i.category, i.d.item_purchase_order_quantity, i.d.item_accept_quantity, i.i.item_status));
            }

            return wcf_purchaseOrders;
        }

        public List<WCF_PurchaseQuantityByItemQuantity> getPurchaseQuantityByItemCategoryWithMonthsBack(string monthsParam)
        {
            List<WCF_PurchaseQuantityByItemQuantity> wcfList = new List<WCF_PurchaseQuantityByItemQuantity>();

            int monthsBack = int.Parse(monthsParam);
            var context = new LogicUniversityEntities();
            var result = context.getPurchaseQuantityByItemCategory(monthsBack);

            foreach(var i in result.ToList())
            {
                wcfList.Add(new WCF_PurchaseQuantityByItemQuantity(i.Category.Trim(), i.PurchaseQuantity));
            }

            return wcfList;
        }

        public List<WCF_PurchaseQuantityByItemQuantity> getPurchaseQuantityByItemCategory()
        {
            return getPurchaseQuantityByItemCategoryWithMonthsBack("0");
        }

        public List<WCF_RequestQuantityByDepartment> getRequisitionQuantityByDepartment()
        {
            List<WCF_RequestQuantityByDepartment> wcfList = new List<WCF_RequestQuantityByDepartment>();

            var context = new LogicUniversityEntities();
            var result = context.getRequisitionQuantityByDepartment();

            foreach (var i in result.ToList())
            {
                wcfList.Add(new WCF_RequestQuantityByDepartment(i.department_id.Trim(), i.item_request_quantity));
            }

            return wcfList;
        }

        public List<WCF_Item> getLowStockItemsByCategory()
        {
            List<WCF_Item> wcfList = new List<WCF_Item>();

            var context = new LogicUniversityEntities();
            var result = context.getLowStockItemsByCategory();

            foreach (var i in result.ToList())
            {
                wcfList.Add(new WCF_Item(i.item_number, i.description, i.current_quantity, i.reorder_level));
            }

            return wcfList;
        }

        public List<WCF_MegaObject> getPendingPurchaseOrderCountBySupplier()
        {
            List<WCF_MegaObject> wcfList = new List<WCF_MegaObject>();

            var context = new LogicUniversityEntities();
            var result = context.getPendingPurchaseOrderCountBySupplier();

            foreach (var i in result.ToList())
            {
                wcfList.Add(new WCF_MegaObject(i.SupplierId.Trim(), i.PendingPurchaseOrderQuantity));
            }

            return wcfList;
        }
    }
}
