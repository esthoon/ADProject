using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Team3ADProject.Services
{



    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAndroidService" in both code and config file together.
    [ServiceContract]
    public interface IAndroidService
    {
        // Helloworld Tester token template
        [OperationContract]
        [WebGet(UriTemplate = "/Hello/{token}", ResponseFormat = WebMessageFormat.Json)]
        string Hello(String token);

        // Logs user in using username and password
        // Returns a token if successful, null if not
        [OperationContract]
        [WebGet(UriTemplate = "/Login/{username}/{password}", ResponseFormat = WebMessageFormat.Json)]
        WCF_Employee Login(string username, string password);

        // Logs user with specified token out
        [OperationContract]
        [WebGet(UriTemplate = "/Logout/{token}", ResponseFormat = WebMessageFormat.Json)]
        string Logout(string token);


        // Returns an employee based on given token
        [OperationContract]
        [WebGet(UriTemplate = "/Employee/{token}", ResponseFormat = WebMessageFormat.Json)]
        WCF_Employee GetEmployeeByToken(String token);




        // Collection List - Outputs weekly collection list - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/WarehouseCollection/List", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_CollectionItem> getCollectionList();

        // Collection List - Takes collectionItem obj to sort & update ROD table - Web Clerk [Joel]
        [OperationContract]
        [WebInvoke(UriTemplate = "/WarehouseCollection/Sort", Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json)]
        void SortCollectedGoods(WCF_CollectionItem ci);

        //CollectionList - Takes collectionItem obj with qty to reduce from inventory - Web Clerk [Joel]
        [OperationContract]
        [WebInvoke(UriTemplate = "/WarehouseCollection/DeductInventory", Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json)]
        void DeductFromInventory(WCF_CollectionItem ci);


        // Disbursement Sorting - displays list of departments that need collection - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/Department/Sorting/DptList", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_DepartmentList> DisplayListofDepartmentsForCollection();

        // Disbursement Sorting - input DptName, get DptId, to be used with GetSortingListByDepartment(dpt_Id);  - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/Department/Sorting/{dptName}", ResponseFormat = WebMessageFormat.Json)]
        string GetDptIdFromDptName(string dptName);

        // Disbursement Sorting - input DptId, get disbursement list; - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/Department/Sorting/List/{dptId}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_SortingItem> GetSortingListByDepartment(string dptId);

        // Disbursement Sorting - input DptId, get place id, to use in updating collection_detail table - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/Department/Sorting/PlaceId/{dptId}", ResponseFormat = WebMessageFormat.Json)]
        int GetPlaceIdFromDptId(string dptId);

        // Disbursement Sorting - after ready for collection, input place id + collectionDate + dptID, insert row to collection_detail table - Web Clerk [Joel]
        [OperationContract]
        [WebInvoke(UriTemplate = "/Department/Sorting/InsertCollectionDetail", Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json)]
        void InsertCollectionDetailsRow(WCF_CollectionDetail cd);

        //Disbursement Sorting - after ready for collection, input dptId insert to disbursementlist table  - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/Department/Sorting/InsertDisbursementDetail/{dptId}", ResponseFormat = WebMessageFormat.Json)]
        void InsertDisbursementListROId(string dptId);



        // ViewRO SpecialRequest - input ROID, Get RO Details - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/SpecialRequest/?roid={roId}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_CollectionItem> GetRODetailsByROId(string roId);

        // ViewRO SpecialRequest - input dptID, get place id - Web Clerk [Joel] - USE ABOVE METHOD.

        // ViewRO SpecialRequest - after ready for collection, input placeId + collectionDate + dptID + ROID, insert row to collection_detail table - Web Clerk [Joel]
        [OperationContract]
        [WebInvoke(UriTemplate = "/SpecialRequest/Sorting/UpdateCDRDD", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void SpecialRequestReadyUpdatesCDRDD(WCF_CollectionDetail cd);

        // ViewRO SpecialRequest - input dptID, get place id - Web Clerk [Joel] - USE ABOVE METHOD.
        [OperationContract]
        [WebInvoke(UriTemplate = "/SpecialRequest/Sorting/UpdateROD", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        void ViewROSpecialRequestUpdateRODTable(WCF_CollectionItem ci);

    }






    [DataContract]
    public class WCF_Employee
    {
        [DataMember]
        public int EmployeeId;

        [DataMember]
        public string EmployeeName;

        [DataMember]
        public string EmailId;

        [DataMember]
        public string UserId;

        [DataMember]
        public string DepartmentId;

        [DataMember]
        public int SupervisorId;

        [DataMember]
        public string Token;

        [DataMember]
        public string Role;

        public WCF_Employee(int employeeId, string employeeName, string emailId, string userId, string departmentId, int? supervisorId, string token, string role)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName.Trim();
            EmailId = emailId.Trim();
            UserId = userId.Trim();
            DepartmentId = departmentId.Trim();
            if (supervisorId == null) { SupervisorId = 0; }
            else { SupervisorId = (int)supervisorId; };
            Token = token;
            Role = role;
        }
    }


    [DataContract]
    public class WCF_Requisition_Order_Details
    {
        [DataMember]
        public string RequisitionId;

        [DataMember]
        public string ItemNumber;

        [DataMember]
        public string ItemRequisitionQty;

        [DataMember]
        public string ItemDistributedQty;

        [DataMember]
        public string ItemPendingQty;

        [DataMember]
        public string ItemRequisitionPrice;

        public WCF_Requisition_Order_Details(string requisition_id, string item_number, int item_requisition_quantity, int item_distributed_quantity, int item_pending_quantity, double item_requisition_price)
        {
            this.RequisitionId = requisition_id;
            this.ItemNumber = item_number;
            this.ItemRequisitionQty = item_requisition_quantity.ToString();
            this.ItemDistributedQty = item_distributed_quantity.ToString();
            this.ItemPendingQty = item_pending_quantity.ToString();
            this.ItemRequisitionPrice = item_requisition_price.ToString();
        }
    }

    [DataContract]
    public class WCF_Adjustment
    {
        [DataMember]
        public string AdjustmentId;

        [DataMember]
        public string AdjustmentDate;

        [DataMember]
        public string EmployeeId;

        [DataMember]
        public string ItemNumber;

        [DataMember]
        public string AdjustmentQty;

        [DataMember]
        public string AdjustmentPrice;

        [DataMember]
        public string AdjustmentStatus;

        [DataMember]
        public string EmployeeRemark;

        [DataMember]
        public string ManagerRemark;

        public WCF_Adjustment(int adjustment_id, DateTime adjustment_date, int employee_id, string item_number, int adjustment_quantity, double adjustment_price, string adjustment_status, string employee_remark, string manager_remark)
        {
            this.AdjustmentId = adjustment_id.ToString();
            this.AdjustmentDate = adjustment_date.ToString("yyyy-MM-dd");
            this.EmployeeId = employee_id.ToString();
            this.ItemNumber = item_number;
            this.AdjustmentQty = adjustment_quantity.ToString();
            this.AdjustmentPrice = adjustment_price.ToString();
            this.AdjustmentStatus = adjustment_status;
            this.EmployeeRemark = employee_remark;
            this.ManagerRemark = manager_remark;
        }
    }

    [DataContract]
    public class WCF_Requisition_Order
    {
        [DataMember]
        public string RequisitionId;

        [DataMember]
        public string Employee_id;

        [DataMember]
        public string RequisitionStatus;

        [DataMember]
        public string RequisitionDate;

        [DataMember]
        public string HeadComment;

        public WCF_Requisition_Order(string requisition_id, int employee_id, string requisition_status, DateTime requisition_date, string head_comment)
        {
            this.RequisitionId = requisition_id;
            this.Employee_id = employee_id.ToString();
            this.RequisitionStatus = requisition_status;
            this.RequisitionDate = requisition_date.ToString("yyyy-MM-dd");
            this.HeadComment = head_comment;
        }
    }

    [DataContract]
    public class WCF_CollectionItem
    {
        [DataMember]
        public string RequisitionId;

        [DataMember]
        public string ItemNumber;

        [DataMember]
        public string Description;

        [DataMember]
        public string UnitOfMeasurement;

        [DataMember]
        public int QuantityOrdered;

        [DataMember]
        public int CollectedQty;

        [DataMember]
        public int PendingQty;

        [DataMember]
        public int CurrentInventoryQty;

        //used by getCollectionList()
        public WCF_CollectionItem(string itemNumber, string description, int quantityOrdered, int currentQuantity, string unitOfMeasurement)
        {
            ItemNumber = itemNumber;
            Description = description;
            CurrentInventoryQty = currentQuantity;
            QuantityOrdered = quantityOrdered;
            UnitOfMeasurement = unitOfMeasurement;
        }

        // Used by SortCollectedGoods()
        public WCF_CollectionItem(string itemNumber, string description, int quantityOrdered, int currentQuantity, int quantityCollected, string unitOfMeasurement)
        {
            ItemNumber = itemNumber;
            Description = description;
            CurrentInventoryQty = currentQuantity;
            QuantityOrdered = quantityOrdered;
            CollectedQty = quantityCollected;
            UnitOfMeasurement = unitOfMeasurement;
        }

        // used by GetRODetailsByROId();
        public WCF_CollectionItem(string requisitionId, string itemNumber, string description, string unitOfMeasurement, int quantityOrdered, int currentQuantity, int pendingQuantity)
        {
            RequisitionId = requisitionId;
            ItemNumber = itemNumber;
            Description = description;
            UnitOfMeasurement = unitOfMeasurement;
            QuantityOrdered = quantityOrdered;
            CurrentInventoryQty = currentQuantity;
            PendingQty = pendingQuantity;
        }


        // full con
        public WCF_CollectionItem(string requisitionId, string itemNumber, string description, string unitOfMeasurement, int quantityOrdered, int collectedQty, int pendingQuantity, int currentQuantity)
        {
            RequisitionId = requisitionId;
            ItemNumber = itemNumber;
            Description = description;
            UnitOfMeasurement = unitOfMeasurement;
            QuantityOrdered = quantityOrdered;
            CollectedQty = collectedQty;
            PendingQty = pendingQuantity;
            CurrentInventoryQty = currentQuantity;
        }
    }

    [DataContract]
    public class WCF_SortingItem
    {
        [DataMember]
        public string ItemNumber;

        [DataMember]
        public string Description;

        [DataMember]
        public int QuantityOrdered;

        [DataMember]
        public int CollectedQty;

        [DataMember]
        public int PendingQty;

        //used by GetSortingListByDepartment(string dpt_Id);
        public WCF_SortingItem(string itemNumber, string description, int quantityOrdered, int collectedQuantity, int pendingQuantity)
        {
            ItemNumber = itemNumber;
            Description = description;
            QuantityOrdered = quantityOrdered;
            CollectedQty = collectedQuantity;
            PendingQty = pendingQuantity;
        }
    }

    [DataContract]
    public class WCF_DepartmentList
    {
        [DataMember]
        public string DepartmentName;

        // used by DisplayListofDepartmentsForCollection()
        public WCF_DepartmentList(string departmentName)
        {
            DepartmentName = departmentName;
        }
    }

    [DataContract]
    public class WCF_CollectionDetail
    {
        [DataMember]
        public int PlaceId;

        [DataMember]
        //public DateTime CollectionDate; IF ANDROID SIDE SENDS DATETIME WHICH CAN READ, USE THIS INSTEAD
        public string CollectionDate;

        [DataMember]
        public string DepartmentId;

        [DataMember]
        public string RoId;


        // used by InsertCollectionDetailsRow(WCF_CollectionDetail collectionDetail)
        public WCF_CollectionDetail(int placeId, string collectionDate, string dptId)
        {
            PlaceId = placeId;
            CollectionDate = collectionDate;
            DepartmentId = dptId;
        }

        //SpecialRequestReadyUpdatesCDRDD(WCF_CollectionDetail cd)
        public WCF_CollectionDetail(int placeId, string collectionDate, string dptId, string roid)
        {
            PlaceId = placeId;
            CollectionDate = collectionDate;
            DepartmentId = dptId;
            RoId = roid;
        }
    }



}
