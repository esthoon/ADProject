using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Team3ADProject.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {

        // Outputs a list of requisition orders
        [OperationContract]
        [WebGet(UriTemplate = "/RequisitionOrder/List", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_RequisitionOrder> GetRequisitionOrders();

        // Outputs a list of departments
        [OperationContract]
        [WebGet(UriTemplate = "/Department/List", ResponseFormat = WebMessageFormat.Json)]
        List<WCF_Department> GetDepartments();
        

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
}
