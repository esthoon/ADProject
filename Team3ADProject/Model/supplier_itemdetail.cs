//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Team3ADProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class supplier_itemdetail
    {
        public string supplier_id { get; set; }
        public string item_number { get; set; }
        public int priority { get; set; }
        public double unit_price { get; set; }
    
        public virtual inventory inventory { get; set; }
        public virtual supplier supplier { get; set; }
    }
}
