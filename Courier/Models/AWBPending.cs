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
    
    public partial class AWBPending
    {
        public int AWBPendingID { get; set; }
        public string AWBNO { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> OriginalAmt { get; set; }
        public Nullable<decimal> RecvdAmt { get; set; }
        public string RefNo { get; set; }
        public string Status { get; set; }
        public Nullable<int> AcCompanyID { get; set; }
        public Nullable<int> InscanID { get; set; }
    }
}
