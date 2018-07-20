using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Team3ADProject.Model;
using System.Transactions;

namespace Team3ADProject.Code
{
    
    public class BusinessLogic
    {
		public static LogicUniversityEntities context = new LogicUniversityEntities();

		public static List<getpendingrequestsbydepartment_Result> ViewPendingRequests(string deptid)
		{
			List<getpendingrequestsbydepartment_Result> list = new List<getpendingrequestsbydepartment_Result>();
			return list = context.getpendingrequestsbydepartment(deptid).ToList();
		}

		public static string getdepartment(string userid)
		{
			var k = (from employee in context.employees where employee.user_id == userid select employee);
			 string dept = k.FirstOrDefault().department_id;
			return dept;
		}

		public static getpendingrequestdetails_Result getdetails(string id)
		{
			return context.getpendingrequestdetails(id).ToList().Single();
		}

		public static List<getitemdetails_Result> pendinggetitemdetails(string reqid)
		{
			List<getitemdetails_Result> list = new List<getitemdetails_Result>();
			return list = context.getitemdetails(reqid).ToList();
		}

		public static void approvestatus(string id)
		{
			var k = from requisition_order in context.requisition_order where requisition_order.requisition_id == id select requisition_order;
			k.FirstOrDefault().requisition_status = "Approved";
			context.SaveChanges();
		}
		public static void rejectstatus(string id)
		{
			var k = from requisition_order in context.requisition_order where requisition_order.requisition_id == id select requisition_order;
			k.FirstOrDefault().requisition_status = "Rejected";
			context.SaveChanges();
		}
		public static List<getrequesthistory_Result> gethistory(string dept)
		{
			List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
			return list = context.getrequesthistory(dept).ToList();
		}
		public static List<getrequesthistory_Result> gethistorybyname(string name,string dept)
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
							  Name=x.Name,
							  status=x.status,
							  Sum=x.Sum,
						  }).ToList();
			return list;
		}
		public static List<getrequesthistory_Result> gethistorybynameandstatus(string name, string dept,string status)
		{
			List<getrequesthistory_Result> list = new List<getrequesthistory_Result>();
			list = (from requisitionorder in context.requisition_order
					join employee in context.employees on requisitionorder.employee_id equals employee.employee_id
					join requisitionorderdetails in context.requisition_order_detail on requisitionorder.requisition_id equals requisitionorderdetails.requisition_id
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

		public static List<employee> getemployeenames(string dept)
		{
			//var q=from employee in context.employees where employee.department_id.Equals(dept) select employee.employee_name;
			return context.employees.Where(x => x.department_id == dept).ToList();					

		}

		public static int getemployeeid(string name)
		{
			var q= from employee in context.employees where employee.employee_name == name select employee.employee_id;
			return q.FirstOrDefault();
		}

		public static void updatetemporaryhead(int id,string dept)
		{
			var q = from department in context.departments where department.department_id == dept select department;
			department d = q.FirstOrDefault();
			d.temp_head_id = id;
			context.SaveChanges();
		}

		public static string gettemporaryheadname(string dept)
		{
			var q = from department in context.departments
					join employee in context.employees on
					department.temp_head_id equals employee.employee_id
					where department.department_id.Equals(dept)
					select employee.employee_name;
			return q.FirstOrDefault();

		}

		public static void revoketemporaryhead(string dept)
		{
			var q = from department in context.departments where department.department_id == dept select department;
			department d = q.FirstOrDefault();
			d.temp_head_id = null;
			context.SaveChanges();

		}

		public static List<employee> getemployeenamebysearch(string dept,string name)
		{
			var q = from e in context.employees
					join d in context.departments on e.department_id equals d.department_id
					where e.employee_name.Contains(name) &&e.department_id.Equals(dept)
					select e;
			return q.ToList();

		}

		public static List<getrepdetails_Result> getpreviousrepdetails(string dept)
		{
			List<getrepdetails_Result> list = new List<getrepdetails_Result>();
			list = context.getrepdetails(dept).ToList();
			return list;

		}

		public static void saverepdetails(string dept, int id)
		{
			using (TransactionScope ts = new TransactionScope())
			{
				var q = from department_rep in context.department_rep
						where department_rep.department_id.Equals(dept) &&
						department_rep.representative_status.Equals("Active")
						select department_rep;
				department_rep d = q.FirstOrDefault();
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
		public static void updatepassword(string dept, int password)
		{
			var q = from department in context.departments where 
					department.department_id.Equals(dept) select department;
			department d = q.FirstOrDefault();
			d.department_pin = password;
			context.SaveChanges();
		}

		public static List<collection> GetCollection()
		{
			var q = from collection c in context.collections select c;
			return q.ToList();

		}

		public static void updatelocation(string dept,int id)
		{
			//var q=from collection_de

		}

        public static List<spGetCollectionList_Result> GetCollectionList()
        {
            List<spGetCollectionList_Result> list = new List<spGetCollectionList_Result>();
            return list = context.spGetCollectionList().ToList();
        }

        public static void DeductFromInventory(List<CollectionListItem> list)
        {
            foreach (var item in list)
            {
                inventory i = context.inventories.Where(x => x.item_number == item.itemNum).First();
                i.current_quantity -= item.qtyPrepared;
                context.SaveChanges();
            }
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


        //List all adjustment form
        public static List<adjustment> GetAdjustment()
        {
            return context.adjustments.Where(x => x.adjustment_status == "pending" && x.adjustment_price <= 250).ToList();
        }
        //Update adjustment form
        public static void Updateadj(int id, string comment)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Approved";
            adj.manager_remark = comment;
            context.SaveChanges();
        }
        public static void rejectAdj(int id, string comment)
        {
            adjustment adj = context.adjustments.Where(x => x.adjustment_id == id).First<adjustment>();
            adj.adjustment_status = "Rejected";
            adj.manager_remark = comment;
            context.SaveChanges();
        }
        public static List<adjustment> Search(DateTime date)
        {
            return context.adjustments.Where(x => x.adjustment_date == date).ToList<adjustment>();
        }       
        //List purchase order
        public static List<purchase_order> GetPurchaseOrders()
        {
            return context.purchase_order.OrderBy(x=>x.suppler_id).Where(x => x.purchase_order_status == "awaiting approval").ToList();
        }
        public static inventory GetInventory(string id)
        {
            return context.inventories.Where(i => i.item_number == id).ToList<inventory>()[0];
        }

        public static System.Collections.IEnumerable GetSupplier(string id)
       //     public static List<(string supplier_name, double unit_price)> GetSupplier(string id)
        {
            var nestedQuery = from s in context.suppliers
                              from sid in s.supplier_itemdetail
                              from i in context.inventories
                              where (sid.item_number == id && i.item_number ==id)
                              orderby (sid.priority)
                              select new { s.supplier_name, sid.unit_price , i.description};
            return nestedQuery.ToList();                
            //return context.supplier_itemdetail.Where(i => i.item_number == id).OrderBy(i => i.priority).ToList<supplier_itemdetail>();
        }
        public static List<inventory> GetActiveInventories()
        {
            return context.inventories.Where(x => x.item_status.ToLower() == "active").ToList();
        }
        public static List<inventory> GetAllInventories()
        {
            return context.inventories.ToList();
        }
        public static List<supplier> GetActiveSuppliers()
        {
            return context.suppliers.Distinct().Where(s => s.supplier_status.ToLower() == "active").ToList();
        }
        public static List<string> GetCategories()
        {
            return context.inventories.OrderBy(x=>x.category).Select(x=> x.category).Distinct().ToList();
        }
        public static int ReturnPendingPOqtyByStatus(inventory item, string status)
        {
            var q = context.purchase_order_detail.Where(x => x.item_purchase_order_status.ToLower().Trim() == "pending" 
            && x.purchase_order.purchase_order_status.ToLower().Trim() == status);
            int qty = 0;
            foreach(var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.item_number.ToLower().Trim()))
                {
                    qty += a.item_purchase_order_quantity;
                }
            }
            return qty; 
        }
        public static int ReturnPendingAdjustmentQty(inventory item)
        {
            var q = context.adjustments.Where(x=>x.adjustment_status.ToLower().Trim() == "pending");
            int qty = 0;
            foreach(var a in q)
            {
                if (a.item_number.ToLower().Trim().Equals(item.item_number.ToLower().Trim()))
                {
                    qty += a.adjustment_quantity;
                }
            }
            return qty;
        }
        public static List<inventory> GetInventoriesByCategory(string category)
        {
            return context.inventories.Where(x => x.category.Trim().ToLower() == category.Trim().ToLower()).ToList();
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

        //ViewRO
        public static int GetPlaceIdFromDptId(string dptId)
        {
            spGetPlaceIdFromDptId_Result result = context.spGetPlaceIdFromDptId(dptId).FirstOrDefault();
            return (int)result.place_id;
        }

        //ViewRO
        public static void SpecialRequestReadyUpdates(int placeId, DateTime collectionDate, string collectionStatus, string ro_id)
        {
            context.spSpecialRequestReady(placeId, collectionDate, collectionStatus, ro_id);
        }

    }
}