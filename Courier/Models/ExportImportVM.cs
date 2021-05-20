﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class ImportShipmentFormModel : ImportShipment
    {
        public ImportShipmentFormModel()
            {
            Shipments = new List<ImportShipmentDetail>();
            }
        public string ConsignorAddress {get;set;}
        public string ConsigneeAddress { get; set; }

        public List<ImportShipmentDetail> Shipments { get; set; }
    }
    public class ImportShipmentDetailVM :ImportShipmentDetail
    {
        //public string HAWB { get; set; }
        //public string AWB { get; set; }
        //public int PCS { get; set; }
        //public decimal Weight { get; set; }
        //public string Contents { get; set; }
        //public string Shipper { get; set; }
        //public decimal Value { get; set; }
        //public string Reciver { get; set; }
        //public string DestinationCountryID { get; set; }
        //public string DestinationCityID { get; set; }
        //public string BagNo { get; set; }
        //public int CurrencyID { get; set; }
        public string CurrencyName{ get; set; }
    }
    public class ImportShipmentVM
    {
        public string AgentName { get; set; }
        public string ManifestNumber { get; set; }
        public string ConsignorAddress { get; set; }
        public string OriginCountry { get; set; }
        public string OriginCity { get; set; }
        public string ConsigneeAddress { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public System.DateTime FlightDate { get; set; }
        public string OriginAirportCity { get; set; }
        public string DestinationAirportCity { get; set; }
        public string FlightNo { get; set; }
        public string MAWB { get; set; }
        public string CD { get; set; }
        
        public string CourierStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> Bags { get; set; }
        public string RunNo { get; set; }
        public string Type { get; set; }
        public int TotalAWB { get; set; }
        public List<ImportShipmentDetail> ImportShipmentDetails { get; set; }
    }
    public class ExportShipmentFormModel : ExportShipment
    { 
        public ExportShipmentFormModel()
        {
            Shipments = new List<ExportShipmentDetail>();
        }
        public string ConsignorAddress { get; set; }
        public string ConsigneeAddress { get; set; }
        public string CreatedByName { get; set; }
        public string AgentName { get; set; }
        public List<ExportShipmentDetail> Shipments { get; set; }

        public List<ExportShipmentDetailVM> ShipmentsVM { get; set; }
    }
    public class ExportShipmentDetailVM : ExportShipmentDetail
    {
        public string PaymentMode { get; set; }
        public string ForwardAgentName {get;set;}
        public string CurrencyName { get; set; }
        public string CurrenySymbol { get; set; }
        public string ConsignorPhone { get; set; }
        public string ConsigneePhone { get; set; }
        public string OriginCountry { get; set; }
        public decimal? AWBOtherCharge { get; set; }
        public decimal? AWBCourierCharge { get; set; }
    }
    public class ExportShipmentVM :ExportShipment
    {
        public string ManifestNumber { get; set; }
        public string ConsignorAddress { get; set; }
        public string OriginCountry { get; set; }
        public string OriginCity { get; set; }
        public string ConsigneeAddress { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public System.DateTime Date { get; set; }
        public string AirportOfShipment { get; set; }
        public string FlightNo { get; set; }
        public string MAWB { get; set; }
        public string CD { get; set; }
        public Nullable<int> Bags { get; set; }
        public string RunNo { get; set; }
        public string Type { get; set; }
        public int TotalAWB { get; set; }
        public List<ExportShipmentDetail> ExportShipmentdetails { get; set; }
    }

    public class DatePicker
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Create { get; set; }

        public int? StatusId { get; set; }
        public int? AgentId { get; set; }
        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string MovementId { get; set; }
        public int[] SelectedValues { get; set; }

        public int paymentId { get; set; }
    }

    public class AccountsReportParam
    {
        public int AcHeadId { get; set; }
        public string AcHeadName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime AsOnDate { get; set; }

        public int AcTypeId { get; set; }
        public int AcGroupId { get; set; }
        public string Output { get; set; } //printer ,pdf,word,excel
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }
        public string VoucherTypeId { get; set; }
        public int[] SelectedValues { get; set; }

    }
    public class AccountsReportParam1
    {
        public int AcHeadId { get; set; }
        public string AcHeadName { get; set; }
        
        public DateTime AsOnDate { get; set; }

        public int AcTypeId { get; set; }
        public int AcGroupId { get; set; }
        public string Output { get; set; } //printer ,pdf,word,excel
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }
        public string VoucherTypeId { get; set; }
        public int[] SelectedValues { get; set; }

    }
    public class AWBReportParam
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Create { get; set; }

        public int? PaymentModeId { get; set; }
        public string ParcelTypeId { get; set; }
        public string InvoicedTo { get; set; }
        public string MovementId { get; set; }
        public int[] SelectedValues { get; set; }
        public string Output { get; set; }
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }
        public string SortBy { get; set; }
    }

    public class TaxReportParam
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Create { get; set; }
                public string TransactionType{ get; set; }        
        public string Output { get; set; }
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }
        public string SortBy { get; set; }
    }
    public class ManifestReportParam
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int TDID { get; set; }
        public string TDNo { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Create { get; set; }
        public string TransactionType { get; set; }
        public string Output { get; set; }
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }
        public string SortBy { get; set; }
        
    }

    public class LabelPrintingParam
    {
        public int LabelStartNo { get; set; }
        public int LabelQty { get; set; }
        public int Increment { get; set; }
        public int InScanId{ get; set; }

        public string ConsignmentNo { get; set; }
        public string Output { get; set; }

        public string ReportFileName { get; set; }
    }
    public class CustomerLedgerReportParam
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime AsonDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Output { get; set; } //printer ,pdf,word,excel
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }

    }
    public class AWBTimeLineReportParam
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Output { get; set; } //printer ,pdf,word,excel
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }

    }
    public class SupplierLedgerReportParam
    {
        public int SupplierTypeId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime AsonDate { get; set; }
        public string Output { get; set; } //printer ,pdf,word,excel
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }

    }

    public class SalesReportParam
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int EmployeeID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Output { get; set; } //printer ,pdf,word,excel
        public string ReportType { get; set; } //sumary details
        public string ReportFileName { get; set; }
        public string Filters { get; set; }

    }
}