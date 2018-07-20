using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;
using System.Transactions;

namespace Team3ADProject.Code
{
    public class BusinessLogic
    {
		public static Model.LogicUniversityEntities context1 = new Model.LogicUniversityEntities();

		public static List<getpendingrequestsbydepartment_Result> ViewPendingRequests(string deptid)
		{
			List<getpendingrequestsbydepartment_Result> list = new List<getpendingrequestsbydepartment_Result>();
			return list = context1.getpendingrequestsbydepartment(deptid).ToList();
		}

		public static string getdepartment(string userid)
		{
			var k = (from employee in context1.employees where employee.user_id == userid select employee);
			 string dept = k.FirstOrDefault().department_id;
			return dept;
		}

		public static getpendingrequestdetails_Result getdetails(string id)
		{
			return context1.getpendingrequestdetails(id).ToList().Single();
		}

		public static List<getitemdetails_Result> pendinggetitemdetails(string reqid)
		{
			List<getitemdetails_Result> list = new List<getitemdetails_Result>();
			return list = context1.getitemdetails(reqid).ToList();
		}

		public static void approvestatus(string id)
		{
			var k = from requisition_order in context1.requisition_order where requisition_order.requisition_id == id select requisition_order;
			k.FirstOrDefault().requisition_status = "Approved";
			context1.SaveChanges();
		}
		public static void rejectstatus(string id)
		{
			var k = from requisition_order in context1.requisition_order where requisition_order.requisition_id == id select requisition_order;
			k.FirstOrDefault().requisition_status = "Rejected";
			context1.SaveChanges();
		}
		public static List<getrequesthistory_Result> gethistory(string dept)
		{
			List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
			return list = context1.getrequesthistory(dept).ToList();
		}
		public static List<getrequesthistory_Result> gethistorybyname(string name,string dept)
		{
			List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
			list = (from requisitionorder in context1.requisition_order
					join employee in context1.employees on requisitionorder.employee_id equals employee.employee_id
					join requisitionorderdetails in context1.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
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
							  Name=x.Name,
							  status=x.status,
							  Sum=x.Sum,
						  }).ToList();
			return list;
		}
		public static List<getrequesthistory_Result> gethistorybynameandstatus(string name, string dept,string status)
		{
			List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
			list = (from requisitionorder in context1.requisition_order
					join employee in context1.employees on requisitionorder.employee_id equals employee.employee_id
					join requisitionorderdetails in context1.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
					where (employee.department_id.Equals(dept) && employee.employee_name.Contains(name)&& requisitionorder.requisition_status.Equals(status))
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

		public static List<getrequesthistory_Result> gethistorybystatus(string dept,string status)
		{
			List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
			list = (from requisitionorder in context1.requisition_order
					join employee in context1.employees on requisitionorder.employee_id equals employee.employee_id
					join requisitionorderdetails in context1.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
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

		public static List<employee> getemployeenames(string dept)
		{
			//var q=from employee in context1.employees where employee.department_id.Equals(dept) select employee.employee_name;
			return context1.employees.Where(x => x.department_id == dept).ToList();					

		}

		public static int getemployeeid(string name)
		{
			var q= from employee in context1.employees where employee.employee_name == name select employee.employee_id;
			return q.FirstOrDefault();
		}

		public static void updatetemporaryhead(int id,string dept)
		{
			var q = from department in context1.departments where department.department_id == dept select department;
			department d = q.FirstOrDefault();
			d.temp_head_id = id;
			context1.SaveChanges();
		}

		public static string gettemporaryheadname(string dept)
		{
			var q = from department in context1.departments
					join employee in context1.employees on
					department.temp_head_id equals employee.employee_id
					where department.department_id.Equals(dept)
					select employee.employee_name;
			return q.FirstOrDefault();

		}

		public static void revoketemporaryhead(string dept)
		{
			var q = from department in context1.departments where department.department_id == dept select department;
			department d = q.FirstOrDefault();
			d.temp_head_id = null;
			context1.SaveChanges();

		}

		public static List<employee> getemployeenamebysearch(string dept,string name)
		{
			var q = from e in context1.employees
					join d in context1.departments on e.department_id equals d.department_id
					where e.employee_name.Contains(name) &&e.department_id.Equals(dept)
					select e;
			return q.ToList();

		}

		public static List<getrepdetails_Result> getpreviousrepdetails(string dept)
		{
			List<getrepdetails_Result> list = new List<getrepdetails_Result>();
			list = context1.getrepdetails(dept).ToList();
			return list;

		}

		public static void saverepdetails(string dept, int id)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				var q = from department_rep in context1.department_rep
						where department_rep.department_id.Equals(dept) &&
						department_rep.representative_status.Equals("Active")
						select department_rep;
				department_rep d = q.FirstOrDefault();
				d.representative_status = "InActive";
				context1.SaveChanges();
				string today = DateTime.Now.ToString("yyyy-MM-dd");
				department_rep dr = new department_rep
				{
					department_id = dept,
					representative_id = id,
					appointed_date = DateTime.ParseExact(today, "yyyy-MM-dd", null),
					representative_status = "Active"
				};
				context1.department_rep.Add(dr);
				context1.SaveChanges();
				ts.Complete();
			}


	}
		public static void updatepassword(string dept, int password)
		{
			var q = from department in context1.departments where 
					department.department_id.Equals(dept) select department;
			department d = q.FirstOrDefault();
			d.department_pin = password;
			context1.SaveChanges();
		}

		public static List<collection> GetCollection()
		{
			var q = from collection c in context1.collections select c;
			return q.ToList();

		}

		public static void updatelocation(string dept,int id)
		{
			//var q=from collection_de

		}






	}
}