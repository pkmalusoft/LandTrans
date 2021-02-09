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
        public ActionResult Index(string pTDNo, string FromDate, string ToDate)
        {
            ViewBag.Employee = db.EmployeeMasters.ToList();
            ViewBag.PickupRequestStatus = db.PickUpRequestStatus.ToList();

            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());

            DateTime pFromDate;
            DateTime pToDate;
            string TDNo= "";
            if (TDNo == null)
            {
                TDNo ="";
            }
            else
            {
                TDNo = pTDNo;
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
           
            
            List<CostUpdateMasterVM> lst = new List<CostUpdateMasterVM>();

            if (TDNo == "" || TDNo == null)
            {
                lst = (from d in db.CostUpdateMasters
                       join c in db.TruckDetails on d.TruckDetailID equals c.TruckDetailID
                       where (d.EntryDate >= pFromDate && d.EntryDate < pToDate)
                       select new CostUpdateMasterVM
                       {
                           ID = d.ID,
                           EntryDate = d.EntryDate,
                           TDDate=c.TDDate,
                           TDNo = c.ReceiptNo,
                           DriverName=c.DriverName,
                           RegNo=c.RegNo
                           
                       }).ToList();
            }
            else
            {
                lst = (from d in db.CostUpdateMasters
                       join c in db.TruckDetails on d.TruckDetailID equals c.TruckDetailID
                       where (c.ReceiptNo == TDNo)
                       select new CostUpdateMasterVM
                       {
                           ID = d.ID,
                           EntryDate  = d.EntryDate,                           
                           TDDate = c.TDDate,
                           TDNo = c.ReceiptNo,
                           DriverName = c.DriverName,
                           RegNo = c.RegNo
                       }).ToList();
            }

            lst.ForEach(d => d.Amount = (from s in db.CostUpdateDetails where s.MasterID == d.ID select s).ToList().Sum(a => a.Amount));
            lst = lst.OrderByDescending(cc => cc.TDDate).ToList();

            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");
            ViewBag.TDNo = TDNo;

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
            int userId = Convert.ToInt32(Session["UserID"].ToString());
            ViewBag.PaymentType = lsttype;
            ViewBag.Currency = db.CurrencyMasters.ToList();
            ViewBag.Trips = db.TruckDetails.ToList();
            CostUpdateMasterVM vm = new CostUpdateMasterVM();
            if (id==0)
            {
                vm.ID = 0;
                vm.DetailVM = new List<CostUpdateDetailVM>();
                vm.EntryDate = DateTime.Now;
                vm.CurrencyId = Convert.ToInt32(Session["CurrencyId"].ToString());
                var emp = db.EmployeeMasters.Where(cc => cc.UserID == userId).FirstOrDefault();
                if (emp != null)
                {
                    vm.EmployeeID = emp.EmployeeID;
                }
                ViewBag.EditMode = "false";
                Session["CostAWBAllocation"] = null;
                
            }
            else
            {
                ViewBag.Title = "Cost Update - Modify";
                CostUpdateMaster v = db.CostUpdateMasters.Find(id);
                vm.ID = v.ID;
                vm.CurrencyId = Convert.ToInt32(Session["CurrencyId"].ToString());
                vm.EntryDate = v.EntryDate;
                vm.EmployeeID = v.EmployeeID;
                vm.TruckDetailID = v.TruckDetailID;
                var truck = db.TruckDetails.Find(v.TruckDetailID);
                if (truck !=null)
                    vm.TDNo = truck.ReceiptNo;
                else
                    vm.TDNo = "";
                vm.BranchID = v.BranchID;
                vm.AcFinancialYearID = v.AcFinancialYearID;
                ViewBag.EditMode = "true";
                List<CostUpdateConsignmentVM> AWBAllocationall = new List<CostUpdateConsignmentVM>();

                AWBAllocationall = (from c in db.CostUpdateConsignments join d in db.InScanMasters on c.InScanID equals d.InScanID where c.CostUpdateMasterId==id select new CostUpdateConsignmentVM { ID = c.ID, CostUpdateMasterId=c.CostUpdateMasterId,  RevenueCostMasterID = c.RevenueCostMasterID, CostUpdateDetailId = c.CostUpdateDetailId,  Amount = c.Amount, InScanID = c.InScanID, ConsignmentNo = d.ConsignmentNo, ConsignmentDate = d.TransactionDate }).ToList();

                Session["CostAWBAllocation"] = AWBAllocationall;
            }
                        
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CostUpdateMasterVM vm)
        {
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int fyearid = Convert.ToInt32(Session["fyearid"].ToString());
            CostUpdateMaster v = new CostUpdateMaster();
            List<CostUpdateConsignmentVM> AWBAllocationall = (List<CostUpdateConsignmentVM>)Session["CostAWBAllocation"];
            if (vm.ID == 0)
            {
                
                v.EntryDate = vm.EntryDate;
                v.EmployeeID = vm.EmployeeID;
                v.TruckDetailID = vm.TruckDetailID;
                v.BranchID = branchid;
                v.AcFinancialYearID = fyearid;
                db.CostUpdateMasters.Add(v);
                db.SaveChanges();
                //update inscan revenue update status 
                var td = db.TruckDetails.Find(vm.TruckDetailID);
                //td.TDRemarks = vm.Remarks;
                td.CostUpdated = true;
                db.Entry(td).State = System.Data.Entity.EntityState.Modified;
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
                //update inscan revenue update status 
                var td = db.TruckDetails.Find(vm.TruckDetailID);
                //td.TDRemarks = vm.Remarks;
                td.CostUpdated = true;
                db.Entry(td).State = System.Data.Entity.EntityState.Modified;
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
                        //adding consignment referece to this entry
                        int acheadid = Convert.ToInt32(vm.DetailVM[i].RevenueCostMasterID);

                        var oldlist = db.CostUpdateConsignments.Where(cc => cc.CostUpdateDetailId==detail.ID && cc.CostUpdateMasterId == detail.MasterID).ToList();
                        if (oldlist != null)
                        {
                            foreach (var olditem in oldlist)
                            {
                                db.CostUpdateConsignments.Remove(olditem);
                                db.SaveChanges();
                            }
                        }
                        if (AWBAllocationall != null)
                        {

                            var list = AWBAllocationall.Where(cc => cc.RevenueCostMasterID == acheadid).ToList();
                            if (list != null && list.Count>0)
                            {
                                foreach (var item2 in list)
                                {
                                    CostUpdateConsignment accons = new CostUpdateConsignment();
                                    accons.CostUpdateMasterId = detail.MasterID;
                                    accons.CostUpdateDetailId = detail.ID;
                                    accons.RevenueCostMasterID = acheadid;
                                    accons.InScanID = Convert.ToInt32(item2.InScanID);
                                    accons.Amount = item2.Amount;
                                    db.CostUpdateConsignments.Add(accons);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                var listnew = (from c in db.InScanMasters where c.IsDeleted == false && (c.TruckDetailId == vm.TruckDetailID) orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                                decimal consignmentamount = 0;
                                consignmentamount = vm.DetailVM[i].Amount / listnew.Count;
                                foreach (var item2 in listnew)
                                {
                                    CostUpdateConsignment accons = new CostUpdateConsignment();
                                    accons.CostUpdateMasterId = detail.MasterID;
                                    accons.CostUpdateDetailId = detail.ID;
                                    accons.RevenueCostMasterID = acheadid;
                                    accons.InScanID = Convert.ToInt32(item2.InScanID);
                                    accons.Amount = consignmentamount;
                                    db.CostUpdateConsignments.Add(accons);
                                    db.SaveChanges();
                                }

                            }
                        }
                        else
                        {
                            var listnew = (from c in db.InScanMasters where c.IsDeleted == false && (c.TruckDetailId == vm.TruckDetailID) orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                            decimal consignmentamount = 0;
                            consignmentamount = vm.DetailVM[i].Amount / listnew.Count;
                            if (listnew != null && listnew.Count>0)
                            {
                                foreach (var item2 in listnew)
                                {
                                    CostUpdateConsignment accons = new CostUpdateConsignment();
                                    accons.CostUpdateMasterId = detail.MasterID;
                                    accons.CostUpdateDetailId = detail.ID;
                                    accons.RevenueCostMasterID = acheadid;
                                    accons.InScanID = Convert.ToInt32(item2.InScanID);
                                    accons.Amount = consignmentamount;
                                    db.CostUpdateConsignments.Add(accons);
                                    db.SaveChanges();
                                }
                            }
                        }
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

                            //adding consignment referece to this entry
                            int acheadid = Convert.ToInt32(vm.DetailVM[i].RevenueCostMasterID);

                            var oldlist = db.CostUpdateConsignments.Where(cc => cc.CostUpdateDetailId == detail.ID && cc.CostUpdateMasterId == detail.MasterID && cc.RevenueCostMasterID==acheadid).ToList();
                            if (oldlist != null)
                            {
                                foreach (var olditem in oldlist)
                                {
                                    db.CostUpdateConsignments.Remove(olditem);
                                    db.SaveChanges();
                                }
                            }
                            if (AWBAllocationall != null && AWBAllocationall.Count>0 )
                            {

                                var list = AWBAllocationall.Where(cc => cc.RevenueCostMasterID == acheadid).ToList();
                                if (list != null && list.Count > 0)
                                {
                                    foreach (var item2 in list)
                                    {
                                        CostUpdateConsignment accons = new CostUpdateConsignment();
                                        accons.CostUpdateMasterId = detail.MasterID;
                                        accons.CostUpdateDetailId = detail.ID;
                                        accons.RevenueCostMasterID = acheadid;
                                        accons.InScanID = Convert.ToInt32(item2.InScanID);
                                        accons.Amount = item2.Amount;
                                        db.CostUpdateConsignments.Add(accons);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    var listnew = (from c in db.InScanMasters where c.IsDeleted == false && (c.TruckDetailId == vm.TruckDetailID) orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                                    if (listnew != null && listnew.Count > 0)
                                    {
                                        decimal consignmentamount = 0;
                                        consignmentamount = vm.DetailVM[i].Amount / listnew.Count;
                                        foreach (var item2 in listnew)
                                        {
                                            CostUpdateConsignment accons = new CostUpdateConsignment();
                                            accons.CostUpdateMasterId = detail.MasterID;
                                            accons.CostUpdateDetailId = detail.ID;
                                            accons.RevenueCostMasterID = acheadid;
                                            accons.InScanID = Convert.ToInt32(item2.InScanID);
                                            accons.Amount = consignmentamount;
                                            db.CostUpdateConsignments.Add(accons);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var listnew = (from c in db.InScanMasters where c.IsDeleted == false && (c.TruckDetailId == vm.TruckDetailID) orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                                if (listnew != null)
                                {
                                    decimal consignmentamount = 0;
                                    consignmentamount = vm.DetailVM[i].Amount / listnew.Count;
                                    foreach (var item2 in listnew)
                                    {
                                        CostUpdateConsignment accons = new CostUpdateConsignment();
                                        accons.CostUpdateMasterId = detail.MasterID;
                                        accons.CostUpdateDetailId = detail.ID;
                                        accons.RevenueCostMasterID = acheadid;
                                        accons.InScanID = Convert.ToInt32(item2.InScanID);
                                        accons.Amount = consignmentamount;
                                        db.CostUpdateConsignments.Add(accons);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }

                            
                        }
                      
                    }
                    else
                    {
                        if (vm.DetailVM[i].ID > 0)
                        {
                        int detailid = vm.DetailVM[i].ID;
                        var consignmentlist = db.CostUpdateConsignments.Where(cc => cc.CostUpdateDetailId == detailid).ToList();
                        foreach (var item2 in consignmentlist)
                        {
                            db.CostUpdateConsignments.Remove(item2);
                            db.SaveChanges();
                        }
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
            if (!String.IsNullOrEmpty(term.Trim()))
            {
                List<SupplierMasterVM> supplierlist = new List<SupplierMasterVM>();
                supplierlist = (from c in db.SupplierMasters
                                join ac in db.AcHeads on c.AcHeadID equals ac.AcHeadID into gj
                                from subpet in gj.DefaultIfEmpty()
                                where c.SupplierName.ToLower().StartsWith(term.ToLower()) orderby c.SupplierName select new SupplierMasterVM { SupplierID = c.SupplierID, SupplierName = c.SupplierName,AcHeadID=c.AcHeadID , SupplierInfo=subpet.AcHead1 ?? string.Empty }).ToList();

                return Json(supplierlist, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<SupplierMasterVM> supplierlist = new List<SupplierMasterVM>();
                supplierlist =  (from c in db.SupplierMasters
                                 join ac in db.AcHeads on c.AcHeadID equals ac.AcHeadID into gj
                                 from subpet in gj.DefaultIfEmpty()
                                 orderby c.SupplierName select new SupplierMasterVM { SupplierID = c.SupplierID, SupplierName = c.SupplierName,AcHeadID=c.AcHeadID,SupplierInfo=subpet.AcHead1 ?? string.Empty  }).ToList();
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

        public JsonResult GetCostUpdateDetails(int Id)
        {
            var RevenueDetails = (from s in db.CostUpdateDetails
                                  where s.MasterID == Id
                                  join cu in db.CurrencyMasters on s.CurrencyId equals cu.CurrencyID
                                  join dh in db.AcHeads on s.AcHeadDebitId equals dh.AcHeadID
                                  join ch in db.AcHeads on s.AcHeadCreditId equals ch.AcHeadID
                                  join cust in db.SupplierMasters on s.SupplierId equals cust.SupplierID
                                  join inv in db.SupplierInvoices on s.InvoiceId equals inv.SupplierInvoiceID into gj
                                  from subpet in gj.DefaultIfEmpty()
                                  select new CostUpdateMasterVM
                                  {
                                      ID = s.ID,
                                      MasterID = s.MasterID,
                                      SupplierID= s.SupplierId,
                                      Currency = cu.CurrencyName,
                                      DebitAccountName = dh.AcHead1,
                                      CreditAccountName = ch.AcHead1,
                                      Amount = s.Amount,
                                      SupplierName = cust.SupplierName                                      
                                      
                                  }).ToList();

            return Json(RevenueDetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CostComponent(string term)
        {
            int branchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            if (!String.IsNullOrEmpty(term.Trim()))
            {
                List<RevenueCostMasterVM> list = new List<RevenueCostMasterVM>();
                list = (from c in db.RevenueCostMasters join a in db.AcHeads on c.RevenueAcHeadID equals a.AcHeadID  where c.CostComponent!=null && c.CostComponent.ToLower().StartsWith(term.ToLower()) orderby c.CostComponent select new RevenueCostMasterVM { RCID = c.RCID, RevenueComponent = c.CostComponent ,RevenueAcHeadID=c.CostAcHeadID , RevenueHeadName =a.AcHead1}).ToList();

                return Json(list, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<RevenueCostMasterVM> list = new List<RevenueCostMasterVM>();
                list = (from c in db.RevenueCostMasters join a in db.AcHeads on c.RevenueAcHeadID equals a.AcHeadID where c.CostComponent != null orderby c.CostComponent select new RevenueCostMasterVM { RCID = c.RCID, RevenueComponent = c.CostComponent, RevenueAcHeadID = c.CostAcHeadID, RevenueHeadName = a.AcHead1 }).ToList();
                //list = (from c in db.RevenueCostMasters orderby c.CostComponent select new RevenueCostMasterVM { RCID = c.RCID, RevenueComponent = c.CostComponent }).ToList();
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

        public ActionResult GetTrips(string term)
        {
            int AcCompanyID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            if (term.Trim() != "")
            {
                var list = (from c in db.TruckDetails where c.IsDeleted == false && c.CostUpdated==false && c.ReceiptNo.Contains(term.Trim()) orderby c.ReceiptNo select new TruckDetailVM { ReceiptNo = c.ReceiptNo, TDDate = c.TDDate, TruckDetailID = c.TruckDetailID }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from c in db.TruckDetails where c.IsDeleted == false &&  c.CostUpdated==false orderby c.ReceiptNo select new TruckDetailVM { ReceiptNo = c.ReceiptNo, TDDate = c.TDDate, TruckDetailID = c.TruckDetailID }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public JsonResult GetAWBAllocation(int Id,int tripno,decimal amount)
        {
            decimal consignmentamount = 0;
            List<CostUpdateConsignmentVM> AWBAllocationall = new List<CostUpdateConsignmentVM>();
            List<CostUpdateConsignmentVM> AWBAllocation = new List<CostUpdateConsignmentVM>();
            AWBAllocationall = (List<CostUpdateConsignmentVM>)Session["CostAWBAllocation"];
            if (AWBAllocationall == null)
            {
                var list = (from c in db.InScanMasters where c.IsDeleted == false && (c.TruckDetailId == tripno || tripno == 0) orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                if (list != null)
                {
                    consignmentamount = amount / list.Count;
                    foreach (var item2 in list)
                    {
                        CostUpdateConsignmentVM obj = new CostUpdateConsignmentVM();
                        obj.RevenueCostMasterID = Id;
                        obj.ConsignmentNo = item2.ConsignmentNo;
                        obj.InScanID = item2.InScanID;
                        obj.Amount = consignmentamount;
                        AWBAllocationall.Add(obj);
                    }
                }
                Session["CostAWBAllocation"] = AWBAllocationall;
                return Json(AWBAllocationall, JsonRequestBehavior.AllowGet);
            }
            else
            {
                AWBAllocation = AWBAllocationall.Where(cc => cc.RevenueCostMasterID == Id).ToList();
            }

            if (AWBAllocation == null || AWBAllocation.Count==0)
            {
                AWBAllocation = new List<CostUpdateConsignmentVM>();
                var list = (from c in db.InScanMasters where c.IsDeleted == false && (c.TruckDetailId == tripno)  orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                consignmentamount = amount / list.Count;
                foreach (var item2 in list)
                {
                    CostUpdateConsignmentVM obj = new CostUpdateConsignmentVM();
                    obj.RevenueCostMasterID = Id;
                    obj.ConsignmentNo = item2.ConsignmentNo;
                    obj.InScanID = item2.InScanID;
                    obj.Amount = consignmentamount;
                    AWBAllocationall.Add(obj);
                }
                Session["CostAWBAllocation"] = AWBAllocationall;
                AWBAllocation = AWBAllocationall.Where(cc => cc.RevenueCostMasterID == Id).ToList();
                
            }

            return Json(AWBAllocation, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveAWBAllocation(List<CostUpdateConsignmentVM> list)
        {

            List<CostUpdateConsignmentVM> AWBAllocationall = new List<CostUpdateConsignmentVM>();
            List<CostUpdateConsignmentVM> AWBAllocation = new List<CostUpdateConsignmentVM>();
            AWBAllocationall = (List<CostUpdateConsignmentVM>)Session["CostAWBAllocation"];

            if (AWBAllocationall == null)
            {
                AWBAllocationall = new List<CostUpdateConsignmentVM>();
                foreach (var item2 in list)
                {
                    AWBAllocationall.Add(item2);

                }

            }
            else
            {
                int acheadid = Convert.ToInt32(list[0].RevenueCostMasterID);
                AWBAllocationall.RemoveAll(cc => cc.RevenueCostMasterID == acheadid);
                foreach (var item2 in list)
                {
                    AWBAllocationall.Add(item2);
                }
            }

            Session["CostAWBAllocation"] = AWBAllocationall;

            return Json(AWBAllocationall, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetConsignment(string term, int tripno)
        {
            int AcCompanyID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            if (term.Trim() != "")
            {
                var list = (from c in db.InScanMasters where c.IsDeleted == false && c.ConsignmentNo.Contains(term.Trim()) && (c.TruckDetailId == tripno || tripno == 0) orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from c in db.InScanMasters where c.IsDeleted == false && (c.TruckDetailId == tripno || tripno == 0) orderby c.ConsignmentNo select new { InScanID = c.InScanID, TransactionDate = c.TransactionDate, ConsignmentNo = c.ConsignmentNo, TruckDetailID = c.TruckDetailId }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
        }
    }
}