using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using LTMSV2.DAL;
namespace LTMSV2.Controllers
{
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
                       select new SupplierInvoiceVM { InvoiceNo = c.InvoiceNo, InvoiceDate = c.InvoiceDate, SupplierName = s.SupplierName, Amount = 0 }).ToList();
            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");

            return View(lst);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}