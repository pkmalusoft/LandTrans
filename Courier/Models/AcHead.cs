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
    
    public partial class AcHead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcHead()
        {
            this.Categories = new HashSet<Category>();
            this.Expenses = new HashSet<Expense>();
            this.ExpenseDetails = new HashSet<ExpenseDetail>();
            this.AcOpeningMasters = new HashSet<AcOpeningMaster>();
            this.AcOPInvoiceMasters = new HashSet<AcOPInvoiceMaster>();
        }
    
        public int AcHeadID { get; set; }
        public string AcHeadKey { get; set; }
        public string AcHead1 { get; set; }
        public Nullable<int> AcGroupID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> HeadOrder { get; set; }
        public Nullable<bool> StatusHide { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Prefix { get; set; }
        public Nullable<bool> StatusControlAC { get; set; }
        public string AccountDescription { get; set; }
        public Nullable<int> AcBranchID { get; set; }
        public Nullable<decimal> TaxPercent { get; set; }
        public bool TaxApplicable { get; set; }
    
        public virtual AcGroup AcGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expense> Expenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpenseDetail> ExpenseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcOpeningMaster> AcOpeningMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcOPInvoiceMaster> AcOPInvoiceMasters { get; set; }
    }
}
