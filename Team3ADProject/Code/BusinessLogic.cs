using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;

namespace Team3ADProject.Code
{
    public class BusinessLogic
    {
        public static inventory GetInventory(string id)
        {
            LogicUniversityEntities model = new LogicUniversityEntities();
            return model.inventories.Where(i => i.item_number == id).ToList<inventory>()[0];
        }
        public static List<supplier_itemdetail> GetSupplier(string id)
        {
            LogicUniversityEntities model = new LogicUniversityEntities();
            return model.supplier_itemdetail.Where(i => i.item_number == id).OrderBy(i => i.priority).ToList<supplier_itemdetail>();
        }
    }
}