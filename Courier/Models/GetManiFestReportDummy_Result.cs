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
    
    public partial class GetManiFestReportDummy_Result
    {
        public string AWBNO { get; set; }
        public string AWBDate { get; set; }
        public string Shipeer { get; set; }
        public string Consignee { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Pieces { get; set; }
        public Nullable<double> StatedWeight { get; set; }
        public string CargoDescription { get; set; }
        public decimal CourierCharge { get; set; }
        public Nullable<decimal> OtherCharge { get; set; }
    }
}
