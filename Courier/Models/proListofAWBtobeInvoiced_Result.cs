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
    
    public partial class proListofAWBtobeInvoiced_Result
    {
        public int ID { get; set; }
        public string AWBNO { get; set; }
        public decimal CourierCharge { get; set; }
        public string StatusPaymentMode { get; set; }
        public System.DateTime InScanDate { get; set; }
        public string Party { get; set; }
        public Nullable<double> StatedWeight { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string CourierService { get; set; }
        public int InscanID { get; set; }
        public string Pieces { get; set; }
        public string AWBType { get; set; }
        public string Consignor { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }
}
