using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using System.Data;
using LTMSV2.DAL;
using System.Data.Entity;


namespace LTMSV2.Controllers
{
    [SessionExpireFilter]
    public class TruckAssignController : Controller
    {
        Entities1 db = new Entities1();
        // GET: TruckAssign
        public ActionResult Index(int? StatusId, string FromDate, string ToDate)
        {
            SessionDataModel.ClearTableVariable();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            DateTime pFromDate;
            DateTime pToDate;
            int pStatusId = 0;
            if (StatusId == null)
            {
                pStatusId = 0;
            }
            else
            {
                pStatusId = Convert.ToInt32(StatusId);
            }
            if (FromDate == null || ToDate == null)
            {
                DateTime localDateTime1 = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
                pFromDate = localDateTime1.Date; // DateTimeOffset.Now.Date;// CommanFunctions.GetFirstDayofMonth().Date; // DateTime.Now.Date; //.AddDays(-1) ; // FromDate = DateTime.Now;
                pToDate = CommanFunctions.GetLastDayofMonth().Date.AddDays(1); // DateTime.Now.Date.AddDays(1); // // ToDate = DateTime.Now;
            }
            else
            {
                pFromDate = Convert.ToDateTime(FromDate); //.AddDays(-1);
                pToDate = Convert.ToDateTime(ToDate).AddDays(1);

            }
                        
            List<TruckAssignVM> lst = (from truck in db.TruckDetails
                                    join  c in db.InScanMasters on truck.TruckDetailID equals c.TruckDetailId                                                                      
                                    
                                    where c.BranchID == branchid && c.DepotID == depotId
                                                                
                                    && (truck.TDDate>= pFromDate && c.TransactionDate < pToDate)
                                    && (c.CourierStatusID == pStatusId || (pStatusId == 0))  //&& c.CourierStatusID >= 4)
                                    && c.IsDeleted == false
                                    orderby truck.TDDate descending, truck.ReceiptNo descending
                                    select new TruckAssignVM {TruckDetailId = truck.TruckDetailID, TDDate = truck.TDDate, ReceiptNo = truck.ReceiptNo, VechileRegistrationNo = truck.RegNo, ConsignmentNo = c.ConsignmentNo, Consignor = c.Consignor, Consignee = c.Consignee, ConsignorCountryName = c.ConsignorCountryName, ConsigneeCountry = c.ConsigneeCountryName, InScanId = c.InScanID, InScanDate = c.TransactionDate }).ToList();  //, requestsource=subpet3.RequestTypeName 

            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");
            ViewBag.CourierStatus = db.CourierStatus.Where(cc => cc.CourierStatusID >= 4).ToList();
            ViewBag.CourierStatusList = db.CourierStatus.Where(cc => cc.CourierStatusID >= 4).ToList();
            ViewBag.StatusTypeList = db.tblStatusTypes.ToList();
            ViewBag.CourierStatusId = 0;
            ViewBag.StatusId = StatusId;
            return View(lst);

        }

        public ActionResult Create()
        {
            ViewBag.Title = "Trick Assign - Create";
            return View();
        }

        public ActionResult GetTripRouteData(string term,string TripDate)
        {

            if (!String.IsNullOrEmpty(term) && TripDate!="")
            {
                DateTime pFromDate;
                pFromDate = Convert.ToDateTime(TripDate);

                List<RouteMasterVM> itemlist = (from c in db.RouteMasters 
                                                join truck in db.TruckDetails on c.RouteID equals truck.RouteID                                                
                                                where c.RouteName.ToLower().StartsWith(term.ToLower()) && truck.TDDate >= pFromDate && truck.TDDate <= pFromDate.AddDays(1)
                                                orderby c.RouteName select new RouteMasterVM { RouteID = c.RouteID, RouteName = c.RouteName }).ToList();

                return Json(itemlist, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<RouteMasterVM> itemlist = (from c in db.RouteMasters select new RouteMasterVM { RouteID = c.RouteID, RouteName = c.RouteName }).ToList();

                return Json(itemlist, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTripRouteVechileData(string TripDate,int RouteId)
        {

            if (TripDate != "" && RouteId>0)
            {
                DateTime pFromDate;
                pFromDate = Convert.ToDateTime(TripDate);

                List<VehicleVM> itemlist = (from c in db.VehicleMasters
                                                join truck in db.TruckDetails on c.VehicleID equals truck.VehicleID
                                                where truck.TDDate >= pFromDate && truck.TDDate <= pFromDate.AddDays(1)  && truck.RouteID==RouteId
                                                orderby c.VehicleDescription
                                                select new VehicleVM { VehicleID = c.VehicleID, VehicleDescription=c.VehicleDescription }).ToList();

             
                return Json(itemlist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}