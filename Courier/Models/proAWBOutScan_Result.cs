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
    
    public partial class proAWBOutScan_Result
    {
        public string AWBNo { get; set; }
        public System.DateTime DRSDate { get; set; }
        public string Pieces { get; set; }
        public Nullable<double> StatedWeight { get; set; }
        public Nullable<decimal> CourierCharge { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string CourierDescription { get; set; }
        public string MovementType { get; set; }
        public string Delivered { get; set; }
        public Nullable<int> AcCompanyID { get; set; }
        public string Consignee { get; set; }
        public string Consignor { get; set; }
        public Nullable<int> ConsigneeCountryID { get; set; }
        public string StatusPaymentmode { get; set; }
        public string RegistrationNo { get; set; }
        public string DRSNo { get; set; }
        public Nullable<int> DeliveredBy { get; set; }
        public string remarks { get; set; }
        public string StatusAWB { get; set; }
    }
}
