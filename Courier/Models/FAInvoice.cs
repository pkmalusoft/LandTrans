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
    
    public partial class FAInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FAInvoice()
        {
            this.FAInvoiceDetails = new HashSet<FAInvoiceDetail>();
        }
    
        public int FAInvoiceID { get; set; }
        public int FAgentID { get; set; }
        public Nullable<System.DateTime> FAInvoiceDate { get; set; }
        public string FAInvoiceNo { get; set; }
        public Nullable<decimal> FAInvoiceTax { get; set; }
        public Nullable<int> AcJournalID { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> FYearID { get; set; }
        public Nullable<int> AcCompanyID { get; set; }
        public Nullable<decimal> FuelSurchargePer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAInvoiceDetail> FAInvoiceDetails { get; set; }
    }
}
