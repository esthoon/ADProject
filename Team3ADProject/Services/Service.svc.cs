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
                WCF_RequisitionOrder wcf_ro = new WCF_RequisitionOrder(ro.requisition_id, ro.employee_id, ro.requisition_status, ro.requisition_date);
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

    }
}
