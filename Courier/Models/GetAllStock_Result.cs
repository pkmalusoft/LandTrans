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
    
    public partial class GetAllStock_Result
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<int> TotalQuantity { get; set; }
        public string FromAWBNo { get; set; }
        public string ToAWBNo { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ReferenceNo { get; set; }
        public string Supplier { get; set; }
    }
}
