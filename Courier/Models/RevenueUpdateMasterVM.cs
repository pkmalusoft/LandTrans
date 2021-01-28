using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class RevenueUpdateMasterVM:RevenueUpdateMaster
    {
        
        public string ConsignmentNo { get; set; }
        public DateTime ConsignmentDate { get; set; }

        public int PickupCashHeadId { get; set; }
        public int ConsignorId { get; set; }
        public int ConsigneeId { get; set; }
        public string Remarks { get; set; }
        public string ConsignorName { get; set; }
        public string ConsigneeName { get; set; }
        public string InvoiceTo { get; set; }
        public string PaymentType { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int CurrencyId { get; set; }
        public string DebitAccountName { get; set; }
        public int DebitAccountId { get; set; }

        public string DebitCashAccountName { get; set; }
        public int DebitCashAccountId { get; set; }
        public string CreditAccountName { get; set; }
        public string CustomerName { get; set; }
        public string RevenueCost { get; set; }
        public List<RevenueUpdateDetailVM> DetailVM { get; set; }
    }

    public class RevenueUpdateDetailVM :RevenueUpdateDetail
    {
        //public string InvoicedTo { get; set; }
        public string CustomerName { get; set; }
        public string PaymentType { get; set; }
        public string Currency { get; set; }
        public string RevenueCost { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditAccountName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
