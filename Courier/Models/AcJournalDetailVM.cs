﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class VoucherTypeVM
    {
        public string TypeName { get; set; }
    }
    public class AcJournalMasterVM:AcJournalMaster
    {
        public decimal Amount { get; set; }
    }
    public class AcJournalDetailVM
    {
        public bool IsDeleted { get; set; }
        public int AcHeadID { get; set; }
        public string AcHead { get; set; }
        public string Rem { get; set; }
        public decimal Amt { get; set; }
        public decimal TaxPercent { get; set; }

        public decimal TaxAmount { get; set; }
        public int AcJournalDetID { get; set; }        
        public bool AmountIncludingTax { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }

//        public int[] SelectedValues { get; set; }
        public string Consignments { get; set; }
        public string InScans { get; set; }
        public List<AcExpenseAllocationVM> AcExpAllocationVM { get; set; }
    }
      
}