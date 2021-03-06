﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Team3ADProject.Model;
using System.Transactions;
using System.Web.Security;

namespace Team3ADProject.Code
{

    public class BusinessLogic
    {
        public static LogicUniversityEntities context = new LogicUniversityEntities();

		// Sruthi- to get the pending ros of the particular department
        public static List<getpendingrequestsbydepartment_Result> ViewPendingRequests(string deptid)
        {
            List<getpendingrequestsbydepartment_Result> list = new List<getpendingrequestsbydepartment_Result>();
            return list = context.getpendingrequestsbydepartment(deptid).ToList();
        }

		//Sruthi- to get the department based on the userid
        public static string getdepartment(string userid)
        {
            employee k = (from employee in context.employees where employee.user_id == userid select employee).FirstOrDefault();
            string dept = k.department_id;
            return dept;
        }

		//Sruthi-to get the details of the ro based on the roid
        public static getpendingrequestdetails_Result getdetails(string id)
        {
            return context.getpendingrequestdetails(id).ToList().Single();
        }

		//Sruthi-to get the item details in the particular ro
        public static List<getitemdetails_Result> pendinggetitemdetails(string reqid)
        {
            List<getitemdetails_Result> list = new List<getitemdetails_Result>();
            return list = context.getitemdetails(reqid).ToList();
        }

