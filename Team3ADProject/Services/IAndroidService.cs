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



        //JOEL - START

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

        //Disbursement Sorting - after ready for collection, system need to send email. Method gets dpt rep email - Web Clerk [Joel]
        [OperationContract]
        [WebGet(UriTemplate = "/Department/Sorting/DptRepEmail/{dptId}", ResponseFormat = WebMessageFormat.Json)]
        string GetDptRepEmailAddFromDptID(string dptId);


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

        // ViewRO SpecialRequest - Deduct from Inventory - USE ABOVE METHOD

        //JOEL - END

        //Tharrani - Start

        //Return active inventory
        [OperationContract]
        [WebGet(UriTemplate = "/NewRequest/AllItems/{*token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Inventory> GetActiveInventory(string token);

        //Return inventory matching search
        [OperationContract]
        [WebGet(UriTemplate = "/NewRequest/SearchItems/{search}/{*token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Inventory> SearchInventory(string token, string search);

        //Return invenotry by ID
        [OperationContract]
        [WebGet(UriTemplate = "/NewRequest/Items/{id}/{*token}", ResponseFormat = WebMessageFormat.Json)]
        WCF_Inventory GetSelectedInventory(string token, string id);

        //Add new request
        [OperationContract]
        [WebInvoke(UriTemplate = "/NewRequest/Addrequest", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string AddNewRequest(WCF_Token token);

        //Add new request detail
        [OperationContract]
        [WebInvoke(UriTemplate = "/NewRequest/Addrequestdetail", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void AddNewRequestDetail(WCF_ReqCart r);

        //GetRequestOrder
        [OperationContract]
        [WebGet(UriTemplate = "/NewRequest/Confirm?id={id}&token={token}", ResponseFormat = WebMessageFormat.Json)]
        WCF_Requisition_Order GetRequestOrder(string token, string id);

        //GetRequestOrderDetails
        [WebGet(UriTemplate = "/NewRequest/Confirm/orderdetail?id={id}&token={token}", ResponseFormat = WebMessageFormat.Json)]
        List<Employee_Request_order_Detail> GetRequestDetail(string token, string id);

        //GetdisbursementList
        [WebGet(UriTemplate = "/Disbursement/Alllist/{*token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Disbursement_List> GetDisbursement_Lists(string token);

        //GetdisbursementDetail
        [WebGet(UriTemplate = "/Disbursement/Detail/{id}/{*token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Disbursement_Detail> GetDisbursement_Detail(string id, string token);

        //AcknowledgedisbursementDetail
        [WebInvoke(UriTemplate = "/Disbursement/Acknowledge", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void AcknowledgeDisbursement_Detail(WCF_Disbursement_Detail DL);

        //ChangeCollectionStatus after Disbursement
        [WebInvoke(UriTemplate = "/Disbursement/ChangeStatus", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void Changecollectionstatus(WCF_Disbursement_Detail DL);


        //Tharrani -End

        //Esther
        [OperationContract]
        [WebInvoke(UriTemplate = "/Adjustment/Create/{token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        string CreateAdjustment(String token, WCF_Adjustment adj);

        [OperationContract]
        [WebGet(UriTemplate = "/Inventory/Active/{token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Inventory> GetActiveInventories(String token);

        [OperationContract]
        [WebGet(UriTemplate = "/Inventory/{search}/{token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Inventory> GetInventorySearchResult(String search, String token);

        [OperationContract]
        [WebGet(UriTemplate = "/Inventory/ItemCode/{itemcode}/{token}", ResponseFormat = WebMessageFormat.Json)]
        WCF_Inventory GetInventoryByItemCode(String itemcode, String token);
        // Yang
        //Add new request detail
        [OperationContract]
        [WebInvoke(UriTemplate = "/Inventory/{ItemNumber}", Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        WCF_Inventory GetInventoryByItemNumber(String ItemNumber);
        // Yang end

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
            this.AdjustmentId = adjustment_id.ToString().Trim();
            this.AdjustmentDate = adjustment_date.ToString("yyyy-MM-dd").Trim();
            this.EmployeeId = employee_id.ToString().Trim();
            this.ItemNumber = item_number.Trim();
            this.AdjustmentQty = adjustment_quantity.ToString().Trim();
            this.AdjustmentPrice = adjustment_price.ToString().Trim();
            this.AdjustmentStatus = adjustment_status.Trim();
            this.EmployeeRemark = employee_remark.Trim();
            this.ManagerRemark = manager_remark.Trim();
        }
        public WCF_Adjustment(string item_number, int adjustment_quantity, string employee_remark)
        {
            this.ItemNumber = item_number.Trim();
            this.AdjustmentQty = adjustment_quantity.ToString().Trim();
            this.EmployeeRemark = employee_remark.Trim();
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


        // full constructor
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

    //Tharrani- Start
    [DataContract]
    public class WCF_Inventory
    {
        [DataMember]
        public string item_number;
        [DataMember]
        public string description;
        [DataMember]
        public string category;
        [DataMember]
        public string unit_of_measurement;
        [DataMember]
        public string current_quantity;
        [DataMember]
        public string reorder_level;
        [DataMember]
        public string reorder_quantity;
        [DataMember]
        public string item_bin;
        [DataMember]
        public string item_status;
        [DataMember]
        public string pending_adj_remove;

        public WCF_Inventory(string item, string desc, string cat, string UOM, string cq, string reol, string req, string bin, string status)
        {
            item_number = item.Trim();
            description = desc;
            category = cat;
            unit_of_measurement = UOM;
            current_quantity = cq;
            reorder_level = reol;
            reorder_quantity = req;
            item_bin = bin;
            item_status = status;
        }

        public WCF_Inventory(string UOM, string cat, string desc, string item)
        {
            item_number = item.Trim();
            description = desc;
            category = cat;
            unit_of_measurement = UOM;
        }

        //Esther
        public WCF_Inventory(string item, string desc, string cat, string UOM, string cq, string reol, string req, string bin, string status, string pending_adj_remove)
        {
            item_number = item.Trim();
            description = desc;
            category = cat;
            unit_of_measurement = UOM;
            current_quantity = cq;
            reorder_level = reol;
            reorder_quantity = req;
            item_bin = bin;
            item_status = status;
            this.pending_adj_remove = pending_adj_remove;
        }
    }

    [DataContract]
    public class WCF_ReqCart
    {
        [DataMember]
        string inventory;

        [DataMember]
        int cart_quantity;

        [DataMember]
        string id;

        [DataMember]
        string token;

        public WCF_ReqCart(string inventory, int cart_quantity, string id)
        {
            this.inventory = inventory;
            this.cart_quantity = q;
            this.id = id;
        }

        public WCF_ReqCart(string inventory, int cart_quantity, string id, string token)
        {
            this.inventory = inventory;
            this.cart_quantity = q;
            this.id = id;
            this.token = token;
        }

        public string getI => inventory;
        public int q => cart_quantity;
        public string Id => id;
        public string gettoken => token;

    }

    [DataContract]
    public class Employee_Request_order_Detail
    {
        [DataMember]
        string category;

        [DataMember]
        string description;

        [DataMember]
        string unit_of_measurement;

        [DataMember]
        string quantity;

        public Employee_Request_order_Detail(string category, string description, string unit_of_measurement, string quantity)
        {
            this.category = category;
            this.description = description;
            this.unit_of_measurement = unit_of_measurement;
            this.quantity = quantity;
        }
    }

    [DataContract]
    public class WCF_Token
    {
        [DataMember]
        string token;

        public WCF_Token(string token)
        { this.token = token; }

        public string gettoken => token;
    }


    [DataContract]
    public class WCF_Disbursement_List
    {
        [DataMember]
        string collection_date;
        [DataMember]
        string collection_location;
        [DataMember]
        string collection_time;
        [DataMember]
        string department_name;
        [DataMember]
        string representative_name;
        [DataMember]
        string collection_id;
        [DataMember]
        string department_pin;

        public WCF_Disbursement_List(string collection_date, string collection_location, string collection_time, string department_name, string representative_name, string collection_id, string department_pin)
        {
            this.collection_date = collection_date;
            this.collection_location = collection_location;
            this.collection_time = collection_time;
            this.department_name = department_name;
            this.representative_name = representative_name;
            this.collection_id = collection_id;
            this.department_pin = department_pin;
        }
    }

    [DataContract]
    public class WCF_Disbursement_Detail
    {

        [DataMember]
        string collection_id;

        [DataMember]
        string item_number;

        [DataMember]
        string description;

        [DataMember]
        string order_quantity;

        [DataMember]
        string receive_quantity;

        [DataMember]
        string altered_quantity;

        [DataMember]
        string token;


        public WCF_Disbursement_Detail(string collection_id, string item_number, string description, string order_quantity, string receive_quantity, string altered_quantity)
        {
            this.collection_id = collection_id;
            this.item_number = item_number;
            this.description = description;
            this.order_quantity = order_quantity;
            this.receive_quantity = receive_quantity;
            this.altered_quantity = altered_quantity;
        }

        public WCF_Disbursement_Detail(string collection_id, string item_number, string description, string order_quantity, string receive_quantity, string altered_quantity, string token) : this(collection_id, item_number, description, order_quantity, receive_quantity, altered_quantity)
        {
            this.token = token;
        }

        public string Collection_id { get => collection_id; set => collection_id = value; }
        public string Item_number { get => item_number; set => item_number = value; }
        public string Receive_quantity { get => receive_quantity; set => receive_quantity = value; }
        public string Altered_quantity { get => altered_quantity; set => altered_quantity = value; }
        public string Token { get => token; set => token = value; }
    }

    //Tharrani – End


}
