using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;

namespace Team3ADProject.Code
{
    public class BusinessLogic
    {
        static LogicUniversityEntities context = new LogicUniversityEntities();

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

    }
}