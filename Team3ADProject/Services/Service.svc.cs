using System;
using System.Collections.Generic;
using System.Globalization;
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
            List<WCF_Item> wcf_items = new List<WCF_Item>();

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

            var purchaseOrders = query.ToList();
            List<WCF_PurchaseOrder> wcf_purchaseOrders = new List<WCF_PurchaseOrder>();

            foreach (var i in purchaseOrders)
            {
                wcf_purchaseOrders.Add(new WCF_PurchaseOrder(i.p.purchase_order_number, i.i.item_number, i.i.category, i.d.item_purchase_order_quantity, i.d.item_accept_quantity, i.i.item_status));
            }

            return wcf_purchaseOrders;
        }

        public List<WCF_PurchaseQuantityByItemCategory> getPurchaseQuantityByItemCategoryWithMonthsBack(string startParam, string endParam)
        {
            List<WCF_PurchaseQuantityByItemCategory> wcfList = new List<WCF_PurchaseQuantityByItemCategory>();

            // Debugger test
            string format = "dd-MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime start = DateTime.ParseExact(startParam, format, provider);
            DateTime end = DateTime.ParseExact(endParam, format, provider);

            var context = new LogicUniversityEntities();
            var query = from po in context.purchase_order
                        join pod in context.purchase_order_detail on po.purchase_order_number equals pod.purchase_order_number
                        join inv in context.inventories on pod.item_number equals inv.item_number
                        where (pod.item_purchase_order_status.Trim() == "Completed" || pod.item_purchase_order_status.Trim() == "Pending")
                        && (po.purchase_order_date.CompareTo(end) <= 0 && po.purchase_order_date.CompareTo(start) >= 0)
                        select new { po, pod, inv };

            // Query is definitely working fine.

            var result = query.GroupBy(cat => cat.inv.category)
                .Select(g => new
                {
                    Category = g.Key.Trim(),
                    PurchaseQuantity = g.Sum(x => x.pod.item_purchase_order_quantity)
                });


            foreach (var i in result.ToList())
            {
                wcfList.Add(new WCF_PurchaseQuantityByItemCategory(i.Category.Trim(), i.PurchaseQuantity));
            }

            return wcfList;
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

        public List<WCF_RequestQuantityByDepartment> getRequisitionQuantityByDepartmentWithinTime(String startParam, String endParam)
        {
            List<WCF_RequestQuantityByDepartment> wcfList = new List<WCF_RequestQuantityByDepartment>();

            string format = "dd-MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime start = DateTime.ParseExact(startParam, format, provider);
            DateTime end = DateTime.ParseExact(endParam, format, provider);

            var context = new LogicUniversityEntities();
            var query = (from dept in context.departments
                         join emp in context.employees on dept.department_id equals emp.department_id
                         join req in context.requisition_order on emp.employee_id equals req.employee_id
                         join reqDetails in context.requisition_order_detail on req.requisition_id equals reqDetails.requisition_id
                         join inv in context.inventories on reqDetails.item_number equals inv.item_number
                         where req.requisition_date.CompareTo(start) >= 0
                         && req.requisition_date.CompareTo(end) <= 0
                         select new { dept, emp, req, reqDetails, inv });

            var result = query.GroupBy(d => d.dept.department_id)
                .Select(g => new
                {
                    DepartmentId = g.Key.Trim(),
                    ItemQuantity = g.Sum(x => x.reqDetails.item_requisition_quantity)
                });

            // String returnString = "";
            foreach (var i in result)
            {
                //returnString = returnString + i;
                wcfList.Add(new WCF_RequestQuantityByDepartment(i.DepartmentId, i.ItemQuantity));
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
