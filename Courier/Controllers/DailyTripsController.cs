using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using LTMSV2.DAL;
using System.Data;
using System.Data.Entity;

namespace LTMSV2.Controllers
{
    [SessionExpireFilter]
    public class DailyTripsController : Controller
    {
        Entities1 db = new Entities1();

        public ActionResult Index(string VehicleType, string FromDate, string ToDate)
        {
            ViewBag.Employee = db.EmployeeMasters.ToList();
            ViewBag.PickupRequestStatus = db.PickUpRequestStatus.ToList();

            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());

            DateTime pFromDate;
            DateTime pToDate;
            int pStatusId = 0;
            
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

            // List<PickupRequestVM> lst = (from c in db.CustomerEnquiries join t1 in db.EmployeeMasters on c.CollectedEmpID equals t1.EmployeeID join t2 in db.EmployeeMasters on c.EmployeeID equals t2.EmployeeID select new PickupRequestVM { EnquiryID = c.EnquiryID, EnquiryDate = c.EnquiryDate, Consignor = c.Consignor, Consignee = c.Consignee, eCollectedBy = t1.EmployeeName, eAssignedTo = t2.EmployeeName,AWBNo=c.AWBNo }).ToList();

            //List<PickupRequestVM> lst = (from c in db.CustomerEnquiries
            //            join status in db.PickUpRequestStatus on c.PickupRequestStatusId equals status.Id
            //            join pet in db.EmployeeMasters on c.CollectedEmpID equals pet.EmployeeID into gj
            //            from subpet in gj.DefaultIfEmpty()
            //            join pet1 in db.EmployeeMasters on c.EmployeeID equals  pet1.EmployeeID into gj1
            //            from subpet1 in gj1.DefaultIfEmpty()
            //            where  c.EnquiryDate >=pFromDate &&  c.EnquiryDate <=pToDate
            //            select new PickupRequestVM { EnquiryID = c.EnquiryID, EnquiryNo=c.EnquiryNo, EnquiryDate = c.EnquiryDate, Consignor = c.Consignor, Consignee = c.Consignee, eCollectedBy =subpet.EmployeeName ?? string.Empty, eAssignedTo = subpet1.EmployeeName ?? string.Empty , AWBNo = c.AWBNo ,PickupRequestStatus=status.PickRequestStatus }).ToList();

            int Customerid = 0;
            if (Session["UserType"].ToString() == "Customer")
            {

                Customerid = Convert.ToInt32(Session["CustomerId"].ToString());

            }
            List<TruckDetailVM> lst = (from c in db.TruckDetails   
                                       //join d in db.DriverMasters on c.DriverID equals d.DriverID
                                       //join o in db.LocationMasters on c.OriginName equals o.LocationID
                                       //join de in db.LocationMasters on c.DestinationName equals de.LocationID
                                         where c.BranchID == branchid && (c.TDDate >= pFromDate && c.TDDate < pToDate) && (c.IsDeleted==false || c.IsDeleted==null)                                      
                                         orderby c.TDDate descending
                                         select new TruckDetailVM{
                                             TruckDetailID=c.TruckDetailID,
                                             ReceiptNo=c.ReceiptNo,
                                             //DriverName=d.DriverName,
                                             Origin=c.OriginName,
                                             Destination=c.DestinationName,
                                             VehicleType=c.VehicleType,
                                             TDDate=c.TDDate,
                                             RegNo=c.RegNo,
                                             DriverID=c.DriverID,
                                         }).ToList();


