using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class CostUpdateMasterVM:CostUpdateMaster
    {
        public int MasterID { get; set; }

        public string TDNo { get; set; }
        public DateTime TDDate { get; set; }                        
        public string DriverName { get; set; }
        public string RegNo { get; set; }        
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditAccountName { get; set; }
        public string RevenueCost { get; set; }
        public int CurrencyId { get; set; }

        public string TDRemarks { get; set; }
        public List<CostUpdateDetailVM> DetailVM { get; set; }
    }

    public class CostUpdateDetailVM :CostUpdateDetail
    {
        public string InvoicedTo { get; set; }
        public string Currency { get; set; }
        public string RevenueCost { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditAccountName { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CostUpdateConsignmentVM : CostUpdateConsignment
    {
        public int TruckDetailID { get; set; }
        public string ConsignmentNo { get; set; }
        public DateTime ConsignmentDate { get; set; }
    }
}