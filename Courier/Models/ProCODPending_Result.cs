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
    
    public partial class ProCODPending_Result
    {
        public string AWBNO { get; set; }
        public Nullable<System.DateTime> InScanDate { get; set; }
        public string Consignor { get; set; }
        public string Consignee { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Nullable<decimal> OriginalAmt { get; set; }
        public Nullable<decimal> RecvdAmt { get; set; }
    }
}
