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
    
    public partial class OtherChargeInvoice
    {
        public int OtherChargeInvoiceID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string DocumentNo { get; set; }
        public Nullable<System.DateTime> Transdate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> AcJournalID { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> FuelPer { get; set; }
        public Nullable<decimal> AdminPer { get; set; }
        public Nullable<decimal> FuelAmt { get; set; }
        public Nullable<decimal> AdminAmt { get; set; }
        public Nullable<decimal> TotAWBAmt { get; set; }
        public Nullable<int> Accompanyid { get; set; }
    }
}
