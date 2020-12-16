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
                                       join d in db.DriverMasters on c.DriverID equals d.DriverID
                                       join o in db.LocationMasters on c.OriginName equals o.LocationID
                                       join de in db.LocationMasters on c.DestinationName equals de.LocationID
                                         where c.BranchID == branchid && (c.TDDate >= pFromDate && c.TDDate < pToDate)                                         
                                         orderby c.TDDate descending
                                         select new TruckDetailVM{
                                             TruckDetailID=c.TruckDetailID,
                                             ReceiptNo=c.ReceiptNo,
                                             DriverName=d.DriverName,
                                             Origin=o.Location,
                                             Destination=de.Location
                                         }).ToList();
            if(!String.IsNullOrEmpty(VehicleType))
            {
                lst = lst.Where(d => d.VehicleType == VehicleType).ToList();
            }

            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");
            ViewBag.StatusId = VehicleType;

          
            return View(lst);
        }
        public ActionResult Create()
        {
            ViewBag.Branch = db.BranchMasters.ToList();
            ViewBag.Routes = db.RouteMasters.ToList();
            ViewBag.Locations = db.LocationMasters.ToList();
            ViewBag.Currency = db.CurrencyMasters.ToList();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            ViewBag.Achead = db.AcHeads.Where(d=>d.AcBranchID== branchid).ToList();
            ViewBag.Drivers = db.DriverMasters.ToList();
            ViewBag.Vehicles = db.VehicleMasters.ToList();
            var TruckDetailVM = new TruckDetailVM();
          
            return View(TruckDetailVM);
        }
        [HttpPost]
        public ActionResult Create(TruckDetailVM model)
        {
            return View();
        }

        public JsonResult SaveHiredVehicle(FormCollection data)
        {
            try
            {
                var TruckDetail = new TruckDetail();
                var Truck = db.TruckDetails.ToList().LastOrDefault();
                var MaxId = 1000;
                if (Truck == null)
                {
                    MaxId = Truck.TruckDetailID + 1;
                }
                var ReceiptNo = "TDS-" + MaxId;
                TruckDetail.ReceiptNo = ReceiptNo;
                TruckDetail.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"]);
                TruckDetail.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
                TruckDetail.VehicleType = Convert.ToString(data["VehicleType"]);
                TruckDetail.TDDate = Convert.ToDateTime(data["TDDate"]);
                TruckDetail.DriverID = Convert.ToInt32(data["DriverID"]);
                TruckDetail.RegNo = Convert.ToString(data["RegNo"]);
                TruckDetail.RouteID = Convert.ToInt32(data["RouteID"]);
                TruckDetail.OriginName = Convert.ToInt32(data["OriginName"]);
                TruckDetail.DestinationName = Convert.ToInt32(data["DestinationName"]);
                TruckDetail.TypeOfLoad = Convert.ToString(data["TypeOfLoad"]);
                TruckDetail.Rent = Convert.ToDecimal(data["Rent"]);
                TruckDetail.CurrencyIDRent = Convert.ToInt32(data["CurrencyIDRent"]);
                TruckDetail.OtherCharges = Convert.ToDecimal(data["OtherCharges"]);
                TruckDetail.CurrencyRent = Convert.ToDecimal(data["CurrencyRent"]);
                TruckDetail.RentAcHeadID = Convert.ToInt32(data["RentAcHeadID"]);
                TruckDetail.TDRemarks = Convert.ToString(data["TDRemarks"]);
                TruckDetail.StatusPaymentMode = Convert.ToString(data["StatusPaymentMode"]);
                TruckDetail.PaymentHeadID = Convert.ToInt32(data["PaymentHeadID"]);
                TruckDetail.TDcontrolAcHeadID = Convert.ToInt32(data["TDcontrolAcHeadID"]);
                TruckDetail.CurrencyAmount = Convert.ToDecimal(data["CurrencyAmount"]);
                TruckDetail.PaymentCurrencyID = Convert.ToInt32(data["PaymentCurrencyID"]);
                TruckDetail.Remarks = Convert.ToString(data["Remarks"]);
                db.TruckDetails.Add(TruckDetail);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { success = false,message=e.Message.ToString() }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}