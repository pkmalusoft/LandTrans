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
    
    public partial class InscanOtherCharge
    {
        public int InscanOtherChargeID { get; set; }
        public Nullable<int> OtherChargeID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> InscanID { get; set; }
        public Nullable<decimal> TaxPercentage { get; set; }
        public bool Reimbursement { get; set; }
    
        public virtual OtherCharge OtherCharge { get; set; }
    }
}
