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
            //List<RevenueUpdateMasterVM> lst = new List<RevenueUpdateMasterVM>();
            List<RevenueUpdateMasterVM> lst = RevenueDAO.GetRevenueUpdateList(ConsignmentNo, pFromDate, pToDate);
                        
            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");            
            ViewBag.ConsignmentNo = ConsignmentNo;
            return View(lst);
        }

        public ActionResult Create(int id=0)
        {
            ViewBag.Title = "Revenue Update - Create";
            ViewBag.employee = db.EmployeeMasters.ToList();
            List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();
            lsttype.Add(new VoucherTypeVM { TypeName = "Pickup Cash" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Shipper" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Consignee" });            

            ViewBag.PaymentType = lsttype;
            ViewBag.Currency = db.CurrencyMasters.ToList();
            ViewBag.Consignment = db.InScanMasters.ToList();
            RevenueUpdateMasterVM vm = new RevenueUpdateMasterVM();
            if (id==0)
            {
                vm.ID = 0;
                vm.DetailVM = new List<RevenueUpdateDetailVM>();
                vm.EntryDate = DateTime.Now;
                ViewBag.EditMode = "false";
            }
            else
            {
                RevenueUpdateMaster v = db.RevenueUpdateMasters.Find(id);
                vm.ID = v.ID;
                vm.EntryDate = v.EntryDate;
                vm.EmployeeID = v.EmployeeID;
                vm.InScanID = v.InScanID;
                vm.BranchID = v.BranchID;
                vm.AcFinancialYearID = v.AcFinancialYearID;
                ViewBag.EditMode = "true";
            }
            //vm.PickupCashHeadId = db.AcHeads.Where(cc => cc.AcHead1 == "Main Cash Account").FirstOrDefault().AcHead1;
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(RevenueUpdateMasterVM vm)
        {
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            RevenueUpdateMaster v = new RevenueUpdateMaster();
            if (vm.ID == 0)
            {
                
                v.EntryDate = vm.EntryDate;
                v.EmployeeID = vm.EmployeeID;
                v.InScanID = vm.InScanID;
                v.BranchID = branchid;
                v.AcFinancialYearID = fyearid;
                db.RevenueUpdateMasters.Add(v);
                db.SaveChanges();
            }
            else
            {
                v = db.RevenueUpdateMasters.Find(vm.ID);
                v.EntryDate = vm.EntryDate;
                v.EmployeeID = vm.EmployeeID;
                v.InScanID = vm.InScanID;
                v.BranchID = branchid;
                v.AcFinancialYearID = fyearid;
                db.Entry(v).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            
                for (int i = 0; i < vm.DetailVM.Count; i++)
                {
                    if (vm.DetailVM[i].IsDeleted != true)
                    {
                        if (vm.DetailVM[i].ID==0)
                        {
                            RevenueUpdateDetail detail = new RevenueUpdateDetail();
                            detail.MasterID = v.ID;
                            detail.RevenueCostMasterID = vm.DetailVM[i].RevenueCostMasterID;
                            detail.AcHeadCreditId = vm.DetailVM[i].AcHeadCreditId;
                            detail.AcHeadDebitId = vm.DetailVM[i].AcHeadDebitId;
                            detail.Amount = vm.DetailVM[i].Amount;
                            detail.CurrencyId = vm.DetailVM[i].CurrencyId;
                            detail.CustomerId = vm.DetailVM[i].CustomerId;
                            detail.ExchangeRate = vm.DetailVM[i].ExchangeRate;
                            detail.PaymentType = vm.DetailVM[i].PaymentType;

                            db.RevenueUpdateDetails.Add(detail);
                            db.SaveChanges();

                        }
                        else
                        {
                            RevenueUpdateDetail detail = db.RevenueUpdateDetails.Find(vm.DetailVM[i].ID);
                            if (detail != null)
                            {
                                detail.MasterID = v.ID;
                                detail.RevenueCostMasterID = vm.DetailVM[i].RevenueCostMasterID;
                                detail.AcHeadCreditId = vm.DetailVM[i].AcHeadCreditId;
                                detail.AcHeadDebitId = vm.DetailVM[i].AcHeadDebitId;
                                detail.Amount = vm.DetailVM[i].Amount;
                                detail.CurrencyId = vm.DetailVM[i].CurrencyId;
                                detail.CustomerId = vm.DetailVM[i].CustomerId;
                                detail.ExchangeRate = vm.DetailVM[i].ExchangeRate;
                                detail.PaymentType = vm.DetailVM[i].PaymentType;

                                db.Entry(detail).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }

                            
                        }
                      
                    }
                    else
                    {
                        if (vm.DetailVM[i].ID > 0)
                        {
                            RevenueUpdateDetail detail = db.RevenueUpdateDetails.Find(vm.DetailVM[i].ID);
                            db.RevenueUpdateDetails.Remove(detail);
                            db.SaveChanges();
                        }
                    }                
               }
            TempData["SuccessMsg"] = "Consignment Revenue Updated Successfull!";
            return RedirectToAction("Index");
            //ViewBag.Title = "Revenue Update - Create";
            //ViewBag.employee = db.EmployeeMasters.ToList();
            //List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();
            //lsttype.Add(new VoucherTypeVM { TypeName = "Pickup Cash" });
            //lsttype.Add(new VoucherTypeVM { TypeName = "Customer" });
            //lsttype.Add(new VoucherTypeVM { TypeName = "Shipper" });

            //ViewBag.PaymentType = lsttype;
            //ViewBag.Currency = db.CurrencyMasters.ToList();
            //return View();
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

        public JsonResult GetConsignmentDetail(int id)
        {
            var inscan = db.InScanMasters.Find(id);

            var cust = (from c in db.CustomerMasters where c.CustomerName == inscan.Consignor select c).FirstOrDefault();
            var receiver = (from c in db.CustomerMasters where c.CustomerName == inscan.Consignee select c).FirstOrDefault();
            int consignorid = 0;
            string consignorname = "";
            int consigneeid = 0;
            string consigneename = "";
            if (cust != null)
            {
                consignorid = cust.CustomerID;
                consignorname = cust.CustomerName;
            }
            if (receiver != null)
            {
                consigneeid = receiver.CustomerID;
                consigneename = receiver.CustomerName;
            }

            return Json(new { ConsignorId = consignorid, ConsignorName = consignorname, ConsigneeId = consigneeid, ConsigneeName = consigneename }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult GetRevenueUpdateDetail(int id)
        {
            //List<RevenueUpdateDetailVM> list = new List<RevenueUpdateDetailVM>();
            List<RevenueUpdateDetailVM> list = RevenueDAO.GetRevenueUpdateDetail(id);

            return Json(new { data=list }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RevenueCost(string term)
        {
            int branchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            if (!String.IsNullOrEmpty(term))
            {
                List<RevenueCostMasterVM> list = new List<RevenueCostMasterVM>();
                list = (from c in db.RevenueCostMasters join a in db.AcHeads on c.RevenueAcHeadID equals a.AcHeadID  where c.RevenueComponent.ToLower().StartsWith(term.ToLower()) orderby c.RevenueComponent select new RevenueCostMasterVM { RCID = c.RCID, RevenueComponent = c.RevenueComponent ,RevenueAcHeadID=c.RevenueAcHeadID , RevenueHeadName =a.AcHead1}).ToList();

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