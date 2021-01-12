﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using LTMSV2.DAL;
using Newtonsoft.Json;
namespace LTMSV2.Controllers
{ [SessionExpire]
    public class SupplierInvoiceController : Controller
    {
        Entities1 db = new Entities1();
        // GET: SupplierInvoice
        public ActionResult Index(string FromDate, string ToDate)
        {
                        
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());

            DateTime pFromDate;
            DateTime pToDate;
                     
            if (FromDate == null || ToDate == null)
            {
                pFromDate = CommanFunctions.GetFirstDayofMonth().Date;//.AddDays(-1); // FromDate = DateTime.Now;
                pToDate = CommanFunctions.GetLastDayofMonth().Date.AddDays(1); // // ToDate = DateTime.Now;
            }
            else
            {
                pFromDate = Convert.ToDateTime(FromDate);//.AddDays(-1);
                pToDate = Convert.ToDateTime(ToDate).AddDays(1);

            }

            var lst = (from c in db.SupplierInvoices
                       join d in db.SupplierInvoiceDetails on c.SupplierInvoiceID equals d.SupplierInvoiceID
                       join s in db.SupplierMasters on c.SupplierID equals s.SupplierID
                       select new SupplierInvoiceVM {SupplierInvoiceID=c.SupplierInvoiceID, InvoiceNo = c.InvoiceNo, InvoiceDate = c.InvoiceDate, SupplierName = s.SupplierName, Amount = 0,SupplierType=s.SupplierType.SupplierType1 }).ToList();
            lst.ForEach(d => d.Amount = (from s in db.SupplierInvoiceDetails where s.SupplierInvoiceID == d.SupplierInvoiceID select s).ToList().Sum(a => a.Rate));
            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");

            return View(lst);
        }

