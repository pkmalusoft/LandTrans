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
    using System.Collections.Generic;
    
    public partial class tblTransfer
    {
        public int ID { get; set; }
        public Nullable<int> InscanID { get; set; }
        public Nullable<int> InscanInternationID { get; set; }
        public Nullable<int> ShippingDepotID { get; set; }
        public Nullable<System.DateTime> ShippingDateTime { get; set; }
        public Nullable<int> ShippedBy { get; set; }
        public Nullable<int> ReceivingDepotID { get; set; }
        public Nullable<System.DateTime> ReceiveDateTime { get; set; }
        public Nullable<int> ReceivedBy { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> DeliveredBy { get; set; }
        public Nullable<int> CollectedBy { get; set; }
        public Nullable<int> Vehicle { get; set; }
    }
}
