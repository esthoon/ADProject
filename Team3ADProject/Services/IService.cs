using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Team3ADProject.Services
{

    /* Written by: Chua Khiong Yang
     * 
     * The iService class is used to provide an API for programmers to use
     * It is primarily used for Charts.js
     *
     * Additional methods here can be added for general use, for Android app features, add them to 
     * IAndroidService.cs
     * 
     * */


    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {

        // Outputs a list of requisition orders
        [OperationContract]
        [WebGet(UriTemplate = "/RequisitionOrder/List/{dept}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_RequisitionOrder> GetRequisitionOrders(string dept);

        // Outputs a list of departments
        [OperationContract]
        [WebGet(UriTemplate = "/Department/List", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Department> GetDepartments();

        // Outputs a list of inventory items
        [OperationContract]
        [WebGet(UriTemplate = "/Inventory/List", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Item> GetInventory();

        // Outputs a list of purchase orders
        [OperationContract]
        [WebGet(UriTemplate = "/PurchaseOrder/List", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_PurchaseOrder> GetPurchaseOrders();

        // Outputs data used with the purchaseOrderCategoryChart chart with number of months to go back
        [OperationContract]
        [WebGet(UriTemplate = "/Chart/PurchaseQuantityByItemCategory/{startParam}/{endParam}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_PurchaseQuantityByItemCategory> getPurchaseQuantityByItemCategoryWithMonthsBack(string startParam, string endParam);


        // Outputs data of items requested by each department
        [OperationContract]
        [WebGet(UriTemplate = "/Chart/getRequisitionQuantityByDepartment", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_RequestQuantityByDepartment> getRequisitionQuantityByDepartment();

        // Outputs data of items requested by each department
        [OperationContract]
        [WebGet(UriTemplate = "/Chart/getRequisitionQuantityByDepartmentWithinTime/{startParam}/{endParam}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_RequestQuantityByDepartment> getRequisitionQuantityByDepartmentWithinTime(String startParam, String endParam);

        // Outputs data used with the purchaseOrderCategoryChart chart
        [OperationContract]
        [WebGet(UriTemplate = "/Chart/getLowStockItemsByCategory", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Item> getLowStockItemsByCategory();

        // Outputs data used with the getPendingPurchaseOrderCountBySupplier chart
        [OperationContract]
        [WebGet(UriTemplate = "/Chart/getPendingPurchaseOrderCountBySupplier", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_MegaObject> getPendingPurchaseOrderCountBySupplier();
    }

    [DataContract]
    public class WCF_RequisitionOrder
    {
        [DataMember]
        public string RequisitionId;

        [DataMember]
        public int EmployeeId;

        [DataMember]
        public string RequisitionStatus;

        [DataMember]
        public DateTime RequisitionDate;

        public WCF_RequisitionOrder(string requisitionId, int employeeId, string requisitionStatus, DateTime requisitionDate)
        {
            RequisitionId = requisitionId;
            EmployeeId = employeeId;
            RequisitionStatus = requisitionStatus;
            RequisitionDate = requisitionDate;
        }
    }

    [DataContract]
    public class WCF_Department
    {
        [DataMember]
        public string DepartmentId;

        [DataMember]
        public int HeadId;

        [DataMember]
        public int? TemporaryHeadId;

        [DataMember]
        public int DepartmentPin;

        [DataMember]
        public string DepartmentName;

        [DataMember]
        public string ContactName;

        [DataMember]
        public int PhoneNumber;

        [DataMember]
        public int FaxNumber;

        public WCF_Department(string departmentId, int headId, int? temporaryHeadId, int departmentPin, string departmentName, string contactName, int phoneNumber, int faxNumber)
        {
            DepartmentId = departmentId;
            HeadId = headId;
            TemporaryHeadId = temporaryHeadId;
            DepartmentPin = departmentPin;
            DepartmentName = departmentName;
            ContactName = contactName;
            PhoneNumber = phoneNumber;
            FaxNumber = faxNumber;
        }
    }

    [DataContract]
    public class WCF_Item
    {
        [DataMember]
        public string ItemNumber;

        [DataMember]
        public string Description;

        [DataMember]
        public string Category;

        [DataMember]
        public string UnitOfMeasurement;

        [DataMember]
        public int CurrentQuantity;

        [DataMember]
        public int ReorderLevel;

        [DataMember]
        public int ReorderQuantity;

        [DataMember]
        public int ItemBin;

        [DataMember]
        public string ItemStatus;

        public WCF_Item(string itemNumber, string description, string category, string unitOfMeasurement, string itemStatus)
        {
            ItemNumber = itemNumber;
            Description = description;
            Category = category;
            UnitOfMeasurement = unitOfMeasurement;
            ItemStatus = itemStatus;
        }

        public WCF_Item(string itemNumber, string description, int currentQuantity, int reorderLevel)
        {
            ItemNumber = itemNumber;
            Description = description;
            CurrentQuantity = currentQuantity;
            ReorderLevel = reorderLevel;
        }
    }

    [DataContract]
    public class WCF_PurchaseOrder
    {
        [DataMember]
        public int PurchaseOrderNumber;

        [DataMember]
        public string ItemNumber;

        [DataMember]
        public string Category;

        [DataMember]
        public int ItemPurchaseOrderQuantity;

        [DataMember]
        public int ItemAcceptQuantity;

        [DataMember]
        public string ItemStatus;


        public WCF_PurchaseOrder(int purchaseOrderNumber, string itemNumber, string category, int itemPurchaseOrderQuantity, int itemAcceptQuantity, string itemStatus)
        {
            PurchaseOrderNumber = purchaseOrderNumber;
            ItemNumber = itemNumber;
            Category = category;
            ItemPurchaseOrderQuantity = itemPurchaseOrderQuantity;
            ItemAcceptQuantity = itemAcceptQuantity;
            ItemStatus = itemStatus;
        }


    }

    [DataContract]
    public class WCF_PurchaseQuantityByItemCategory
    {
        [DataMember]
        public string category;

        [DataMember]
        public int? quantity;

        public WCF_PurchaseQuantityByItemCategory(string category, int? quantity)
        {
            this.category = category;
            this.quantity = quantity;
        }
    }

    [DataContract]
    public class WCF_RequestQuantityByDepartment
    {
        [DataMember]
        public string DepartmentId;

        [DataMember]
        public int? ItemRequestQuantity;

        public WCF_RequestQuantityByDepartment(string departmentId, int? itemRequestQuantity)
        {
            DepartmentId = departmentId;
            ItemRequestQuantity = itemRequestQuantity;
        }
    }

    [DataContract]
    public class WCF_MegaObject
    {
        [DataMember]
        public string SupplierId;

        [DataMember]
        public int? Count;

        public WCF_MegaObject(string supplierId, int? count)
        {
            SupplierId = supplierId;
            Count = count;
        }
    }

}
