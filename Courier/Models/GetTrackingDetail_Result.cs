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
    
    public partial class GetTrackingDetail_Result
    {
        public string AWBNo { get; set; }
        public string Consignee { get; set; }
        public string Consignor { get; set; }
        public string Pieces { get; set; }
        public Nullable<double> StatedWeight { get; set; }
        public System.DateTime InScanDate { get; set; }
        public Nullable<int> IAgentID { get; set; }
        public string ReceivedBy { get; set; }
        public string AWBStatus { get; set; }
        public string Origin { get; set; }
        public string OriginCity { get; set; }
        public string Destination { get; set; }
        public string DestinationCity { get; set; }
        public string ForwardingAgentName { get; set; }
    }
}
