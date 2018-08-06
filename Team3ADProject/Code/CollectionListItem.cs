using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team3ADProject.Code
{
    //JOEL START 
    // class used to keep track of items on the collectionlist.
    public class CollectionListItem
    {
        public string itemNum { get; set; }
        public string description { get; set; }
        public string uom { get; set; }
        public int qtyOrdered { get; set; }
        public int qtyAvailable { get; set; }
        public int qtyPrepared { get; set; }

        //Default con
        public CollectionListItem()
        {
        }

        public CollectionListItem(string itemNum, int qtyPrepared)
        {
            this.itemNum = itemNum;
            this.qtyPrepared = qtyPrepared;
        }

        public CollectionListItem(string itemNum, string description, string uom, int qtyOrdered, int qtyAvailable, int qtyPrepared)
        {
            this.itemNum = itemNum;
            this.description = description;
            this.uom = uom;
            this.qtyOrdered = qtyOrdered;
            this.qtyAvailable = qtyAvailable;
            this.qtyPrepared = qtyPrepared;
        }

    }
}