            if(!String.IsNullOrEmpty(VehicleType))
            {
                lst = lst.Where(d => d.VehicleType == VehicleType).ToList();
            }
            lst.ForEach(d => d.DriverName = (from s in db.DriverMasters where s.DriverID == d.DriverID select s).FirstOrDefault() == null ? "" : (from s in db.DriverMasters where s.DriverID == d.DriverID select s).FirstOrDefault().DriverName);

            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");
            ViewBag.StatusId = VehicleType;

          
            return View(lst);
        }
        public ActionResult Create(int Id)
        {
            ViewBag.Branch = db.BranchMasters.ToList();
            ViewBag.Routes = db.RouteMasters.ToList();
            ViewBag.Locations = db.LocationMasters.ToList();
            ViewBag.Currency = db.CurrencyMasters.ToList();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int companyId = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            ViewBag.Achead = db.AcHeads.Where(d => d.AcBranchID == branchid).ToList();
            ViewBag.Drivers = db.DriverMasters.ToList();
            ViewBag.Vehicles = db.VehicleMasters.ToList();
            ViewBag.Countries = db.CountryMasters.ToList();
            ViewBag.parceltype = db.ParcelTypes.ToList();
            ViewBag.Documents = db.DocumentSetups.ToList();
            ViewBag.VehicleTypes = db.VehicleTypes.ToList();
            ViewBag.FwdAgents = db.SupplierMasters.Where(c => c.SupplierTypeID == 2).ToList();
            ViewBag.OtherCharge = db.OtherCharges.ToList();
            var transtypes = new SelectList(new[]
                                        {
                                        new { ID = "N", trans = "Select" },                                            
                                            new { ID = "C", trans = "Cash" },
                                            new { ID = "B", trans = "Bank" },

                                        },
            "ID", "trans", "N");

            ViewBag.StatusPaymentMode = transtypes;
            ViewBag.Title = "Daily Trips - Create";
            TruckDetailVM1 vm1 = new TruckDetailVM1();
            var truckDetail = (from d in db.TruckDetails where d.TruckDetailID == Id select d).FirstOrDefault();
            if (truckDetail == null)
            {
                truckDetail = new TruckDetail();
                ViewBag.VehicleType = "H";
                var Truck = db.TruckDetails.ToList().LastOrDefault();
                var MaxId = 1000;
                if (Truck == null)
                {
                    MaxId = 1;
                }
                else
                {
                    MaxId = Truck.TruckDetailID + 1;
                }
                var ReceiptNo = "TDS-" + MaxId;
                PickupRequestDAO _dao = new PickupRequestDAO();
                vm1.ReceiptNo = _dao.GetTDSReceiptNo(companyId,branchid);
                vm1.VehicleType = "H";
                vm1.CurrencyIDRent = Convert.ToInt32(Session["CurrencyId"].ToString());
                vm1.PaymentCurrencyID = Convert.ToInt32(Session["CurrencyId"].ToString());

                List<TruckDetailOtherChargeVM> otherchargesvm = new List<TruckDetailOtherChargeVM>();
                vm1.otherchargesVM = otherchargesvm;
                ViewBag.EditMode = "false";
            }
            else
            {
                ViewBag.EditMode = "true";
                List<TruckDetailOtherChargeVM> otherchargesvm = new List<TruckDetailOtherChargeVM>();
                ViewBag.Title = "Daily Trips - Modify";              
                vm1.TruckDetailID = truckDetail.TruckDetailID;
                vm1.VehicleType = truckDetail.VehicleType;
                vm1.DriverName = truckDetail.DriverName;
                vm1.ReceiptNo = truckDetail.ReceiptNo;
                vm1.TDDate = truckDetail.TDDate;
                vm1.DriverID = truckDetail.DriverID;
                vm1.VehicleID = truckDetail.VehicleID;
                vm1.RouteID = truckDetail.RouteID;
                vm1.OriginName = truckDetail.OriginName;
                vm1.DestinationName = truckDetail.DestinationName;
                vm1.RegNo = truckDetail.RegNo;
                vm1.Rent = truckDetail.Rent;
                vm1.RentAcHeadID = truckDetail.RentAcHeadID;
                vm1.RentAcHead = db.AcHeads.Find(truckDetail.RentAcHeadID).AcHead1;
                vm1.ParcelTypeId = truckDetail.ParcelTypeId;
                vm1.TDRemarks = truckDetail.TDRemarks;
                vm1.VehicleType = truckDetail.VehicleType;
                vm1.OtherCharges = truckDetail.OtherCharges;
                vm1.StatusPaymentMode = truckDetail.StatusPaymentMode.Trim();
                vm1.ChequeDate = truckDetail.ChequeDate;
                vm1.ChequeNo = truckDetail.ChequeNo;
                vm1.ConsignmentNoNote = truckDetail.ConsignmentNoNote;
                vm1.Amount = truckDetail.Amount;
                vm1.FYearID = truckDetail.FYearID;
                vm1.CurrencyIDRent = truckDetail.CurrencyIDRent;
                vm1.PaymentCurrencyID = truckDetail.PaymentCurrencyID;
                vm1.PaymentHeadID = truckDetail.PaymentHeadID;
                vm1.Remarks = truckDetail.Remarks;
                if (truckDetail.PaymentHeadID != null)
                {
                    vm1.PaymentHead = db.AcHeads.Find(truckDetail.PaymentHeadID).AcHead1;
                }
                if (truckDetail.TDcontrolAcHeadID!=null)
                {
                    vm1.TDcontrolAcHeadID = truckDetail.TDcontrolAcHeadID;
                    vm1.TDcontrolAcHead = db.AcHeads.Find(truckDetail.TDcontrolAcHeadID).AcHead1;
                }

                if (truckDetail.VehicleType=="F")
                {
                    vm1.ForwardAgentID = truckDetail.ForwardAgentID;
                }
                otherchargesvm = (from c in db.TruckDetailOtherCharges join o in db.OtherCharges on c.OtherChargeID equals o.OtherChargeID where c.TruckDetailId == Id select new TruckDetailOtherChargeVM { TruckDetailId = Id, OtherChargeID = c.OtherChargeID, OtherChargeName = o.OtherCharge1, Amount = c.Amount }).ToList();
                if (otherchargesvm == null)
                {
                    otherchargesvm = new List<TruckDetailOtherChargeVM>();
                    vm1.otherchargesVM = otherchargesvm;
                }
                else
                {
                    vm1.otherchargesVM = otherchargesvm;
                }

            }

            return View(vm1);
        }

