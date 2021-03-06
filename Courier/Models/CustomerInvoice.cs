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
    
    public partial class CustomerInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerInvoice()
        {
            this.CustomerInvoiceDetails = new HashSet<CustomerInvoiceDetail>();
        }
    
        public int CustomerInvoiceID { get; set; }
        public string CustomerInvoiceNo { get; set; }
        public int CustomerID { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public Nullable<decimal> TaxPercent { get; set; }
        public Nullable<int> AcJournalID { get; set; }
        public bool StatusClose { get; set; }
        public int FYearID { get; set; }
        public Nullable<int> AcCompanyID { get; set; }
        public Nullable<decimal> InvoiceTotal { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> AcFinancialYearID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerInvoiceDetail> CustomerInvoiceDetails { get; set; }
    }
}
