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
    
    public partial class CostUpdateMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CostUpdateMaster()
        {
            this.CostUpdateDetails = new HashSet<CostUpdateDetail>();
            this.CostUpdateDetails1 = new HashSet<CostUpdateDetail>();
        }
    
        public int ID { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int BranchID { get; set; }
        public int TruckDetailID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> AcFinancialYearID { get; set; }
    
        public virtual BranchMaster BranchMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostUpdateDetail> CostUpdateDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostUpdateDetail> CostUpdateDetails1 { get; set; }
        public virtual TruckDetail TruckDetail { get; set; }
    }
}
