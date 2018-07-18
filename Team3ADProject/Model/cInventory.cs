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
        private int pendingAdjustmentQty;

        public cInventory(inventory item)
        {
            this.inventory = item;
            this.orderedQty = BusinessLogic.ReturnPendingPOqtyByStatus(item, "pending");
            this.pendingApprovalQty = BusinessLogic.ReturnPendingPOqtyByStatus(item, "awaiting approval");
            this.pendingAdjustmentQty = BusinessLogic.ReturnPendingAdjustmentQty(item);
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
        public int PendingAdjustmentQty
        {
            get
            {
                return this.pendingAdjustmentQty;
            }
            set
            {
                this.pendingAdjustmentQty = value;
            }
        }
    }
}