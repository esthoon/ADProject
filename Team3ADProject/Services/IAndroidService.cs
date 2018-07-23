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
            EmployeeName = employeeName;
            EmailId = emailId;
            UserId = userId;
            DepartmentId = departmentId;
            if (supervisorId == null) { SupervisorId = 0; }
            else { SupervisorId = (int)supervisorId; };
            Token = token;
            Role = role;
        }
    }

}
