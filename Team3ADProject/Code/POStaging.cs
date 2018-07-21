using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;

namespace Team3ADProject.Code
{
    public class POStaging
    {
        private inventory item;
        private supplier supplier;
        private int orderedqty;
        private double unitprice;
        private DateTime dateRequired;
        private employee employee;

        public POStaging(inventory item, string supplier, int orderedqty, double unitprice, DateTime dateRequired, employee employee)
        {
            this.item = item;
            this.supplier = BusinessLogic.FindSupplierBySupplierID(supplier.Trim().ToLower());
            this.orderedqty = orderedqty;
            this.unitprice = unitprice;
            this.dateRequired = dateRequired;
            this.employee = employee;
        }
        public inventory Inventory
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }
        public supplier Supplier
        {
            get
            {
                return supplier;
            }
            set
            {
                supplier = value;
            }
        }
        public int OrderedQty
        {
            get
            {
                return orderedqty;
            }
            set
            {
                orderedqty = value;
            }
        }
        public double UnitPrice
        {
            get
            {
                return unitprice;
            }
            set
            {
                unitprice = value;
            }
        }
        public DateTime DateRequired
        {
            get
            {
                return dateRequired;
            }
            set
            {
                dateRequired = value;
            }
        }
        public employee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
            }
        }
    }
}