using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using Team3ADProject.Model;
using Team3ADProject.Code;

namespace Team3ADProject.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AndroidService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AndroidService.svc or AndroidService.svc.cs at the Solution Explorer and start debugging.
    public class AndroidService : IAndroidService
    {
        // Template for working with Android services
        public string Hello(string token)
        {
            // If token is valid, do stuff
            if (AuthenticateToken(token))
            {
                return "hello!";
            }

            // If token is invalid, return null
            else
            {
                return null;
            }
        }

        /* Token methods ===========================*/


        // Authenticates a token
        // Returns true if token exists in employee table
        // Returns false if token doesn't exist in employee table
        protected bool AuthenticateToken(String token)
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.employees where x.token == token select x;

            if (query.Count() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Generates a token.
        // This token is unique, and contains the time created in the token itself.
        // To get the time created of the token, use the GetTokenCreation time method.
        protected string GenerateToken()
        {
            string key = Guid.NewGuid().ToString();
            string token = key;

            return token;
        }


        /* Service methods =========================================*/

        // Fetches an employee by using a token.
        public WCF_Employee GetEmployeeByToken(String token)
        {
            if (AuthenticateToken(token))
            {
                var context = new LogicUniversityEntities();
                var query = from x in context.employees where x.token == token select x;

                // If there exists the token for a user, create a wcf employee and return it
                if (query.Count() != 0)
                {
                    var first = query.First();
                    String role = null;

                    role = Roles.GetRolesForUser(first.user_id).FirstOrDefault();

                    return new WCF_Employee(first.employee_id, first.employee_name, first.email_id, first.user_id, first.department_id, first.supervisor_id, first.token, role);
                }

                else
                {
                    return null;
                }
            }
            return null;
        }

        // Takes username and password in
        // Returns a token and employee data if there is one for the user, null if there is none.
        public WCF_Employee Login(string username, string password)
        {
            WCF_Employee wcfEmployee = null;

            // If login succeeds, fetch the token, otherwise, return null
            // Validate username and password
            if (Membership.ValidateUser(username, password))
            {
                // Fetch or generate token
                var context = new LogicUniversityEntities();
                var query = from x in context.employees where x.user_id == username select x;
                var result = query.ToList();

                if (query.Any())
                {
                    // Generate a token for the resulting employee.
                    String token = GenerateToken();

                    // Store token in database
                    var first = result.First();
                    first.token = token;
                    System.Diagnostics.Debug.WriteLine(context.SaveChanges());

                    // Pass the token to the service consumer
                    wcfEmployee = new WCF_Employee(first.employee_id, first.employee_name, first.email_id, username, first.department_id, first.supervisor_id, token, Roles.GetRolesForUser(username).FirstOrDefault());
                }
            }

            // Return the token to user
            return wcfEmployee;
        }

        // Query with the token, and set it to null
        public string Logout(string token)
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.employees where x.token == token select x;

            var result = query.First();
            result.token = null;

            context.SaveChanges();

            return "done";
        }



        //JOEL START

        //CollectionList
        public List<WCF_CollectionItem> getCollectionList()
        {
            List<WCF_CollectionItem> wcfList = new List<WCF_CollectionItem>();

            var result = BusinessLogic.GetCollectionList();

            foreach (var i in result)
            {
                wcfList.Add(new WCF_CollectionItem(i.item_number.Trim(), i.description.Trim(), (int)i.quantity_ordered, i.current_quantity, i.unit_of_measurement.Trim()));
            }

            return wcfList;
        }

        //CollectionList
        public void SortCollectedGoods(WCF_CollectionItem ci)
        {
            List<CollectionListItem> allDptCollectionList = new List<CollectionListItem>();
            allDptCollectionList.Add(new CollectionListItem(ci.ItemNumber.Trim(), ci.Description.Trim(), ci.UnitOfMeasurement.Trim(), ci.QuantityOrdered, ci.CurrentInventoryQty, ci.CollectedQty));

            BusinessLogic.SortCollectedGoods(allDptCollectionList);
        }

        //CollectionList
        public void DeductFromInventory(WCF_CollectionItem ci)
        {
            List<CollectionListItem> allDptCollectionList = new List<CollectionListItem>();
            allDptCollectionList.Add(new CollectionListItem(ci.ItemNumber.Trim(), ci.Description.Trim(), ci.UnitOfMeasurement.Trim(), ci.QuantityOrdered, ci.CurrentInventoryQty, ci.CollectedQty));

            BusinessLogic.DeductFromInventory(allDptCollectionList);
        }



        //Disbursement Sorting
        public List<WCF_DepartmentList> DisplayListofDepartmentsForCollection()
        {
            List<WCF_DepartmentList> wcfList = new List<WCF_DepartmentList>();
            var result = BusinessLogic.DisplayListofDepartmentsForCollection();

            foreach (var i in result)
            {
                wcfList.Add(new WCF_DepartmentList(i.ToString().Trim()));
            }
            return wcfList;
        }

        //Disbursement Sorting
        public string GetDptIdFromDptName(string dptName)
        {
            string dptID;
            return dptID = BusinessLogic.GetDptIdFromDptName(dptName.Trim()).Trim();
        }

        //Disbursement Sorting
        public List<WCF_SortingItem> GetSortingListByDepartment(string dpt_Id)
        {
            List<WCF_SortingItem> wcfList = new List<WCF_SortingItem>();
            var result = BusinessLogic.GetSortingListByDepartment(dpt_Id);

            foreach (var i in result)
            {
                wcfList.Add(new WCF_SortingItem(i.item_number.Trim(), i.description.Trim(), (int)i.required_qty, (int)i.supply_qty, (int)i.item_pending_quantity));
            }

            return wcfList;
        }

        //Disbursement Sorting
        public int GetPlaceIdFromDptId(string dptId)
        {
            int placeId;
            return placeId = BusinessLogic.GetPlaceIdFromDptId(dptId);
        }

        //Disbursement Sorting
        public void InsertCollectionDetailsRow(WCF_CollectionDetail cd)
        {
            BusinessLogic.InsertCollectionDetailsRow(cd.PlaceId, DateTime.Parse(cd.CollectionDate), cd.DepartmentId);
        }

        //Disbursement Sorting
        public void InsertDisbursementListROId(string dptId)
        {
            BusinessLogic.InsertDisbursementListROId(dptId);
        }

        //ViewRO
        public List<WCF_CollectionItem> GetRODetailsByROId(string roId)
        {
            List<WCF_CollectionItem> wcfList = new List<WCF_CollectionItem>();

            var result = BusinessLogic.GetRODetailsByROId(roId);
            foreach (var i in result)
            {
                wcfList.Add(new WCF_CollectionItem(i.requisition_id.Trim(), i.item_number.Trim(), i.description.Trim(), i.unit_of_measurement.Trim(), (int)i.item_requisition_quantity, (int)i.current_quantity, (int)i.item_pending_quantity));
            }

            return wcfList;
        }

        //ViewRO
        public void SpecialRequestReadyUpdatesCDRDD(WCF_CollectionDetail cd)
        {
            BusinessLogic.SpecialRequestReadyUpdatesCDRDD(cd.PlaceId, DateTime.Parse(cd.CollectionDate), cd.RoId.Trim(), cd.DepartmentId.Trim());
        }

        //ViewRO
        public void ViewROSpecialRequestUpdateRODTable(WCF_CollectionItem ci)
        {
            List<CollectionListItem> clList = new List<CollectionListItem>();
            clList.Add(new CollectionListItem(ci.ItemNumber.Trim(), ci.Description.Trim(), ci.UnitOfMeasurement.Trim(), ci.QuantityOrdered, ci.CurrentInventoryQty, ci.CollectedQty));
            string roid = ci.RequisitionId.Trim();

            BusinessLogic.ViewROSpecialRequestUpdateRODTable(clList, roid);

        }


        //JOEL END

        //Tharrani - start
        //return active inventory list
        public List<WCF_Inventory> GetActiveInventory(string token)
        {
            if (AuthenticateToken(token))
            {
                List<inventory> i = BusinessLogic.GetActiveInventory();
                List<WCF_Inventory> list = new List<WCF_Inventory>();
                foreach (inventory x in i)
                {
                    list.Add(new WCF_Inventory(x.item_number.Trim(), x.description.Trim(), x.category.Trim(), x.unit_of_measurement.Trim(), x.current_quantity.ToString(), x.reorder_level.ToString(), x.reorder_quantity.ToString(), x.item_bin.Trim(), x.item_status.Trim()));
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        //return inventory matching search criteria
        public List<WCF_Inventory> SearchInventory(string token, string search)
        {
            if (AuthenticateToken(token))
            {
                List<WCF_Inventory> list = new List<WCF_Inventory>();
                List<inventory> i = BusinessLogic.SearchActiveInventory(search);
                foreach (inventory x in i)
                {
                    list.Add(new WCF_Inventory(x.item_number.Trim(), x.description.Trim(), x.category.Trim(), x.unit_of_measurement.Trim(), x.current_quantity.ToString(), x.reorder_level.ToString(), x.reorder_quantity.ToString(), x.item_bin.Trim(), x.item_status.Trim()));
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public WCF_Inventory GetSelectedInventory(string token, string id)
        {
            if (AuthenticateToken(token))
            {
                inventory x = BusinessLogic.GetInventoryById(id);
                return new WCF_Inventory(x.item_number.Trim(), x.description.Trim(), x.category.Trim(), x.unit_of_measurement.Trim(), x.current_quantity.ToString(), x.reorder_level.ToString(), x.reorder_quantity.ToString(), x.item_bin.Trim(), x.item_status.Trim());
            }
            else
            {
                return null;
            }
        }

        //Add new requisition order
        public string AddNewRequest(WCF_Token token)
        {
            string x = token.gettoken.Replace(@"\", "").Trim();
            if (AuthenticateToken(x))
            {
                WCF_Employee emp = GetEmployeeByToken(x);
                int emp_id = Convert.ToInt32(emp.EmployeeId); //16;
                string Depid = emp.DepartmentId.Trim(); //ENGL;
                DateTime d = DateTime.Now.Date;
                unique_id u = BusinessLogic.getlastrequestid(Depid);
                int i = (int)u.req_id + 1;
                string id = Depid + "/" + DateTime.Now.Year.ToString() + "/" + i;
                BusinessLogic.AddNewRequisitionOrder(id, emp_id, d);
                BusinessLogic.updatelastrequestid(Depid, i);
                return id;
            }
            else
                return null;
        }

        //Add new requisition order detail
        public void AddNewRequestDetail(WCF_ReqCart r)
        {
            string token = r.gettoken.Replace(@"\", "").Trim();
            if (AuthenticateToken(token))
            {
                int q = r.q;
                string id = r.Id.Replace(@"\", "");
                inventory inv = BusinessLogic.GetInventoryById(r.getI.Trim()); ;
                cart c = new cart(inv, q);
                BusinessLogic.AddRequisitionOrderDetail(c, id.Trim());
            }
        }

        public WCF_Requisition_Order GetRequestOrder(string token, string id)
        {
            if (AuthenticateToken(token))
            {
                //string request = id.Substring(0, 5) + "/" + id.Substring(6, 10) + "/" + id.Substring(11);
                string requestid = id.Replace(@"\", "");
                requisition_order r = BusinessLogic.GetRequisitionOrderById(requestid.Trim());
                WCF_Requisition_Order ro = new WCF_Requisition_Order(r.requisition_id, r.employee_id, r.requisition_status, r.requisition_date, r.head_comment);
                return ro;
            }

            else
                return null;
        }

        public List<Employee_Request_order_Detail> GetRequestDetail(string token, string id)
        {
            if (AuthenticateToken(token))
            {
                string requestid = id.Replace(@"\", "");
                List<getRequisitionOrderDetails_Result> rd = BusinessLogic.GetRequisitionorderDetail(requestid);
                List<Employee_Request_order_Detail> rod = new List<Employee_Request_order_Detail>();
                for (int i = 0; i < rd.Count; i++)
                {
                    rod.Add(new Employee_Request_order_Detail(rd[i].category.Trim(), rd[i].description.Trim(), rd[i].unit_of_measurement.Trim(), Convert.ToString(rd[i].item_requisition_quantity).Trim()));
                }
                return rod;
            }
            else
            { return null; }
        }
        //Tharrani – End

        //Esther
        public string CreateAdjustment(String token, WCF_Adjustment adj)
        {
            if (AuthenticateToken(token))
            {

                String now = DateTime.Now.ToString("yyyy-MM-dd");
                WCF_Employee employee = GetEmployeeByToken(token);
                adjustment a = new adjustment()
                {
                    adjustment_date = DateTime.ParseExact(now, "yyyy-MM-dd", null),
                    employee_id = employee.EmployeeId,
                    item_number = adj.ItemNumber.Trim(),
                    adjustment_quantity = Int32.Parse(adj.AdjustmentQty.Trim()),
                    adjustment_price = BusinessLogic.Adjprice(adj.ItemNumber) * Int32.Parse(adj.AdjustmentQty.Trim()),
                    adjustment_status = "Pending",
                    employee_remark = adj.EmployeeRemark,
                    manager_remark = null,
                };
                String result1= BusinessLogic.CreateAdjustment(a);
                String email = BusinessLogic.SendEmailAdjustmentApproval(a);
                try
                {
                    BusinessLogic.sendMail(email, "New Adjustment Request", employee.EmployeeName+" raised new adjustment request.");
                }
                catch(Exception ex)
                {
                    return "email exception "+ex.Message;   
                }
                return result1;
            }
            else
            {
                return "invalid token";
            }
        }

        public List<WCF_Inventory> GetActiveInventories(String token)
        {
            if (AuthenticateToken(token))
            {
                List<inventory> list = BusinessLogic.GetActiveInventories();
                List<WCF_Inventory> returnlist = new List<WCF_Inventory>();
                foreach (inventory a in list)
                {
                    int pqty = BusinessLogic.ReturnPendingMinusAdjustmentQty(a.item_number);
                    returnlist.Add(new WCF_Inventory(a.item_number.Trim(), a.description.Trim(), a.category.Trim(), a.unit_of_measurement.Trim(), a.current_quantity.ToString(), a.reorder_level.ToString(), a.reorder_quantity.ToString(), a.item_bin, a.item_status, pqty.ToString()));
                }
                return returnlist;
            }
            else
            {
                return null;
            }
        }

        public List<WCF_Inventory> GetInventorySearchResult(String search, String token)
        {
            if (AuthenticateToken(token))
            {
                List<inventory> list = BusinessLogic.GetActiveInventories().Where(x => x.description.ToLower().Contains(search.ToLower())).ToList();
                List<WCF_Inventory> returnlist = new List<WCF_Inventory>();
                foreach (inventory a in list)
                {
                    int pqty = BusinessLogic.ReturnPendingMinusAdjustmentQty(a.item_number);
                    returnlist.Add(new WCF_Inventory(a.item_number.Trim(), a.description.Trim(), a.category.Trim(), a.unit_of_measurement.Trim(), a.current_quantity.ToString(), a.reorder_level.ToString(), a.reorder_quantity.ToString(), a.item_bin, a.item_status, pqty.ToString()));
                }
                return returnlist;
            }
            else
            {
                return null;
            }
        }

        public WCF_Inventory GetInventoryByItemCode(String itemcode, String token)
        {
            if (AuthenticateToken(token))
            {
                inventory a = BusinessLogic.GetInventoryByItemCode(itemcode);
                int pqty = BusinessLogic.ReturnPendingMinusAdjustmentQty(itemcode);
                WCF_Inventory wcfinv = new WCF_Inventory(a.item_number.Trim(), a.description.Trim(), a.category.Trim(), a.unit_of_measurement.Trim(), a.current_quantity.ToString(), a.reorder_level.ToString(), a.reorder_quantity.ToString(), a.item_bin, a.item_status, pqty.ToString());
                return wcfinv;
            }
            else
            {
                return null;
            }
        }
        //Esther end
    }
}


