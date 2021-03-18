using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class TruckDetailVM:TruckDetail
    {
        public int TruckDetailID { get; set; }
        public string ReceiptNo { get; set; }
        public int VehicleID { get; set; }
        public string RegNo { get; set; }
        public string VehicleType { get; set; }
        public System.DateTime TDDate { get; set; }
        public Nullable<int> OriginName { get; set; }
        public Nullable<int> DestinationName { get; set; }
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
        public string TypeOfLoad { get; set; }
        public IEnumerable<BranchMaster> branchMasters { get; set; }
        public IEnumerable<LocationMaster> locationMasters { get; set; }
        public IEnumerable<RouteMaster> routeMasters { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class TruckDetailVM1 : TruckDetail
    {
        public string RentAcHead { get;set;}
        public string PaymentHead { get; set; }
        public string TDcontrolAcHead { get; set; }
        public List<TruckDetailOtherChargeVM> otherchargesVM { get; set; }
    }

    public class TruckDetailOtherChargeVM:TruckDetailOtherCharge
    {
        public bool deleted { get; set; }
        public string OtherChargeName { get; set; }
    }
}