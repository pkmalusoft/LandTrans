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
    
    public partial class AreaMaster
    {
        public int AreaID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string AreaName { get; set; }
    
        public virtual LocationMaster LocationMaster { get; set; }
    }
}
