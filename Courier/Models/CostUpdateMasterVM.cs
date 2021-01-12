using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class CostUpdateMasterVM:CostUpdateMaster
    {
        
        public string TDNo { get; set; }
        public DateTime TDDate { get; set; }

        public int PickupCashHeadId { get; set; }
        public int ConsignorId { get; set; }
        public int ConsigneeId { get; set; }

        public int ConsignorName { get; set; }
        public int ConsigneeName { get; set; }
        public string InvoicedTo { get; set; }
        public string PaymentType { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditAccountName { get; set; }
        public string RevenueCost { get; set; }
        public int CurrencyId { get; set; }
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
}