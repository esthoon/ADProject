using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team3ADProject.Code
{
    public class Constants
    {
        // Roles
        public static string ROLES_STORE_CLERK = "storeclerk";
        public static string ROLES_STORE_SUPERVISOR = "storesup";
        public static string ROLES_STORE_MANAGER = "storemanager";

        public static string ROLES_EMPLOYEE = "employee";
        public static string ROLES_DEPARTMENT_REPRESENTATIVE = "deprep";
        public static string ROLES_DEPARTMENT_HEAD = "dephead";
    
        // Chart values
        public static string CHART_TEST = "testChart";
        public static string CHART_REQUISITION_ORDER_STATUS_PERCENTAGE = "requisitionOrderStatusChart";
        public static string CHART_REQUISITION_ORDER_DATE_CREATED = "requisitionOrderDateChart";
        public static string CHART_REQUISITION_ITEM_QUANTITY_BY_DEPARTMENT = "requisitionQuantityByDepartmentChart";
        public static string CHART_STATIONARIES_ORDERED_BY_CATEGORY = "purchaseQuantityByItemCategoryBarChart";
        public static string CHART_PENDING_PURCHASE_ORDER_BY_SUPPLIER = "pendingPurchaseOrderCountBySupplierChart";
    }
}