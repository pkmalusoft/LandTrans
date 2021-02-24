using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using System.Dynamic;
using System.Data;
using LTMSV2.DAL;
//using Microsoft.Reporting.WebForms;

namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class DebitNoteController : Controller
    {
        SourceMastersModel MM = new SourceMastersModel();
        RecieptPaymentModel RP = new RecieptPaymentModel();
        CustomerRcieptVM cust = new CustomerRcieptVM();
        Entities1 db = new Entities1();

        EditCommanFu editfu = new EditCommanFu();
        public ActionResult Index()
        {
            var data = db.DebitNotes.ToList();

            List<DebitNoteVM> lst = new List<DebitNoteVM>();
            foreach (var item in data)
            {
                string jobcode = "";
                if (item.InvoiceType == "TR")
                {
                    var purchaseinvoice = (from d in db.SupplierInvoices where d.SupplierInvoiceID == item.InvoiceID select d).FirstOrDefault();
                    jobcode = purchaseinvoice.InvoiceNo;
                }
                else
                {
                    var purchaseinvoice = (from d in db.AcOPInvoiceDetails where d.AcOPInvoiceDetailID == item.InvoiceID select d).FirstOrDefault();
                    jobcode = purchaseinvoice.InvoiceNo;

                }

                string supplier = (from c in db.SupplierMasters where c.SupplierID == item.SupplierID select c.SupplierName).FirstOrDefault();

                DebitNoteVM v = new DebitNoteVM();
                v.DebitNoteNo = item.DebitNoteNo;
                v.DebitNoteId = item.DebitNoteID;
                v.JobNo = jobcode;
                v.Date = item.DebitNoteDate.Value;
                v.SupplierName = supplier;
                v.Amount = item.Amount.Value;
                lst.Add(v);

            }

            return View(lst);

        }


        public ActionResult Create(int id = 0)
        {
            //List<Invoices> lst = new List<Invoices>();
            //ViewBag.Supplier = db.SupplierMasters.OrderBy(x => x.SupplierName).ToList();
            //ViewBag.AcHead = db.AcHeads.OrderBy(x => x.AcHead1).ToList();
            //ViewBag.Invoice = lst;
            //return View();

            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            ViewBag.Supplier = db.SupplierMasters.OrderBy(x => x.SupplierName).ToList();
            ViewBag.AcHead = db.AcHeads.OrderBy(x => x.AcHead1).ToList();

            if (id == 0)
            {
                ViewBag.Title = "Debit Note - Create";
                DebitNoteVM vm = new DebitNoteVM();
                vm.DebitNoteNo = AccountsDAO.GetMaxDebiteNoteNo(fyearid);
                vm.Date = CommanFunctions.GetLastDayofMonth().Date;
                return View(vm);
            }
            else
            {
                ViewBag.Title = "Debit Note - Modify";
                DebitNoteVM vm = new DebitNoteVM();
                var v = db.DebitNotes.Find(id);
                vm.DebitNoteId = v.DebitNoteID;
                vm.AcJournalID = Convert.ToInt32(v.AcJournalID);
                vm.DebitNoteNo = v.DebitNoteNo;
                vm.SupplierID = Convert.ToInt32(v.SupplierID);
                vm.AcHeadID = Convert.ToInt32(v.AcHeadID);
                vm.Amount = Convert.ToDecimal(v.Amount);
                vm.Remarks = v.Remarks;
                vm.InvoiceID = Convert.ToInt32(v.InvoiceID);
                if (v.InvoiceType == "TR")
                {
                    var invoice = db.SupplierInvoices.Find(vm.InvoiceID);
                    if (invoice != null)
                    {
                        vm.InvoiceNo = invoice.InvoiceNo;
                        vm.InvoiceDate = invoice.InvoiceDate.ToString("dd/MM/yyyy");
                        vm.InvoiceAmount = Convert.ToDecimal(invoice.InvoiceTotal);
                    }
                }
                else if (v.InvoiceType == "OP")
                {
                    var invoice1 = db.AcOPInvoiceDetails.Where(cc => cc.AcOPInvoiceDetailID == vm.InvoiceID).FirstOrDefault();
                    if (invoice1 != null)
                    {
                        vm.InvoiceNo = invoice1.InvoiceNo;
                        vm.InvoiceDate = Convert.ToDateTime(invoice1.InvoiceDate).ToString("dd/MM/yyyy");
                        vm.InvoiceAmount = Convert.ToDecimal(invoice1.Amount);
                    }
                }
                //SetTradeInvoiceOfCustomer(vm.CustomerID, 0, vm.CreditNoteID);
                vm.Date = Convert.ToDateTime(v.DebitNoteDate);
                SetTradeInvoiceOfSupplier(vm.SupplierID, 0, vm.DebitNoteId);
                return View(vm);
            }
        }
        
        [HttpPost]
        public ActionResult Create(DebitNoteVM v)
        {
            AcJournalMaster ajm = new AcJournalMaster();
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            if (v.DebitNoteId > 0)
            {
                ajm = db.AcJournalMasters.Find(v.AcJournalID);
            }
            if (v.DebitNoteId == 0 || ajm != null)
            {
                int acjm = 0;
                acjm = (from c in db.AcJournalMasters orderby c.AcJournalID descending select c.AcJournalID).FirstOrDefault();

                ajm.AcJournalID = acjm + 1;
                ajm.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
                ajm.AcFinancialYearID = fyearid;
                ajm.PaymentType = 1;
                var customer = db.SupplierMasters.Find(v.SupplierID).SupplierName;
                ajm.Remarks = v.Remarks +"DN: for " + customer + " invoice : " + v.InvoiceNo;
                ajm.StatusDelete = false;
                ajm.VoucherNo = AccountsDAO.GetMaxVoucherNo("DN", fyearid);
                ajm.TransDate = v.Date;
                ajm.TransType = 1;
                ajm.VoucherType = "DN";
                db.AcJournalMasters.Add(ajm);
                db.SaveChanges();
            }

            AcJournalDetail b = new AcJournalDetail();
            b = db.AcJournalDetails.Where(cc => cc.AcJournalID == ajm.AcJournalID && cc.Amount > 0).FirstOrDefault();
            if (b == null)
            {
                b = new AcJournalDetail();
                b.AcJournalDetailID = 0;
            }
            if (b.AcJournalDetailID == 0)
            {
                int maxacj = 0;
                maxacj = (from c in db.AcJournalDetails orderby c.AcJournalDetailID descending select c.AcJournalDetailID).FirstOrDefault();

                b.AcJournalDetailID = maxacj + 1;
                b.AcJournalID = ajm.AcJournalID;
            }
            b.AcHeadID = v.AcHeadID;
            b.Amount = -1 * v.Amount;
            b.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            b.Remarks = "debit note";
            if (b.ID == 0)
            {
                db.AcJournalDetails.Add(b);
                db.SaveChanges();
            }
            else
            {
                db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            AcJournalDetail a = new AcJournalDetail();
            a = db.AcJournalDetails.Where(cc => cc.AcJournalID == ajm.AcJournalID && cc.Amount < 0).FirstOrDefault();
            if (a == null)
            {
                a = new AcJournalDetail();
                a.AcJournalDetailID = 0;
            }
            if (a.AcJournalDetailID == 0)
            {
                int maxacj = (from c in db.AcJournalDetails orderby c.AcJournalDetailID descending select c.AcJournalDetailID).FirstOrDefault();
                a.AcJournalDetailID = maxacj + 1;
                a.AcJournalID = ajm.AcJournalID;
                var customercon = db.AcHeads.Where(cc => cc.AcHead1 == "Supplier Control A/c ( Cr)").FirstOrDefault();
                a.AcHeadID = customercon.AcHeadID; ;
            }

            a.Amount =  v.Amount;
            a.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            a.Remarks = "debit note";

            if (a.ID == 0)
            {
                db.AcJournalDetails.Add(a);
                db.SaveChanges();
            }
            else
            {
                db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            var invid = 0;
            int? recpayid = 0;
            //if (v.TradingInvoice == false)
            //{
            //    int jobid = (from j in db.JobGenerations where j.JobCode == v.JobNO select j.JobID).FirstOrDefault();

            //     invid = (from c in db.JInvoices where c.JobID == jobid select c.InvoiceID).FirstOrDefault();


            //    var recpay = (from c in db.RecPayDetails where c.InvoiceID == invid select c).FirstOrDefault();
            //    if (recpay != null)
            //    {
            //        recpayid = recpay.RecPayID;
            //    }
            //}
            //else
            //{
            //    invid =Convert.ToInt32(v.JobNO);

            //  var  recpay = (from c in db.RecPayDetails where c.InvoiceID == invid select c).FirstOrDefault();
            //    if(recpay != null)
            //    {
            //        recpayid = recpay.RecPayID;
            //    }
            //}
            // invid = Convert.ToInt32(v.JobNO);
            //var ids = (from x in db.SalesInvoiceDetails where x.SalesInvoiceID == invid select (int?)x.SalesInvoiceDetailID).ToList();

            // recpayid = (from c in db.RecPayDetails where ids.Contains(c.InvoiceID) select c.RecPayID).FirstOrDefault().Value;

            DebitNote d = new DebitNote();
            if (v.DebitNoteId == 0)
            {
                int maxid = 0;

                var data = (from c in db.DebitNotes orderby c.DebitNoteID descending select c).FirstOrDefault();

                if (data == null)
                {
                    maxid = 1;
                }
                else
                {
                    maxid = data.DebitNoteID + 1;
                }

                d.DebitNoteID=maxid;
                d.DebitNoteNo = AccountsDAO.GetMaxDebiteNoteNo(fyearid);
            }
            d.InvoiceID = v.InvoiceID;
            d.InvoiceType = v.InvoiceType;
            d.DebitNoteDate = v.Date;
            d.Amount = v.Amount;
            d.AcJournalID = ajm.AcJournalID;
            d.FYearID = Convert.ToInt32(Session["fyearid"].ToString());
            d.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            d.RecPayID = recpayid;
            d.AcHeadID = v.AcHeadID;
            d.SupplierID = v.SupplierID;
            //d.statusclose = false;
            d.InvoiceType = v.InvoiceType;
            d.Remarks = v.Remarks;
            d.IsShipping = true;
            if (v.DebitNoteId== 0)
            {
                db.DebitNotes.Add(d);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Successfully Added Debit Note";
            }
            else
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Successfully Updated Debit Note";
            }

            return RedirectToAction("Index", "DebitNote");





        }
             [HttpPost]
        public JsonResult GetTradeInvoiceOfSupplier(int? ID, decimal? amountreceived)
        {
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());

            DateTime fromdate = Convert.ToDateTime(Session["FyearFrom"].ToString());
            DateTime todate = Convert.ToDateTime(Session["FyearTo"].ToString());
            var receipts = (from d in db.RecPays where d.FYearID == fyearid && d.SupplierID == ID select d.FMoney).Sum();
            var receiptdetails = (from d in db.RecPays join c in db.RecPayDetails on d.RecPayID equals c.RecPayID where d.FYearID == fyearid && d.SupplierID == ID && c.InvoiceID > 0 && c.Amount < 0 select (-1 * c.Amount)).Sum();

            var advance = receipts - receiptdetails;
            var salesinvoice = new List<CustomerTradeReceiptVM>();

            var AllOPInvoices = (from d in db.AcOPInvoiceDetails join m in db.AcOPInvoiceMasters on d.AcOPInvoiceMasterID equals m.AcOPInvoiceMasterID where m.AcFinancialYearID == fyearid && m.StatusSDSC != "C" && m.PartyID == ID && d.RecPayDetailId == null && (d.RecPayStatus == null || d.RecPayStatus < 2) select d).ToList();

            foreach (var item in AllOPInvoices)
            {
                decimal? totamt = 0;
                decimal? totamtpaid = 0;
                decimal? totadjust = 0;
                decimal? CreditAmount = 0;
                var allrecpay = (from d in db.RecPayDetails where d.AcOPInvoiceDetailID == item.AcOPInvoiceDetailID select d).ToList();
                totamtpaid = allrecpay.Sum(d => d.Amount) * -1;
                totadjust = allrecpay.Sum(d => d.AdjustmentAmount);
                totamt = totamtpaid + totadjust + CreditAmount;
                var Invoice = new CustomerTradeReceiptVM();
                Invoice.AcOPInvoiceDetailID = item.AcOPInvoiceDetailID;
                Invoice.SalesInvoiceID = 0;
                Invoice.InvoiceType = "OP";
                Invoice.JobCode = "OP" + item.AcOPInvoiceDetailID.ToString();
                Invoice.InvoiceNo = item.InvoiceNo; ;
                Invoice.InvoiceAmount = item.Amount * -1;
                Invoice.date = item.InvoiceDate;
                Invoice.DateTime = Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy");
                Invoice.AmountReceived = totamt;
                Invoice.Balance = Invoice.InvoiceAmount - totamtpaid;
                Invoice.AdjustmentAmount = totadjust;
                Invoice.Amount = 0;
                if (Invoice.Balance > 0)
                {
                    if (amountreceived != null)
                    {
                        if (amountreceived >= Invoice.Balance)
                        {
                            Invoice.Amount = Invoice.Balance;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else if (amountreceived > 0)
                        {
                            Invoice.Amount = amountreceived;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else
                        {
                            Invoice.Amount = 0;
                        }
                    }
                    salesinvoice.Add(Invoice);
                }

            }

            //transaction
            var AllInvoices = (from d in db.SupplierInvoices where d.SupplierID == ID select d).ToList();
            foreach (var item in AllInvoices)
            {
                //var invoicedeails = (from d in db.SalesInvoiceDetails where d.SalesInvoiceID == item.SalesInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
                var invoicedeails = (from d in db.SupplierInvoiceDetails where d.SupplierInvoiceID == item.SupplierInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
                //where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
                decimal? totamt = 0;
                decimal? totamtpaid = 0;
                decimal? totadjust = 0;
                decimal? CreditAmount = 0;
                //foreach (var det in invoicedeails)
                //{
                var allrecpay = (from d in db.RecPayDetails where d.InvoiceID == item.SupplierInvoiceID select d).ToList();
                totamtpaid = allrecpay.Sum(d => d.Amount) * -1;
                totadjust = allrecpay.Sum(d => d.AdjustmentAmount);
                //var CreditNote = (from d in db.CreditNotes where d.InvoiceID == det.SalesInvoiceDetailID && d.CustomerID == item.CustomerID select d).ToList();
                //var CreditNote = (from d in db.CreditNotes where d.InvoiceID == det.CustomerInvoiceDetailID && d.CustomerID == item.CustomerID select d).ToList();

                //if (CreditNote.Count > 0)
                //{
                //    CreditAmount = CreditNote.Sum(d => d.Amount);
                //}
                totamt = totamtpaid + totadjust + CreditAmount;
                //}

                var Invoice = new CustomerTradeReceiptVM();
                //Invoice.JobID = det.JobID;
                Invoice.JobCode = "TR" + item.SupplierInvoiceID.ToString();
                Invoice.SalesInvoiceID = item.SupplierInvoiceID; // SalesInvoiceID;
                Invoice.InvoiceNo = item.InvoiceNo;
                Invoice.InvoiceType = "TR";
                //Invoice.SalesInvoiceDetailID = det.CustomerInvoiceDetailID;
                Invoice.InvoiceAmount = item.InvoiceTotal; // CourierCharge;
                Invoice.date = item.InvoiceDate;
                Invoice.DateTime = item.InvoiceDate.ToString("dd/MM/yyyy");
                //var RecPay = (from d in db.RecPayDetails where d.RecPayDetailID == det.RecPayDetailId select d).FirstOrDefault();

                Invoice.AmountReceived = totamt;
                Invoice.Balance = Invoice.InvoiceAmount - totamt;
                Invoice.AdjustmentAmount = totadjust;
                Invoice.Amount = 0;
                if (Invoice.Balance > 0)
                {
                    if (amountreceived != null)
                    {
                        if (amountreceived >= Invoice.Balance)
                        {
                            Invoice.Amount = Invoice.Balance;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else if (amountreceived > 0)
                        {
                            Invoice.Amount = amountreceived;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else
                        {
                            Invoice.Amount = 0;
                        }
                    }
                    salesinvoice.Add(Invoice);
                }
            }
            Session["SupplierInvoice"] = salesinvoice;
            return Json(salesinvoice, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult SetTradeInvoiceOfSupplier(int? ID, decimal? amountreceived, int DebitNoteID)
        {
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());

            DateTime fromdate = Convert.ToDateTime(Session["FyearFrom"].ToString());
            DateTime todate = Convert.ToDateTime(Session["FyearTo"].ToString());
            var receipts = (from d in db.RecPays where d.FYearID == fyearid && d.SupplierID == ID select d.FMoney).Sum();
            var receiptdetails = (from d in db.RecPays join c in db.RecPayDetails on d.RecPayID equals c.RecPayID where d.FYearID == fyearid && d.SupplierID == ID && c.InvoiceID > 0 && c.Amount < 0 select (-1 * c.Amount)).Sum();

            var advance = receipts - receiptdetails;
            var salesinvoice = new List<CustomerTradeReceiptVM>();

            var AllOPInvoices = (from d in db.AcOPInvoiceDetails join m in db.AcOPInvoiceMasters on d.AcOPInvoiceMasterID equals m.AcOPInvoiceMasterID where m.AcFinancialYearID == fyearid && m.StatusSDSC != "C" && m.PartyID == ID && d.RecPayDetailId == null && (d.RecPayStatus == null || d.RecPayStatus < 2) select d).ToList();

            foreach (var item in AllOPInvoices)
            {
                decimal? totamt = 0;
                decimal? totamtpaid = 0;
                decimal? totadjust = 0;
                decimal? CreditAmount = 0;
                var allrecpay = (from d in db.RecPayDetails where d.AcOPInvoiceDetailID == item.AcOPInvoiceDetailID select d).ToList();
                totamtpaid = allrecpay.Sum(d => d.Amount) * -1;
                totadjust = allrecpay.Sum(d => d.AdjustmentAmount);
                totamt = totamtpaid + totadjust + CreditAmount;
                var Invoice = new CustomerTradeReceiptVM();
                Invoice.AcOPInvoiceDetailID = item.AcOPInvoiceDetailID;
                Invoice.SalesInvoiceID = 0;
                Invoice.InvoiceType = "OP";
                Invoice.JobCode = "OP" + item.AcOPInvoiceDetailID.ToString();
                Invoice.InvoiceNo = item.InvoiceNo; ;
                Invoice.InvoiceAmount = item.Amount * -1;
                Invoice.date = item.InvoiceDate;
                Invoice.DateTime = Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy");
                Invoice.AmountReceived = totamt;
                Invoice.Balance = Invoice.InvoiceAmount - totamtpaid;
                Invoice.AdjustmentAmount = totadjust;
                Invoice.Amount = 0;
                if (Invoice.Balance > 0)
                {
                    if (amountreceived != null)
                    {
                        if (amountreceived >= Invoice.Balance)
                        {
                            Invoice.Amount = Invoice.Balance;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else if (amountreceived > 0)
                        {
                            Invoice.Amount = amountreceived;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else
                        {
                            Invoice.Amount = 0;
                        }
                    }
                    salesinvoice.Add(Invoice);
                }

            }

            //transaction
            var AllInvoices = (from d in db.SupplierInvoices where d.SupplierID == ID select d).ToList();
            foreach (var item in AllInvoices)
            {
                //var invoicedeails = (from d in db.SalesInvoiceDetails where d.SalesInvoiceID == item.SalesInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
                var invoicedeails = (from d in db.SupplierInvoiceDetails where d.SupplierInvoiceID == item.SupplierInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
                //where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
                decimal? totamt = 0;
                decimal? totamtpaid = 0;
                decimal? totadjust = 0;
                decimal? CreditAmount = 0;
                //foreach (var det in invoicedeails)
                //{
                var allrecpay = (from d in db.RecPayDetails where d.InvoiceID == item.SupplierInvoiceID select d).ToList();
                totamtpaid = allrecpay.Sum(d => d.Amount) * -1;
                totadjust = allrecpay.Sum(d => d.AdjustmentAmount);
                var DebitNote = (from d in db.DebitNotes where d.InvoiceID == item.SupplierInvoiceID && d.SupplierID == item.SupplierID select d).ToList();
                //var CreditNote = (from d in db.CreditNotes where d.InvoiceID == det.CustomerInvoiceDetailID && d.CustomerID == item.CustomerID select d).ToList();

                if (DebitNote.Count > 0)
                {
                    CreditAmount = DebitNote.Sum(d => d.Amount);
                }
                totamt = totamtpaid + totadjust + CreditAmount;
                //}

                var Invoice = new CustomerTradeReceiptVM();
                //Invoice.JobID = det.JobID;
                Invoice.JobCode = "TR" + item.SupplierInvoiceID.ToString();
                Invoice.SalesInvoiceID = item.SupplierInvoiceID; // SalesInvoiceID;
                Invoice.InvoiceNo = item.InvoiceNo;
                Invoice.InvoiceType = "TR";
                //Invoice.SalesInvoiceDetailID = det.CustomerInvoiceDetailID;
                Invoice.InvoiceAmount = item.InvoiceTotal; // CourierCharge;
                Invoice.date = item.InvoiceDate;
                Invoice.DateTime = item.InvoiceDate.ToString("dd/MM/yyyy");
                //var RecPay = (from d in db.RecPayDetails where d.RecPayDetailID == det.RecPayDetailId select d).FirstOrDefault();

                Invoice.AmountReceived = totamt;
                Invoice.Balance = Invoice.InvoiceAmount - totamt;
                Invoice.AdjustmentAmount = totadjust;
                Invoice.Amount = 0;
                if (Invoice.Balance > 0)
                {
                    if (amountreceived != null)
                    {
                        if (amountreceived >= Invoice.Balance)
                        {
                            Invoice.Amount = Invoice.Balance;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else if (amountreceived > 0)
                        {
                            Invoice.Amount = amountreceived;
                            amountreceived = amountreceived - Invoice.Amount;
                        }
                        else
                        {
                            Invoice.Amount = 0;
                        }
                    }
                    salesinvoice.Add(Invoice);
                }
            }
            Session["SupplierInvoice"] = salesinvoice;
            return Json(salesinvoice, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDebitNote(int id)
        {
            //int k = 0;
            if (id != 0)
            {
                string result = AccountsDAO.DeleteDebiteNote(id);                
                TempData["SuccessMsg"] = result;
                
            }

            return RedirectToAction("Index", "DebitNote");

        }
        public JsonResult GetInvoiceNo(string term)
        {
            var salesinvoice = new List<CustomerTradeReceiptVM>();
            var lst = new List<CustomerTradeReceiptVM>();
            salesinvoice = (List<CustomerTradeReceiptVM>)Session["SupplierInvoice"];
            if (term.Trim() != "")
            {
                lst = salesinvoice.Where(cc => cc.InvoiceNo.Contains(term)).OrderBy(cc => cc.InvoiceNo).ToList();
            }
            else
            {
                lst = salesinvoice.OrderBy(cc => cc.InvoiceNo).ToList();
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public class Invoices
        {
            public int InvoiceNo { get; set; }
            public bool Istrading { get; set; }
            public string InvoiceNum { get; set; }
            public decimal? Amount { get; set; }
        }

        [HttpPost]
        public ActionResult Create1(DebitNoteVM v)
        {
            AcJournalMaster ajm = new AcJournalMaster();

            int acjm = 0;
            acjm = (from c in db.AcJournalMasters orderby c.AcJournalID descending select c.AcJournalID).FirstOrDefault();

            ajm.AcJournalID = acjm + 1;

            ajm.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            ajm.AcFinancialYearID = Convert.ToInt32(Session["fyearid"].ToString());
            ajm.PaymentType = 1;
            ajm.Remarks = "Debit Note";
            ajm.StatusDelete = false;
            ajm.TransDate = v.Date;
            ajm.VoucherNo = "DB-" + ajm.AcJournalID;
            ajm.TransType = 1;
            ajm.VoucherType = "";

            db.AcJournalMasters.Add(ajm);
            db.SaveChanges();


            AcJournalDetail a = new AcJournalDetail();

            int maxacj = 0;
            maxacj = (from c in db.AcJournalDetails orderby c.AcJournalDetailID descending select c.AcJournalDetailID).FirstOrDefault();

            a.AcJournalDetailID = maxacj + 1;

            a.AcJournalID = ajm.AcJournalID;
            a.AcHeadID = v.AcHeadID;
            a.Amount = v.Amount;
            a.BranchID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            a.Remarks = "";

            db.AcJournalDetails.Add(a);
            db.SaveChanges();


            AcJournalDetail b = new AcJournalDetail();
            maxacj = (from c in db.AcJournalDetails orderby c.AcJournalDetailID descending select c.AcJournalDetailID).FirstOrDefault();
            b.AcJournalDetailID = maxacj + 1;
            b.AcJournalID = ajm.AcJournalID;
            b.AcHeadID = v.AcHeadID;
            b.Amount = -v.Amount;
            b.BranchID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            b.Remarks = "";





            db.AcJournalDetails.Add(b);
            db.SaveChanges();

            //var ids = (from x in db.PurchaseInvoiceDetails where x.PurchaseInvoiceID == v.InvoiceNo select (int?)x.PurchaseInvoiceDetailID).ToList();

            //int recpayid = (from c in db.RecPayDetails where ids.Contains(c.InvoiceID) select c.RecPayID).FirstOrDefault().Value;
            int recpayid = 0;
            int max = 0;

            var data = (from c in db.DebitNotes orderby c.DebitNoteID descending select c).FirstOrDefault();
            if (data == null)
            {
                max = 1;
            }
            else
            {
                max = data.DebitNoteID + 1;
            }


            DebitNote d = new DebitNote();
            d.DebitNoteID = max + 1;
            d.DebitNoteNo = "D-" + (max + 1);
            d.InvoiceID = v.InvoiceID;
            d.DebitNoteDate = v.Date;
            d.Amount = v.Amount;
            d.AcJournalID = ajm.AcJournalID;
            d.FYearID = Convert.ToInt32(Session["fyearid"].ToString());
            d.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            d.RecPayID = recpayid;
            d.AcHeadID = v.AcHeadID;
            d.SupplierID = v.SupplierID;
            d.InvoiceType = "S";
            d.IsShipping = true;
            d.Remarks = v.Remarks;
            db.DebitNotes.Add(d);
            db.SaveChanges();

            TempData["SuccessMsg"] = "Successfully Added Debit Note";
            return RedirectToAction("List", "DebitNote");




        }
        public ActionResult GetInvoices(int id, string term)
        {
            List<Invoices> lst = new List<Invoices>();


            var data1 = (from c in db.PurchaseInvoices where c.SupplierID == id && c.PurchaseInvoiceNo.Contains(term) select c).ToList();
            foreach (var item in data1)
            {
                var purchaseinvoicedetails = (from d in db.PurchaseInvoiceDetails where d.PurchaseInvoiceID == item.PurchaseInvoiceID select d).ToList();

                Invoices s = new Invoices();
                s.InvoiceNo = item.PurchaseInvoiceID;
                s.Istrading = true;
                s.InvoiceNum = item.PurchaseInvoiceNo + "/ " + purchaseinvoicedetails.Sum(d => d.Rate);

                lst.Add(s);

            }
            //lst = lst.Where(d => d.InvoiceNum.Contains(term)).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public class Amounts
        {
            public decimal InvAmt { get; set; }
            public decimal AmtPaid { get; set; }
        }

        public JsonResult GetAmount(int invno)
        {
            Amounts a = new Amounts();
            var purchaseinvoicedetails = (from d in db.PurchaseInvoiceDetails where d.PurchaseInvoiceID == invno select d).ToList();

            decimal pamt = Math.Abs((from x in db.RecPayDetails where x.InvoiceID == invno select x.Amount).FirstOrDefault().Value);

            a.InvAmt = Convert.ToDecimal(purchaseinvoicedetails.Sum(d => d.Rate));
            a.AmtPaid = pamt;

            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAmountByinvono(int invno, bool IsTrading)
        {
            Amounts a = new Amounts();
            decimal iamt = 0;
            decimal pamt = 0;

            var PurchaseInvoice = (from d in db.PurchaseInvoices where d.PurchaseInvoiceID == invno select d).FirstOrDefault();
            var PinDetails = (from c in db.PurchaseInvoiceDetails where c.PurchaseInvoiceID == PurchaseInvoice.PurchaseInvoiceID select c).ToList();
            List<int?> pinvoiceids = PinDetails.Select(d => (int?)d.PurchaseInvoiceDetailID).ToList();

            iamt = Convert.ToDecimal(PinDetails.Sum(d => d.Rate));
            var recpay = (from x in db.RecPayDetails where pinvoiceids.Contains(x.InvoiceID) select x).ToList();
            if (recpay.Count > 0)
            {
                pamt = Convert.ToDecimal(Math.Abs(recpay.Sum(s => s.Amount.Value)));
            }



            a.InvAmt = iamt;
            a.AmtPaid = pamt;

            return Json(a, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult ServiceIndex(DebitNoteVM v)
        //{
        //    AcJournalMaster ajm = new AcJournalMaster();

        //    int acjm = 0;
        //    acjm = (from c in db.AcJournalMasters orderby c.AcJournalID descending select c.AcJournalID).FirstOrDefault();

        //    ajm.AcJournalID = acjm + 1;

        //    ajm.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
        //    ajm.AcFinancialYearID = Convert.ToInt32(Session["fyearid"].ToString());
        //    ajm.PaymentType = 1;
        //    ajm.Remarks = "Debit Note";
        //    ajm.StatusDelete = false;
        //    ajm.TransDate = v.Date;
        //    ajm.VoucherNo = "DB-" + ajm.AcJournalID;
        //    ajm.TransType = 1;
        //    ajm.VoucherType = "";

        //    db.AcJournalMasters.Add(ajm);
        //    db.SaveChanges();


        //    AcJournalDetail a = new AcJournalDetail();

        //    int maxacj = 0;
        //    maxacj = (from c in db.AcJournalDetails orderby c.AcJournalDetailID descending select c.AcJournalDetailID).FirstOrDefault();

        //    a.AcJournalDetailID = maxacj + 1;

        //    a.AcJournalID = ajm.AcJournalID;
        //    a.AcHeadID = v.AcHeadID;
        //    a.Amount = v.Amount;
        //    a.BranchID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
        //    a.Remarks = "";

        //    db.AcJournalDetails.Add(a);
        //    db.SaveChanges();


        //    AcJournalDetail b = new AcJournalDetail();
        //    maxacj = (from c in db.AcJournalDetails orderby c.AcJournalDetailID descending select c.AcJournalDetailID).FirstOrDefault();
        //    b.AcJournalDetailID = maxacj + 1;
        //    b.AcJournalID = ajm.AcJournalID;
        //    b.AcHeadID = v.AcHeadID;
        //    b.Amount = -v.Amount;
        //    b.BranchID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
        //    b.Remarks = "";





        //    db.AcJournalDetails.Add(b);
        //    db.SaveChanges();

        //    var ids = (from x in db.PurchaseInvoiceDetails where x.PurchaseInvoiceID == v.InvoiceNo select (int?)x.PurchaseInvoiceDetailID).ToList();

        //    int recpayid = (from c in db.RecPayDetails where ids.Contains(c.InvoiceID) select c.RecPayID).FirstOrDefault().Value;

        //    int max = 0;

        //    var data = (from c in db.DebitNotes orderby c.DebitNoteID descending select c).FirstOrDefault();
        //    if (data == null)
        //    {
        //        max = 1;
        //    }
        //    else
        //    {
        //        max = data.DebitNoteID + 1;
        //    }


        //    DebitNote d = new DebitNote();

        //    d.DebitNoteID = max + 1;
        //    d.DebitNoteNo = "D-" + (max + 1);
        //    d.InvoiceID = v.InvoiceID;
        //    d.DebitNoteDate = v.Date;
        //    d.Amount = v.Amount;
        //    d.AcJournalID = ajm.AcJournalID;
        //    d.FYearID = Convert.ToInt32(Session["fyearid"].ToString());
        //    d.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
        //    d.RecPayID = recpayid;
        //    d.AcHeadID = v.AcHeadID;
        //    d.SupplierID = v.SupplierID;
        //    d.InvoiceType = "S";
        //    d.IsShipping = false;
        //    db.DebitNotes.Add(d);
        //    db.SaveChanges();

        //    TempData["SuccessMsg"] = "Successfully Added Debit Note";
        //    return RedirectToAction("ServiceList", "DebitNote");




        //}

    }
}