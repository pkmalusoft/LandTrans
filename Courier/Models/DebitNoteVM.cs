using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class DebitNoteVM
    {
        public int DebitNoteId { get; set; }
        public string DebitNoteNo { get; set; }
        public DateTime? Date { get; set; }
        public int SupplierID { get; set; }
        public int AcHeadID { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceType { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Amount { get; set; }

        public string JobNo { get; set; }
        public string SupplierName { get; set; }
        public string Remarks { get; set; }
        public bool ForInvoice { get; set; }
        public string TransType { get; set; }
        public int RecPayID { get; set; }
        public int AcJournalID { get; set; }
        public string AcDetailRemarks { get; set; }
        public decimal AcDetailAmount { get; set; }
        public string AcDetailAmountType { get; set; }
        public int AcDetailHeadID { get; set; }
        public List<DebitNoteDetailVM> Details { get; set; }
    }
    public class DebitNoteDetailVM : DebitNoteDetail
    {
        public string AcHeadName { get; set; }
    }
}