        public ActionResult Create(int id)
        {
           var suppliers = db.SupplierMasters.ToList();
            ViewBag.Supplier = suppliers;
            ViewBag.SupplierType = db.SupplierTypes.ToList();
            ViewBag.Currency = db.CurrencyMasters.ToList();
            SupplierInvoiceVM _supinvoice = new SupplierInvoiceVM();
            ViewBag.CurrencyId = Convert.ToInt32(Session["CurrencyId"].ToString());
            if (id > 0)
            {
                ViewBag.Title = "Supplier Invoice -Modify";
                var _invoice = db.SupplierInvoices.Find(id);
                _supinvoice.SupplierInvoiceID = _invoice.SupplierInvoiceID;
                _supinvoice.InvoiceDate = _invoice.InvoiceDate;
                _supinvoice.InvoiceNo = _invoice.InvoiceNo;
                _supinvoice.SupplierID = _invoice.SupplierID;
                _supinvoice.Remarks = _invoice.Remarks;
                var supplier = suppliers.Where(d => d.SupplierID == _invoice.SupplierID).FirstOrDefault();
                if (supplier != null)
                {
                    _supinvoice.SupplierName = supplier.SupplierName;
                    _supinvoice.SupplierTypeId = Convert.ToInt32(supplier.SupplierTypeID);
                }

                List<SupplierInvoiceDetail> _details = new List<SupplierInvoiceDetail>();
                _details = (from c in db.SupplierInvoiceDetails
                            where c.SupplierInvoiceID == id
                            select c).ToList();


                _supinvoice.SupplierInvoiceDetails = _details;
                
                Session["SInvoiceListing"] = _details;
            }
            else
            {
                ViewBag.Title = "Supplier Invoice - Create";
                var Maxnumber = db.SupplierInvoices.ToList().LastOrDefault();
                if (Maxnumber == null)
                {
                    _supinvoice.InvoiceNo = "TD10001";

                }
                else
                {
                    _supinvoice.InvoiceNo = "TD" + (10000 + Maxnumber.SupplierInvoiceID);
                }
            }
            return View(_supinvoice);

        }
        public JsonResult SetSupplierInvDetails(string invno,string Particulars, decimal Rate, int Qty, int currency, decimal Taxpercent)
        {
            Random rnd = new Random();
            int dice = rnd.Next(1, 7);   // creates a number between 1 and 6
           
            var invoice = new SupplierInvoiceDetailVM();
            invoice.InvNo = invno+"_"+ dice;
            invoice.Particulars = Particulars;
            invoice.Rate =Rate;
            invoice.Quantity = Qty;
            invoice.CurrencyID = currency;
            var currencyMaster = db.CurrencyMasters.Find(currency);
            invoice.CurrencyAmount =Convert.ToDecimal(currencyMaster.ExchangeRate);
            invoice.Currency =currencyMaster.CurrencyName;
            var amount = (Qty * Rate);
            var value = amount + (amount * Taxpercent / 100);
          
            invoice.Amount = amount;
            invoice.Value =value;
            invoice.TaxPercentage = Taxpercent;

            return Json(new { InvoiceDetails = invoice }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierInvDetails(int Id)
        {
            Random rnd = new Random();
             // creates a number between 1 and 6

            var _invoice = db.SupplierInvoices.Find(Id);
            List<SupplierInvoiceDetailVM> _details = new List<SupplierInvoiceDetailVM>();
          var details = (from c in db.SupplierInvoiceDetails
                        where c.SupplierInvoiceID == Id
                        select c).ToList();
            foreach (var item in details)
            {
                int dice = rnd.Next(1, 7);
                var invoice = new SupplierInvoiceDetailVM();
                invoice.InvNo = _invoice.InvoiceNo + "_" + dice;
                invoice.Particulars = item.Particulars;
                invoice.Rate = item.Rate;
                invoice.Quantity = item.Quantity;
                invoice.CurrencyID = item.CurrencyID;
                var currencyMaster = db.CurrencyMasters.Find(item.CurrencyID);
                invoice.CurrencyAmount = Convert.ToDecimal(currencyMaster.ExchangeRate);
                invoice.Currency = currencyMaster.CurrencyName;
                var amount = (item.Quantity * item.Rate);
                var value = amount + (amount * item.TaxPercentage / 100);
                invoice.Amount = amount;
                invoice.Value = value;
                invoice.TaxPercentage = item.TaxPercentage;
                _details.Add(invoice);
            }
            return Json(new { InvoiceDetails = _details }, JsonRequestBehavior.AllowGet);
        }
        //SaveSupplierInvoice
        public JsonResult SaveSupplierInvoice(int Id, int SupplierID, string InvoiceDate, string InvoiceNo,  string Details)
        {
            try
            {
                var IDetails = JsonConvert.DeserializeObject<List<SupplierInvoiceDetailVM>>(Details);

                var Supplierinvoice = (from d in db.SupplierInvoices where d.SupplierInvoiceID == Id select d).FirstOrDefault();
                if (Supplierinvoice == null)
                {
                    Supplierinvoice = new SupplierInvoice();
                }
                else
                {
                    var details = (from d in db.SupplierInvoiceDetails where d.SupplierInvoiceID == Supplierinvoice.SupplierInvoiceID select d).ToList();
                    db.SupplierInvoiceDetails.RemoveRange(details);
                    db.SaveChanges();
                }
                Supplierinvoice.SupplierID = SupplierID;
                Supplierinvoice.InvoiceDate = Convert.ToDateTime(InvoiceDate);
                Supplierinvoice.InvoiceNo = InvoiceNo;
                Supplierinvoice.AccompanyID = Convert.ToInt32(Session["CurrentCompanyID"]); 
                Supplierinvoice.BranchId = Convert.ToInt32(Session["CurrentBranchID"]); 
                Supplierinvoice.FyearID = Convert.ToInt32(Session["fyearid"]);
                Supplierinvoice.InvoiceTotal = IDetails.Sum(d => d.Amount);
                Supplierinvoice.StatusClose = false;
                Supplierinvoice.IsDeleted = false;
                if (Supplierinvoice.SupplierInvoiceID == 0)
                {
                    db.SupplierInvoices.Add(Supplierinvoice);
                }
                db.SaveChanges();
                foreach (var item in IDetails)
                {
                    var InvoiceDetail = new SupplierInvoiceDetail();
                    InvoiceDetail.SupplierInvoiceID = Supplierinvoice.SupplierInvoiceID;
                    InvoiceDetail.Particulars = item.Particulars;
                    InvoiceDetail.Quantity = item.Quantity;
                    InvoiceDetail.Rate = item.Rate;
                    InvoiceDetail.CurrencyID = item.CurrencyID;
                    InvoiceDetail.CurrencyAmount = item.CurrencyAmount;
                    InvoiceDetail.Amount = item.Amount;
                    InvoiceDetail.TaxPercentage = item.TaxPercentage;
                    InvoiceDetail.Value = item.Value;

                    db.SupplierInvoiceDetails.Add(InvoiceDetail);
                    db.SaveChanges();

                }
                return Json(new { status = "ok", message = "Invoice Submitted Successfully!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = "failed", message = e.Message.ToString() }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Delete (int id)
        {
            var details = (from d in db.SupplierInvoiceDetails where d.SupplierInvoiceID == id select d).ToList();
            db.SupplierInvoiceDetails.RemoveRange(details);
            db.SaveChanges();
            SupplierInvoice invoice = (from d in db.SupplierInvoices where d.SupplierInvoiceID == id select d).FirstOrDefault();
            //invoice.SupplierInvoiceDetails = null;
            db.SupplierInvoices.Remove(invoice);
            db.SaveChanges();
           
            TempData["SuccessMsg"] = "You have successfully Deleted Supplier Invoice.";
            return RedirectToAction("Index");
        }
    }
}