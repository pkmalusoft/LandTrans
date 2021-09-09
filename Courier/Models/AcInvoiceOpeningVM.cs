using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class AcInvoiceOpeningVM :AcOPInvoiceMaster
    {
        public string PartyName { get; set; }
        public string PartyType { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public List<AcInvoiceOpeningDetailVM> InvoiceDetailVM { get; set; }
    }
    public class AcInvoiceOpeningDetailVM : AcOPInvoiceDetail
    {
        public bool IsDeleted { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }

    public class YearEndProcessSearch
    {
        public string CurrentYear { get; set; }
        public string CurrentStartDate { get; set; }
        public string CurrentEndDate { get; set; }

        public string NextYear { get; set; }
        public string NextYearStartDate { get; set; }
        public string NextYearEndDate { get; set; }
        public int CurrentFinancialYearId { get; set; }
        public int NextFinancialYearId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string ProcessType { get; set; }
        public string Remarks { get; set; }
        public string Comments { get; set; }
        public List<YearEndProcessAccounts> OpeningDetails { get; set; }
        public List<YearEndProcessIncomeExpense> IncomeExpDetails { get; set; }
        public List<YearEndProcessPL> PLDetails { get; set; }
        public List<YearEndProcessCustomer> CustomerInvDetails { get; set; }
        public List<YearEndProcessSupplier> SupplierInvDetails { get; set; }
    }

    public class YearEndProcessAccounts
    {
        public string Particulars { get; set; }

        public string AcHeadName { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal Transactions { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal NextYearOpening { get; set; }
        public decimal Credit { get; set; }
    }
    public class YearEndProcessIncomeExpense
    {
        public int AcHeadId { get; set; }
        public string AcHeadName { get; set; }
        public decimal ClosingBalance { get; set; }


    }
    public class YearEndProcessPL
    {
        public string VoucherNo { get; set; }
        public int AcHeadId { get; set; }
        public string AcHeadName { get; set; }
        public decimal Amount { get; set; }
        public bool updatestatus { get; set; }

    }

    public class YearEndProcessCustomer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal Amount { get; set; }


    }

    public class YearEndProcessSupplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal Amount { get; set; }


    }
}