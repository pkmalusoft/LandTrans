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
    
    public partial class GetAllSalesInvoice_Result
    {
        public string DocumentNo { get; set; }
        public int SalesInvoiceID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public Nullable<decimal> OtherCharges { get; set; }
        public string InvoiceNo { get; set; }
        public string EmployeeName { get; set; }
        public string SalesMan { get; set; }
        public string CustomerName { get; set; }
        public Nullable<decimal> NetTotal { get; set; }
    }
}
