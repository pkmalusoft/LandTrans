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
    
    public partial class GetCustomerInvoiceReport_Result
    {
        public string CustomerInvoiceNo { get; set; }
        public System.DateTime Date { get; set; }
        public string AWBNo { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> OtherCharge { get; set; }
        public string Phone { get; set; }
        public string Source { get; set; }
        public string CountryName { get; set; }
        public string Consignee { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Destination { get; set; }
        public string CourierDescription { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Origin { get; set; }
        public string Location { get; set; }
    }
}
