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
    public class RevenueUpdateController : Controller
    {
        Entities1 db = new Entities1();
        // GET: RevenueUpdate
        public ActionResult Index(string pConsignmentNo, string FromDate, string ToDate)
        {
            ViewBag.Employee = db.EmployeeMasters.ToList();
            ViewBag.PickupRequestStatus = db.PickUpRequestStatus.ToList();

            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());

            DateTime pFromDate;
            DateTime pToDate;
            string ConsignmentNo= "";
            if (ConsignmentNo == null)
            {
                ConsignmentNo ="";
            }
            else
            {
                ConsignmentNo = pConsignmentNo;
            }
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

       
            int Customerid = 0;
            if (Session["UserType"].ToString() == "Customer")
            {

                Customerid = Convert.ToInt32(Session["CustomerId"].ToString());

            }
            List<RevenueUpdateMasterVM> lst = new List<RevenueUpdateMasterVM>();
            //List<RevenueUpdateMasterVM> lst = RevenueDAO.GetRevenueUpdateList(ConsignmentNo, pFromDate, pToDate);
                        
            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");            
            ViewBag.ConsignmentNo = ConsignmentNo;
            return View(lst);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Revenue Update - Create";
            ViewBag.employee = db.EmployeeMasters.ToList();
            List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();
            lsttype.Add(new VoucherTypeVM { TypeName = "Pickup Cash" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Customer" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Shipper" });            

            ViewBag.PaymentType = lsttype;
            ViewBag.Currency = db.CurrencyMasters.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(RevenueUpdateMasterVM vm)
        {
            RevenueUpdateMaster v = new RevenueUpdateMaster();
            v.EntryDate = vm.EntryDate;
            v.EmployeeID = vm.EmployeeID;
            v.InScanID = vm.InScanID;

            ViewBag.Title = "Revenue Update - Create";
            ViewBag.employee = db.EmployeeMasters.ToList();
            List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();
            lsttype.Add(new VoucherTypeVM { TypeName = "Pickup Cash" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Customer" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Shipper" });

            ViewBag.PaymentType = lsttype;
            ViewBag.Currency = db.CurrencyMasters.ToList();
            return View();
        }

        public ActionResult Customer(string term)
        {
            int branchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            if (!String.IsNullOrEmpty(term))
            {
                List<CustmorVM> supplierlist = new List<CustmorVM>();
                supplierlist = (from c in db.CustomerMasters where c.CustomerName.ToLower().StartsWith(term.ToLower()) orderby c.CustomerName select new CustmorVM { CustomerID = c.CustomerID, CustomerName = c.CustomerName    + "( " + c.VATTRN + ")" }).ToList();

                return Json(supplierlist, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<CustmorVM> supplierlist = new List<CustmorVM>();
                supplierlist =  (from c in db.CustomerMasters orderby c.CustomerName select new CustmorVM { CustomerID = c.CustomerID, CustomerName = c.CustomerName + "( " + c.VATTRN + ")" }).ToList();
                return Json(supplierlist, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult RevenueCost(string term)
        {
            int branchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            if (!String.IsNullOrEmpty(term))
            {
                List<RevenueCostMasterVM> list = new List<RevenueCostMasterVM>();
                list = (from c in db.RevenueCostMasters where c.RevenueComponent.ToLower().StartsWith(term.ToLower()) orderby c.RevenueComponent select new RevenueCostMasterVM { RCID = c.RCID, RevenueComponent = c.RevenueComponent }).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<RevenueCostMasterVM> list = new List<RevenueCostMasterVM>();
                list = (from c in db.RevenueCostMasters orderby c.RevenueComponent select new RevenueCostMasterVM { RCID = c.RCID, RevenueComponent = c.RevenueComponent }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCurrencyExRate(int id)
        {
            decimal exrate = 0;
            var currency = db.CurrencyMasters.Find(id);
            exrate =Convert.ToDecimal(currency.ExchangeRate);

            return Json(new { data = exrate }, JsonRequestBehavior.AllowGet);

        }
    }
}