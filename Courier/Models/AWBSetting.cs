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
    
    public partial class AWBSetting
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> IsShowShipper { get; set; }
        public Nullable<bool> IsShowConsignee { get; set; }
        public Nullable<bool> IsShowWeight { get; set; }
        public Nullable<bool> IsShowCargo { get; set; }
        public Nullable<bool> IsShowCollectedBy { get; set; }
        public Nullable<bool> IsShowCourierCharges { get; set; }
        public Nullable<bool> IsShowForwardingAgent { get; set; }
        public Nullable<bool> IsShowMaterialCost { get; set; }
    }
}
