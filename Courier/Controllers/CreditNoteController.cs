using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using LTMSV2.DAL;

namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class CreditNoteController : Controller
    {

        SourceMastersModel objSourceMastersModel = new SourceMastersModel();

        Entities1 db = new Entities1();

        public ActionResult Index()
        {
            var data = db.CreditNotes.ToList().OrderByDescending(c => c.CreditNoteDate).ToList();

            List<CreditNoteVM> lst = new List<CreditNoteVM>();

            foreach (var item in data)
            {
                ////var job = (from c in db.JInvoices where c.InvoiceID == item.InvoiceID select c).FirstOrDefault();
                //string jobcode = "";

                //if (item.InvoiceType == "TR")
                //{
                //    var purchaseinvoice = (from d in db.CustomerInvoices where d.CustomerInvoiceID == item.InvoiceID select d).FirstOrDefault();
                //    if (purchaseinvoice!=null)
                //        jobcode = purchaseinvoice.CustomerInvoiceNo;
                //}
                //else if (item.InvoiceType == "OP")
                //{
                //    var purchaseinvoice = (from d in db.AcOPInvoiceDetails where d.AcOPInvoiceDetailID == item.InvoiceID select d).FirstOrDefault();
                //    if (purchaseinvoice!=null)
                //        jobcode = purchaseinvoice.InvoiceNo;

                //}

                string customer = (from c in db.CustomerMasters where c.CustomerID == item.CustomerID && (c.CustomerType == "CR" || c.CustomerType == "CL") select c.CustomerName).FirstOrDefault();

                CreditNoteVM v = new CreditNoteVM();
                //v.JobNO = jobcode;

                v.CreditNoteNo = item.CreditNoteNo;
                v.CreditNoteID = item.CreditNoteID;
                v.Date = item.CreditNoteDate.Value;
                v.CustomerName = customer;
                v.Description = item.Description;
                v.Amount = item.Amount.Value;
                lst.Add(v);

            }

            return View(lst);

        }


        public ActionResult Create(int id = 0)
        {
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            //ViewBag.customer = db.CustomerMasters.Where(d => d.CustomerType == "CR").OrderBy(x => x.CustomerName).ToList();
            ViewBag.achead = db.AcHeads.ToList();

            if (id == 0)
            {
                ViewBag.Title = "CREDIT NOTE/CUSTOMER JOURNAL";
                CreditNoteVM vm = new CreditNoteVM();
                vm.CreditNoteNo = AccountsDAO.GetMaxCreditNoteNo(fyearid);
                vm.Date = CommanFunctions.GetLastDayofMonth().Date;
                vm.TransType = "";
                vm.AcHeadID = 52; //Customer control account
                vm.AmountType = "0";
                vm.AcDetailAmountType = "1";
                List<CreditNoteDetailVM> list = new List<CreditNoteDetailVM>();
                vm.Details = list;

                Session["CreditNoteDetail"] = list;

                return View(vm);
            }
            else
            {
                ViewBag.Title = "CREDIT NOTE/CUSTOMER JOURNAL - Modify";
                CreditNoteVM vm = new CreditNoteVM();
                var v = db.CreditNotes.Find(id);
                vm.CreditNoteID = v.CreditNoteID;
                vm.CreditNoteNo = v.CreditNoteNo;
                vm.Date = Convert.ToDateTime(v.CreditNoteDate);
                vm.AcJournalID = Convert.ToInt32(v.AcJournalID);
                vm.CustomerID = Convert.ToInt32(v.CustomerID);
                var customer = db.CustomerMasters.Find(v.CustomerID).CustomerName;
                if (customer != null)
                    vm.CustomerName = customer;
                vm.AcHeadID = Convert.ToInt32(v.AcHeadID);
                vm.Amount = Convert.ToDecimal(v.Amount);
                vm.Description = v.Description;
                vm.TransType = v.TransType;
                if (v.RecPayID != null && v.RecPayID != 0)
                    vm.RecPayID = Convert.ToInt32(v.RecPayID);
                else
                    vm.RecPayID = 0;

                var detaillist = (from c in db.CreditNoteDetails join d in db.AcHeads on c.AcHeadID equals d.AcHeadID where c.CreditNoteID == v.CreditNoteID select new CreditNoteDetailVM { AcHeadID = c.AcHeadID, AcHeadName = d.AcHead1, Amount = c.Amount, Remarks = c.Remarks }).ToList();
                vm.Details = detaillist;
                Session["CreditNoteDetail"] = detaillist;
                if (v.InvoiceID != null && v.InvoiceID != 0 && v.TransType == "CN")
                {
                    vm.InvoiceID = Convert.ToInt32(v.InvoiceID);
                    vm.InvoiceType = v.InvoiceType;
                    vm.ForInvoice = true;


                    SetTradeInvoiceOfCustomer(vm.CustomerID, 0, vm.CreditNoteID, vm.TransType);
                    List<CustomerTradeReceiptVM> lst = (List<CustomerTradeReceiptVM>)Session["CustomerInvoice"];

                    if (v.InvoiceType == "TR")
                    {
                        var invoice = lst.Where(cc => cc.SalesInvoiceID == vm.InvoiceID && cc.InvoiceType == "TR").FirstOrDefault();
                        if (invoice != null)
                        {
                            vm.InvoiceNo = invoice.InvoiceNo;
                            vm.InvoiceDate = invoice.DateTime;
                            vm.InvoiceAmount = Convert.ToDecimal(invoice.InvoiceAmount);
                            vm.ReceivedAmount = Convert.ToDecimal(invoice.AmountReceived);
                        }
                    }
                    else if (v.InvoiceType == "OP")
                    {
                        //var invoice1 = db.AcOPInvoiceDetails.Where(cc=>cc.AcOPInvoiceDetailID ==vm.InvoiceID).FirstOrDefault();
                        //if (invoice1 != null)
                        //{
                        //    vm.InvoiceNo = invoice1.InvoiceNo;
                        //    vm.InvoiceDate = Convert.ToDateTime(invoice1.InvoiceDate).ToString("dd/MM/yyyy");
                        //    vm.InvoiceAmount = Convert.ToDecimal(invoice1.Amount);
                        //}
                        var invoice = lst.Where(cc => cc.SalesInvoiceID == vm.InvoiceID && cc.InvoiceType == "OP").FirstOrDefault();
                        vm.InvoiceNo = invoice.InvoiceNo;
                        vm.InvoiceDate = invoice.DateTime;
                        vm.InvoiceAmount = Convert.ToDecimal(invoice.InvoiceAmount);
                        vm.ReceivedAmount = Convert.ToDecimal(invoice.AmountReceived);
                    }
                }
                else if (v.RecPayID != null && v.RecPayID != 0 && v.TransType == "CJ")
                {
                    vm.InvoiceID = Convert.ToInt32(v.RecPayID);
                    vm.InvoiceType = v.InvoiceType;
                    vm.ForInvoice = true;

                    SetTradeInvoiceOfCustomer(vm.CustomerID, 0, vm.CreditNoteID, vm.TransType);
                    List<CustomerTradeReceiptVM> lst = (List<CustomerTradeReceiptVM>)Session["CustomerInvoice"];

                    if (v.InvoiceType == "TR")
                    {
                        var invoice = lst.Where(cc => cc.SalesInvoiceID == vm.InvoiceID && cc.InvoiceType == "TR").FirstOrDefault();
                        if (invoice != null)
                        {
                            vm.InvoiceNo = invoice.InvoiceNo;
                            vm.InvoiceDate = invoice.DateTime;
                            vm.InvoiceAmount = Convert.ToDecimal(invoice.InvoiceAmount);
                            vm.ReceivedAmount = Convert.ToDecimal(invoice.AmountReceived);
                        }
                    }
                    else if (v.InvoiceType == "OP")
                    {
                        var invoice = lst.Where(cc => cc.SalesInvoiceID == vm.InvoiceID && cc.InvoiceType == "OP").FirstOrDefault();
                        vm.InvoiceNo = invoice.InvoiceNo;
                        vm.InvoiceDate = invoice.DateTime;
                        vm.InvoiceAmount = Convert.ToDecimal(invoice.InvoiceAmount);
                        vm.ReceivedAmount = Convert.ToDecimal(invoice.AmountReceived);
                    }
                }
                else
                {
                    vm.ForInvoice = false;
                }


                vm.Date = Convert.ToDateTime(v.CreditNoteDate);
                //vm.
                return View(vm);

            }


        }

        [HttpPost]
        public ActionResult Create(CreditNoteVM v)
        {
            var userid = Convert.ToInt32(Session["UserID"]);
            int BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            AcJournalMaster ajm = new AcJournalMaster();
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            if (v.AcJournalID > 0)
            {
                ajm = db.AcJournalMasters.Find(v.AcJournalID);
                var ajmd = db.AcJournalDetails.Where(cc => cc.AcJournalID == v.AcJournalID).ToList();
                db.AcJournalDetails.RemoveRange(ajmd);
                db.AcJournalMasters.Remove(ajm);
                db.SaveChanges();
                v.AcJournalID = 0;
            }
            if (v.AcJournalID == 0)
            {
                int acjm = 0;
                acjm = (from c in db.AcJournalMasters orderby c.AcJournalID descending select c.AcJournalID).FirstOrDefault();

                ajm.AcJournalID = acjm + 1;
                ajm.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
                ajm.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                ajm.AcFinancialYearID = fyearid;
                ajm.PaymentType = 1;
                ajm.Remarks = v.Description; //  "Credit Note for " + v.CustomerName + " invoice : " + v.InvoiceNo;
                ajm.StatusDelete = false;

                ajm.VoucherNo = AccountsDAO.GetMaxVoucherNo(v.TransType, fyearid);
                ajm.TransDate = v.Date;

                ajm.TransType = 2;
                ajm.VoucherType = v.TransType;


                db.AcJournalMasters.Add(ajm);
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
                a.AcHeadID = v.AcHeadID;// customercon.AcHeadID; ;
            }
            if (v.TransType == "CN")
                a.Amount = -1 * v.Amount;
            else
                a.Amount = v.Amount;
            a.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            a.Remarks = v.Description;

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
            foreach (var detail in v.Details)
            {
                AcJournalDetail b = new AcJournalDetail();
                b.AcJournalDetailID = 0;
                if (b.AcJournalDetailID == 0)
                {
                    int maxacj = 0;
                    maxacj = (from c in db.AcJournalDetails orderby c.AcJournalDetailID descending select c.AcJournalDetailID).FirstOrDefault();

                    b.AcJournalDetailID = maxacj + 1;
                    b.AcJournalID = ajm.AcJournalID;
                }
                b.AcHeadID = detail.AcHeadID;
                if (v.TransType == "CN")
                    b.Amount = detail.Amount;
                else
                    b.Amount = -1 * detail.Amount;
                b.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                b.Remarks = detail.Remarks;
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
            }

            var invid = 0;
            int? recpayid = 0;

            CreditNote d = new CreditNote();
            if (v.CreditNoteID == 0)
            {
                int maxid = 0;

                var data = (from c in db.CreditNotes orderby c.CreditNoteID descending select c).FirstOrDefault();

                if (data == null)
                {
                    maxid = 1;
                }
                else
                {
                    maxid = data.CreditNoteID + 1;
                }

                d.CreditNoteID = maxid;
                d.CreditNoteNo = AccountsDAO.GetMaxCreditNoteNo(fyearid);
                d.FYearID = Convert.ToInt32(Session["fyearid"].ToString());
                d.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
                d.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                d.statusclose = false;
                d.CreatedBy = userid;
                d.CreatedDate = CommanFunctions.GetCurrentDateTime();
                d.ModifiedBy = userid;
                d.ModifiedDate = CommanFunctions.GetCurrentDateTime();
            }
            else
            {
                d = db.CreditNotes.Find(v.CreditNoteID);
                var det = db.CreditNoteDetails.Where(cc => cc.CreditNoteID == v.CreditNoteID).ToList();
                if (det != null)
                {
                    db.CreditNoteDetails.RemoveRange(det);
                    db.SaveChanges();
                }

                d.ModifiedBy = userid;
                d.ModifiedDate = CommanFunctions.GetCurrentDateTime();
            }

            d.InvoiceType = v.InvoiceType;
            d.CreditNoteDate = v.Date;
            if (v.TransType == "CN")
                d.Amount = v.Amount;
            else
                d.Amount = -1 * v.Amount;
            d.AcJournalID = ajm.AcJournalID;

            d.AcHeadID = v.AcHeadID;
            d.CustomerID = v.CustomerID;
            d.Description = v.Description;
            d.TransType = v.TransType;
            if (v.InvoiceID != 0)
            {
                if (v.TransType == "CN")
                    d.InvoiceID = v.InvoiceID;
                else
                    d.RecPayID = v.InvoiceID;
            }
            else
            {
                d.InvoiceID = 0;
            }
            //d.IsShipping = true;
            if (v.CreditNoteID == 0)
            {
                db.CreditNotes.Add(d);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Successfully Added Credit Note";
            }
            else
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Successfully Updated Credit Note";
            }

            foreach (var detail in v.Details)
            {
                CreditNoteDetail det = new CreditNoteDetail();
                det.AcHeadID = detail.AcHeadID;
                det.Amount = detail.Amount;
                det.Remarks = detail.Remarks;
                det.CreditNoteID = d.CreditNoteID;
                db.CreditNoteDetails.Add(det);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "CreditNote");





        }

        [HttpPost]
        public JsonResult GetTradeInvoiceOfCustomer(int? ID, decimal? amountreceived, int? CreditNoteId, string TransType = "CN")
        {
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            DateTime fromdate = Convert.ToDateTime(Session["FyearFrom"].ToString());
            DateTime todate = Convert.ToDateTime(Session["FyearTo"].ToString());
            //var AllInvoices = (from d in db.CustomerInvoices where d.CustomerID == ID select d).OrderBy(cc => cc.InvoiceDate).ToList();
            List<ReceiptAllocationDetailVM> AWBAllocation = new List<ReceiptAllocationDetailVM>();
            var salesinvoice = new List<CustomerTradeReceiptVM>();
            //var AllOPInvoices = (from d in db.AcOPInvoiceDetails join m in db.AcOPInvoiceMasters on d.AcOPInvoiceMasterID equals m.AcOPInvoiceMasterID where d.Amount > 0 && m.AcFinancialYearID == fyearid && m.StatusSDSC == "C" && m.PartyID == ID select d).OrderBy(cc => cc.InvoiceDate).ToList();
            if (TransType == "CN")
                salesinvoice = ReceiptDAO.SP_GetCustomerInvoicePending(Convert.ToInt32(ID), 0, 0, Convert.ToInt32(CreditNoteId), "OP");
            else
                salesinvoice = ReceiptDAO.SP_GetCustomerReceiptPending(Convert.ToInt32(ID), 0, 0, Convert.ToInt32(CreditNoteId), "OP");



            //    foreach (var item in AllOPInvoices)
            //{
            //    decimal? totamt = 0;
            //    decimal? totamtpaid = 0;
            //    decimal? totadjust = 0;
            //    totamtpaid = ReceiptDAO.SP_GetCustomerInvoiceReceived(Convert.ToInt32(ID), item.AcOPInvoiceDetailID, 0, Convert.ToInt32(CreditNoteId), "OP");
            //    var Invoice = new CustomerTradeReceiptVM();
            //    Invoice.AcOPInvoiceDetailID = item.AcOPInvoiceDetailID;
            //    Invoice.InvoiceType = "OP";
            //    Invoice.SalesInvoiceID = item.AcOPInvoiceDetailID;
            //    Invoice.InvoiceNo = item.InvoiceNo;
            //    Invoice.InvoiceAmount = item.Amount;
            //    Invoice.date = item.InvoiceDate;
            //    Invoice.DateTime = Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy");
            //    Invoice.AmountReceived = totamtpaid;
            //    Invoice.Balance = Invoice.InvoiceAmount - totamtpaid;
            //    Invoice.AdjustmentAmount = totadjust;

            //    if (Invoice.Balance > 0)
            //    {
            //        if (amountreceived != null)
            //        {
            //            if (amountreceived >= Invoice.Balance)
            //            {
            //                Invoice.Allocated = true;
            //                Invoice.Amount = Invoice.Balance;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else if (amountreceived > 0)
            //            {
            //                Invoice.Allocated = true;
            //                Invoice.Amount = amountreceived;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else
            //            {
            //                Invoice.Amount = 0;
            //            }
            //        }
            //        salesinvoice.Add(Invoice);
            //    }

            //}
            //foreach (var item in AllInvoices)
            //{
            //    //var invoicedeails = (from d in db.SalesInvoiceDetails where d.SalesInvoiceID == item.SalesInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
            //    //var invoicedeails = (from d in db.CustomerInvoiceDetails where d.CustomerInvoiceID == item.CustomerInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null)  select d).ToList();
            //    var invoicedeails = (from d in db.CustomerInvoiceDetails where d.CustomerInvoiceID == item.CustomerInvoiceID select d).ToList();
            //    //where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
            //    decimal? totamt = 0;
            //    decimal? totamtpaid = 0;
            //    decimal? totadjust = 0;
            //    decimal? CreditAmount = 0;

            //    totamtpaid = ReceiptDAO.SP_GetCustomerInvoiceReceived(Convert.ToInt32(ID), item.CustomerInvoiceID, 0, Convert.ToInt32(CreditNoteId), "TR");

            //    totamt = totamtpaid + totadjust + CreditAmount;


            //    var Invoice = new CustomerTradeReceiptVM();
            //    //Invoice.JobID = det.JobID;
            //    Invoice.InvoiceType = "TR";
            //    Invoice.JobCode = "";
            //    Invoice.SalesInvoiceID = item.CustomerInvoiceID; // SalesInvoiceID;
            //    Invoice.InvoiceNo = item.CustomerInvoiceNo;
            //    //Invoice.SalesInvoiceDetailID = det.CustomerInvoiceDetailID;
            //    Invoice.InvoiceAmount = item.InvoiceTotal; // CourierCharge;
            //    Invoice.date = item.InvoiceDate;
            //    Invoice.DateTime = item.InvoiceDate.ToString("dd/MM/yyyy");
            //    //var RecPay = (from d in db.RecPayDetails where d.RecPayDetailID == det.RecPayDetailId select d).FirstOrDefault();

            //    Invoice.AmountReceived = totamt;
            //    Invoice.Balance = Invoice.InvoiceAmount - totamtpaid;
            //    Invoice.AdjustmentAmount = totadjust;

            //    if (Invoice.Balance > 0)
            //    {
            //        if (amountreceived != null)
            //        {
            //            if (amountreceived >= Invoice.Balance)
            //            {
            //                Invoice.Allocated = true;
            //                Invoice.Amount = Invoice.Balance;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else if (amountreceived > 0)
            //            {
            //                Invoice.Amount = amountreceived;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else
            //            {
            //                Invoice.Amount = 0;
            //            }
            //        }
            //        salesinvoice.Add(Invoice);
            //        //if (RecPayId == null)
            //        //{
            //        //    AWBAllocation = ReceiptDAO.GetAWBAllocation(AWBAllocation, Convert.ToInt32(Invoice.SalesInvoiceID), Convert.ToDecimal(Invoice.Amount), 0); //customer invoiceid,amount
            //        //}
            //        //else
            //        //{
            //        //    AWBAllocation = ReceiptDAO.GetAWBAllocation(AWBAllocation, Convert.ToInt32(Invoice.SalesInvoiceID), Convert.ToDecimal(Invoice.Amount), Convert.ToInt32(RecPayId)); //customer invoiceid,amount
            //        //}
            //    }
            //}


            Session["CustomerInvoice"] = salesinvoice;
            return Json(salesinvoice, JsonRequestBehavior.AllowGet);
        }


        public void SetTradeInvoiceOfCustomer(int? ID, decimal? amountreceived, int? CreditNoteId, string TransType)
        {
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            //DateTime fromdate = Convert.ToDateTime(Session["FyearFrom"].ToString());
            //DateTime todate = Convert.ToDateTime(Session["FyearTo"].ToString());
            //var AllInvoices = (from d in db.CustomerInvoices where d.CustomerID == ID select d).ToList();
            List<ReceiptAllocationDetailVM> AWBAllocation = new List<ReceiptAllocationDetailVM>();
            var salesinvoice = new List<CustomerTradeReceiptVM>();
            if (TransType == "CN")
                salesinvoice = ReceiptDAO.SP_GetCustomerInvoicePending(Convert.ToInt32(ID), 0, 0, Convert.ToInt32(CreditNoteId), "OP");
            else
                salesinvoice = ReceiptDAO.SP_GetCustomerReceiptPending(Convert.ToInt32(ID), 0, 0, Convert.ToInt32(CreditNoteId), "OP");


            //var AllOPInvoices = (from d in db.AcOPInvoiceDetails join m in db.AcOPInvoiceMasters on d.AcOPInvoiceMasterID equals m.AcOPInvoiceMasterID where m.AcFinancialYearID == fyearid && m.StatusSDSC == "C" && m.PartyID == ID && d.RecPayDetailId == null && (d.RecPayStatus == null || d.RecPayStatus < 2) select d).ToList();

            //foreach (var item in AllOPInvoices)
            //{

            //    decimal? totamtpaid = 0;
            //    decimal? totadjust = 0;

            //    totamtpaid = ReceiptDAO.SP_GetCustomerInvoiceReceived(Convert.ToInt32(ID), Convert.ToInt32(item.AcOPInvoiceDetailID), 0, Convert.ToInt32(CreditNoteId), "OP");

            //    var Invoice = new CustomerTradeReceiptVM();
            //    Invoice.AcOPInvoiceDetailID = item.AcOPInvoiceDetailID;
            //    Invoice.SalesInvoiceID = item.AcOPInvoiceDetailID;
            //    Invoice.InvoiceType = "OP";
            //    Invoice.InvoiceNo = item.InvoiceNo; ;
            //    Invoice.InvoiceAmount = item.Amount;
            //    Invoice.date = item.InvoiceDate;
            //    Invoice.DateTime = Convert.ToDateTime(item.InvoiceDate).ToString("dd/MM/yyyy");
            //    Invoice.AmountReceived = totamtpaid;
            //    Invoice.Balance = Invoice.InvoiceAmount - totamtpaid;
            //    Invoice.AdjustmentAmount = totadjust;

            //    if (Invoice.Balance > 0)
            //    {
            //        if (amountreceived != null)
            //        {
            //            if (amountreceived >= Invoice.Balance)
            //            {
            //                Invoice.Allocated = true;
            //                Invoice.Amount = Invoice.Balance;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else if (amountreceived > 0)
            //            {
            //                Invoice.Amount = amountreceived;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else
            //            {
            //                Invoice.Amount = 0;
            //            }
            //        }
            //        salesinvoice.Add(Invoice);
            //    }

            //}
            //foreach (var item in AllInvoices)
            //{
            //    //var invoicedeails = (from d in db.SalesInvoiceDetails where d.SalesInvoiceID == item.SalesInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();
            //    //var invoicedeails = (from d in db.CustomerInvoiceDetails where d.CustomerInvoiceID == item.CustomerInvoiceID where (d.RecPayStatus < 2 || d.RecPayStatus == null) select d).ToList();                                
            //    decimal? totamtpaid = 0;
            //    decimal? totadjust = 0;

            //    totamtpaid = ReceiptDAO.SP_GetCustomerInvoiceReceived(Convert.ToInt32(ID), Convert.ToInt32(item.CustomerInvoiceID), 0, Convert.ToInt32(CreditNoteId), "TR");

            //    var Invoice = new CustomerTradeReceiptVM();
            //    Invoice.InvoiceType = "TR";
            //    Invoice.JobCode = "";
            //    Invoice.SalesInvoiceID = item.CustomerInvoiceID; // SalesInvoiceID;
            //    Invoice.InvoiceNo = item.CustomerInvoiceNo;
            //    Invoice.InvoiceAmount = item.InvoiceTotal; // CourierCharge;
            //    Invoice.date = item.InvoiceDate;
            //    Invoice.DateTime = item.InvoiceDate.ToString("dd/MM/yyyy");
            //    Invoice.AmountReceived = totamtpaid;
            //    Invoice.Balance = Invoice.InvoiceAmount - totamtpaid;
            //    Invoice.AdjustmentAmount = totadjust;

            //    if (Invoice.Balance > 0)
            //    {
            //        if (amountreceived != null)
            //        {
            //            if (amountreceived >= Invoice.Balance)
            //            {
            //                Invoice.Allocated = true;
            //                Invoice.Amount = Invoice.Balance;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else if (amountreceived > 0)
            //            {
            //                Invoice.Amount = amountreceived;
            //                amountreceived = amountreceived - Invoice.Amount;
            //            }
            //            else
            //            {
            //                Invoice.Amount = 0;
            //            }
            //        }
            //        salesinvoice.Add(Invoice);
            //        //if (RecPayId == null)
            //        //{
            //        //    AWBAllocation = ReceiptDAO.GetAWBAllocation(AWBAllocation, Convert.ToInt32(Invoice.SalesInvoiceID), Convert.ToDecimal(Invoice.Amount), 0); //customer invoiceid,amount
            //        //}
            //        //else
            //        //{
            //        //    AWBAllocation = ReceiptDAO.GetAWBAllocation(AWBAllocation, Convert.ToInt32(Invoice.SalesInvoiceID), Convert.ToDecimal(Invoice.Amount), Convert.ToInt32(RecPayId)); //customer invoiceid,amount
            //        //}
            //    }
            //}

            Session["CustomerInvoice"] = salesinvoice;

        }

        [HttpPost]
        public ActionResult AddAccount(int? AcHeadID, decimal? Amount, string Remarks, string TransType = "CN")
        {
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            DateTime fromdate = Convert.ToDateTime(Session["FyearFrom"].ToString());
            DateTime todate = Convert.ToDateTime(Session["FyearTo"].ToString());
            List<CreditNoteDetailVM> list = (List<CreditNoteDetailVM>)Session["CreditNoteDetail"];
            CreditNoteDetailVM item = new CreditNoteDetailVM();
            if (list != null)
            {
                item = list.Where(cc => cc.AcHeadID == AcHeadID).FirstOrDefault();
            }
            else
            {
                list = new List<CreditNoteDetailVM>();
            }
            if (item == null)
            {
                item = new CreditNoteDetailVM();
                item.AcHeadID = AcHeadID;
                item.AcHeadName = db.AcHeads.Find(item.AcHeadID).AcHead1;
                item.Amount = Amount;
                item.Remarks = Remarks;
                list.Add(item);
            }

            Session["CreditNoteDetail"] = list;
            CreditNoteVM vm = new CreditNoteVM();
            vm.Details = list;
            return PartialView("CreditNoteDetail", vm);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int index)
        {
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            DateTime fromdate = Convert.ToDateTime(Session["FyearFrom"].ToString());
            DateTime todate = Convert.ToDateTime(Session["FyearTo"].ToString());
            List<CreditNoteDetailVM> list = (List<CreditNoteDetailVM>)Session["CreditNoteDetail"];
            List<CreditNoteDetailVM> list1 = new List<CreditNoteDetailVM>();
            CreditNoteDetailVM item = new CreditNoteDetailVM();
            list.RemoveAt(index);

            Session["CreditNoteDetail"] = list;
            CreditNoteVM vm = new CreditNoteVM();
            vm.Details = list;
            return PartialView("CreditNoteDetail", vm);
        }


        [HttpGet]
        public JsonResult GetCustomerName(string term)
        {
            var customerlist = (from c1 in db.CustomerMasters
                                where c1.CustomerID > 0 && (c1.CustomerType == "CR" || c1.CustomerType == "CL") && c1.CustomerName.ToLower().StartsWith(term.ToLower())
                                orderby c1.CustomerName ascending
                                select new { CustomerID = c1.CustomerID, CustomerName = c1.CustomerName, CustomerType = c1.CustomerType }).Take(100).ToList();

            return Json(customerlist, JsonRequestBehavior.AllowGet);

        }
        //public JsonResult GetJobNo(int id)
        //{
        //    List<jobno> lst = new List<jobno>();
        //    var jobs = (from c in db.JobGenerations where c.ShipperID == id select c).ToList();
        //    if (jobs != null)
        //    {
        //        foreach (var item in jobs)
        //        {
        //            jobno obj = new jobno();
        //            obj.JobNo = item.JobCode;
        //            lst.Add(obj);
        //        }
        //    }

        //    return Json(lst,JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetInvoiceNo(string term)
        {
            var salesinvoice = new List<CustomerTradeReceiptVM>();
            var lst = new List<CustomerTradeReceiptVM>();
            salesinvoice = (List<CustomerTradeReceiptVM>)Session["CustomerInvoice"];
            if (term.Trim()!="")
            {
                lst = salesinvoice.Where(cc => cc.InvoiceNo.Contains(term)).OrderBy(cc => cc.InvoiceNo).ToList();
            }
            else
            {
                lst = salesinvoice.OrderBy(cc => cc.InvoiceNo).ToList();
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public class jobno
        {
          
            public string JobNo { get; set; }
            public int JobNum { get; set; }
            public bool Istrading { get; set; }
         
        }
       
        public ActionResult DeleteCreditNote(int id)
        {
            //int k = 0;
            if (id != 0)
            {
                string result = AccountsDAO.DeleteCreditNote(id);
                TempData["SuccessMsg"] = result;

            }

            return RedirectToAction("Index", "CreditNote");

        }
        public JsonResult GetAmountByinvono(int invno, bool IsTrading)
        {
            Getamtclass ob = new Getamtclass();
            ob.invoiceamt = 0;
            ob.recamt = 0;
            if (IsTrading == false)
            {
                //int jobid = (from j in db.JobGenerations where j.JobCode == invno select j.JobID).FirstOrDefault();

                //int invid = (from c in db.JInvoices where c.JobID == jobid select c.InvoiceID).FirstOrDefault();

                //decimal invamt = (from c in db.JInvoices where c.InvoiceID == invid select c.SalesHome).FirstOrDefault().Value;

                //decimal recamt = (from r in db.RecPayDetails where r.InvoiceID == invid select r.Amount).FirstOrDefault().Value;


                //ob.invoiceamt = invamt;
                //ob.recamt = recamt;
            }
            else
            {
                var sinvoice = (from d in db.CustomerInvoices where d.CustomerInvoiceID == invno select d).FirstOrDefault();
                var sinDetails = (from c in db.CustomerInvoiceDetails where c.CustomerInvoiceID == sinvoice.CustomerInvoiceID select c).ToList();
                List<int?> pinvoiceids = sinDetails.Select(d => (int?)d.CustomerInvoiceDetailID).ToList();
                ob.invoiceamt = Convert.ToDecimal(sinDetails.Sum(d => d.NetValue));
                var recpay = (from x in db.RecPayDetails where pinvoiceids.Contains(x.InvoiceID) select x).ToList();

                if (recpay.Count > 0)
                {
                    ob.recamt = Convert.ToDecimal(Math.Abs(recpay.Sum(s => s.Amount.Value)));
                }

            }


            return Json(ob, JsonRequestBehavior.AllowGet);
        }


     

        public JsonResult CreditNoteVoucher(int id)
        {
            string reportpath = "";           
            if (id != 0)
            {
                reportpath = AccountsReportsDAO.GenerateCreditNoteVoucherPrint(id);

            }

            return Json(new { path = reportpath, result = "ok" }, JsonRequestBehavior.AllowGet);

        }
     
        public class Getamtclass
        {
            public decimal? invoiceamt { get; set; }
            public decimal? recamt { get; set; }

        }

    }
}
