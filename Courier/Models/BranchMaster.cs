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
    
    public partial class BranchMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BranchMaster()
        {
            this.AcGroups = new HashSet<AcGroup>();
            this.AcOpeningMasters = new HashSet<AcOpeningMaster>();
            this.AcOPInvoiceMasters = new HashSet<AcOPInvoiceMaster>();
            this.AnalysisHeads = new HashSet<AnalysisHead>();
            this.CustomerMasters = new HashSet<CustomerMaster>();
            this.PurchaseInvoices = new HashSet<PurchaseInvoice>();
            this.Quotations = new HashSet<Quotation>();
            this.SalesInvoices = new HashSet<SalesInvoice>();
            this.SupplierMasters = new HashSet<SupplierMaster>();
            this.RevenueUpdateMasters = new HashSet<RevenueUpdateMaster>();
            this.CostUpdateMasters = new HashSet<CostUpdateMaster>();
            this.TruckDetails = new HashSet<TruckDetail>();
        }
    
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string KeyPerson { get; set; }
        public Nullable<int> DesignationID { get; set; }
        public string Phone { get; set; }
        public string PhoneNo1 { get; set; }
        public string PhoneNo2 { get; set; }
        public string PhoneNo3 { get; set; }
        public string PhoneNo4 { get; set; }
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string EMail { get; set; }
        public string Website { get; set; }
        public string BranchPrefix { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> AcCompanyID { get; set; }
        public Nullable<bool> StatusAssociate { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string LocationName { get; set; }
        public string InvoicePrefix { get; set; }
        public string InvoiceFormat { get; set; }
        public string VATRegistrationNo { get; set; }
        public Nullable<decimal> VATPercent { get; set; }
        public Nullable<int> AcFinancialYearID { get; set; }
        public string CODReceiptPrefix { get; set; }
        public string CODReceiptFormat { get; set; }
        public Nullable<int> VATAccountId { get; set; }
        public string ConsignmentFormat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcGroup> AcGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcOpeningMaster> AcOpeningMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcOPInvoiceMaster> AcOPInvoiceMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnalysisHead> AnalysisHeads { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerMaster> CustomerMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quotation> Quotations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierMaster> SupplierMasters { get; set; }
        public virtual CityMaster CityMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevenueUpdateMaster> RevenueUpdateMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostUpdateMaster> CostUpdateMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TruckDetail> TruckDetails { get; set; }
    }
}
