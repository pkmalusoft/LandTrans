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
    
    public partial class AcJournalDetailSelectByAcJournalID_Result
    {
        public int AcJournalDetailID { get; set; }
        public Nullable<int> AcHeadID { get; set; }
        public string Remarks { get; set; }
        public string AcHead { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> TaxPercent { get; set; }
        public Nullable<decimal> TaxAmount { get; set; }
        public bool AmountIncludingTax { get; set; }
    }
}
