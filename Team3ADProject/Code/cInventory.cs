using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Code;

namespace Team3ADProject.Model
{
    public class cInventory
    {
        private inventory inventory;
        private int orderedQty;
        private int pendingApprovalQty;
        private int pendingMinusAdjustmentQty;
        private int pendingPlusAdjustmentQty;
        private int reorderQty;

        public cInventory(inventory item)
        {
            this.inventory = item;
            this.orderedQty = BusinessLogic.ReturnPendingPOqtyByStatus(item, "pending");
            this.pendingApprovalQty = BusinessLogic.ReturnPendingPOqtyByStatus(item, "awaiting approval");
            this.pendingMinusAdjustmentQty = BusinessLogic.ReturnPendingMinusAdjustmentQty(item.item_number);
            this.pendingPlusAdjustmentQty = BusinessLogic.ReturnPendingPlusAdjustmentQty(item.item_number);
            this.reorderQty = ReorderQuantity(item);
        }

        public inventory Inventory
        {
            get
            {
                return this.inventory;
            }
            set
            {
                this.inventory = value;
            }
        }

        public int OrderedQty
        {
            get
            {
                return this.orderedQty;
            }
            set
            {
                this.orderedQty = value;
            }
        }

        public int PendingApprovalQty
        {
            get
            {
                return this.pendingApprovalQty;
            }
            set
            {
                this.pendingApprovalQty = value;
            }
        }
        public int PendingMinusAdjustmentQty
        {
            get
            {
                return this.pendingMinusAdjustmentQty;
            }
            set
            {
                this.pendingMinusAdjustmentQty = value;
            }
        }
        public int PendingPlusAdjustmentQty
        {
            get
            {
                return this.pendingPlusAdjustmentQty;
            }
            set
            {
                this.pendingPlusAdjustmentQty = value;
            }
        }
        public int reorder_quantity
        {
            get
            {
                return this.reorderQty;
            }
            set
            {
                this.reorderQty = value;
            }
        }
        protected int ReorderQuantity(inventory item)
        {
            int x = item.reorder_level - item.current_quantity;
            if (x > 0)
            {
                return x;
            }
            else
            {
                return 0;
            }
        }
    }
}