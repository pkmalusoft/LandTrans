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
    
    public partial class CustomerRateDet
    {
        public int CustomerRateDetID { get; set; }
        public int CustomerRateID { get; set; }
        public decimal AdditionalWeightFrom { get; set; }
        public decimal AdditionalWeightTo { get; set; }
        public decimal IncrementalWeight { get; set; }
        public decimal AdditionalRate { get; set; }
    
        public virtual CustomerRate CustomerRate { get; set; }
    }
}
