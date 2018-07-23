using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using Team3ADProject.Model;

namespace Team3ADProject.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AndroidService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AndroidService.svc or AndroidService.svc.cs at the Solution Explorer and start debugging.
    public class AndroidService : IAndroidService
    {
        // Fetches an employee by using a token.
        public WCF_Employee GetEmployeeByToken(String token)
        {
            {
                var context = new LogicUniversityEntities();
                var query = context.getUserByToken(token);

                // If there exists the token for a user, create an employee and return it
                if (query.Count() != 0)
                {
                    var first = query.First();
                    return new WCF_Employee(first.employee_id, first.employee_name, first.email_id, first.user_id, first.department_id, first.supervisor_id, first.token);
                }

                // Otherwise, return null
                else
                {
                    return null;
                }
            }
        }

        public string Hello(string token)
        {
            var context = new LogicUniversityEntities();
            var query = context.getUserByToken(token);

            // If the token exists for a user, return hello
            if (query.Count() != 0)
            {
                return "hello!";
            }

            // Otherwise, return not signed in.
            else
            {
                return "You are not signed in!";
            }
        }

        // Takes username and password in
        // Returns a token if there is one for the user, null if there is none.
        // TODO: Add username and password verification, the service only fetches a token for now
        // TODO: Generate a new token on successful login
        public WCF_Employee Login(string username, string password)
        {
            WCF_Employee wcfEmployee = null;
            
            // If login succeeds, fetch the token, otherwise, return null
            // if(Membership.ValidateUser(username, password))
            // {
                // TODO: Generate and return token if correct
                var context = new LogicUniversityEntities();
                var query = context.getUserTokenByUsername(username);
                var result = query.ToList();

                if (result.Count() != 0)
                {
                    wcfEmployee = new WCF_Employee(0, null, null, null, null, null, result.First().token);
                }
            //}

            return wcfEmployee;
        }

        // Query for the token, and set it to null
        public string Logout(string token)
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.employees where x.token == token select x;

            var result = query.First();
            result.token = null;

            context.SaveChanges();

            return "done";
        }
    }
}
