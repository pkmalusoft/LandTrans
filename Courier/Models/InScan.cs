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
    
    public partial class InScan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InScan()
        {
            this.InScanDomestics = new HashSet<InScanDomestic>();
            this.InScanInternationalDeatils = new HashSet<InScanInternationalDeatil>();
            this.RevenueUpdateMasters = new HashSet<RevenueUpdateMaster>();
        }
    
        public int InScanID { get; set; }
        public string AWBNo { get; set; }
        public string ReferenceAWBNo { get; set; }
        public System.DateTime InScanDate { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> ConsigneeID { get; set; }
        public Nullable<int> ConsignorID { get; set; }
        public Nullable<int> OriginID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string DestinationID { get; set; }
        public Nullable<int> CustomerRateTypeID { get; set; }
        public string Pieces { get; set; }
        public Nullable<double> StatedWeight { get; set; }
        public Nullable<int> CourierDescriptionID { get; set; }
        public Nullable<int> CourierServiceID { get; set; }
        public Nullable<int> CollectedBy { get; set; }
        public int ReceivedBy { get; set; }
        public decimal CourierCharge { get; set; }
        public Nullable<decimal> OtherCharge { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CourierStatusID { get; set; }
        public string StatusPaymentMode { get; set; }
        public Nullable<bool> StatusClose { get; set; }
        public string StatusInScan { get; set; }
        public string StatusPickUp { get; set; }
        public Nullable<bool> StatusReconciled { get; set; }
        public Nullable<int> InscanSheetNo { get; set; }
        public bool StatusTransit { get; set; }
        public Nullable<int> TransitID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> EnquiryID { get; set; }
        public Nullable<int> MovementID { get; set; }
        public Nullable<bool> Statuspost { get; set; }
        public Nullable<int> ConsignorCityID { get; set; }
        public Nullable<int> ConsigneeCityID { get; set; }
        public Nullable<int> ExportNo { get; set; }
        public Nullable<int> AcCompanyID { get; set; }
        public Nullable<bool> StatusExport { get; set; }
        public Nullable<System.DateTime> ExportDate { get; set; }
        public Nullable<int> ImportNo { get; set; }
        public Nullable<System.DateTime> ImportDate { get; set; }
        public Nullable<int> AcJournalID { get; set; }
        public Nullable<int> TaxconfigurationID { get; set; }
        public Nullable<bool> StatusSuspenseEntry { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string Consignee { get; set; }
        public string Consignor { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsignorAddress { get; set; }
        public string ConsigneePhone { get; set; }
        public string ConsignorPhone { get; set; }
        public Nullable<int> ConsignorCountryID { get; set; }
        public Nullable<int> ConsigneeCountryID { get; set; }
        public Nullable<int> ConsignorLocationID { get; set; }
        public Nullable<int> ConsigneeLocationID { get; set; }
        public Nullable<int> AcTaxJournalID { get; set; }
        public string StatusAWB { get; set; }
        public Nullable<int> IAgentID { get; set; }
        public string BagNo { get; set; }
        public Nullable<decimal> FuelSurcharge { get; set; }
        public string PalletNo { get; set; }
        public Nullable<bool> StatusAWBPending { get; set; }
        public Nullable<decimal> BalanceAmt { get; set; }
        public Nullable<bool> StatusPrepaid { get; set; }
        public Nullable<bool> StatusPrepaidConsignee { get; set; }
        public Nullable<int> DestinationBranchID { get; set; }
        public Nullable<bool> StatusInboundVerify { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<int> ReceivedByID { get; set; }
        public Nullable<int> TypeOfGoodID { get; set; }
        public string InvoiceValue { get; set; }
        public Nullable<int> PrepaidAwbDetailID { get; set; }
        public string ConsigneeAddress1 { get; set; }
        public string ConsignorAddress1 { get; set; }
        public string ConsigneeAddress2 { get; set; }
        public string ConsignorAddress2 { get; set; }
        public string ConsigneeContact { get; set; }
        public string ConsignorContact { get; set; }
        public Nullable<int> BranchID { get; set; }
        public string CargoDescription { get; set; }
        public string HandlingInstruction { get; set; }
        public Nullable<decimal> ServiceCharge { get; set; }
        public Nullable<decimal> PackingCharge { get; set; }
        public Nullable<decimal> CustomsValue { get; set; }
        public Nullable<int> CustomsCollectedBy { get; set; }
        public string ConsigneeLocation { get; set; }
        public string ConsignorLocation { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<bool> IsSkyLarkUpdate { get; set; }
        public Nullable<int> HeldBy { get; set; }
        public Nullable<System.DateTime> HeldOn { get; set; }
        public string HeldReason { get; set; }
        public Nullable<int> ReleasedBy { get; set; }
        public Nullable<System.DateTime> ReleasedOn { get; set; }
        public string ReleasedReason { get; set; }
        public Nullable<int> DepotCountryID { get; set; }
        public Nullable<int> DepotID { get; set; }
        public Nullable<bool> AWBNumberMode { get; set; }
        public Nullable<int> AgentCollectionID { get; set; }
        public Nullable<decimal> AgentCollectionCharges { get; set; }
        public Nullable<int> AgentDeliveryID { get; set; }
        public Nullable<decimal> AgentDeliveryCharges { get; set; }
        public Nullable<decimal> MaterialCost { get; set; }
        public string DeviceID { get; set; }
        public Nullable<decimal> CollectedAmount { get; set; }
        public Nullable<int> ColAgentInvoiceID { get; set; }
        public Nullable<int> DelAgentInvoiceID { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> RevisedRate { get; set; }
        public string MaterialDescription { get; set; }
        public string SpecialInstructions { get; set; }
        public Nullable<decimal> PickupCharges { get; set; }
        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public string CourierType { get; set; }
        public string Destination { get; set; }
        public Nullable<decimal> CBM { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> NetTotal { get; set; }
        public string SalesExec { get; set; }
        public string FAgent { get; set; }
        public string FAWBNo { get; set; }
        public string NCND { get; set; }
        public string Department { get; set; }
        public string ReceivedByName { get; set; }
        public string CollectedByName { get; set; }
        public Nullable<int> ParcelTypeID { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ConsignorCountryName { get; set; }
        public string ConsignorCityName { get; set; }
        public string ConsigneeCountryName { get; set; }
        public string ConsigneeCityName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InScanDomestic> InScanDomestics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InScanInternationalDeatil> InScanInternationalDeatils { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevenueUpdateMaster> RevenueUpdateMasters { get; set; }
    }
}
