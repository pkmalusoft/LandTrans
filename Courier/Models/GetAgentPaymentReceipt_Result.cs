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
    
    public partial class GetAgentPaymentReceipt_Result
    {
        public int ID { get; set; }
        public string VoucherNo { get; set; }
        public Nullable<System.DateTime> RecPayDate { get; set; }
        public string Agent { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