        [HttpPost]
        public ActionResult Create(TruckDetailVM1 data)
        {
            try
            {
                int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                int companyId = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
                int FYearId = Convert.ToInt32(Session["fyearid"].ToString());
                var TruckDetail = new TruckDetail();
                var TruckDetailID = Convert.ToInt32(data.TruckDetailID);
                PickupRequestDAO _dao = new PickupRequestDAO();
                TruckDetail = (from d in db.TruckDetails where d.TruckDetailID == TruckDetailID select d).FirstOrDefault();
                if (TruckDetail == null)
                {
                    TruckDetail = new TruckDetail();
                    var Truck = db.TruckDetails.ToList().LastOrDefault();
                    var MaxId = 1000;
                    if (Truck == null)
                    {
                        MaxId = 1;
                    }
                    else
                    {
                        MaxId = Truck.TruckDetailID + 1;
                    }                                        
                 
                    TruckDetail.ReceiptNo = _dao.GetTDSReceiptNo(companyId, branchid);
                }

                TruckDetail.VehicleID = Convert.ToInt32(data.VehicleID);
                var vehicle = db.VehicleMasters.Find(data.VehicleID);
                TruckDetail.FYearID = FYearId;
                TruckDetail.AcCompanyID = companyId;
                TruckDetail.BranchID = branchid;
                TruckDetail.VehicleType = Convert.ToString(data.VehicleType);

                if (TruckDetail.VehicleType == "F")
                {
                    TruckDetail.ForwardAgentID = Convert.ToInt32(data.ForwardAgentID);
                    TruckDetail.RegNo = Convert.ToString(data.RegNo);
                }
                else
                {
                    TruckDetail.ForwardAgentID = null;
                    TruckDetail.RegNo = vehicle.RegistrationNo;
                    TruckDetail.DriverID = Convert.ToInt32(data.DriverID);
                    TruckDetail.DriverName = Convert.ToString(data.DriverName);
                }

                TruckDetail.TDDate = Convert.ToDateTime(data.TDDate);
                if (data.StatusPaymentMode != null)
                    TruckDetail.StatusPaymentMode = Convert.ToString(data.StatusPaymentMode);
                else
                    TruckDetail.StatusPaymentMode = "N";
                if (TruckDetail.StatusPaymentMode.Trim() == "B")
                {
                    if (data.ChequeNo != null)
                        TruckDetail.ChequeNo = Convert.ToString(data.ChequeNo);
                    try
                    {
                        if (data.ChequeDate != null)
                            TruckDetail.ChequeDate = Convert.ToDateTime(data.ChequeDate);
                    }
                    catch (Exception ex1)
                    {
                        TruckDetail.ChequeDate = null;
                    }
                }
                if (data.RouteID != null)
                    TruckDetail.RouteID = Convert.ToInt32(data.RouteID);

                TruckDetail.OriginName = Convert.ToString(data.OriginName);
                TruckDetail.DestinationName = Convert.ToString(data.DestinationName);
                TruckDetail.ParcelTypeId = data.ParcelTypeId;            
                TruckDetail.Rent = Convert.ToDecimal(data.Rent);
                TruckDetail.CurrencyIDRent = Convert.ToInt32(data.CurrencyIDRent);
                TruckDetail.OtherCharges = Convert.ToDecimal(data.OtherCharges);
                TruckDetail.ConsignmentNoNote = Convert.ToString(data.ConsignmentNoNote);
                //TruckDetail.CurrencyRent = Convert.ToDecimal(data.CurrencyRent);
                TruckDetail.RentAcHeadID = Convert.ToInt32(data.RentAcHeadID);
                TruckDetail.TDRemarks = Convert.ToString(data.TDRemarks);
                
                if (TruckDetail.StatusPaymentMode.Trim() == "C" || TruckDetail.StatusPaymentMode.Trim() == "B")
                {
                    TruckDetail.PaymentHeadID = Convert.ToInt32(data.PaymentHeadID);
                    TruckDetail.TDcontrolAcHeadID = Convert.ToInt32(data.TDcontrolAcHeadID);
                    TruckDetail.CurrencyAmount = Convert.ToDecimal(data.Amount);
                    TruckDetail.Amount = Convert.ToDecimal(data.Amount);
                    TruckDetail.PaymentCurrencyID = Convert.ToInt32(data.PaymentCurrencyID);
                    TruckDetail.Remarks = Convert.ToString(data.Remarks);
                }
                else
                {
                    TruckDetail.StatusPaymentMode = "N";
                }

                TruckDetail.IsDeleted = false;

                if (TruckDetail.TruckDetailID == 0)
                {
                    db.TruckDetails.Add(TruckDetail);
                }

                db.SaveChanges();

                //Other charge data update into inscanothercharge table
                if (data.otherchargesVM != null)
                {
                    for (int j = 0; j < data.otherchargesVM.Count; j++)
                    {
                        //InscanOtherCharge objOtherCharge = new InscanOtherCharge();
                        int oid = Convert.ToInt32(data.otherchargesVM[j].OtherChargeID);
                        TruckDetailOtherCharge objOtherCharge = db.TruckDetailOtherCharges.Where(cc => cc.TruckDetailId == TruckDetail.TruckDetailID && cc.OtherChargeID == oid).FirstOrDefault();
                        if (objOtherCharge == null)
                        {
                            objOtherCharge = new TruckDetailOtherCharge();
                            var maxid = (from c in db.TruckDetailOtherCharges orderby c.TruckDetailOtherChargeID descending select c.TruckDetailOtherChargeID).FirstOrDefault();
                            objOtherCharge.TruckDetailOtherChargeID = maxid + 1;
                            objOtherCharge.TruckDetailId = TruckDetail.TruckDetailID;
                            objOtherCharge.OtherChargeID = data.otherchargesVM[j].OtherChargeID;
                            objOtherCharge.Amount = data.otherchargesVM[j].Amount;
                            db.TruckDetailOtherCharges.Add(objOtherCharge);
                            db.SaveChanges();
                            db.Entry(objOtherCharge).State = EntityState.Detached;
                        }
                        else
                        {
                            //objOtherCharge.OtherChargeID = v.otherchargesVM[j].OtherChargeID;
                            objOtherCharge.Amount = data.otherchargesVM[j].Amount;
                            db.Entry(objOtherCharge).State = EntityState.Modified;
                            db.SaveChanges();
                            db.Entry(objOtherCharge).State = EntityState.Detached;
                        }
                    }

                    var otherchargeitems = db.TruckDetailOtherCharges.Where(cc => cc.TruckDetailId==data.TruckDetailID);
                    var exportdetailsid = data.otherchargesVM.Select(s => s.OtherChargeID).ToList();
                    foreach (var e_details in otherchargeitems)
                    {
                        var _found = data.otherchargesVM.Where(cc => cc.OtherChargeID == e_details.OtherChargeID).FirstOrDefault();
                        if (_found == null)
                        {
                            db.Entry(e_details).State = EntityState.Deleted;

                        }
                    }

                    db.SaveChanges();

                }
                /// other charges saving               
                _dao.GenerateDailyTripsPosting(TruckDetail.TruckDetailID);
                TempData["SuccessMsg"] = "Daily Trips Saved Successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.parceltype = db.ParcelTypes.ToList();
                ViewBag.Routes = db.RouteMasters.ToList();             
                ViewBag.Currency = db.CurrencyMasters.ToList();
                int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                ViewBag.Achead = db.AcHeads.Where(d => d.AcBranchID == branchid).ToList();                
                ViewBag.Documents = db.DocumentSetups.ToList();
                ViewBag.VehicleTypes = db.VehicleTypes.ToList();
                ViewBag.FwdAgents = db.SupplierMasters.Where(c => c.SupplierTypeID == 2).ToList();
                ViewBag.OtherCharge = db.OtherCharges.ToList();
                TempData["ErrorMsg"] = e.Message;
                if (data.otherchargesVM==null)
                {
                    data.otherchargesVM = new List<TruckDetailOtherChargeVM>();
                }
                return View(data);

            }
        }
         public ActionResult Drivers(string term)
        {
           
            if (!String.IsNullOrEmpty(term.Trim()))
            {
                var Driver = new List<DriverMaster>();
                var lst = (from c in db.DriverMasters
                           join v in db.VehicleMasters on c.VehicleID equals v.VehicleID into gj
                           from subpet in gj.DefaultIfEmpty()
                           where c.DriverName.ToLower().Contains(term.Trim().ToLower())
                           orderby c.DriverName
                           select new { DriverID = c.DriverID, DriverName = c.DriverName +"-" + subpet.RegistrationNo ?? string.Empty  , VehicleId = c.VehicleID, RegNo = subpet.RegistrationNo ?? string.Empty }).ToList();
                         
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Driver = new List<DriverMaster>();
                var lst = (from c in db.DriverMasters
                           join v in db.VehicleMasters on c.VehicleID equals v.VehicleID into gj
                           from subpet in gj.DefaultIfEmpty()                           
                           orderby c.DriverName
                           select new { DriverID = c.DriverID, DriverName = c.DriverName + "-" + subpet.RegistrationNo ?? string.Empty, VehicleId = c.VehicleID, RegNo = subpet.RegistrationNo ?? string.Empty }).ToList();
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
        }
               
            public ActionResult VehicleRegNo(string term)
        {
            if (!String.IsNullOrEmpty(term.Trim()))
            {
                List<VehicleVM> list= new List<VehicleVM>();
                list = (from c in  db.VehicleMasters join s in db.SupplierMasters on c.SupplierID equals s.SupplierID where c.RegistrationNo.ToLower().Contains(term.ToLower()) orderby c.RegistrationNo select new VehicleVM { VehicleID = c.VehicleID, RegistrationNo = c.RegistrationNo + "-" + s.SupplierName, VehicleOwner = s.SupplierName}).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<VehicleVM> list = new List<VehicleVM>();
                list = (from c in db.VehicleMasters join s in db.SupplierMasters on c.SupplierID equals s.SupplierID orderby c.RegistrationNo select new VehicleVM { VehicleID = c.VehicleID, RegistrationNo = c.RegistrationNo +"-" + s.SupplierName , VehicleOwner = s.SupplierName }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AcHead(string term,string type)
        {
            int AcCompanyID = Convert.ToInt32(Session["CurrentBranchID"].ToString());            
            if (type == "C")
            {
               
                   var x1 = (from c in db.AcHeads join g in db.AcGroups on c.AcGroupID equals g.AcGroupID where g.AcGroup1 == "Cash" orderby c.AcHead1 select new { AcHeadID = c.AcHeadID, AcHead = c.AcHead1 }).ToList();
                   return Json(x1, JsonRequestBehavior.AllowGet);
               
            }
            else if (type == "B")
            {
                var x1 = (from c in db.AcHeads join g in db.AcGroups on c.AcGroupID equals g.AcGroupID where g.AcGroup1 == "Bank" select new { AcHeadID = c.AcHeadID, AcHead = c.AcHead1 }).ToList();
                return Json(x1, JsonRequestBehavior.AllowGet);
            }
            else
            {

                if (!String.IsNullOrEmpty(term.Trim()))
                {
                    var achead = new List<AcHeadSelectAllVM>();
                    achead = (from d in db.AcHeads
                              where d.AcHead1.ToLower().Contains(term.ToLower())
                              select new AcHeadSelectAllVM
                              {
                                  AcHead = d.AcHead1,
                                  AcHeadID = d.AcHeadID
                              }).ToList();
                    return Json(achead, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var achead = new List<AcHeadSelectAllVM>();
                    achead = (from d in db.AcHeads

                              select new AcHeadSelectAllVM
                              {
                                  AcHead = d.AcHead1,
                                  AcHeadID = d.AcHeadID
                              }).ToList();
                    return Json(achead, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult GetAcHeadsById(int acheadid, int paymentheadid, int ControlAc)
        {
            var Achead = (from d in db.AcHeads where d.AcHeadID == acheadid select d).FirstOrDefault().AcHead1;
            var PaymentAc = (from d in db.AcHeads where d.AcHeadID == paymentheadid select d).FirstOrDefault().AcHead1;
            var controlac = (from d in db.AcHeads where d.AcHeadID == ControlAc select d).FirstOrDefault().AcHead1;
            return Json(new { Achead = Achead, PaymentAc = PaymentAc, controlac = controlac }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetContractAcHeadsById(int paymentheadid, int ControlAc)
        {
            var PaymentAc = (from d in db.AcHeads where d.AcHeadID == paymentheadid select d).FirstOrDefault().AcHead1;
            var controlac = (from d in db.AcHeads where d.AcHeadID == ControlAc select d).FirstOrDefault().AcHead1;
            return Json(new { PaymentAc = PaymentAc, controlac = controlac }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFwdAcHeadsById( int Acheadid)
        {
            var controlac = (from d in db.AcHeads where d.AcHeadID == Acheadid select d).FirstOrDefault().AcHead1;
            return Json(new {  controlac = controlac }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDriverById(int DriverId)
        {
            var Driver = (from d in db.DriverMasters where d.DriverID == DriverId select d).FirstOrDefault().DriverName;
            
            return Json(new { Driver = Driver }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRouteDetail(int RouteId)
        {
            var route = (from d in db.RouteMasters where d.RouteID == RouteId select d).FirstOrDefault();
            string Origin = "";
            string Destination = "";

            if (route!=null)
            {
                Origin = db.LocationMasters.Find(route.OrginLocationID).Location.ToString();
                Destination= db.LocationMasters.Find(route.DestinationLocationID).Location.ToString();
            }

            return Json(new { Origin = Origin, Destination=Destination }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            TruckDetail a = db.TruckDetails.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {
                a.IsDeleted = true;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();

            }
           
            return RedirectToAction("Index");
        }
    }
}