using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class SupplierInvoiceVM:SupplierInvoice
    {
        public string SupplierName { get; set; }
        public string SupplierType { get; set; }
        public decimal Amount { get; set; }
    }
}