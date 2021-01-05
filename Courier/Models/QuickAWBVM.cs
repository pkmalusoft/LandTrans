﻿// Decompiled with JetBrains decompiler
// Type: LTMSV2.Models.QuickAWBVM
// Assembly: Courier_27_09_16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B3B4E05-393A-455A-A5DE-86374CE9B081
// Assembly location: D:\Courier09022018\Decompiled\obj\Release\Package\PackageTmp\bin\LTMSV2dll

using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LTMSV2.Models
{
    public class QuickAWBVM
    {
        public string EnquiryNo { get; set; }
        public int EnquiryID { get; set; }
        public string ConsignmentNo { get; set; }

        public int InScanID { get; set; }

        public DateTime? InScanDate { get; set; }

        public int CustomerID { get; set; }

        public bool CustomerandShipperSame { get; set; }
        //public string ShipperName { get; set; }
        public string Consignor { get; set; }

        [Required(ErrorMessage = "Please enter Consignor Contact!")]
        public string ConsignorContact { get; set; }

        public string ConsignorPhone { get; set; }
        public string ConsignorAddress1_Building { get; set; }
        public string ConsignorAddress2_Street { get; set; }
        public string ConsignorAddress3_PinCode { get; set; }
        public string ConsignorCityName { get; set; }
        public string ConsignorCountryName { get; set; }

        public string ConsignorLocationName { get; set; }
        public int ConsignorLocationID { get; set; }
        public int ConsignorCityID { get; set; }
        public int ConsignorCountryID { get; set; }
        public string ConsignorFax { get; set; }
        public string Consignee { get; set; }
        public string ConsigneeCityName { get; set; }
        public string ConsigneeLocationName { get; set; }
        public int ConsigneeLocationID { get; set; }
        public int ConsigneeCityID { get; set; }
        public int ConsigneeCountryID { get; set; }
        public string ConsigneeContact { get; set; }
        public string ConsigneePhone { get; set; }
        public string ConsigneeAddress1_Building { get; set; }
        public string ConsigneeAddress2_Street { get; set; }
        public string ConsigneeAddress3_PinCode { get; set; }
        public string ConsigneeCountryName { get; set; }
        public string ConsigneeFax { get; set; }
        public string ItemName { get; set; }
        public int ItemId { get; set; }
            public int PackageId { get; set; }
        public string PackageName { get; set; }
        
        public string paymentmode { get; set; }
        public int? PaymentModeId { get; set; }
        public string InvoiceTo { get; set; }
        public string code { get; set; }
        public string CBM_Unit { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Volume { get; set; }
        public decimal VolumeWeight { get; set; }
        public decimal CustomsInvoiceValue { get; set; }
        public int DocumentSetupId { get; set; }
        public string DocumentSetupName { get; set; }
        public int DocumentTypeId { get; set; }
        public decimal Freight { get; set; }
        public string ExportImportCode { get; set; }
        public  bool IsNCND { get; set; }
        public bool SeparateDoc { get; set; }
        public DateTime DespatchDate { get; set; }
        public string RouteName { get; set; }
        public int TruckDetailID { get; set; }
        public string VehicleRegNo { get; set; }
        public int RouteID { get; set; }
        public Decimal? CourierCharge { get; set; }

        public Decimal? OtherCharge { get; set; }

        public Decimal? PackingCharge { get; set; }

        public Decimal CustomCharge { get; set; }

        public Decimal? totalCharge { get; set; }

        public Decimal ForwardingCharge { get; set; }

        public string remarks { get; set; }

        public Decimal? materialcost { get; set; }

        public string Description { get; set; }

        public string Pieces { get; set; }

        public Decimal? Weight { get; set; }

        public int CourierType { get; set; }

        public int CourierMode { get; set; }

        public int ProductType { get; set; }

        public int? MovementTypeID { get; set; }

        public int ParcelTypeID { get; set; }

        public int ProductTypeID { get; set; }

        public int CustomerRateTypeID { get; set; }

        public int? PickedBy { get; set; }

        public int? ReceivedBy { get; set; }

        public int FagentID { get; set; }

        public string FAWBNo { get; set; }

        public Decimal VerifiedWeight { get; set; }

        public DateTime ForwardingDate { get; set; }

        public bool StatusAssignment { get; set; }

        public int TaxconfigurationID { get; set; }

        public string customer { get; set; }

        public string shippername { get; set; }

        public string consigneename { get; set; }

        public string origin { get; set; }

        public string destination { get; set; }

        public int AcJournalID { get; set; }
        public int BranchID { get; set; }
        public int DepotID { get; set; }
        public int UserID { get; set; }
        public int AcCompanyID { get; set; }
        public int? PickupRequestStatusId { get; set; }
        public int? CourierStatusId { get; set; }
        public DateTime TransactionDate { get; set; }

        public string CourierStatus { get; set; }
        public string StatusType { get; set; }
        public int? StatusTypeId { get; set; }

        public string requestsource { get; set; }

        public string AWBTermsConditions { get; set; }

        public string CreatedByName { get; set; }
        public string LastModifiedByName { get; set; }
        public string CreatedByDate { get; set; }
        public string LastModifiedDate { get; set; }
        public bool PrintLabel { get; set; }
        public string TypeofLoad { get; set; }
        public bool COM { get; set; }
        public string CargoDescription { get; set; }
        public List<OtherChargeDetailVM> otherchargesVM { get; set; }
        

    }

    public class StaffNotesVM:StaffNote
    {
        public string EmployeeName { get; set; }
   }
    public class CustomerNotificationVM:CustomerNotification
    {
        public string EmployeeName { get; set; }
        public string CustomerEmail { get; set; }

        public string CustomerName { get; set; }
        public string AWBNo { get; set; }
    }
    public class OtherChargeDetailVM : InscanOtherCharge
    { 
        public string OtherChargeName { get; set; }
    }
}
