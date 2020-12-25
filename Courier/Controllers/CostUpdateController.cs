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
    public class CostUpdateController : Controller
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
            
            List<CostUpdateMasterVM> lst = RevenueDAO.GetCostUpdateList(pFromDate, pToDate);
                        
            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");            
            
            return View(lst);
        }

        public ActionResult Create(int id=0)
        {
            ViewBag.Title = "Cost Update - Create";
            ViewBag.employee = db.EmployeeMasters.ToList();
            List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();
            lsttype.Add(new VoucherTypeVM { TypeName = "Pickup Cash" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Shipper" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Consignee" });            

            ViewBag.PaymentType = lsttype;
            ViewBag.Currency = db.CurrencyMasters.ToList();
            ViewBag.Trips = db.TruckDetails.ToList();
            CostUpdateMasterVM vm = new CostUpdateMasterVM();
            if (id==0)
            {
                vm.ID = 0;
                vm.DetailVM = new List<CostUpdateDetailVM>();
                vm.EntryDate = DateTime.Now;
                ViewBag.EditMode = "false";
            }
            else
            {
                CostUpdateMaster v = db.CostUpdateMasters.Find(id);
                vm.ID = v.ID;
                vm.EntryDate = v.EntryDate;
                vm.EmployeeID = v.EmployeeID;
                vm.TruckDetailID = v.TruckDetailID;
                vm.BranchID = v.BranchID;
                vm.AcFinancialYearID = v.AcFinancialYearID;
                ViewBag.EditMode = "true";
            }
                        
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CostUpdateMasterVM vm)
        {
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            CostUpdateMaster v = new CostUpdateMaster();
            if (vm.ID == 0)
            {
                
                v.EntryDate = vm.EntryDate;
                v.EmployeeID = vm.EmployeeID;
                v.TruckDetailID = vm.TruckDetailID;
                v.BranchID = branchid;
                v.AcFinancialYearID = fyearid;
                db.CostUpdateMasters.Add(v);
                db.SaveChanges();
            }
            else
            {
                v = db.CostUpdateMasters.Find(vm.ID);
                v.EntryDate = vm.EntryDate;
                v.EmployeeID = vm.EmployeeID;
                v.TruckDetailID = vm.TruckDetailID;
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
                            CostUpdateDetail detail = new CostUpdateDetail();
                            detail.MasterID = v.ID;
                            detail.RevenueCostMasterID = vm.DetailVM[i].RevenueCostMasterID;
                            detail.AcHeadCreditId = vm.DetailVM[i].AcHeadCreditId;
                            detail.AcHeadDebitId = vm.DetailVM[i].AcHeadDebitId;
                            detail.Amount = vm.DetailVM[i].Amount;
                            detail.CurrencyId = vm.DetailVM[i].CurrencyId;
                            detail.SupplierId = vm.DetailVM[i].SupplierId;
                            detail.ExchangeRate = vm.DetailVM[i].ExchangeRate;
                            

                            db.CostUpdateDetails.Add(detail);
                            db.SaveChanges();

                        }
                        else
                        {
                            CostUpdateDetail detail = db.CostUpdateDetails.Find(vm.DetailVM[i].ID);
                            if (detail != null)
                            {
                                detail.MasterID = v.ID;
                                detail.RevenueCostMasterID = vm.DetailVM[i].RevenueCostMasterID;
                                detail.AcHeadCreditId = vm.DetailVM[i].AcHeadCreditId;
                                detail.AcHeadDebitId = vm.DetailVM[i].AcHeadDebitId;
                                detail.Amount = vm.DetailVM[i].Amount;
                                detail.CurrencyId = vm.DetailVM[i].CurrencyId;
                                detail.SupplierId = vm.DetailVM[i].SupplierId;
                                detail.ExchangeRate = vm.DetailVM[i].ExchangeRate;
                                

                                db.Entry(detail).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }

                            
                        }
                      
                    }
                    else
                    {
                        if (vm.DetailVM[i].ID > 0)
                        {
                            CostUpdateDetail detail = db.CostUpdateDetails.Find(vm.DetailVM[i].ID);
                            db.CostUpdateDetails.Remove(detail);
                            db.SaveChanges();
                        }
                    }                
               }
            TempData["SuccessMsg"] = "Trips Cost Updated Successfully!";
            return RedirectToAction("Index");
            
        }

        public ActionResult Supplier(string term)
        {
            int branchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            if (!String.IsNullOrEmpty(term))
            {
                List<SupplierMasterVM> supplierlist = new List<SupplierMasterVM>();
                supplierlist = (from c in db.SupplierMasters where c.SupplierName.ToLower().StartsWith(term.ToLower()) orderby c.SupplierName select new SupplierMasterVM { SupplierID = c.SupplierID, SupplierName = c.SupplierName }).ToList();

                return Json(supplierlist, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<SupplierMasterVM> supplierlist = new List<SupplierMasterVM>();
                supplierlist =  (from c in db.SupplierMasters orderby c.SupplierName select new SupplierMasterVM { SupplierID = c.SupplierID, SupplierName = c.SupplierName}).ToList();
                return Json(supplierlist, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTruckDetail(int id)
        {
            var inscan = db.TruckDetails.Find(id);

            var vehicle = (from c in db.VehicleMasters where c.VehicleID == inscan.VehicleID select c).FirstOrDefault();
            var driver = (from c in db.DriverMasters  where c.DriverID == inscan.DriverID select c).FirstOrDefault();
            int VehicleId = 0;
            string Vehiclename = "";
            int DriverId= 0;
            string DriverName = "";
            if (vehicle != null)
            {
                VehicleId = vehicle.VehicleID;
                Vehiclename = vehicle.RegistrationNo;
            }
            if (driver!= null)
            {
                DriverId = driver.DriverID;
                DriverName = driver.DriverName;
            }

            return Json(new { VehicleId = VehicleId, Vehiclename = Vehiclename, DriverId = DriverId,DriverName=DriverName }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult GetCostUpdateDetail(int id)
        {            
            List<CostUpdateDetailVM> list = RevenueDAO.GetCostUpdateDetail(id);

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