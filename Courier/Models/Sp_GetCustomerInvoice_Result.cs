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
    
    public partial class Sp_GetCustomerInvoice_Result
    {
        public string AWBNo { get; set; }
        public System.DateTime InScanDate { get; set; }
        public string Party { get; set; }
        public string ORIGIN { get; set; }
        public string Destination { get; set; }
        public Nullable<double> StatedWeight { get; set; }
        public Nullable<decimal> CourierCharge { get; set; }
        public Nullable<decimal> OtherCharge { get; set; }
        public string StatusPaymentMode { get; set; }
        public Nullable<int> TaxPercentage { get; set; }
    }
}
