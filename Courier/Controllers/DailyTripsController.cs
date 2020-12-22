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
            ViewBag.Achead = db.AcHeads.Where(d=>d.AcBranchID== branchid).ToList();
            ViewBag.Drivers = db.DriverMasters.ToList();
            ViewBag.Vehicles = db.VehicleMasters.ToList();
            ViewBag.Countries = db.CountryMasters.ToList();
            ViewBag.Documents = db.DocumentSetups.ToList();
            ViewBag.VehicleTypes = db.VehicleTypes.ToList();
            ViewBag.FwdAgents = db.ForwardingAgentMasters.ToList();

            var   truckDetail = (from d in db.TruckDetails where d.TruckDetailID == Id select d).FirstOrDefault();
            if(truckDetail==null)
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
                truckDetail.ReceiptNo = ReceiptNo;
            }
            else
            {
                if(truckDetail.VehicleType=="H")
                {
                    ViewBag.VehicleType = "H";
                }
                else if(truckDetail.VehicleType == "O")
                {
                    ViewBag.VehicleType = "O";
                }
                else if (truckDetail.VehicleType == "C")
                {
                    ViewBag.VehicleType = "C";
                }
                else if (truckDetail.VehicleType == "F")
                {
                    ViewBag.VehicleType = "F";
                }
                else
                {
                    ViewBag.VehicleType = "H";
                }
            }
           
            return View(truckDetail);
        }
       
        public JsonResult SaveHiredVehicle(FormCollection data)
        {
            try
            {
                var TruckDetail = new TruckDetail();
                var TruckDetailID = Convert.ToInt32(data["TruckDetailID"]);
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
                    var ReceiptNo = "TDS-" + MaxId;
                    TruckDetail.ReceiptNo = ReceiptNo;
                }
               
                TruckDetail.VehicleID= Convert.ToInt32(data["VehicleID"]);
                var vehicle = db.VehicleMasters.Find(TruckDetail.VehicleID);
                
                TruckDetail.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"]);
                TruckDetail.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                TruckDetail.VehicleType = Convert.ToString(data["VehicleType"]);
                TruckDetail.TDDate = Convert.ToDateTime(data["TDDate"]);
                TruckDetail.DriverID = Convert.ToInt32(data["DriverID"]);
                TruckDetail.DriverName = Convert.ToString(data["DriverName"]);
                TruckDetail.RegNo = vehicle.RegistrationNo;
                TruckDetail.RouteID = Convert.ToInt32(data["RouteID"]);               
                TruckDetail.OriginName = Convert.ToString(data["OriginName"]);
                TruckDetail.DestinationName = Convert.ToString(data["DestinationName"]);
                TruckDetail.TypeOfLoad = Convert.ToString(data["TypeOfLoad"]);
                TruckDetail.Rent = Convert.ToDecimal(data["Rent"]);
                TruckDetail.CurrencyIDRent = Convert.ToInt32(data["CurrencyIDRent"]);
                TruckDetail.OtherCharges = Convert.ToDecimal(data["OtherCharges"]);
                //TruckDetail.CurrencyRent = Convert.ToDecimal(data["CurrencyRent"]);
                TruckDetail.RentAcHeadID = Convert.ToInt32(data["RentAcHeadID"]);
                TruckDetail.TDRemarks = Convert.ToString(data["TDRemarks"]);
                TruckDetail.StatusPaymentMode = Convert.ToString(data["StatusPaymentMode"]);
                TruckDetail.PaymentHeadID = Convert.ToInt32(data["PaymentHeadID"]);
                TruckDetail.TDcontrolAcHeadID = Convert.ToInt32(data["TDcontrolAcHeadID"]);
                TruckDetail.CurrencyAmount = Convert.ToDecimal(data["CurrencyAmount"]);
                TruckDetail.PaymentCurrencyID = Convert.ToInt32(data["PaymentCurrencyID"]);
                TruckDetail.Remarks = Convert.ToString(data["Remarks"]);
                TruckDetail.IsDeleted =false;
                if (TruckDetail.TruckDetailID == 0)
                {
                    db.TruckDetails.Add(TruckDetail);
                }
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { success = false,message=e.Message.ToString() }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult SaveContractVehicle(FormCollection data)
        {
            try
            {
                var TruckDetail = new TruckDetail();
                var TruckDetailID= Convert.ToInt32(data["TruckDetailID"]);
                TruckDetail = (from d in db.TruckDetails where d.TruckDetailID == TruckDetailID select d).FirstOrDefault();
                if(TruckDetail==null)
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
                    var ReceiptNo = "TDS-" + MaxId;
                    TruckDetail.ReceiptNo = ReceiptNo;
                }
              

                TruckDetail.VehicleID = Convert.ToInt32(data["VehicleID"]);
                var vehicle = db.VehicleMasters.Find(TruckDetail.VehicleID);
                TruckDetail.VehicleType = Convert.ToString(data["VehicleType"]);

                TruckDetail.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"]);
                TruckDetail.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                TruckDetail.TDDate = Convert.ToDateTime(data["TDDate"]);
                TruckDetail.DriverID = Convert.ToInt32(data["DriverID"]);
                TruckDetail.DriverName = Convert.ToString(data["DriverName"]);
                TruckDetail.RegNo = vehicle.RegistrationNo;
                TruckDetail.OriginCountry = Convert.ToString(data["OriginCountry"]);
                TruckDetail.OriginName = Convert.ToString(data["OriginName"]);
                TruckDetail.DestinationName = Convert.ToString(data["DestinationName"]);
                TruckDetail.VehicleTypeID = Convert.ToInt32(data["VehicleTypeID"]);
                TruckDetail.Code = Convert.ToString(data["Code"]);
                  TruckDetail.StatusPaymentMode = Convert.ToString(data["StatusPaymentMode"]);
                TruckDetail.PaymentHeadID = Convert.ToInt32(data["PaymentHeadID"]);
                TruckDetail.TDcontrolAcHeadID = Convert.ToInt32(data["TDcontrolAcHeadID"]);
                TruckDetail.CurrencyAmount = Convert.ToDecimal(data["CurrencyAmount"]);
                TruckDetail.DocumentID = Convert.ToInt32(data["DocumentID"]);
                TruckDetail.PaymentCurrencyID = Convert.ToInt32(data["PaymentCurrencyID"]);
                TruckDetail.Remarks = Convert.ToString(data["Remarks"]);
                TruckDetail.IsDeleted = false;
                if (TruckDetail.TruckDetailID == 0)
                {
                    db.TruckDetails.Add(TruckDetail);
                }
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message.ToString() }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult SaveForwardVehicle(FormCollection data)
        {
            try
            {
                var TruckDetail = new TruckDetail();
                var TruckDetailID = Convert.ToInt32(data["TruckDetailID"]);
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
                    var ReceiptNo = "TDS-" + MaxId;
                    TruckDetail.ReceiptNo = ReceiptNo;
                }


                TruckDetail.VehicleType = Convert.ToString(data["VehicleType"]);

                TruckDetail.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"]);
                TruckDetail.PhoneNumber = Convert.ToString(data["PhoneNumber"]);
                TruckDetail.ForwardAgentID = Convert.ToInt32(data["ForwardAgentID"]);
                TruckDetail.RouteID = Convert.ToInt32(data["RouteID"]);
                TruckDetail.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                TruckDetail.TDDate = Convert.ToDateTime(data["TDDate"]);
                TruckDetail.OriginCountry = Convert.ToString(data["OriginCountry"]);
                TruckDetail.OriginName = Convert.ToString(data["OriginName"]);
                TruckDetail.OriginCity = Convert.ToString(data["OriginCity"]);
                TruckDetail.RentAcHeadID = Convert.ToInt32(data["RentAcHeadID"]);
                TruckDetail.PaymentHeadID = Convert.ToInt32(data["PaymentHeadID"]);
                TruckDetail.TDRemarks = Convert.ToString(data["TDRemarks"]);
                TruckDetail.IsDeleted = false;
                if (TruckDetail.TruckDetailID == 0)
                {
                    db.TruckDetails.Add(TruckDetail);
                }
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message.ToString() }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Drivers(string term)
        {
           
            if (!String.IsNullOrEmpty(term))
            {
                var Driver = new List<DriverMaster>();
                Driver = db.DriverMasters.Where(d => d.DriverName.ToLower().Contains(term.ToLower())).ToList();
                return Json(Driver, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Driver = new List<DriverMaster>();
                Driver = db.DriverMasters.ToList();
                return Json(Driver, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AcHead(string term)
        {

            if (!String.IsNullOrEmpty(term))
            {
                var achead = new List<AcHeadSelectAllVM>();
                achead = (from d in db.AcHeads where d.AcHead1.ToLower().Contains(term.ToLower()) select new AcHeadSelectAllVM {
                    AcHead = d.AcHead1,
                    AcHeadID=d.AcHeadID
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