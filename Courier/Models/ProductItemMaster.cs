//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LTMSV2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductItemMaster
    {
        public int ProductItemID { get; set; }
        public string ProductItemName { get; set; }
        public Nullable<int> ProductCategoryID { get; set; }
        public Nullable<decimal> MinimumCharges { get; set; }
    
        public virtual ProductCategoryMaster ProductCategoryMaster { get; set; }
    }
}
