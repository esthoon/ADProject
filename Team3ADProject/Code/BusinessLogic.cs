using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;

namespace Team3ADProject.Code
{
    public class BusinessLogic
    {
        // Fetches a list of requisition orders from the database
        public static List<requisition_order> GetRequisitionOrders()
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.requisition_order select x;
            return query.ToList();
        }

        // Fetches a list of requisition orders from the database, filtered by most recent.
        public static List<requisition_order> GetRecentRequisitionOrders(int length)
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.requisition_order orderby x.requisition_date select x;
            query.OrderByDescending(x => x.requisition_date);

            return query.Take(length).ToList();
        }


    }
}