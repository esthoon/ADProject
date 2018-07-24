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

                    if (System.Web.Security.Roles.Enabled)
                    {
                        role = Roles.GetRolesForUser(first.user_id).FirstOrDefault();
                    }

                    else
                    {
                        role = Constants.ROLES_STORE_CLERK;
                    }

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
        // Returns a token if there is one for the user, null if there is none.
        // TODO: Add username and password verification, the service only fetches a token for now
        public WCF_Employee Login(string username, string password)
        {
            WCF_Employee wcfEmployee = null;

            // If login succeeds, fetch the token, otherwise, return null
            // TODO: Validate username and password
            var context = new LogicUniversityEntities();
            var query = from x in context.employees where x.user_id == username select x;
            var result = query.ToList();

            if (query.Any())
            {
                // Generate a token for the resulting employee.
                String token = GenerateToken();

                // Store token in database
                result.First().token = token;
                System.Diagnostics.Debug.WriteLine(context.SaveChanges());

                // Pass the token to the service consumer
                wcfEmployee = new WCF_Employee(0, "", "", username, "", 0, token, "");
            }
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

        public List<WCF_Requisition_Order> GetAllRequisitionByEmployee(string id)
        {
            int employeeId = Int32.Parse(id.Trim());
            List<requisition_order> list = BusinessLogic.GetAllRequisitionByEmployee(employeeId);
            List<WCF_Requisition_Order> returnlist = new List<WCF_Requisition_Order>();
            foreach(requisition_order a in list)
            {
                returnlist.Add(new WCF_Requisition_Order(a.requisition_id, a.employee_id, a.requisition_status, a.requisition_date, a.head_comment));
            }

            return returnlist;
        }
    }
}
