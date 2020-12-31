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
    
    public partial class SupplierInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierInvoice()
        {
            this.SupplierInvoiceDetails = new HashSet<SupplierInvoiceDetail>();
        }
    
        public int SupplierInvoiceID { get; set; }
        public int SupplierID { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> AccompanyID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<int> AcJOurnalID { get; set; }
        public Nullable<int> AcDiscJournalID { get; set; }
        public Nullable<int> AcHeadID { get; set; }
        public Nullable<int> FyearID { get; set; }
        public Nullable<int> cid { get; set; }
        public Nullable<int> BranchId { get; set; }
    
        public virtual SupplierMaster SupplierMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierInvoiceDetail> SupplierInvoiceDetails { get; set; }
    }
}
