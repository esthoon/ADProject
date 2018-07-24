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
        [WebGet(UriTemplate = "/Logout/{*token}", ResponseFormat = WebMessageFormat.Json)]
        string Logout(string token);


        // Returns an employee based on given token
        [OperationContract]
        [WebGet(UriTemplate = "/Employee/{*token}", ResponseFormat = WebMessageFormat.Json)]
        WCF_Employee GetEmployeeByToken(String token);
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

        public WCF_Requisition_Order_Details(int requisition_id, string item_number, int item_requisition_quantity, int item_distributed_quantity, int item_pending_quantity, double item_requisition_price)
        {
            this.RequisitionId = requisition_id.ToString();
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

        public WCF_Requisition_Order(int requisition_id, int employee_id, string requisition_status, DateTime requisition_date, string head_comment)
        {
            this.RequisitionId = requisition_id.ToString();
            this.Employee_id = employee_id.ToString();
            this.RequisitionStatus = requisition_status;
            this.RequisitionDate = requisition_date.ToString("yyyy-MM-dd");
            this.HeadComment = head_comment;
        }
    }

}
