using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using System.Data;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using LTMSV2.DAL;
using System.Data.Entity;
//using System.IO;
//using Newtonsoft.Json;
//using System.Text.RegularExpressions;
//using System.Net.Mail;
//using System.Configuration;
//using System.Collections.Specialized;
//using System.Net;
//using System.Text;
//using LTMSV2.DAL;
//using System.Data.Entity;

namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class CODReceiptController : Controller
    {
        SourceMastersModel MM = new SourceMastersModel();
        RecieptPaymentModel RP = new RecieptPaymentModel();
        CustomerRcieptVM cust = new CustomerRcieptVM();
        Entities1 db = new Entities1();

        EditCommanFu editfu = new EditCommanFu();
        // GET: CODReceipt
        public ActionResult Index()
        {
            DatePicker datePicker = SessionDataModel.GetTableVariable();
            DatePicker model = new DatePicker();
            //string tz = "Arabian Standard Time";
            //DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            if (datePicker == null)
            {
                model = new DatePicker
                {
                    FromDate = CommanFunctions.GetFirstDayofMonth().Date,
                    ToDate = CommanFunctions.GetLastDayofMonth().Date //DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddHours(8)

                    //      Delete = (bool)Token.Permissions.Deletion,
                    //    Update = (bool)Token.Permissions.Updation,
                    //  Create = (bool)Token.Permissions.Creation
                };
            }
            else
            {
                model.FromDate = datePicker.FromDate;
                model.ToDate = datePicker.ToDate;

            }
            ViewBag.Token = model;
            SessionDataModel.SetTableVariable(model);
            return View(model);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "FromDate,ToDate")] DatePicker picker)
        {

            DatePicker model = new DatePicker
            {
                FromDate = picker.FromDate,
                ToDate = picker.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                Delete = true, // (bool)Token.Permissions.Deletion,
                Update = true, //(bool)Token.Permissions.Updation,
                Create = true //.ToStrin//(bool)Token.Permissions.Creation
            };
            ViewBag.Token = model;
            SessionDataModel.SetTableVariable(model);
            return View(model);

        }

        public ActionResult Table()
        {
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());
            int FyearId = Convert.ToInt32(Session["fyearid"]);
            DatePicker datePicker = SessionDataModel.GetTableVariable();
            ViewBag.Token = datePicker;
            List<ReceiptVM> Receipts = new List<ReceiptVM>();
            Receipts = ReceiptDAO.GetCODReceipts(FyearId, datePicker.FromDate, datePicker.ToDate);

            return View("Table", Receipts);

        }

        public ActionResult Create(int id=0)
        {
            int FyearId = Convert.ToInt32(Session["fyearid"]);
            CustomerRcieptVM cust = new CustomerRcieptVM();
            cust.CustomerRcieptChildVM = new List<CustomerRcieptChildVM>();
            List<CurrencyMaster> Currencys = new List<CurrencyMaster>();
            Currencys = MM.GetCurrency();
            ViewBag.Currency = new SelectList(Currencys, "CurrencyID", "CurrencyName");
            if (Session["UserID"] != null)
            {
                var branchid = Convert.ToInt32(Session["CurrentBranchID"]);

                if (id > 0)
                {
                    ViewBag.Title = "COD Receipt - Modify";
                    cust = RP.GetRecPayByRecpayID(id);

                    var acheadforcash = (from c in db.AcHeads join g in db.AcGroups on c.AcGroupID equals g.AcGroupID where g.AcGroup1 == "Cash" select new { AcHeadID = c.AcHeadID, AcHead = c.AcHead1 }).ToList();
                    var acheadforbank = (from c in db.AcHeads join g in db.AcGroups on c.AcGroupID equals g.AcGroupID where g.AcGroup1 == "Bank" select new { AcHeadID = c.AcHeadID, AcHead = c.AcHead1 }).ToList();
                    ViewBag.achead = acheadforcash;
                    ViewBag.acheadbank = acheadforbank;
                    cust.recPayDetail = db.RecPayDetails.Where(item => item.RecPayID == id).ToList();
                    int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
                    var salesinvoice = new List<CustomerTradeReceiptVM>();
                    salesinvoice = ReceiptDAO.GetCODPending(fyearid, id);
                    Session["CODAWBList"] = salesinvoice;
                    cust.CustomerRcieptChildVM = new List<CustomerRcieptChildVM>();
                    foreach (var item in cust.recPayDetail)
                    {

                        if (item.InScanID > 0 )
                        {
                            if (salesinvoice.Count > 0)
                            { 
                                CustomerTradeReceiptVM sales = salesinvoice.Where(cc => cc.InScanID == Convert.ToInt32(item.InScanID)).FirstOrDefault();
                                decimal invoicetotal =Convert.ToDecimal(sales.InvoiceAmount);
                                decimal received = Convert.ToDecimal(sales.AmountReceived);
                                var allrecpay = (from d in db.RecPayDetails where d.InScanID== item.InScanID select d).ToList();
                                var totamtpaid = allrecpay.Sum(d => d.Amount) * -1;
                                var totadjust = allrecpay.Sum(d => d.AdjustmentAmount);                                
                                
                                var customerinvoice = new CustomerRcieptChildVM();
                                                                
                                customerinvoice.InvoiceType = "D";
                                var inscan=db.InScanMasters.Find(item.InScanID);

                                if (inscan != null)
                                {
                                    customerinvoice.InScanID = inscan.InScanID;
                                    customerinvoice.ConsignmentNo = inscan.ConsignmentNo;
                                    customerinvoice.strDate = Convert.ToDateTime(inscan.TransactionDate).ToString("dd/MM/yyyy");
                                }

                                customerinvoice.AmountToBeRecieved = invoicetotal;
                                customerinvoice.AmountToBePaid = received;
                                customerinvoice.Amount = Convert.ToDecimal(item.Amount) * -1;
                                customerinvoice.Balance = invoicetotal - received; 
                                customerinvoice.RecPayDetailID = item.RecPayDetailID;

                                customerinvoice.RecPayID = Convert.ToInt32(item.RecPayID);
                                customerinvoice.AdjustmentAmount = 0;
                                cust.CustomerRcieptChildVM.Add(customerinvoice);
                            }
                        }
                    }
                    Session["AWBAllocation"] = cust.AWBAllocation;

                }
                else
                {
                    ViewBag.Title = "COD Receipt - Create";
                    var codcust = db.CustomerMasters.Where(cc => cc.CustomerName == "Cod Customer").FirstOrDefault();

                    var acheadforcash = (from c in db.AcHeads join g in db.AcGroups on c.AcGroupID equals g.AcGroupID where g.AcGroup1 == "Cash" select new { AcHeadID = c.AcHeadID, AcHead = c.AcHead1 }).ToList();
                    var acheadforbank = (from c in db.AcHeads join g in db.AcGroups on c.AcGroupID equals g.AcGroupID where g.AcGroup1 == "Bank" select new { AcHeadID = c.AcHeadID, AcHead = c.AcHead1 }).ToList();

                    ViewBag.achead = acheadforcash;
                    ViewBag.acheadbank = acheadforbank;

                    DateTime pFromDate = AccountsDAO.CheckParamDate(DateTime.Now, FyearId).Date;
                    cust.RecPayDate = pFromDate;
                    cust.RecPayID = 0;
                    cust.CustomerID = codcust.CustomerID;
                    cust.DocumentNo = ReceiptDAO.SP_GetMaxCODID();
                    cust.CurrencyId = Convert.ToInt32(Session["CurrencyId"].ToString());

                    var salesinvoice = new List<CustomerTradeReceiptVM>();
                    if (codcust != null)
                    {
                        int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
                        salesinvoice = ReceiptDAO.GetCODPending(fyearid,0);
                        Session["CODAWBList"] = salesinvoice;
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            var StaffNotes = (from d in db.StaffNotes where d.PageTypeId == 2 orderby d.NotesId descending select d).ToList();
            var users = (from d in db.UserRegistrations select d).ToList();

            var staffnotemodel = new List<StaffNoteModel>();
            foreach (var item in StaffNotes)
            {
                var model = new StaffNoteModel();
                model.id = item.NotesId;
                model.employeeid = item.EmployeeId;
                //model.jobid = item.JobId;
                model.TaskDetails = item.Notes;
                model.Datetime = item.EntryDate;
                model.EmpName = users.Where(d => d.UserID == item.EmployeeId).FirstOrDefault().UserName;
                staffnotemodel.Add(model);
            }
            ViewBag.StaffNoteModel = staffnotemodel;
            var customerdetails = (from d in db.CustomerMasters where d.CustomerID == cust.CustomerID && d.CustomerType == "CS" select d).FirstOrDefault();
            if (customerdetails == null)
            {
                customerdetails = new CustomerMaster();
            }
            ViewBag.CustomerDetail = customerdetails;
            var CustomerNotification = (from d in db.CustomerNotifications where d.RecPayID == id && d.PageTypeId == 2 orderby d.NotificationId descending select d).ToList();

            var customernotification = new List<CustomerNotificationModel>();
            foreach (var item in CustomerNotification)
            {
                var model = new CustomerNotificationModel();
                model.id = item.NotificationId;
                model.employeeid = item.UserId;
                model.jobid = item.RecPayID;
                model.Message = item.MessageText;
                model.Datetime = item.EntryDate;
                model.IsEmail = item.NotifyByEmail;
                model.IsSms = item.NotifyBySMS;
                model.IsWhatsapp = item.NotifyByWhatsApp;
                model.EmpName = users.Where(d => d.UserID == item.UserId).FirstOrDefault().UserName;
                customernotification.Add(model);
            }
            cust.AWBAllocation = new List<ReceiptAllocationDetailVM>();
            ViewBag.CustomerNotification = customernotification;
            return View(cust);
        }



        [HttpPost]
        public ActionResult Create(CustomerRcieptVM RecP, string Command, string Currency)
        {
            int RPID = 0;
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            int i = 0;
            RecP.FYearID = Convert.ToInt32(Session["fyearid"]);
            RecP.UserID = Convert.ToInt32(Session["UserID"]);
            var StaffNotes = (from d in db.StaffNotes where d.RecPayID == RecP.RecPayID && d.PageTypeId == 2 orderby d.NotesId descending select d).ToList();
            var branchid = Convert.ToInt32(Session["CurrentBranchID"]);
            var users = (from d in db.UserRegistrations select d).ToList();
            List<ReceiptAllocationDetailVM> AWBAllocationall = (List<ReceiptAllocationDetailVM>)Session["AWBAllocation"];
            var staffnotemodel = new List<StaffNoteModel>();
            foreach (var item in StaffNotes)
            {
                var model = new StaffNoteModel();
                model.id = item.NotesId;
                model.employeeid = item.EmployeeId;
                //model.jobid = item.JobId;
                model.TaskDetails = item.Notes;
                model.Datetime = item.EntryDate;
                model.EmpName = users.Where(d => d.UserID == item.EmployeeId).FirstOrDefault().UserName;
                staffnotemodel.Add(model);
            }
            ViewBag.StaffNoteModel = staffnotemodel;
            //if (RecP.RecPayID > 0)
            //{
            //    RP.EditCustomerRecPay(RecP, Session["UserID"].ToString());
            //    RP.EditCustomerRecieptDetails(RecP.recPayDetail, RecP.RecPayID);
            //}
            if (RecP.CashBank != null)
            {
                RecP.StatusEntry = "CS";
                int acheadid = Convert.ToInt32(RecP.CashBank);
                var achead = (from t in db.AcHeads where t.AcHeadID == acheadid select t.AcHead1).FirstOrDefault();
                RecP.BankName = achead;
            }
            else
            {
                RecP.StatusEntry = "BK";
                int acheadid = Convert.ToInt32(RecP.ChequeBank);
                var achead = (from t in db.AcHeads where t.AcHeadID == acheadid select t.AcHead1).FirstOrDefault();
                RecP.BankName = achead;
            }
            if (RecP.CustomerRcieptChildVM == null)
            {
                RecP.CustomerRcieptChildVM = new List<CustomerRcieptChildVM>();
            }
            //Adding Entry in Rec PAY

            ///Insert Entry For RecPay Details 
            ///
            if (RecP.RecPayID <= 0)
            {
                decimal Fmoney = 0;
                //for (int j = 0; j < RecP.CustomerRcieptChildVM.Count; j++)
                //{
                //    Fmoney = Fmoney + Convert.ToDecimal(RecP.CustomerRcieptChildVM[j].Amount);
                //}
                //if (Fmoney > 0)
                //{
                //    RecP.FMoney = Fmoney;
                //}
                RecP.AcCompanyID = branchid;

                RPID = ReceiptDAO.AddCustomerRecieptPayment(RecP, Session["UserID"].ToString()); //.AddCustomerRecieptPayment(RecP, Session["UserID"].ToString());

                RecP.RecPayID = (from c in db.RecPays orderby c.RecPayID descending select c.RecPayID).FirstOrDefault();
                decimal TotalAmount = 0;

                foreach (var item in RecP.CustomerRcieptChildVM)
                {
                    if (item.Amount > 0)
                    {
                        RecPayDetail recpd = new RecPayDetail();
                        int? maxrecpaydetailid = (from c in db.RecPayDetails orderby c.RecPayDetailID descending select c.RecPayDetailID).FirstOrDefault();
                        if (maxrecpaydetailid == null)
                            maxrecpaydetailid = 1;
                        else
                            maxrecpaydetailid = maxrecpaydetailid + 1;

                        recpd.RecPayDetailID = Convert.ToInt32(maxrecpaydetailid);
                        recpd.Amount = -(item.Amount);
                        recpd.CurrencyID = item.CurrencyId;
                        recpd.AdjustmentAmount = 0;
                        recpd.InvDate = item.InvoiceDate.Value;
                        recpd.RecPayID = RecP.RecPayID;
                        recpd.Remarks = item.Remarks;
                        recpd.InvoiceID = 0;
                        recpd.InScanID = item.InScanID;
                        recpd.AcOPInvoiceDetailID = 0;
                        recpd.StatusInvoice = "D";
                        db.RecPayDetails.Add(recpd);
                        db.SaveChanges();
                    }
                }


                //To Balance Invoice AMount
                if (RecP.FMoney > 0)
                {
                    //int l = ReceiptDAO.InsertRecpayDetailsForCust(RecP.RecPayID, 0, 0, Convert.ToDecimal(RecP.FMoney), null, "D", false, null, null, null, Convert.ToInt32(RecP.CurrencyId), 4, 0);
                    int fyaerId = Convert.ToInt32(Session["fyearid"].ToString());
                    ReceiptDAO.InsertJournalOfCustomer(RecP.RecPayID, fyaerId);

                }
                // var Recpaydata = (from d in db.RecPays where d.RecPayID == RecP.RecPayID select d).FirstOrDefault();

                //Recpaydata.RecPayID = RecP.RecPayID;
                //Recpaydata.IsTradingReceipt = true;
                //db.Entry(Recpaydata).State = EntityState.Modified;
                //db.SaveChanges();

            }
            else //edit mode
            {


                var details = (from d in db.RecPayDetails where d.RecPayID == RecP.RecPayID select d).ToList();
                if (details != null)
                { db.RecPayDetails.RemoveRange(details);
                    db.SaveChanges();
                }

                RecPay recpay = new RecPay();
                recpay = db.RecPays.Find(RecP.RecPayID);
                recpay.RecPayDate = RecP.RecPayDate;
                recpay.RecPayID = RecP.RecPayID;
                recpay.AcJournalID = RecP.AcJournalID;
                recpay.BankName = RecP.BankName;
                recpay.ChequeDate = RecP.ChequeDate;
                recpay.ChequeNo = RecP.ChequeNo;
                recpay.CustomerID = RecP.CustomerID;
                recpay.DocumentNo = RecP.DocumentNo;
                recpay.EXRate = RecP.EXRate;
                recpay.FYearID = RecP.FYearID;
                recpay.FMoney = RecP.FMoney;
                recpay.StatusEntry = RecP.StatusEntry;
                recpay.IsTradingReceipt = true;
                recpay.FMoney = RecP.FMoney;
                recpay.Remarks = RecP.Remarks;
                recpay.ModifiedBy = RecP.UserID;
                recpay.ModifiedDate = DateTime.Now;
                db.Entry(recpay).State = EntityState.Modified;
                db.SaveChanges();

                foreach (var item in RecP.CustomerRcieptChildVM)
                {
                    if (item.Amount > 0)
                    {
                        RecPayDetail recpd = new RecPayDetail();

                        int? maxrecpaydetailid = (from c in db.RecPayDetails orderby c.RecPayDetailID descending select c.RecPayDetailID).FirstOrDefault();
                        if (maxrecpaydetailid == null)
                            maxrecpaydetailid = 1;
                        else
                            maxrecpaydetailid = maxrecpaydetailid + 1;

                        recpd.RecPayDetailID = Convert.ToInt32(maxrecpaydetailid);
                        recpd.Amount = -(item.Amount);
                        recpd.CurrencyID = item.CurrencyId;
                        recpd.AdjustmentAmount = 0;
                        if (item.InvoiceDate != null)
                            recpd.InvDate = item.InvoiceDate.Value;
                        recpd.RecPayID = RecP.RecPayID;
                        recpd.Remarks = item.Remarks;
                        recpd.InvoiceID = 0;
                        recpd.InScanID = item.InScanID;
                        recpd.AcOPInvoiceDetailID = 0;
                        recpd.StatusInvoice = "D";
                        db.RecPayDetails.Add(recpd);
                        db.SaveChanges();
                    }
                }
                //int editrecPay = 0;
                //var sumOfAmount = db.RecPayDetails.Where(m => m.RecPayID == RecP.RecPayID && m.InvoiceID != 0).Sum(c => c.Amount);
                //editrecPay = editfu.EditRecpayDetailsCustR(RecP.RecPayID, Convert.ToInt32(sumOfAmount));
                if (RecP.AcJournalID == null)
                {
                    ReceiptDAO.InsertJournalOfCustomer(RecP.RecPayID, fyearid);
                }
                else
                {
                    int editAcJdetails = editfu.EditAcJDetails(RecP.AcJournalID.Value, Convert.ToDecimal(RecP.FMoney));
                }
                
            }

                        
            return RedirectToAction("Index", "CODReceipt");

        }
                
        public JsonResult GetConsignmentDetail(int Id)
        {
            List<CustomerTradeReceiptVM> list = (List<CustomerTradeReceiptVM>)Session["CODAWBList"];
            var result = list.Where(cc => cc.InScanID == Id).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetCODItems(decimal? amountreceived, int RecPayId=0)
        {
            var codcust=db.CustomerMasters.Where(cc => cc.CustomerName == "Cod Customer").FirstOrDefault();
            var salesinvoice = new List<CustomerTradeReceiptVM>();
            if (codcust != null)
            {
                int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
                salesinvoice = ReceiptDAO.GetCODPending(fyearid,RecPayId);
                Session["CODAWBList"] = salesinvoice;
                return Json(salesinvoice, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(salesinvoice, JsonRequestBehavior.AllowGet);
            }

            
        }

        [HttpPost]
        public JsonResult GetManifestID(CODReceiptVM ship)
        {
            ship.ManifestID = "";
            if (ship.SelectedValues != null)
            {
                foreach (var item in ship.SelectedValues)
                {
                    if (ship.ManifestID == "")
                    {
                        ship.ManifestID = item.ToString();
                    }
                    else
                    {
                        ship.ManifestID = ship.ManifestID + "," + item.ToString();
                    }

                }
            }
            return Json(new { manifestids = ship.ManifestID }, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult Getconsignment(string term)
        {
            List<CustomerTradeReceiptVM> list = (List<CustomerTradeReceiptVM>)Session["CODAWBList"];

            if (term.Trim() != "")
            {
                var result = list.Where(cc => cc.ConsignmentNo.Contains(term.Trim())).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult DeleteConfirmed(int id)
        {
            //int k = 0;
            if (id != 0)
            {
                DataTable dt = ReceiptDAO.DeleteCOdCustomerReceipt(id);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //if (dt.Rows[0][0] == "OK")
                        TempData["SuccessMsg"] = dt.Rows[0][1].ToString();
                    }

                }
                else
                {
                    TempData["ErrorMsg"] = "Error at delete";
                }
            }

            return RedirectToAction("Index", "CODReceipt");
        }

        public ActionResult Details(int id)
        {
        
            CODReceiptVM vm = new CODReceiptVM();
            var branchid = Convert.ToInt32(Session["CurrentBranchID"]);
            var companyid = Convert.ToInt32(Session["CurrentCompanyID"]);                    
                        
            vm.ReceiptDetails = new List<CODReceiptDetailVM>();
            if (id > 0)
            {
                var receipt = db.CODReceipts.Find(id);
                vm.ReceiptID = receipt.ReceiptID;
                vm.ReceiptDate = receipt.ReceiptDate;
                vm.ReceiptNo = receipt.ReceiptNo;
                vm.Remarks = receipt.Remarks;
                vm.ManifestID = receipt.ManifestID;
                vm.CurrencyID = receipt.CurrencyID;
                vm.EXRate = receipt.EXRate;
                vm.AchHeadID = receipt.AchHeadID;
                vm.AcHeadName = db.AcHeads.Find(receipt.AchHeadID).AcHead1;
                vm.CurrencyName = db.CurrencyMasters.Find(receipt.CurrencyID).CurrencyName;
                vm.Amount = receipt.Amount;
                vm.AgentID = receipt.AgentID;
                vm.AgentName = db.AgentMasters.Find(vm.AgentID).Name;
                List<CODReceiptDetailVM> receiptdetails = (from c in db.CODReceiptDetails
                                                           join ins in db.InScanMasters on c.InScanId equals ins.InScanID
                                                           join i in db.ExportShipments on c.ManifestID equals i.ID

                                                           where c.ReceiptID == vm.ReceiptID
                                                           select new CODReceiptDetailVM
                                                           {
                                                               InScanId = c.InScanId,
                                                               ManifestID = c.ManifestID,
                                                               ManifestNumber = i.ManifestNumber,
                                                               AWBNo = c.AWBNo,
                                                               AWBDate = ins.TransactionDate,
                                                               Consignee = c.Consignee,
                                                               ConsigneePhone = c.ConsigneePhone,
                                                               CourierCharge = c.CourierCharge,
                                                               OtherCharge = c.OtherCharge,
                                                               TotalCharge = c.TotalCharge,
                                                               AmountAllocate = c.AmountAllocate,
                                                               Discount = c.Discount
                                                           }).ToList();

                vm.ReceiptDetails = receiptdetails;            
            }
            

            return View(vm);
        }
    }
}