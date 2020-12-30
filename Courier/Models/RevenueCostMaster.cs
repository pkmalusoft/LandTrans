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
    
    public partial class RevenueCostMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RevenueCostMaster()
        {
            this.RevenueUpdateDetails = new HashSet<RevenueUpdateDetail>();
            this.CostUpdateDetails = new HashSet<CostUpdateDetail>();
        }
    
        public int RCID { get; set; }
        public string RCCode { get; set; }
        public string RevenueComponent { get; set; }
        public Nullable<decimal> RevenueRate { get; set; }
        public Nullable<bool> RevenueMandatory { get; set; }
        public Nullable<int> RevenueAcHeadID { get; set; }
        public string CostComponent { get; set; }
        public Nullable<decimal> CostRate { get; set; }
        public Nullable<bool> CostMandatory { get; set; }
        public Nullable<int> CostAcHeadID { get; set; }
        public Nullable<int> AcCompanyId { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string RevenueGroup { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevenueUpdateDetail> RevenueUpdateDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostUpdateDetail> CostUpdateDetails { get; set; }
    }
}