		//Sruthi -to approve the ro
        public static void approvestatus(string id, string status, string dept, int sum)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                var k = from requisition_order in context.requisition_order where requisition_order.requisition_id == id select requisition_order;
                k.FirstOrDefault().requisition_status = "Approved";
                k.FirstOrDefault().head_comment = status;
                context.SaveChanges();
                int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                string month = DateTime.Now.ToString("MMM");
                var q = (from b in context.budgets where b.department_id.Equals(dept) && b.year.Equals(year) && b.month.Equals(month) select b);
                budget b1 = q.FirstOrDefault();
                if (q.FirstOrDefault().spent.HasValue)
                {
                    b1.spent = q.FirstOrDefault().spent.Value + sum;
                    context.SaveChanges();
                }
                else
                {
                    b1.spent = sum;
                    context.SaveChanges();
                }
                ts.Complete();
            }


        }
		//Sruthi- to reject the ro
        public static void rejectstatus(string id, string status)
        {
            var k = from requisition_order in context.requisition_order where requisition_order.requisition_id == id select requisition_order;
            k.FirstOrDefault().requisition_status = "Rejected";
            k.FirstOrDefault().head_comment = status;
            context.SaveChanges();
        }

		//Sruthi- to get the history of the ros of the department
        public static List<getrequesthistory_Result> gethistory(string dept)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            return list = context.getrequesthistory(dept).ToList();
        }

		//Sruthi- to get the history of the ros based on the name
        public static List<getrequesthistory_Result> gethistorybyname(string name, string dept)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            list = (from requisitionorder in context.requisition_order
                    join employee in context.employees on requisitionorder.employee_id equals employee.employee_id
                    join requisitionorderdetails in context.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
                    where (employee.department_id.Equals(dept) && employee.employee_name.Contains(name))
                    group requisitionorderdetails by requisitionorderdetails.requisition_id into reqgp
                    select new
                    {
                        id = reqgp.FirstOrDefault().requisition_id,
                        Date = reqgp.FirstOrDefault().requisition_order.requisition_date,
                        Name = reqgp.FirstOrDefault().requisition_order.employee.employee_name,
                        status = reqgp.FirstOrDefault().requisition_order.requisition_status,
                        Sum = reqgp.Sum(pt => pt.item_requisition_price)
                    }).ToList().
                          Select(x => new getrequesthistory_Result()
                          {
                              id = x.id,
                              Date = x.Date,
                              Name = x.Name,
                              status = x.status,
                              Sum = x.Sum,
                          }).ToList();
            return list;
        }
		// Sruthi-to get the history of the requisition orders by name and status
        public static List<getrequesthistory_Result> gethistorybynameandstatus(string name, string dept, string status)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            list = (from requisitionorder in context.requisition_order
                    join employee in context.employees on requisitionorder.employee_id equals employee.employee_id
                    join requisitionorderdetails in context.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
                    where (employee.department_id.Equals(dept) && employee.employee_name.Contains(name) && requisitionorder.requisition_status.Equals(status))
                    group requisitionorderdetails by requisitionorderdetails.requisition_id into reqgp
                    select new
                    {
                        id = reqgp.FirstOrDefault().requisition_id,
                        Date = reqgp.FirstOrDefault().requisition_order.requisition_date,
                        Name = reqgp.FirstOrDefault().requisition_order.employee.employee_name,
                        status = reqgp.FirstOrDefault().requisition_order.requisition_status,
                        Sum = reqgp.Sum(pt => pt.item_requisition_price)
                    }).ToList().
                          Select(x => new getrequesthistory_Result()
                          {
                              id = x.id,
                              Date = x.Date,
                              Name = x.Name,
                              status = x.status,
                              Sum = x.Sum,
                          }).ToList();
            return list;
        }
		//Sruthi- to get the history of the requisition orders by status
        public static List<getrequesthistory_Result> gethistorybystatus(string dept, string status)
        {
            List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
            list = (from requisitionorder in context.requisition_order
                    join employee in context.employees on requisitionorder.employee_id equals employee.employee_id
                    join requisitionorderdetails in context.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
                    where (employee.department_id.Equals(dept) && requisitionorder.requisition_status.Equals(status))
                    group requisitionorderdetails by requisitionorderdetails.requisition_id into reqgp
                    select new
                    {
                        id = reqgp.FirstOrDefault().requisition_id,
                        Date = reqgp.FirstOrDefault().requisition_order.requisition_date,
                        Name = reqgp.FirstOrDefault().requisition_order.employee.employee_name,
                        status = reqgp.FirstOrDefault().requisition_order.requisition_status,
                        Sum = reqgp.Sum(pt => pt.item_requisition_price)
                    }).ToList().
                          Select(x => new getrequesthistory_Result()
                          {
                              id = x.id,
                              Date = x.Date,
                              Name = x.Name,
                              status = x.status,
                              Sum = x.Sum,
                          }).ToList();
            return list;
        }

		//Sruthi- to get the list of the employees of the department
        public static List<employee> getemployeenames(string dept)
        {
            //var q=from employee in context.employees where employee.department_id.Equals(dept) select employee.employee_name;
            return context.employees.Where(x => x.department_id == dept && x.supervisor_id != null).ToList();

        }

		//Sruthi- to get the employee id based on name
        public static int getemployeeid(string name)
        {
            var q = from employee in context.employees where employee.employee_name == name select employee.employee_id;
            return q.FirstOrDefault();
        }

		//Sruthi-to update the temporary head
        public static void updatetemporaryhead(int id, string dept)
        {
            var q = from department in context.departments where department.department_id == dept select department;
            department d = q.FirstOrDefault();
            d.temp_head_id = id;
            context.SaveChanges();
            employee y = context.employees.Where(x => x.employee_id == id).FirstOrDefault();
            Roles.AddUserToRole(y.user_id, Constants.ROLES_DEPARTMENT_HEAD_TEMP);
            string messagebody = "The following person has been appointed as the temporary head for the approval of requisition order \n \n" + y.employee_name.ToString();
            string messagebody1 = "Congratulations \n \n You have been appointed as the temporary head for the approval \n \n";
            var allemployees = context.employees.Where(x => x.department_id == dept && x.employee_id != id && x.employee_id != d.head_id).ToList<employee>();
            List<string> allemails = new List<string>();
            foreach (employee email in allemployees)
            {
                allemails.Add(email.email_id);
            }
            BusinessLogic.sendMail(allemails, "Temporary head", messagebody);
            BusinessLogic.sendMail(y.email_id, "Temporary head", messagebody1);
        }

        //Shijun
		//Sruthi- to get the temporary head of the department

        public static string gettemporaryheadname(string dept)
        {
            var q = from department in context.departments
                    join employee in context.employees on
                    department.temp_head_id equals employee.employee_id
                    where department.department_id.Equals(dept)
                    select employee.employee_name;
            return q.FirstOrDefault();

        }

        //Shijun
		//Sruthi- to revoke the temporary head

        public static void revoketemporaryhead(string dept)
        {
            var q = from department in context.departments where department.department_id == dept select department;
            department d = q.FirstOrDefault();
            employee y = context.employees.Where(x => x.employee_id == d.temp_head_id).FirstOrDefault();
            Roles.RemoveUserFromRole(y.user_id, Constants.ROLES_DEPARTMENT_HEAD_TEMP);
            d.temp_head_id = null;
            context.SaveChanges();

        }

		//Sruthi- to get employee name by search
        public static List<employee> getemployeenamebysearch(string dept, string name)
        {
            var q = from e in context.employees
                    join d in context.departments on e.department_id equals d.department_id
                    where e.employee_name.Contains(name) && e.department_id.Equals(dept) && e.supervisor_id!=null
                    select e;
            return q.ToList();

        }

		//Sruthi- to get previous representative details
        public static List<getrepdetails_Result> getpreviousrepdetails(string dept)
        {
            List<getrepdetails_Result> list = new List<getrepdetails_Result>();
            list = context.getrepdetails(dept).ToList();
            return list;

        }

        //Shijun
		//Sruthi- to save the representative details of a department
        public static void saverepdetails(string dept, int id)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                var q = from department_rep in context.department_rep
                        where department_rep.department_id.Equals(dept) &&
                        department_rep.representative_status.Equals("Active")
                        select department_rep;
                department_rep d = q.FirstOrDefault();
                //find the active user first and remove active           
                Roles.AddUserToRole(d.employee.user_id, Constants.ROLES_EMPLOYEE);
                Roles.RemoveUserFromRole(d.employee.user_id, Constants.ROLES_DEPARTMENT_REPRESENTATIVE);
                d.representative_status = "InActive";
                context.SaveChanges();
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                department_rep dr = new department_rep
                {
                    department_id = dept,
                    representative_id = id,
                    appointed_date = DateTime.ParseExact(today, "yyyy-MM-dd", null),
                    representative_status = "Active"
                };
                context.department_rep.Add(dr);
                context.SaveChanges();
                ts.Complete();
            }


        }

		// Sruthi- to update the department pin
        public static void updatepassword(string dept, int password)
        {
            var q = from department in context.departments
                    where department.department_id.Equals(dept)
                    select department;
            department d = q.FirstOrDefault();
            d.department_pin = password;
            context.SaveChanges();
        }

		//Sruthi- to get the list of collections
        public static List<collection> GetCollection()
        {
            var q = from collection c in context.collections select c;
            return q.ToList();

        }

        public static void updatelocation(string dept, int id)
        {
            //var q=from collection_de

        }

        public static List<spGetUndisbursedROList_Result> GetUndisbursedROList()
        {
            List<spGetUndisbursedROList_Result> list = new List<spGetUndisbursedROList_Result>();
            return list = context.spGetUndisbursedROList().ToList();
        }

        public static requisition_order_detail GetRODetailByROIdAndItemNum(string roId, string itemNum)
        {
            requisition_order_detail rod = new requisition_order_detail();
            return rod = context.requisition_order_detail.Where(x => (x.requisition_id == roId) && (x.item_number == itemNum)).FirstOrDefault();
        }


        public static void UpdateRODetails(requisition_order_detail rod)
        {
            requisition_order_detail rodUpdate = context.requisition_order_detail.Where(x => (x.requisition_id == rod.requisition_id) && (x.item_number == rod.item_number)).First();
            rodUpdate.item_distributed_quantity = rod.item_distributed_quantity;
            rodUpdate.item_pending_quantity = rod.item_pending_quantity;
            context.SaveChanges();
        }

        public static List<spGetDepartmentList_Result> GetDepartmentList()
        {
            List<spGetDepartmentList_Result> list = new List<spGetDepartmentList_Result>();
            return list = context.spGetDepartmentList().ToList();
        }

        public static List<spGetRODetailsByROId_Result> GetRODetailsByROId(string roId)
        {
            List<spGetRODetailsByROId_Result> list = new List<spGetRODetailsByROId_Result>();
            return list = context.spGetRODetailsByROId(roId).ToList();
        }

        public static List<spViewCollectionList_Result> ViewCollectionList()
        {
            List<spViewCollectionList_Result> list = new List<spViewCollectionList_Result>();
            return list = context.spViewCollectionList().ToList();
        }

        public static int GetDepartmentPin(string departmentname)
        {
            return (int)context.spGetDepartmentPin(departmentname).ToList().Single();
        }
        //Shijun
        public static System.Collections.IEnumerable GetSupplier(string id)
        {
            var nestedQuery = from s in context.suppliers
                              from sid in s.supplier_itemdetail
                              from i in context.inventories
                              where (sid.item_number == id && i.item_number == id)
                              orderby (sid.priority)
                              select new { s.supplier_name, sid.unit_price, s.supplier_id };
            return nestedQuery.ToList();

        }
        // Returns a suggested reorder quantity when give an item code
        // Returns zero if there are no purchase order in the past.
        public static int GetSuggestedReorderQuantity(string itemCode)
        {

            var context = new LogicUniversityEntities();
            var result = context.getRequestedItemQuantityLastYear(itemCode).ToList();
            if (result.Count == 1)
            {
                // Formula: Quantity requested every month
                int quantity = (int)result.First().quantity_requested;
                quantity = quantity / 12;
                return quantity;
            }

            return 0;
        }



        //Alan-start

        //List all adjustment form
        public static List<adjustment> StoreSupGetAdj()
        {
            return context.adjustments.Where(x => x.adjustment_status.Trim().ToLower() == "pending" && Math.Abs(x.adjustment_price) <= 250).ToList();
        }
        public static List<adjustment> StoreManagerGetAdj()
        {
            return context.adjustments.Where(x => x.adjustment_status.Trim().ToLower() == "pending" && Math.Abs(x.adjustment_price) >= 250).ToList();
        }


        //update upon approval adjustment form
        public static void Updateadj(int id, string comment, string itemno, int qty)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).FirstOrDefault<adjustment>();
            adj.adjustment_status = "Approved";
            adj.manager_remark = comment;
            //to update current qty in inventory
            inventory item = context.inventories.Where(x => x.item_number == itemno).FirstOrDefault<inventory>();
            item.current_quantity = item.current_quantity + qty;

            context.SaveChanges();

        }

        //update upon reject adjustment form
        public static void RejectAdj(int id, string comment)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).FirstOrDefault<adjustment>();
            adj.adjustment_status = "Rejected";
            adj.manager_remark = comment;
            context.SaveChanges();

        }




        //searchdateforstoremanager
        public static List<adjustment> StoreManagerSearchAdj(DateTime date)
        {

            return context.adjustments.Where(x => x.adjustment_date == date && x.adjustment_status.Trim().ToLower() == "pending" && Math.Abs(x.adjustment_price) >= 250).ToList<adjustment>();


        }
        //searchdateforstoresup
        public static List<adjustment> StoreSupSearchAdj(DateTime date)
        {

            return context.adjustments.Where(x => x.adjustment_date == date && x.adjustment_status.Trim().ToLower() == "pending" && Math.Abs(x.adjustment_price) < 250).ToList<adjustment>();


        }



        //To list pending purchase orders 
        public static List<sp_getPendingPOList_Result> GetPurchaseOrders()
        {
            return context.sp_getPendingPOList().ToList();
        }

        //To pending PO purchase order details
        public static List<sp_getPendingPODetails_Result> GetPODetails(int id)
        {
            return context.sp_getPendingPODetails(id).ToList();
        }

        //extra details for individual PO
        public static List<sp_geteachPendingPOList_Result> GetEachPODetail(int id)
        {
            return context.sp_geteachPendingPOList(id).ToList();
        }

        //approval of PO
        public static void UpdateUponPOApproval(int id, string mremark, string email)
        {
            //change email format
            purchase_order po = context.purchase_order.Where(x => x.purchase_order_number == id).FirstOrDefault<purchase_order>();
            po.purchase_order_status = "Pending";
            po.manager_remark = mremark;
            int pono = po.purchase_order_number;

            List<purchase_order_detail> pod = context.purchase_order_detail.Where(x => x.purchase_order_number == id).ToList();

            List<string> xxx = new List<string>();
            for (int i = 0; i < pod.Count; i++)
            {
                var xx = pod[i].item_number.ToString();
                var yy = pod[i].item_purchase_order_quantity.ToString();


                xxx.Add(xx);
                xxx.Add(yy);
                xxx.Add(Environment.NewLine);
            }

            string ot = string.Join("\t", xxx.ToArray());
            string pt = string.Join("\n", ot);

            sendMail(email, $"Email for Purchase Order {pono}",
                $"Dear Supplier,\n This is an email to notify you on the purchase order  {pono} from Logic University." +
                $"\nItem No. \tOrdered Qty\n\t {pt}\n\n " +
                $"\nThis is a system generated message.");

            context.SaveChanges();
        }

        //reject of PO
        public static void UpdateUponPOReject(int id, string mremark)
        {
            purchase_order po = context.purchase_order.Where(x => x.purchase_order_number == id).FirstOrDefault<purchase_order>();
            po.purchase_order_status = "Rejected";
            po.manager_remark = mremark;
            context.SaveChanges();
        }

        //E-mail util class
        public static void sendMail(string to, string sub, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("adteam3@gmail.com");
            mail.To.Add(to);
            mail.Subject = sub;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("adteam3@gmail.com", "testing!23");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

        //send email function
        public static void sendMail(List<string> to, string sub, string body)
        {
            foreach(string email in to)
            {
                sendMail(email, sub, body);
            }           

        }


        //alan-end

        // Tharrani start
        //List All Active Inventory
        public static List<inventory> GetActiveInventory()
        {
            return context.inventories.Where(p => p.item_status == "Active").ToList<inventory>();
        }

        //Return inventory which matches description
        public static List<inventory> SearchActiveInventory(string search)
        {
            return context.inventories.Where(P => P.item_status == "Active" && P.description.Contains(search)).ToList<inventory>();
        }

        //Return selected inventory item
        public static inventory GetInventoryById(string Id)
        {
            return context.inventories.Where(P => P.item_number == Id).FirstOrDefault();
        }

        //Return selected inventory unit price
        public static double GetItemUnitPrice(string Id)
        {
            List<supplier_itemdetail> l = new List<supplier_itemdetail>();
            l = context.supplier_itemdetail.Where(P => P.item_number == Id).ToList<supplier_itemdetail>();
            double s = 0;
            s = l[0].unit_price;
            return s;
        }

        //Add new entry to Requisition order table
        public static void AddNewRequisitionOrder(string id, int emp, DateTime d)
        {

            requisition_order r = new requisition_order();
            r.requisition_id = id;
            r.employee_id = emp;
            r.requisition_date = d;
            r.requisition_status = "Pending";
            context.requisition_order.Add(r);
            context.SaveChanges();
        }

        //Add new entry to Requisition order detail table
        public static void AddRequisitionOrderDetail(cart c, string id)
        {
            requisition_order_detail rod = new requisition_order_detail();
            rod.requisition_id = id;
            rod.item_number = c.Inventory.item_number;
            rod.item_requisition_quantity = c.Quantity;
            rod.item_distributed_quantity = 0;
            rod.item_pending_quantity = c.Quantity;
            rod.item_requisition_price = c.Itemprice;
            context.requisition_order_detail.Add(rod);
            context.SaveChanges();
        }

        //return detail of requisition order by Id (For Request Confirmation)
        public static requisition_order GetRequisitionOrderById(string Id)
        {
            return context.requisition_order.Where(x => x.requisition_id == Id).First();
        }

        //Return status of requisition orders for dropdownlist
        public static List<GetRequisitionStatus_Result> GetRequisitionStatus()
        {
            return context.GetRequisitionStatus().ToList();
        }

        //Return Pending requisition order by employee
        public static List<requisition_order> GetPendingRequisitionByEmployee(int id)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == "Pending").ToList<requisition_order>();
        }

        //Return all requisition by employee
        public static List<requisition_order> GetAllRequisitionByEmployee(int id)
        {
            return context.requisition_order.Where(x => x.employee_id == id).ToList<requisition_order>();
        }

        //Return requisition by employee with date for all status
        public static List<requisition_order> GetRequisitionByEmployeeSearchDateAllStatus(int i, DateTime d)
        {
            return context.requisition_order.Where(x => x.employee_id == i && x.requisition_date == d).ToList();
        }

        //return requisition by employee with date for status
        public static List<requisition_order> GetRequisitionByEmployeeSearchDate(int id, DateTime d, string status)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == status && x.requisition_date == d).ToList();
        }

        //return requisition by employee with status
        public static List<requisition_order> GetRequisitionByEmployeeSearchStatus(int id, string status)
        {
            return context.requisition_order.Where(x => x.employee_id == id && x.requisition_status == status).ToList();
        }

        //Return requisition order detail by id
        public static List<getRequisitionOrderDetails_Result> GetRequisitionorderDetail(string id)
        {
            List<getRequisitionOrderDetails_Result> l = new List<getRequisitionOrderDetails_Result>();
            return l = context.getRequisitionOrderDetails(id).ToList();
        }

        //Cancel requisition order status to cancel
        public static void Cancelrequisition(string id)
        {
            requisition_order r = context.requisition_order.Where(x => x.requisition_id == id).First();
            r.requisition_status = "Cancel";
            context.SaveChanges();
        }

        //Save changes in edit request to requisition order
        public static void UpdateRequisitionOrderDetail(string id, List<getRequisitionOrderDetails_Result> order)
        {
            if (order.Count > 0)
            {
                List<requisition_order_detail> rod = new List<requisition_order_detail>();
                rod = context.requisition_order_detail.Where(x => x.requisition_id == id).ToList();
                for (int i = 0; i < rod.Count; i++)
                {
                    bool found = false;
                    for (int j = 0; j < order.Count; j++)
                    {
                        if (rod[i].item_number == order[j].item_number)
                        {
                            found = true;
                            string item = order[j].item_number;
                            requisition_order_detail r = context.requisition_order_detail.Where(x => x.requisition_id == id && x.item_number == item).First<requisition_order_detail>();
                            r.item_requisition_quantity = order[j].item_requisition_quantity;
                            r.item_distributed_quantity = 0;
                            r.item_pending_quantity = order[j].item_requisition_quantity;
                            r.item_requisition_price = order[j].item_requisition_price;
                            context.SaveChanges();
                        }
                    }
                    if (!found)
                    {
                        string it = rod[i].item_number;
                        requisition_order_detail re = context.requisition_order_detail.Where(x => x.requisition_id == id && x.item_number == it).First<requisition_order_detail>();
                        context.requisition_order_detail.Remove(re);
                    }
                }
            }
            else
            {
                Cancelrequisition(id);
            }
        }

        //return all PO - with total PO item count
        public static List<getAllViewPOHistorytotalcount_Result> viewpohistorytotal()
        {
            return context.getAllViewPOHistorytotalcount().ToList();
        }

        //return list of supplier for dropdown in ViewPoHistory
        public static List<supplier> getSupplierNames()
        {
            return context.suppliers.ToList();
        }

        //return supplier code based on supplier name
        public static supplier getSupplierCode(string text)
        {
            return context.suppliers.Where(x => x.supplier_name == text).FirstOrDefault();
        }

        //Return list of PO status for dropdown in ViewPoHistory
        public static List<purchase_order> getPOStatus()
        {
            return context.purchase_order.ToList();
        }

        //return PO by status- with total PO item count
        public static List<getViewPOHistorytotalcountByStatus_Result> ViewPOHistorytotalcountByStatus(string s)
        {
            return context.getViewPOHistorytotalcountByStatus(s).ToList();
        }

        //return  PO by Po number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPO_Result> viewPOHistorytotalcountbyPO(int po)
        {
            return context.getViewPOHistorytotalcountbyPO(po).ToList();
        }

        //return  PO by Po number and status - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandstatus_Result> ViewPOHistorytotalcountbyPOandstatus(int po, string status)
        {
            return context.getViewPOHistorytotalcountbyPOandstatus(po, status).ToList();
        }

        //return  PO by supplier - with total PO item count
        public static List<getViewPOHistorytotalcountbySupplier_Result> viewPOHistorytotalcountbySupplier(string supplier)
        {
            return context.getViewPOHistorytotalcountbySupplier(supplier).ToList();
        }

        //return  PO by supplier and PO number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandSupplier_Result> viewPOHistorytotalcountbyPOandSupplier(int po, string supplier)
        {
            return context.getViewPOHistorytotalcountbyPOandSupplier(po, supplier).ToList();
        }

        //return  PO by supplier and status - with total PO item count
        public static List<getViewPOHistorytotalcountbysupandstatus_Result> viewPOHistorytotalcountbysupandstatus(string supplier, string status)
        {
            return context.getViewPOHistorytotalcountbysupandstatus(supplier, status).ToList();
        }

        //return  PO by supplier and status and Po number - with total PO item count
        public static List<getViewPOHistorytotalcountbyPOandstatusandSupplier_Result> viewPOHistorytotalcountbyPOandstatusandSupplier(string supplier, int po, string status)
        {
            return context.getViewPOHistorytotalcountbyPOandstatusandSupplier(supplier, po, status).ToList();
        }

        //return all PO - with total PO item count
        public static List<getAllViewPOHistorypendingcount_Result> viewPOHistorypendingcount()
        {
            return context.getAllViewPOHistorypendingcount().ToList();
        }

        //return PO detail based on PO ID
        public static List<purchase_order_detail> getpurchaseorderdetail(int id)
        {
            return context.purchase_order_detail.Where(x => x.purchase_order_number == id).ToList();
        }

        //return supplier for PO by PO id
        public static supplier getSupplierNameforPurchaseorder(int id)
        {
            purchase_order p = getpurchaseorder(id);
            string sid = p.suppler_id;
            return context.suppliers.Where(x => x.supplier_id == sid).FirstOrDefault();
        }

        //return po for id
        public static purchase_order getpurchaseorder(int id)
        {
            return context.purchase_order.Where(x => x.purchase_order_number == id).FirstOrDefault();
        }

        //return employee name
        public static employee GetEmployee(int id)
        {
            return context.employees.Where(x => x.employee_id == id).FirstOrDefault();
        }

        //accept item from supplier
        public static void acceptitemfromsupplier(int po, string item, int quantity, string remark)
        {
            purchase_order_detail pod = context.purchase_order_detail.Where(x => x.purchase_order_number == po && x.item_number == item).FirstOrDefault();
            pod.item_accept_quantity = quantity;
            pod.purchase_order_item_remark = remark;
            pod.item_purchase_order_status = "Accepted";
            pod.item_accept_date = DateTime.Now.Date;
            context.SaveChanges();
            updatePOstatus(po);
            inventory i = BusinessLogic.GetInventoryById(item);
            i.current_quantity = i.current_quantity + quantity;
            context.SaveChanges();

        }


        //update PO status to complete if no pending item
        public static void updatePOstatus(int po)
        {
            List<getAllViewPOHistorypendingcountbyPO_Result> pending_count = context.getAllViewPOHistorypendingcountbyPO(po).ToList();
            bool flag = false;
            for (int i = 0; i < pending_count.Count; i++)
            {
                if (pending_count[i].purchase_order_number == po)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                purchase_order p = getpurchaseorder(po);
                p.purchase_order_status = "Completed";
                context.SaveChanges();
            }
        }
        //return employee based on userid
        public static employee GetEmployeeByUserID(string userid)
        {
            return context.employees.Where(x => x.user_id.Trim() == userid.Trim()).FirstOrDefault();
        }

        //return department based on department id
        public static department GetDepartmenthead(string dept)
        {
            return context.departments.Where(x => x.department_id.Trim() == dept.Trim()).FirstOrDefault();
        }

        //get last placed request id by department

        public static unique_id getlastrequestid(string Depid)
        {
            return context.unique_id.Where(x => x.department_id.Trim() == Depid).FirstOrDefault();
        }

        //update latest request id for department
        public static void updatelastrequestid(string Depid, int i)
        {
            unique_id u = context.unique_id.Where(x => x.department_id.Trim() == Depid).FirstOrDefault();
            u.req_id = i;
            context.SaveChanges();
        }
        // Tharrani end


        //Esther
        //ClerkInventory
        //return Active Inventories
        public static List<inventory> GetActiveInventories()
        {
            return context.inventories.Where(x => x.item_status.ToLower() == "active").ToList();
        }

        //return all inventories
        public static List<inventory> GetAllInventories()
        {
            return context.inventories.ToList();
        }

        //return string categories for dropdownlist
        public static List<string> GetCategories()
        {
            return context.inventories.OrderBy(x => x.category).Select(x => x.category).Distinct().ToList();
        }

        //return pending PO for cInventory
        public static int ReturnPendingPOqtyByStatus(inventory item, string status)
        {
            var q = context.purchase_order_detail.Where(x => x.item_purchase_order_status.ToLower().Trim() == "pending"
            && x.purchase_order.purchase_order_status.ToLower().Trim() == status);
            int qty = 0;
            foreach (var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.item_number.ToLower().Trim()))
                {
                    qty += a.item_purchase_order_quantity;
                }
            }
            return qty;

        }

        //return pendingadjqty for cInventory
        public static int ReturnPendingMinusAdjustmentQty(string item)
        {
            var q = context.adjustments.Where(x => x.adjustment_status.ToLower().Trim() == "pending" && x.adjustment_quantity < 0);
            int qty = 0;
            foreach (var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.ToLower().Trim()) && a.adjustment_quantity < 0)
                {
                    qty += a.adjustment_quantity;
                }
            }
            return qty;
        }
        public static int ReturnPendingPlusAdjustmentQty(string item)
        {
            var q = context.adjustments.Where(x => x.adjustment_status.ToLower().Trim() == "pending" && x.adjustment_quantity > 0);
            int qty = 0;
            foreach (var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.ToLower().Trim()))
                {
                    qty += a.adjustment_quantity;
                }
            }
            return qty;
        }

        //return low-in-stockinventories
        public static List<inventory> GetLowInStockInventories()
        {
            return context.inventories.Where(x => x.current_quantity < x.reorder_level && x.item_status.ToLower().Trim() == "active").ToList();
        }

        //return adjustment price based on priority supplier
        public static double Adjprice(string itemcode)
        {
            return context.supplier_itemdetail.Where(x => x.item_number.Trim().ToLower() == itemcode.ToLower().Trim() && x.priority == 1).Select(x => x.unit_price).FirstOrDefault();
        }

        //get inventory by itemcode
        public static inventory GetInventory(string itemcode)
        {
            return context.inventories.Where(x => x.item_number.Trim().ToLower() == itemcode.Trim().ToLower()).FirstOrDefault();
        }

        //create new adjustment
        public static string CreateAdjustment(adjustment a)
        {
            try
            {
                context.adjustments.Add(a);
                context.SaveChanges();
                return (a.item_number.Trim() + " adj request submitted");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static employee GetEmployeeById(int id)
        {
            return context.employees.Where(x => x.employee_id == id).FirstOrDefault();
        }
        public static supplier_itemdetail GetPrioritySupplierItemDetail(string itemcode)
        {
            return context.supplier_itemdetail.Where(x => x.priority == 1 && x.item_number.Trim().ToLower() == itemcode.Trim().ToLower()).FirstOrDefault();
        }

        public static supplier FindSupplierBySupplierID(string supplierid)
        {
            return context.suppliers.Where(x => x.supplier_id.Trim().ToLower() == supplierid).FirstOrDefault();
        }
        public static int ReturnIndex(List<POStaging> StagingList, POStaging item)
        {
            string itemcode = item.Inventory.item_number.Trim().ToLower();
            string supplier = item.Supplier.supplier_id.Trim().ToLower();
            int value = -1;
            if (StagingList != null)
            {
                for (int i = 0; i < StagingList.Count(); i++)
                {
                    string aItemCode = StagingList[i].Inventory.item_number.Trim().ToLower();
                    string aSupplier = StagingList[i].Supplier.supplier_id.Trim().ToLower();
                    if (itemcode.Equals(aItemCode) && supplier.Equals(aSupplier))
                    {
                        value = i;
                    }
                }
            }
            return value;
        }

        public static List<POStaging> AddToStaging(List<POStaging> StagingList, POStaging item)
        {
            int value = ReturnIndex(StagingList, item);
            if (value == -1)
            {
                StagingList.Add(item);
                return StagingList;
            }
            else
            {
                StagingList[value].OrderedQty += item.OrderedQty;
                return StagingList;
            }
        }

        //return last POIndex
        public static int POIndex()
        {
            return context.purchase_order.OrderByDescending(x => x.purchase_order_number).Select(x => x.purchase_order_number).FirstOrDefault();
        }
        //create PO
        public static void CreatePO(purchase_order purchaseorder)
        {
            context.purchase_order.Add(purchaseorder);
            context.SaveChanges();
        }

        //create PO Details
        public static void CreatePOdetails(purchase_order_detail purchaseorderdetails)
        {
            context.purchase_order_detail.Add(purchaseorderdetails);
            context.SaveChanges();
        }

        //get supplier id
        public static string GetSupplierID(string supplier_name)
        {
            return context.suppliers.Where(x => x.supplier_name.Trim().ToLower() == supplier_name.Trim().ToLower()).Select(x => x.supplier_id).FirstOrDefault();
        }

        //dialog box
        public static string MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            return msg;
        }

        public static string RetrieveEmailByEmployeeID(int id)
        {
            return context.employees.Where(x => x.employee_id == id).Select(x => x.email_id).FirstOrDefault();
        }

        public static List<adjustment> GetPendingAdjustmentsByItemCode(string itemcode)
        {
            return context.adjustments.Where(x => x.item_number.Trim().ToLower() == itemcode.Trim().ToLower() && x.adjustment_status.ToLower().Trim() == "pending").ToList();
        }

        public static int? GetSupervisorID(int employeeid)
        {
            return context.employees.Where(x => x.employee_id == employeeid).Select(x => x.supervisor_id).First();
        }

        public static int DepartmentHeadID(employee employee)
        {
            return context.departments.Where(x => x.department_id.Trim().ToLower() == employee.department_id.Trim().ToLower()).Select(x => x.head_id).First();
        }

        public static inventory GetInventoryByItemCode(string ItemCode)
        {
            return context.inventories.Where(x => x.item_number.ToLower().Trim() == ItemCode.ToLower().Trim()).FirstOrDefault();
        }

        public static string SendEmailAdjustmentApproval(adjustment a)
        {
            string email;
            if (a.adjustment_price > 250)
            {
                int id = DepartmentHeadID(a.employee);
                email = RetrieveEmailByEmployeeID(id);
            }
            else
            {
                int? id = GetSupervisorID(a.employee_id);
                if (id != null)
                {
                    int supid = (int)id;
                    email = RetrieveEmailByEmployeeID(supid);
                }
                else
                {
                    int headid = DepartmentHeadID(a.employee);
                    email = RetrieveEmailByEmployeeID(headid);
                }
            }
            return email;
        }
        //get all department object
        public static List<department> ReturnDep()
        {
            return context.departments.ToList();
        }
        //Esther end

        //Rohit - start
        public static List<spAcknowledgeDistributionList_Result> ViewAcknowledgementList(int collection_id)
        {
            //get ItemNumber,Description,Ordered Quantity, Supplied Quantity for a particular collectionID
            List<spAcknowledgeDistributionList_Result> list = new List<spAcknowledgeDistributionList_Result>();
            return list = context.spAcknowledgeDistributionList(collection_id).ToList();
        }

        //Rohit
        public static int getActualSupplyQuantityValue(int collectionID, String ItemCode)
        {
            //get the supplied quantity of a particular item in a collection to compare with user's input(value entered by Dept rep during collection)
            return (int)context.spCheckSupplyQuantity(ItemCode, collectionID).ToList().Single();
        }

        public static List<spGetRequisitionIDAndItemQuantity_Result> getRequisitionIDandItemQuantity(int collectionID, string itemCode)
        {
            //Each collection can contain one or more requistionIDs.
            //Theis procedure gets all the Requisition IDs and ItemDistributedQuantity for a particular item.
            List<spGetRequisitionIDAndItemQuantity_Result> list = new List<spGetRequisitionIDAndItemQuantity_Result>();
            return list = context.spGetRequisitionIDAndItemQuantity(collectionID, itemCode).ToList();
        }

        public static void UpdateItemDistributedQuantity(string ItemCode, string requisitionID, int itemDistributedQuantity)
        {
            //if user(dept rep) collects less than what was supplied.
            context.spUpdateItemDistributedQuantity(ItemCode, requisitionID, itemDistributedQuantity);
        }

        public static void updateInventory(string itemCode, int difference)
        {
            //rohit's sp context.spUpdateInventory(itemCode, difference);
            inventory i = context.inventories.Where(x => x.item_number.Trim().ToLower() == itemCode.Trim().ToLower()).FirstOrDefault();
            i.current_quantity = i.current_quantity + difference;
            context.SaveChanges();
        }

        public static void updateCollectionStatus(int collectionID)
        {
            //update that the collection has been completed
            context.spUpdateCollectionStatusCollected(collectionID);
        }

        public static List<spViewCollectionList_Result> ViewCollectionListNew()
        {
            //View Collection of items that are to be collected by the dept rep. (CollectionID of all departments)
            List<spViewCollectionList_Result> list = new List<spViewCollectionList_Result>();
            return list = context.spViewCollectionList().ToList();
        }

        //Rohit
        public static void AcknowledgeDL(int collection_id, string ItemCode, int ActualSupplyQuantityValue, int UserInput)
        {
            if (UserInput == ActualSupplyQuantityValue)
            {
                //logic for equal
            }
            else
            {
                //logic for lesser in else block



                //Getting data into lists of their datatypes.
                List<Team3ADProject.Model.spGetRequisitionIDAndItemQuantity_Result> mylist = getRequisitionIDandItemQuantity(collection_id, ItemCode);
                List<int> myIntegerList = new List<int>(mylist.Count);
                List<string> myRequsitionIDList = new List<string>(mylist.Count);
                for (int j = 0; j < mylist.Count; j++)
                {
                    myIntegerList.Add(Convert.ToInt32(mylist[j].item_distributed_quantity));
                    myRequsitionIDList.Add(mylist[j].requisition_id.ToString());
                }

                int counter = UserInput;
                int myIntegerListSize = myIntegerList.Count;
                int minimum = counter / myIntegerListSize;

                int adder = 0, k = -1;
                while (true)
                {
                    k++;
                    if (adder < UserInput)
                    {
                        adder = adder + myIntegerList[k];
                    }
                    if (adder >= UserInput)
                    {
                        break;
                    }
                }


                int temp = adder - UserInput;
                myIntegerList[k] = myIntegerList[k] - temp;
                k++;

                for (; k < myIntegerListSize; k++)
                {
                    myIntegerList[k] = 0;
                }





                //update back with itemcode, requisitonID(myRequsitionIDList) and [itemdistributed quantity(myIntegerList)]
                for (int j = 0; j < myRequsitionIDList.Count; j++)
                {
                    string requisitionID = myRequsitionIDList[j];
                    int itemDistributedQuantity = myIntegerList[j];

                    UpdateItemDistributedQuantity(ItemCode, requisitionID, itemDistributedQuantity);
                }




                //update back the difference(UserInput - ActualSupplyQuantityValue) in inventory
                int difference = ActualSupplyQuantityValue - UserInput;
                updateInventory(ItemCode, difference);

            }
        }


        //Rohit -end


        //Sruthi - start

			//sruthi- to update the collection location for a department
        public static void updatecollectionlocation(string dept, int id)
        {
            context.updatecollectiondepartment(dept, id);
            string location = context.collections.Where(x => x.place_id == id).FirstOrDefault<collection>().collection_place.ToString();
            string department = context.departments.Where(x => x.department_id == dept).FirstOrDefault<department>().department_name.ToString();
            string messagebody = "The following location for" + department + "has been selected as new location for collection of stationery \n \n" + location;
            //sending the email to store on location change
            var query =context.employees.Where(x => x.department_id == "STOR" && x.supervisor_id == 13).ToList();

            List<string> emailList = new List<string>();
            foreach(employee email in query)
            {
                emailList.Add(email.email_id);
            }
            BusinessLogic.sendMail(emailList, "Location Change", messagebody);
        }

		//sruthi- to get the budget for the particular department in the year
        public static List<budget> getbudget(string dept)
        {
            var q = from b in context.budgets where b.year.Equals(DateTime.Now.Year) && b.department_id.Equals(dept) select b;
            List<budget> list = q.ToList();
            list = (List<budget>)list.OrderBy(x => DateTime.ParseExact(x.month, "MMM", System.Globalization.CultureInfo.InvariantCulture).Month).ToList();
            return list;
        }

		//sruthi- to update the budget for the department in a particular month
        public static void updatebudget(string dept, string month, int budget)
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            var q = from b in context.budgets where b.department_id.Equals(dept) && b.month.Equals(month) && b.year.Equals(year) select b;
            budget b1 = q.FirstOrDefault();
            b1.budget1 = budget;
            context.SaveChanges();

        }
		//sruthi-to get the budget allocated for the department in the current month
        public static int getbudgetbydept(string dept)
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            string month = DateTime.Now.ToString("MMM");
            var q = (from b in context.budgets where b.department_id.Equals(dept) && b.year.Equals(year) && b.month.Equals(month) select b);
            int b1 = 0;
            if (q.FirstOrDefault().budget1.HasValue)
            {
                b1 = Convert.ToInt32(q.FirstOrDefault().budget1.Value);
            }
            return b1;
        }

		//Sruthi- to get the spent budget allocated for the department in the current month
		public static int getspentbudgetbydept(string dept)
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            string month = DateTime.Now.ToString("MMM");
            var q = (from b in context.budgets where b.department_id.Equals(dept) && b.year.Equals(year) && b.month.Equals(month) select b);
            int b1 = 0;
            if (q.FirstOrDefault().spent.HasValue)
            {
                b1 = Convert.ToInt32(q.FirstOrDefault().spent.Value);
            }
            return b1;
        }

		//Sruthi- To get the user email id based on employee id
        public static string GetUserID(int employee_id)
        {
            return context.employees.Where(x => x.employee_id == employee_id).Select(x => x.user_id).FirstOrDefault();
        }

		//Sruthi- To get the email of the employees of the particular department
        public static List<string> getEmployeesEmailFromDept(string dept)
        {
            var query = context.employees.Where(x => x.department_id == dept).ToList<employee>();

            var query1 = context.department_rep.Where(x => x.representative_status == "Active" && x.department_id == dept).FirstOrDefault();

            var query2 = context.departments.Where(x => x.department_id == dept).FirstOrDefault();

            var nestedQuery = query.Where(x => x.employee_id != query1.representative_id && x.employee_id != query2.head_id).ToList<employee>();

            List<string> listOfEmail = new List<string>();

            foreach(employee userid in nestedQuery)
            {
                MembershipUser mu = Membership.GetUser(userid.user_id);
                listOfEmail.Add(mu.Email);
            }

            return listOfEmail;
        }

		//Sruthi- to get the collection details of the department
        public static List<getcollectiondetailsbydepartment_Result> getdepartmentcollection(string dept)
        {
            return context.getcollectiondetailsbydepartment(dept).ToList();
        }


        //Sruthi - End



        //JOEL - START

        //CollectionList - Gets CollectionList, list of items to collect from warehouse.
        public static List<spGetCollectionList_Result> GetCollectionList()
        {
            List<spGetCollectionList_Result> list = new List<spGetCollectionList_Result>();
            return list = context.spGetCollectionList().ToList();
        }

        //CollectionList - for each item num, gets the total qty ordered by all dpts. 
        public static int GetAllDptRequiredQtyByItem(string itemNum)
        {
            spFindAllDptRequiredQtyByItem_Result demand = new spFindAllDptRequiredQtyByItem_Result();
            demand = context.spFindAllDptRequiredQtyByItem(itemNum).FirstOrDefault();

            return Convert.ToInt32(demand.total_required_qty);
        }

        //CollectionList - finds the dpt id and the required qty by item number
        public static List<spFindDptIdAndRequiredQtyByItem_Result> spFindDptIdAndRequiredQtyByItem(string itemNum)
        {
            List<spFindDptIdAndRequiredQtyByItem_Result> list = new List<spFindDptIdAndRequiredQtyByItem_Result>();
            return list = context.spFindDptIdAndRequiredQtyByItem(itemNum).ToList();
        }

        //CollectionList - gets the full list of ROIDs that have been approved but haven't been collected
        public static List<spGetFullCollectionROIDList_Result> GetFullCollectionROIDList()
        {
            List<spGetFullCollectionROIDList_Result> list = new List<spGetFullCollectionROIDList_Result>();
            return list = context.spGetFullCollectionROIDList().ToList();
        }

        //CollectionList - allocates goods to departments, prioritising by date of requisition. dates with earlier RO dates will be filled first.
        public static void SortCollectedGoods(List<CollectionListItem> allDptCollectionList)
        {
            List<spGetFullCollectionROIDList_Result> list = BusinessLogic.GetFullCollectionROIDList();

            foreach (var collectedItem in allDptCollectionList)
            {
                int store = collectedItem.qtyPrepared;
                foreach (var roidListItem in list)
                {
                    {
                        if (collectedItem.itemNum.Trim() == roidListItem.item_number.Trim())
                        {
                            if (collectedItem.qtyPrepared >= roidListItem.item_pending_quantity)
                            {
                                roidListItem.item_distributed_quantity += roidListItem.item_pending_quantity;
                                collectedItem.qtyPrepared -= roidListItem.item_pending_quantity;
                                roidListItem.item_pending_quantity = 0;

                                requisition_order_detail rod = new requisition_order_detail();
                                rod.requisition_id = roidListItem.requisition_id;
                                rod.item_number = roidListItem.item_number;
                                rod.item_distributed_quantity = roidListItem.item_distributed_quantity;
                                rod.item_pending_quantity = roidListItem.item_pending_quantity;
                                BusinessLogic.UpdateRODetails(rod);
                            }
                            else
                            {
                                roidListItem.item_distributed_quantity += collectedItem.qtyPrepared;
                                roidListItem.item_pending_quantity -= collectedItem.qtyPrepared;
                                collectedItem.qtyPrepared = 0;

                                requisition_order_detail rod = new requisition_order_detail();
                                rod.requisition_id = roidListItem.requisition_id;
                                rod.item_number = roidListItem.item_number;
                                rod.item_distributed_quantity = roidListItem.item_distributed_quantity;
                                rod.item_pending_quantity = roidListItem.item_pending_quantity;
                                BusinessLogic.UpdateRODetails(rod);
                            }
                        }
                    }
                }

                collectedItem.qtyPrepared = store;
            }
        }

        //CollectionList - After collection is done, this method deducts qty from inventory
        public static void DeductFromInventory(List<CollectionListItem> list)
        {
            foreach (var item in list)
            {
                inventory i = context.inventories.Where(x => x.item_number == item.itemNum).First();
                i.current_quantity -= item.qtyPrepared;
                context.SaveChanges();
            }
        }

        //DisbursementSorting - gets the list of ROIDs for sorting based on the dptID and item num
        public static List<spGetRODListForSorting_Result> GetRODListForSorting(string dptId, string itemNum)
        {
            List<spGetRODListForSorting_Result> result = new List<spGetRODListForSorting_Result>();
            return result = context.spGetRODListForSorting(dptId, itemNum).ToList();
        }

        //DisbursementSorting - gets the list of items to be sorted / allocated for the selected department
        public static List<spGetSortingTableByDpt_Result> GetSortingListByDepartment(string selectedDptName)
        {
            string dpt_Id = context.departments.Where(x => x.department_name == selectedDptName).FirstOrDefault().department_id;
            List<spGetSortingTableByDpt_Result> list = new List<spGetSortingTableByDpt_Result>();
            return list = context.spGetSortingTableByDpt(dpt_Id).ToList();
        }

        //DisbursementSorting - gets dpt id from the dpt name
        public static string GetDptIdFromDptName(string dptName)
        {
            string s = context.departments.Where(x => x.department_name == dptName).FirstOrDefault().department_id;
            return s.Trim();
        }

        //DisbursementSorting - inserts a row into collection details table to generate a new collection id
        public static void InsertCollectionDetailsRow(int placeId, DateTime collectionDate, string dpt_Id)
        {
            string collectionStatus = "Pending";
            context.spInsertCollectionDetail(placeId, collectionDate, collectionStatus, dpt_Id);
        }

        //DisbursementSorting - gets the latest collection id that was generated after writing to the Collection Details table. To be used to write into the Requisition Disbursement Detail table. 
        public static int GetLatestCollectionId()
        {
            spGetLatestCollectionDetailId_Result latest = new spGetLatestCollectionDetailId_Result();
            latest = context.spGetLatestCollectionDetailId().FirstOrDefault();
            return Convert.ToInt32(latest.collection_id);
        }

        //DisbursementSorting - gets a department's list of ROIDs that need to be disbursed. 
        public static List<string> GetListOfROIDForDisbursement(string dpt_Id)
        {
            List<spGetListOfROIDForDisbursement_Result> list = context.spGetListOfROIDForDisbursement(dpt_Id).ToList();
            List<string> sList = new List<string>();
            foreach (var v in list)
            {
                sList.Add(v.requisition_id);
            }

            return sList;
        }

        //DisbursementSorting - displays the list of departments which have items for collection
        public static List<string> DisplayListofDepartmentsForCollection()
        {
            List<spGetFullCollectionROIDList_Result> roidList = new List<spGetFullCollectionROIDList_Result>();
            roidList = BusinessLogic.GetFullCollectionROIDList();
            List<string> dptList = new List<string>();

            foreach (spGetFullCollectionROIDList_Result q in roidList)
            {
                string dptName = q.department_name.Trim();
                if (isExisting(dptName, dptList) > 0)
                {
                    dptList.Add(dptName);
                }
            }

            return dptList;
        }

        //DisbursementSorting - used to check if department name is already in list of departments for disbursement. 
        public static int isExisting(string department_name, List<string> dptList)
        {
            foreach (string dptName in dptList)
            {
                if (dptName.Contains(department_name))
                {
                    return -1;
                }
            }
            return 1;
        }

        //DisbursementSorting - after a new collection id is created for the department, this method adds all the associated ROIDs to the disbursement table. Also sends email to dpt rep for collection
        public static void InsertDisbursementListROId(string dpt_Id)
        {
            int latestCollectionId = GetLatestCollectionId();
            List<string> roList = GetListOfROIDForDisbursement(dpt_Id);

            foreach (var v in roList)
            {
                context.spInsertDisbursementListROId(v, latestCollectionId);
            }

            //(4) send email to dpt rep
            string emailAdd = BusinessLogic.GetDptRepEmailAddFromDptID(dpt_Id);
            string subj = "Your ordered stationery is ready for collection";
            string body = "Your order is ready for collection. Please procede to your usual collection point at the correct time.";

            BusinessLogic.sendMail(emailAdd, subj, body);
        }

        public static string GetDptRepEmailAddFromDptID(string dptId)
        {
            //get the ID of the representative of the Department from department rep table
            department_rep dRep = context.department_rep.Where(x => x.department_id == dptId && x.representative_status =="Active").FirstOrDefault();
            //get the user_id of the representative of the Department
            employee e = context.employees.Where(x => x.employee_id == dRep.representative_id).FirstOrDefault();
            //get the user in aspnet db via user_id
            MembershipUser mu = Membership.GetUser(e.user_id);
            //sent the user's email back to the calling method
            return mu.Email;
        }

        //ViewROSpecialRequest - gets collection place ID from the dpt ID
        public static int GetPlaceIdFromDptId(string dptId)
        {
            spGetPlaceIdFromDptId_Result result = context.spGetPlaceIdFromDptId(dptId).FirstOrDefault();
            return (int)result.place_id;
        }

        //ViewROSpecialRequest - when special request is collected, method updates Collection Details & Requisition Disbursement Details tables. sends email to inform dpt rep for collection
        public static void SpecialRequestReadyUpdatesCDRDD(int placeId, DateTime collectionDate, string ro_id, string dpt_id)
        {
            string collectionStatus = "Pending";
            string emailAdd = BusinessLogic.GetDptRepEmailAddFromDptID(dpt_id);
            string subj = "Your ordered stationery is ready for collection";
            string body = "Dear Department Rep, your stationery order is ready for collection. Please procede to your usual collection point at the correct time.";
            BusinessLogic.sendMail(emailAdd, subj, body);
            context.spSpecialRequestReady(placeId, collectionDate, collectionStatus, ro_id, dpt_id);
        }

        //ViewROSpecialRequest - when special request is collected, method updates the Requisition Order Details table.
        public static void ViewROSpecialRequestUpdateRODTable(List<CollectionListItem> clList, string ro_id)
        {
            foreach (var item in clList)
            {
                requisition_order_detail rodFromDB = BusinessLogic.GetRODetailByROIdAndItemNum(ro_id, item.itemNum);

                if (rodFromDB != null)
                {
                    rodFromDB.item_distributed_quantity += item.qtyPrepared;
                    rodFromDB.item_pending_quantity -= item.qtyPrepared;
                    BusinessLogic.UpdateRODetails(rodFromDB);
                }
            }
        }

        //Reallocate - gets list of departments that ordered this item and the qtys they ordered. 
        public static List<spReallocateQty_Result> GetReallocateList(string itemNum)
        {
            List<spReallocateQty_Result> list = new List<spReallocateQty_Result>();
            return list = context.spReallocateQty(itemNum).ToList();
        }

        //Reallocate - to get the total collected qty for this item
        public static int GetTotalCollectedVolumeForChosenItem(string itemNum)
        {
            List<spReallocateQty_Result> list = BusinessLogic.GetReallocateList(itemNum);
            int itemCount = 0;
            foreach (var q in list)
            {
                itemCount += (int)q.item_distributed_quantity;
            }
            return itemCount;
        }

        //Reallocate - when rewriting to ROD table, need to reset the qtys to 0 first, to ensure no ROIDs are left unwritten after the reallocation
        public static void ResetRODTable(string dpt_id, string itemNum)
        {
            List<spGetFullCollectionROIDList_Result> roidList = BusinessLogic.GetFullCollectionROIDList();

            foreach (spGetFullCollectionROIDList_Result q in roidList)
            {
                if ((dpt_id.ToUpper().Trim() == q.requisition_id.Substring(0, 4).ToUpper().Trim()) && (itemNum.ToUpper().Trim() == q.item_number.ToUpper().Trim()))
                {
                    q.item_distributed_quantity = 0;
                    q.item_pending_quantity = q.item_requisition_quantity;

                    requisition_order_detail rod = new requisition_order_detail();
                    rod.requisition_id = q.requisition_id;
                    rod.item_number = q.item_number;
                    rod.item_distributed_quantity = q.item_distributed_quantity;
                    rod.item_pending_quantity = q.item_pending_quantity;
                    BusinessLogic.UpdateRODetails(rod);
                }
            }
        }

        //Reallocate - updates ROD tables with reallocated amounts.
        public static void UpdateRODTableOnReallocate(string dpt_id, string itemNum, int distriQty)
        {
            List<spGetFullCollectionROIDList_Result> roidList = BusinessLogic.GetFullCollectionROIDList();

            foreach (spGetFullCollectionROIDList_Result q in roidList)
            {
                if ((dpt_id.ToUpper().Trim() == q.requisition_id.Substring(0, 4).ToUpper().Trim()) && (itemNum.ToUpper().Trim() == q.item_number.ToUpper().Trim()))
                {
                    if (distriQty >= q.item_pending_quantity)
                    {
                        q.item_distributed_quantity += q.item_pending_quantity;
                        distriQty -= q.item_pending_quantity;
                        q.item_pending_quantity = 0;

                        requisition_order_detail rod = new requisition_order_detail();
                        rod.requisition_id = q.requisition_id;
                        rod.item_number = q.item_number;
                        rod.item_distributed_quantity = q.item_distributed_quantity;
                        rod.item_pending_quantity = q.item_pending_quantity;
                        BusinessLogic.UpdateRODetails(rod);
                    }
                    else
                    {
                        q.item_distributed_quantity += distriQty;
                        q.item_pending_quantity -= distriQty;
                        distriQty = 0;

                        requisition_order_detail rod = new requisition_order_detail();
                        rod.requisition_id = q.requisition_id;
                        rod.item_number = q.item_number;
                        rod.item_distributed_quantity = q.item_distributed_quantity;
                        rod.item_pending_quantity = q.item_pending_quantity;
                        BusinessLogic.UpdateRODetails(rod);
                    }
                }
            }
        }

        //Reallocate - calculates how much to return to inventory
        public static void ReturnToInventory(int returnBalance, string itemNum)
        {
            CollectionListItem item = new CollectionListItem();
            item.itemNum = itemNum;
            item.qtyPrepared = returnBalance;

            BusinessLogic.AddtoInventory(item);
        }

        //Reallocate - if reallocated amounts are less than what was collected, means some items need to be returned to inventory, since they won't be disbursed.
        public static void AddtoInventory(CollectionListItem item)
        {
            inventory i = context.inventories.Where(x => x.item_number == item.itemNum).FirstOrDefault();
            i.current_quantity += item.qtyPrepared;
            context.SaveChanges();
        }


        //JOEL - END



        public static double getUnitPrice(string supplier_id, string item_number)
        {
            var query = context.supplier_itemdetail.Where(x => x.supplier_id == supplier_id && x.item_number == item_number).FirstOrDefault();
            return query.unit_price;
        }

        public static List<deptusagechargeback_Result> UsageChargeBack(DateTime startdate, DateTime enddate, string deptid)
        {
            return context.deptusagechargeback(startdate, enddate, deptid).ToList();
        }

        public static int departmentheadidbydeptid(string deptid)
        {
            department d=  context.departments.Where(x => x.department_id.Trim() == deptid.Trim()).FirstOrDefault();
            return d.head_id;
        }
    }
}