using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;

namespace Team3ADProject.Code
{
    public class cart
    {
        private inventory inventory = new inventory();
        private int quantity;
        private double up;
        private double itemprice;

        public inventory Inventory { get => inventory; set => inventory = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Up { get => up; set => up = value; }
        public double Itemprice { get => itemprice; set => itemprice = value; }

        public cart()
        { }

        public cart(inventory i, int q)
        {
            this.Inventory = i;
            this.Up = BusinessLogic.GetItemUnitPrice(i.item_number);
            this.Quantity = q;
            this.Itemprice = q * Up;
        }


    }
}