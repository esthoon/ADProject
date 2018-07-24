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


        //Tharrani - Start

        //Return active inventory
        [OperationContract]
        [WebGet(UriTemplate = "/NewRequest/AllItems/{*token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Inventory> GetActiveInventory(string token);

        //Retrun inventory matching search
        [OperationContract]
        [WebGet(UriTemplate = "/NewRequest/SearchItems/{search}/{*token}", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Inventory> SearchInventory(string token, string search);

        //Add new request
        [OperationContract]
        [WebGet(UriTemplate = "/NewRequest/Addrequest/{*token}", ResponseFormat = WebMessageFormat.Json)]
        string AddNewRequest(string token);

        //Add new request detail
        [OperationContract]
        [WebInvoke(UriTemplate = "/NewRequest/Addrequestdetail/{*token}", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void AddNewRequestDetail(string token, WCF_Inventory i, string quantity, string id);
        //Tharrani -End

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
        public int current_quantity;
        [DataMember]
        public int reorder_level;
        [DataMember]
        public int reorder_quantity;
        [DataMember]
        public string item_bin;
        [DataMember]
        public string item_status;

        public WCF_Inventory(string item, string desc, string category, string UOM, int cq, int reol, int req, string bin, string status)
        {
            item_number = item.Trim();
            description = desc;
            category = category.Trim();
            unit_of_measurement = UOM;
            current_quantity = cq;
            reorder_level = reol;
            reorder_quantity = req;
            item_bin = bin;
            item_status = status;
        }
    }


}
