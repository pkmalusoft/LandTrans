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
    
    public partial class TruckDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TruckDetail()
        {
            this.CostUpdateMasters = new HashSet<CostUpdateMaster>();
        }
    
        public int TruckDetailID { get; set; }
        public string ReceiptNo { get; set; }
        public int VehicleID { get; set; }
        public string RegNo { get; set; }
        public string VehicleType { get; set; }
        public System.DateTime TDDate { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string DriverName { get; set; }
        public int VehicleTypeID { get; set; }
        public Nullable<decimal> Rent { get; set; }
        public Nullable<decimal> OtherCharges { get; set; }
        public Nullable<int> RefTDID { get; set; }
        public Nullable<int> AcJournalID { get; set; }
        public Nullable<int> CurrencyIDRent { get; set; }
        public Nullable<int> CurrencyIDOC { get; set; }
        public string StatusPaymentMode { get; set; }
        public Nullable<int> PaymentHeadID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> PaymentCurrencyID { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> AcCompanyID { get; set; }
        public Nullable<int> TDcontrolAcHeadID { get; set; }
        public Nullable<int> OtherChargesAcHeadID { get; set; }
        public Nullable<int> FYearID { get; set; }
        public Nullable<decimal> CurrencyRent { get; set; }
        public Nullable<decimal> CurrencyOtherCharges { get; set; }
        public Nullable<decimal> CurrencyAmount { get; set; }
        public string Code { get; set; }
        public string PayeeName { get; set; }
        public Nullable<int> DriverID { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string Reason { get; set; }
        public Nullable<int> DocumentID { get; set; }
        public Nullable<int> RentAcHeadID { get; set; }
        public Nullable<int> RouteID { get; set; }
        public string TDRemarks { get; set; }
        public Nullable<int> InscanID { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string OriginCountry { get; set; }
        public Nullable<int> ForwardAgentID { get; set; }
        public string OriginCity { get; set; }
        public Nullable<int> SupplierInvoiceId { get; set; }
        public Nullable<int> RecPayId { get; set; }
        public string ConsignmentNoNote { get; set; }
        public Nullable<int> ParcelTypeId { get; set; }
    
        public virtual BranchMaster BranchMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostUpdateMaster> CostUpdateMasters { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }
        public virtual CurrencyMaster CurrencyMaster1 { get; set; }
        public virtual RouteMaster RouteMaster { get; set; }
    }
}
