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
        public string InvoicedTo { get; set; }
        public string PaymentType { get; set; }
        public decimal ExchangeReate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditAccountName { get; set; }
        public string RevenueCost { get; set; }
        public List<RevenueUpdateDetailVM> DetailVM { get; set; }
    }

    public class RevenueUpdateDetailVM :RevenueUpdateDetail
    {
        public string InvoicedTo { get; set; }
        public string Currency { get; set; }
        public string RevenueCost { get; set; }
        public string DebitAccountName { get; set; }
        public string CreditAccountName { get; set; }
    }
}