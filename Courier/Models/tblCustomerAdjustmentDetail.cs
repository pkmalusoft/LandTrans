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
    
    public partial class tblCustomerAdjustmentDetail
    {
        public int ID { get; set; }
        public Nullable<int> CustomerAdjustmentID { get; set; }
        public Nullable<int> AcHeadID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ReferenceNo { get; set; }
        public string Remarks { get; set; }
    
        public virtual tblCustomerAdjustment tblCustomerAdjustment { get; set; }
    }
}
