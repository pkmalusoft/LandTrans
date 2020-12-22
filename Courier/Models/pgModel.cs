using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class pgModel
    {
    }
    public class A
    {
        //public string T { get; set; }
    }

    public class VehicleMasterVM
    {
        public string VehicleID { get; set; }
        public string VehicleDescription { get; set; }
        public string RegistrationNo { get; set; }
        public string Model { get; set; }
        public string PurchaseValue { get; set; }
        public string PurchaseDate { get; set; }
        public string RegExpirydate { get; set; }
        public string VehicleTypeID { get; set; }
        public string InsuranceExpDate { get; set; }
        public string AcCompanyID { get; set; }
        public string Mode { get; set; }
        public string FinanceCompany { get; set; }
        public string InsuranceCompName { get; set; }
        public string PolicyNo { get; set; }
        public string InsuredValue { get; set; }
        public string Category { get; set; }
        public string DepreciationDate { get; set; }
        public string ScrapValue { get; set; }
        public string BranchID { get; set; }
        public string MakeYear { get; set; }
        public string RegisteredUnder { get; set; }
        public string VehicleType { get; set; }
        public string TruckDetailID { get; set; }
        public string DriverName { get; set; }
        public string TDDate { get; set; }
        public string RouteID { get; set; }
        public string ReceiptNo { get; set; }
        public string RegNo { get; set; }
        public string OriginCountry { get; set; }
        public string OriginCity { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationCity { get; set; }
        public string eMode { get; set; }
        public static List<VehicleMasterVM> GetVehicleMaster(VehicleMasterVM o)
        {
            o = o ?? new VehicleMasterVM();
            Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "VehicleID",o.VehicleID??"" },
                 { "VehicleDescription",o.VehicleDescription??"" },
                 { "RegistrationNo",o.RegistrationNo??"" },
                 { "Model",o.Model??"" },
                 { "PurchaseValue",o.PurchaseValue??"" },
                 { "PurchaseDate",o.PurchaseDate??"" },
                 { "RegExpirydate",o.RegExpirydate??"" },
                 { "VehicleTypeID",o.VehicleTypeID??"" },
                 { "InsuranceExpDate",o.InsuranceExpDate??"" },
                 { "AcCompanyID",o.AcCompanyID??"" },
                 { "Mode",o.Mode??"" },
                 { "FinanceCompany",o.FinanceCompany??"" },
                 { "InsuranceCompName",o.InsuranceCompName??"" },
                 { "PolicyNo",o.PolicyNo??"" },
                 { "InsuredValue",o.InsuredValue??"" },
                 { "Category",o.Category??"" },
                 { "DepreciationDate",o.DepreciationDate??"" },
                 { "ScrapValue",o.ScrapValue??"" },
                 { "BranchID",o.BranchID??"" },
                 { "MakeYear",o.MakeYear??"" },
                 { "RegisteredUnder",o.RegisteredUnder??"" },
                 { "TDDate",o.TDDate??"" },
                 { "RouteID",o.RouteID??"" },
                 { "eMode", o.eMode??"" },
                 };
            return pgFun.GetData<VehicleMasterVM>("[dbo].[usp_VehicleMaster_Get]", input);
        }
        public static string ManageVehicleMaster(VehicleMasterVM o)
        {
            o = o ?? new VehicleMasterVM();
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "VehicleID",o.VehicleID??"" },
                 { "VehicleDescription",o.VehicleDescription??"" },
                 { "RegistrationNo",o.RegistrationNo??"" },
                 { "Model",o.Model??"" },
                 { "PurchaseValue",o.PurchaseValue??"" },
                 { "PurchaseDate",o.PurchaseDate??"" },
                 { "RegExpirydate",o.RegExpirydate??"" },
                 { "VehicleTypeID",o.VehicleTypeID??"" },
                 { "InsuranceExpDate",o.InsuranceExpDate??"" },
                 { "AcCompanyID",o.AcCompanyID??"" },
                 { "Mode",o.Mode??"" },
                 { "FinanceCompany",o.FinanceCompany??"" },
                 { "InsuranceCompName",o.InsuranceCompName??"" },
                 { "PolicyNo",o.PolicyNo??"" },
                 { "InsuredValue",o.InsuredValue??"" },
                 { "Category",o.Category??"" },
                 { "DepreciationDate",o.DepreciationDate??"" },
                 { "ScrapValue",o.ScrapValue??"" },
                 { "BranchID",o.BranchID??"" },
                 { "MakeYear",o.MakeYear??"" },
                 { "RegisteredUnder",o.RegisteredUnder??"" },
                 { "eMode", o.eMode??"" },
                 };
                Dictionary<string, string> output = new Dictionary<string, string>() { { "Result", "" } };
                Dictionary<string, string> res = pgFun.ExecuteSQL("[dbo].[usp_VehicleMaster_Manage]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }
    }
    public class ManufacturerForVehicle
    {
        public string VehicleDescription { get; set; }
        public static List<ManufacturerForVehicle> GetManufacturerForVehicle(ManufacturerForVehicle o)
        {
            o = o ?? new ManufacturerForVehicle();
            return pgFun.GetData<ManufacturerForVehicle>("[dbo].[GetManufacturerForVehicle]");
        }
    }
    public class InsCompanyForVehicle
    {
        public string InsuranceCompName { get; set; }
        public static List<InsCompanyForVehicle> GetInsCompanyForVehicle(InsCompanyForVehicle o)
        {
            o = o ?? new InsCompanyForVehicle();
            return pgFun.GetData<InsCompanyForVehicle>("[dbo].[GetInsCompanyForVehicle]");
        }
    }
    public class ModelForVehicle
    {
        public string Model { get; set; }
        public static List<ModelForVehicle> GetModelForVehicle(ModelForVehicle o)
        {
            o = o ?? new ModelForVehicle();
            return pgFun.GetData<ModelForVehicle>("[dbo].[GetModelForVehicle]");
        }
    }
    public class VehicleTypes
    {
        public string VehicleType { get; set; }
        public string VehicleTypeID { get; set; }
        public static List<VehicleTypes> GetVehicleTypes(VehicleTypes o)
        {
            o = o ?? new VehicleTypes();
            return pgFun.GetData<VehicleTypes>("[dbo].[VehicleTypeSelectAll]");
        }
    }
    public class RegisteredUnderForVehicle
    {
        public string RegisteredUnder { get; set; }
        public static List<RegisteredUnderForVehicle> GetRegisteredUnderForVehicle(RegisteredUnderForVehicle o)
        {
            o = o ?? new RegisteredUnderForVehicle();
            return pgFun.GetData<RegisteredUnderForVehicle>("[dbo].[GetRegisteredUnderForVehicle]");
        }
    }
    public class AutoComplete
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Term { get; set; }
        public string Limit { get; set; }
        public string Val1 { get; set; }
        public string Mode { get; set; }
        public static List<AutoComplete> GetAutoComplete(AutoComplete o)
        {
            o = o ?? new AutoComplete();
            Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "ID",o.ID??"" },
                 { "Term",o.Term??"" },
                 { "Limit",o.Limit??"" },
                 { "Mode",o.Mode??"" },
                 };
            return pgFun.GetData<AutoComplete>("[dbo].[usp_AutoComplete]", input);
        }
    }
   
    public class LabelPrinting
    {
        public string InScanID { get; set; }
        public string AWBNo { get; set; }
        public string InScanDate { get; set; }
        public string InScanTime { get; set; }
        public string DepartmentID { get; set; }
        public string ConsigneeID { get; set; }
        public string ConsignorID { get; set; }
        public string OriginID { get; set; }
        public string Origin { get; set; }
        public string DestinationID { get; set; }
        public string Destination { get; set; }
        public string CustomerRateTypeID { get; set; }
        public string Pieces { get; set; }
        public string StatedWeight { get; set; }
        public string CourierServiceID { get; set; }
        public string CourierStatus { get; set; }
        public string CollectedBy { get; set; }
        public string ReceivedBy { get; set; }
        public string Remarks { get; set; }
        public string CourierStatusID { get; set; }
        public string StatusPaymentMode { get; set; }
        public string StatusClose { get; set; }
        public string StatusInScan { get; set; }
        public string StatusReconciled { get; set; }
        public string InscanSheetNo { get; set; }
        public string Volume { get; set; }
        public string VolumeWeight { get; set; }
        public string StatusOutbound { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string StatusTransit { get; set; }
        public string StatusCustomsDocument { get; set; }
        public string DespatchDate { get; set; }
        public string TDID { get; set; }
        public string Description { get; set; }
        public string OriginBranchID { get; set; }
        public string DestinationBranchID { get; set; }
        public string TransitMasterID { get; set; }
        public string UserID { get; set; }
        public string shipperContactPerson { get; set; }
        public string ConsigneeContactPerson { get; set; }
        public string AcJournalID { get; set; }
        public string FyearID { get; set; }
        public string OtherChargeDescription { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveredBy { get; set; }
        public string PODRemarks { get; set; }
        public string DeliveredByID { get; set; }
        public string AcJournalID2 { get; set; }
        public string StatusTS { get; set; }
        public string EnquiryID { get; set; }
        public string OutScanDate { get; set; }
        public string TakenByID { get; set; }
        public string TransitID { get; set; }
        public string RouteID { get; set; }
        public string DocumentID { get; set; }
        public string ConsigneeDepotID { get; set; }
        public string ConsigneeDepot { get; set; }
        public string CustInvValue { get; set; }
        public string Freight { get; set; }
        public string IsNCND { get; set; }
        public string MaterialCost { get; set; }
        public string MaterialCostInvoiceNo { get; set; }
        public string NCNDRemark { get; set; }
        public string PackageID { get; set; }
        public string TransitDepotID { get; set; }
        public string BranchId { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PaymentType { get; set; }
        public string IsLocalTD { get; set; }
        public string SalesManID { get; set; }
        public string EnquiryNo { get; set; }
        public string EnquiryName { get; set; }
        public string Shipper { get; set; }
        public string Consignee { get; set; }
        public string DriverName { get; set; }
        public string DocumentName { get; set; }
        public string IMPEXPCode { get; set; }
        public string SCountryName { get; set; }
        public string CCountryName { get; set; }
        public string SCityName    { get; set; }
        public string CCityName { get; set; }
        public string VehicleNo { get; set; }
        public string ModifyBy { get; set; }
        public string DRSID { get; set; }
        public string DRSNo { get; set; }
        public string DRSDate { get; set; }
        public string COD { get; set; }
        public string RegistrationNo { get; set; }
        public string CheckedBy { get; set; }
        public string DeliveryBy { get; set; }
        public string VehicleID { get; set; }
        public string Details { get; set; }
        public string DriverID { get; set; }
        public string EmployeeID { get; set; }
        public string DriverFlag { get; set; }
        public string VehicleType { get; set; }
        public string RegNo { get; set; }
        public string TransferDate { get; set; }
        public string PackageType { get; set; }
        public string Mode { get; set; }
        public string LastTop { get; set; }
        public static List<LabelPrinting> GetLabelPrinting(LabelPrinting o)
        {
            o = o ?? new LabelPrinting();
            Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "InScanID",o.InScanID??"" },
                 { "AWBNo",o.AWBNo??"" },
                 { "InScanDate",o.InScanDate??"" },
                 { "DepartmentID",o.DepartmentID??"" },
                 { "ConsigneeID",o.ConsigneeID??"" },
                 { "ConsignorID",o.ConsignorID??"" },
                 { "OriginID",o.OriginID??"" },
                 { "DestinationID",o.DestinationID??"" },
                 { "CustomerRateTypeID",o.CustomerRateTypeID??"" },
                 { "Pieces",o.Pieces??"" },
                 { "StatedWeight",o.StatedWeight??"" },
                 { "CourierServiceID",o.CourierServiceID??"" },
                 { "CollectedBy",o.CollectedBy??"" },
                 { "ReceivedBy",o.ReceivedBy??"" },
                 { "Remarks",o.Remarks??"" },
                 { "CourierStatusID",o.CourierStatusID??"" },
                 { "StatusPaymentMode",o.StatusPaymentMode??"" },
                 { "StatusClose",o.StatusClose??"" },
                 { "StatusInScan",o.StatusInScan??"" },
                 { "StatusReconciled",o.StatusReconciled??"" },
                 { "InscanSheetNo",o.InscanSheetNo??"" },
                 { "Volume",o.Volume??"" },
                 { "VolumeWeight",o.VolumeWeight??"" },
                 { "StatusOutbound",o.StatusOutbound??"" },
                 { "ItemID",o.ItemID??"" },
                 { "StatusTransit",o.StatusTransit??"" },
                 { "StatusCustomsDocument",o.StatusCustomsDocument??"" },
                 { "DespatchDate",o.DespatchDate??"" },
                 { "TDID",o.TDID??"" },
                 { "Description",o.Description??"" },
                 { "OriginBranchID",o.OriginBranchID??"" },
                 { "DestinationBranchID",o.DestinationBranchID??"" },
                 { "TransitMasterID",o.TransitMasterID??"" },
                 { "UserID",o.UserID??"" },
                 { "shipperContactPerson",o.shipperContactPerson??"" },
                 { "ConsigneeContactPerson",o.ConsigneeContactPerson??"" },
                 { "AcJournalID",o.AcJournalID??"" },
                 { "FyearID",o.FyearID??"" },
                 { "OtherChargeDescription",o.OtherChargeDescription??"" },
                 { "DeliveryDate",o.DeliveryDate??"" },
                 { "DeliveredBy",o.DeliveredBy??"" },
                 { "PODRemarks",o.PODRemarks??"" },
                 { "DeliveredByID",o.DeliveredByID??"" },
                 { "AcJournalID2",o.AcJournalID2??"" },
                 { "StatusTS",o.StatusTS??"" },
                 { "EnquiryID",o.EnquiryID??"" },
                 { "OutScanDate",o.OutScanDate??"" },
                 { "TakenByID",o.TakenByID??"" },
                 { "TransitID",o.TransitID??"" },
                 { "RouteID",o.RouteID??"" },
                 { "DocumentID",o.DocumentID??"" },
                 { "ConsigneeDepotID",o.ConsigneeDepotID??"" },
                 { "CustInvValue",o.CustInvValue??"" },
                 { "Freight",o.Freight??"" },
                 { "IsNCND",o.IsNCND??"" },
                 { "MaterialCost",o.MaterialCost??"" },
                 { "MaterialCostInvoiceNo",o.MaterialCostInvoiceNo??"" },
                 { "NCNDRemark",o.NCNDRemark??"" },
                 { "PackageID",o.PackageID??"" },
                 { "TransitDepotID",o.TransitDepotID??"" },
                 { "BranchId",o.BranchId??"" },
                 { "CustomerID",o.CustomerID??"" },
                 { "PaymentType",o.PaymentType??"" },
                 { "IsLocalTD",o.IsLocalTD??"" },
                 { "SalesManID",o.SalesManID??"" },
                 { "DRSID",o.DRSID??"" },
                 { "DRSNo",o.DRSNo??"" },
                 { "Mode", o.Mode??"" },
                 { "LastTop", o.LastTop??"" },
                 };
            return pgFun.GetData<LabelPrinting>("[dbo].[usp_LabelPrinting_Get]", input);
        }
        public static string ManageLabelPrinting(LabelPrinting o)
        {
            o = o ?? new LabelPrinting();
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "InScanID",o.InScanID??"" },
                 { "AWBNo",o.AWBNo??"" },
                 { "InScanDate",o.InScanDate??"" },
                 { "DepartmentID",o.DepartmentID??"" },
                 { "ConsigneeID",o.ConsigneeID??"" },
                 { "ConsignorID",o.ConsignorID??"" },
                 { "OriginID",o.OriginID??"" },
                 { "DestinationID",o.DestinationID??"" },
                 { "CustomerRateTypeID",o.CustomerRateTypeID??"" },
                 { "Pieces",o.Pieces??"" },
                 { "StatedWeight",o.StatedWeight??"" },
                 { "CourierServiceID",o.CourierServiceID??"" },
                 { "CollectedBy",o.CollectedBy??"" },
                 { "ReceivedBy",o.ReceivedBy??"" },
                 { "Remarks",o.Remarks??"" },
                 { "CourierStatusID",o.CourierStatusID??"" },
                 { "StatusPaymentMode",o.StatusPaymentMode??"" },
                 { "StatusClose",o.StatusClose??"" },
                 { "StatusInScan",o.StatusInScan??"" },
                 { "StatusReconciled",o.StatusReconciled??"" },
                 { "InscanSheetNo",o.InscanSheetNo??"" },
                 { "Volume",o.Volume??"" },
                 { "VolumeWeight",o.VolumeWeight??"" },
                 { "StatusOutbound",o.StatusOutbound??"" },
                 { "ItemID",o.ItemID??"" },
                 { "StatusTransit",o.StatusTransit??"" },
                 { "StatusCustomsDocument",o.StatusCustomsDocument??"" },
                 { "DespatchDate",o.DespatchDate??"" },
                 { "TDID",o.TDID??"" },
                 { "Description",o.Description??"" },
                 { "OriginBranchID",o.OriginBranchID??"" },
                 { "DestinationBranchID",o.DestinationBranchID??"" },
                 { "TransitMasterID",o.TransitMasterID??"" },
                 { "UserID",o.UserID??"" },
                 { "shipperContactPerson",o.shipperContactPerson??"" },
                 { "ConsigneeContactPerson",o.ConsigneeContactPerson??"" },
                 { "AcJournalID",o.AcJournalID??"" },
                 { "FyearID",o.FyearID??"" },
                 { "OtherChargeDescription",o.OtherChargeDescription??"" },
                 { "DeliveryDate",o.DeliveryDate??"" },
                 { "DeliveredBy",o.DeliveredBy??"" },
                 { "PODRemarks",o.PODRemarks??"" },
                 { "DeliveredByID",o.DeliveredByID??"" },
                 { "AcJournalID2",o.AcJournalID2??"" },
                 { "StatusTS",o.StatusTS??"" },
                 { "EnquiryID",o.EnquiryID??"" },
                 { "OutScanDate",o.OutScanDate??"" },
                 { "TakenByID",o.TakenByID??"" },
                 { "TransitID",o.TransitID??"" },
                 { "RouteID",o.RouteID??"" },
                 { "DocumentID",o.DocumentID??"" },
                 { "ConsigneeDepotID",o.ConsigneeDepotID??"" },
                 { "CustInvValue",o.CustInvValue??"" },
                 { "Freight",o.Freight??"" },
                 { "IsNCND",o.IsNCND??"" },
                 { "MaterialCost",o.MaterialCost??"" },
                 { "MaterialCostInvoiceNo",o.MaterialCostInvoiceNo??"" },
                 { "NCNDRemark",o.NCNDRemark??"" },
                 { "PackageID",o.PackageID??"" },
                 { "TransitDepotID",o.TransitDepotID??"" },
                 { "BranchId",o.BranchId??"" },
                 { "CustomerID",o.CustomerID??"" },
                 { "PaymentType",o.PaymentType??"" },
                 { "IsLocalTD",o.IsLocalTD??"" },
                 { "SalesManID",o.SalesManID??"" },
                 { "ModifyBy",o.ModifyBy??"" },
                 { "DRSID",o.DRSID??"" },
                 { "DRSNo",o.DRSNo??"" },
                 { "DRSDate",o.DRSDate??"" },
                 { "VehicleID",o.VehicleID??"" },
                 { "Details",o.Details??"" },
                 { "DriverID",o.DriverID??"" },
                 { "EmployeeID",o.EmployeeID??"" },
                 { "DriverFlag",o.DriverFlag??"" },
                 { "VehicleType",o.VehicleType??"" },
                 { "RegNo",o.RegNo??"" },
                 { "TransferDate",o.TransferDate??"" },
                 { "DriverName",o.DriverName??"" },
                 { "Mode", o.Mode??"" },
                 };
                Dictionary<string, string> output = new Dictionary<string, string>() { { "Result", "" } };
                Dictionary<string, string> res = pgFun.ExecuteSQL("[dbo].[usp_LabelPrinting_Manage]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }
    }
    public class AWBStatus
    {
        public string AWBStatusID { get; set; }
        public string InScanID { get; set; }
        public string StatusDate { get; set; }
        public string CourierStatusID { get; set; }
        public string Remarks { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string CourierStatus { get; set; }
        public string Mode { get; set; }
        public static List<AWBStatus> GetAWBStatus(AWBStatus o)
        {
            o = o ?? new AWBStatus();
            Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "AWBStatusID",o.AWBStatusID??"" },
                 { "InScanID",o.InScanID??"" },
                 { "StatusDate",o.StatusDate??"" },
                 { "CourierStatusID",o.CourierStatusID??"" },
                 { "Remarks",o.Remarks??"" },
                 { "ModifyBy",o.ModifyBy??"" },
                 { "ModifyDate",o.ModifyDate??"" },
                 { "CourierStatus",o.CourierStatus??"" },
                 { "Mode", o.Mode??"" },
                 };
            return pgFun.GetData<AWBStatus>("[dbo].[usp_AWBStatus_Get]", input);
        }
      
    }
    public class Routes
    {
        public string RouteID { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string DepotIDs { get; set; }
        public string AutoID { get; set; }
        public string DepotID { get; set; }
        public string Order { get; set; }
        public string Depot { get; set; }
        public string TDDate { get; set; }
        public string Mode { get; set; }
        public static List<Routes> GetRoutes(Routes o)
        {
            o = o ?? new Routes();
            Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "RouteID",o.RouteID??"" },
                 { "RouteCode",o.RouteCode??"" },
                 { "RouteName",o.RouteName??"" },
                 { "TDDate",o.TDDate??"" },
                 { "Mode", o.Mode??"" },
                 };
            return pgFun.GetData<Routes>("[dbo].[usp_Routes_Get]", input);
        }
        public static string ManageRoutes(Routes o)
        {
            o = o ?? new Routes();
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "RouteID",o.RouteID??"" },
                 { "RouteCode",o.RouteCode??"" },
                 { "RouteName",o.RouteName??"" },
                 { "DepotIDs",o.DepotIDs??"" },
                 { "OriginName",o.OriginName??"" },
                 { "DestinationName",o.DestinationName??"" },
                 { "Mode", o.Mode??"" },
                 };
                Dictionary<string, string> output = new Dictionary<string, string>() { { "Result", "" } };
                Dictionary<string, string> res = pgFun.ExecuteSQL("[dbo].[usp_Routes_Manage]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }
    }
    public class Jobs
    {
        public string JobID { get; set; }
        public string JobNo { get; set; }
        public string JobDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string VehicleIDs { get; set; }
        public string InScanIDs { get; set; }
        public string LastTop { get; set; }
        public string Mode { get; set; }
        public static List<Jobs> GetJobs(Jobs o)
        {
            o = o ?? new Jobs();
            Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "JobID",o.JobID??"" },
                 { "JobNo",o.JobNo??"" },
                 { "JobDate",o.JobDate??"" },
                 { "FromDate",o.FromDate??"" },
                 { "ToDate",o.ToDate??"" },
                 { "VehicleIDs",o.VehicleIDs??"" },
                 { "InScanIDs",o.InScanIDs??"" },
                 { "LastTop",o.LastTop??"" },
                 { "Mode", o.Mode??"" },
                 };
            return pgFun.GetData<Jobs>("[dbo].[usp_Jobs_Get]", input);
        }
        public static string ManageJobs(Jobs o)
        {
            o = o ?? new Jobs();
            string Res = "Error";
            try
            {
                Dictionary<string, string> input = new Dictionary<string, string>() {
                 { "JobID",o.JobID??"" },
                 { "JobNo",o.JobNo??"" },
                 { "JobDate",o.JobDate??"" },
                 { "FromDate",o.FromDate??"" },
                 { "ToDate",o.ToDate??"" },
                 { "VehicleIDs",o.VehicleIDs??"" },
                 { "InScanIDs",o.InScanIDs??"" },
                 { "LastTop",o.LastTop??"" },
                 { "Mode", o.Mode??"" },
                 };
                Dictionary<string, string> output = new Dictionary<string, string>() { { "Result", "" } };
                Dictionary<string, string> res = pgFun.ExecuteSQL("[dbo].[usp_Jobs_Manage]", input, output, true);
                int LastID = 0;
                bool isdone = int.TryParse(res["Result"], out LastID);
                if (isdone)
                {
                    Res = res["Result"];
                }
                else
                {
                    Res = res["Result"];
                }
                return Res;
            }
            catch (Exception p)
            {
                return p.Message;
            }
        }
    }

}