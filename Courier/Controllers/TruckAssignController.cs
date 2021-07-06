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
        public ActionResult Index(string TDHNo, string FromDate, string ToDate)
        {
            SessionDataModel.ClearTableVariable();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());
            int yearid = Convert.ToInt32(Session["fyearid"].ToString());

            DateTime pFromDate;
            DateTime pToDate;
            string pTDHNo = "";
            if (TDHNo == null)
            {
                pTDHNo = "";
            }
            else
            {
                pTDHNo = TDHNo;
            }
            if (FromDate == null || ToDate == null)
            {
                DateTime localDateTime1 = CommanFunctions.GetCurrentDateTime();
                pFromDate = localDateTime1.Date; // DateTimeOffset.Now.Date;// CommanFunctions.GetFirstDayofMonth().Date; // DateTime.Now.Date; //.AddDays(-1) ; // FromDate = DateTime.Now;
                pToDate = CommanFunctions.GetLastDayofMonth().Date.AddDays(1); // DateTime.Now.Date.AddDays(1); // // ToDate = DateTime.Now;
            }
            else
            {
                pFromDate = Convert.ToDateTime(FromDate); //.AddDays(-1);
                pToDate = Convert.ToDateTime(ToDate).AddDays(1);

            }

            //List<TruckAssignVM> lst = (from truck in db.TruckDetails
            //                           join c in db.InScanMasters on truck.TruckDetailID equals c.TruckDetailId

            //                           where c.BranchID == branchid && c.DepotID == depotId

            //                           && (truck.TDDate >= pFromDate && c.TransactionDate < pToDate)
            //                           && (c.CourierStatusID == pStatusId || (pStatusId == 0))  //&& c.CourierStatusID >= 4)
            //                           && c.IsDeleted == false
            //                           orderby truck.TDDate descending, truck.ReceiptNo descending
            //                           select new TruckAssignVM { TruckDetailId = truck.TruckDetailID, TDDate = truck.TDDate, ReceiptNo = truck.ReceiptNo, VechileRegistrationNo = truck.RegNo, ConsignmentNo = c.ConsignmentNo, Consignor = c.Consignor, Consignee = c.Consignee, ConsignorCountryName = c.ConsignorCountryName, ConsigneeCountry = c.ConsigneeCountryName, InScanId = c.InScanID, InScanDate = c.TransactionDate }).ToList();  //, requestsource=subpet3.RequestTypeName 
            List<TruckAssignVM> lst = RevenueDAO.GetTruckDetailConsignments(pTDHNo, "", pFromDate, pToDate);
            ViewBag.FromDate = pFromDate.Date.ToString("dd-MM-yyyy");
            ViewBag.ToDate = pToDate.Date.AddDays(-1).ToString("dd-MM-yyyy");
            ViewBag.CourierStatus = db.CourierStatus.Where(cc => cc.CourierStatusID >= 4).ToList();
            ViewBag.CourierStatusList = db.CourierStatus.Where(cc => cc.CourierStatusID >= 4).ToList();
            ViewBag.StatusTypeList = db.tblStatusTypes.ToList();
            ViewBag.CourierStatusId = 0;
            ViewBag.StatusId = pTDHNo;
            return View(lst);

        }

        public ActionResult Create(int id=0)
        {
            TruckAssignVM VM = new TruckAssignVM();                                      

            if (id>0)
            {
                var c=db.TruckDetails.Find(id);
                VM.TruckDetailId = c.TruckDetailID;
                VM.VehicleID = c.VehicleID;                
                VM.VechicleName = db.VehicleMasters.Find(c.VehicleID).RegistrationNo + "-" + c.DriverName;
                VM.ReceiptNo = c.ReceiptNo;
                VM.TDDate = c.TDDate;
                VM.RouteID =Convert.ToInt32(c.RouteID);
                VM.RouteName = db.RouteMasters.Find(VM.RouteID).RouteName;
                ViewBag.EditMode = "True";
                ViewBag.Title = "Truck Assign - Modify";
            }
            else
            {
                ViewBag.EditMode = "False";
                VM.TruckDetailId = 0;
                ViewBag.Title = "Truck Assign - Create";
            }
            
            return View(VM);
        }

        public ActionResult GetTripRouteData(string term, string TripDate)
        {

            if (!String.IsNullOrEmpty(term.Trim()) && TripDate != "")
            {
                DateTime pFromDate;
                pFromDate = Convert.ToDateTime(TripDate);
                var toDate = pFromDate.AddDays(1);

                List<RouteMasterVM> itemlist = (from c in db.RouteMasters
                                                join truck in db.TruckDetails on c.RouteID equals truck.RouteID
                                                where c.RouteName.ToLower().Contains(term.ToLower()) && truck.TDDate >= pFromDate && truck.TDDate <= toDate && truck.VehicleType.Trim()!="F"
                                                orderby c.RouteName
                                                select new RouteMasterVM { RouteID = c.RouteID, RouteName = c.RouteName }).ToList();
               
                    itemlist = itemlist.GroupBy(d => d.RouteID).Select(g => g.First()).ToList();
               

                return Json(itemlist, JsonRequestBehavior.AllowGet);


            }
            else
            {
                //List<RouteMasterVM> itemlist = (from c in db.RouteMasters select new RouteMasterVM { RouteID = c.RouteID, RouteName = c.RouteName }).ToList();

                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTripRouteVechileData(string term, string TripDate, int RouteId)
        {

            if (TripDate != "" && RouteId > 0)
            {
                DateTime pFromDate;
                pFromDate = Convert.ToDateTime(TripDate);
                var toDate = pFromDate.AddDays(1);

                List<VehicleVM> itemlist = (from c  in db.TruckDetails 
                                            where c.TDDate >= pFromDate && c.TDDate <= toDate && c.RouteID == RouteId
                                            orderby c.RegNo
                                            select new VehicleVM { VehicleID = c.VehicleID, VehicleDescription = c.RegNo+" - "+c.DriverName }).ToList();
                if (!String.IsNullOrEmpty(term.Trim()))
                {
                    itemlist = itemlist.Where(d => d.VehicleDescription.ToLower().Contains(term.Trim().ToLower())).ToList();
                }

                itemlist = itemlist.GroupBy(d => d.VehicleID).Select(g => g.First()).ToList();
                return Json(itemlist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetConsignmentsByDate(string TripDate)
        {
            if (TripDate != "")
            {
                DateTime pFromDate;
                pFromDate = Convert.ToDateTime(TripDate);
                var toDate = pFromDate.AddDays(1);
                var Consignments = (from d in db.InScanMasters
                                    where (d.TransactionDate >= pFromDate && d.TransactionDate <= toDate)
                                     && d.IsDeleted == false && (d.TruckDetailId == null || d.TruckDetailId == 0)
                                    select new InScanMasterVM {
                                       InScanID=d.InScanID,
                                      AWBNo=d.ConsignmentNo
                                    }).ToList();
                return Json(Consignments, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitTruckAssign(string TripDate,int RouteId, int VehicleId, string InscanId)
        {
            try
            {
                DateTime pFromDate;
                pFromDate = Convert.ToDateTime(TripDate);
                var toDate = pFromDate.AddDays(1);
                var InscanIds = InscanId.Split(',').Select(Int32.Parse).ToList();
                var TruckDetail = (from d in db.TruckDetails where (d.TDDate >= pFromDate && d.TDDate <= toDate) && d.RouteID == RouteId && d.VehicleID == VehicleId && d.IsDeleted == false select d).FirstOrDefault();
                List<InScanMaster> Inscan = (from d in db.InScanMasters where InscanIds.Contains(d.InScanID) select d).ToList();
                Inscan.ForEach(d => d.TruckDetailId = TruckDetail.TruckDetailID);
                db.SaveChanges();
                int uid = Convert.ToInt32(Session["UserID"].ToString());
                var courierstatus = db.CourierStatus.Where(d => d.CourierStatus.ToLower() == "assign truck").FirstOrDefault();

                foreach (var item in Inscan)
                {
                    AWBTrackStatu _awbstatus = new AWBTrackStatu();
                    int? id = (from c in db.AWBTrackStatus orderby c.AWBTrackStatusId descending select c.AWBTrackStatusId).FirstOrDefault();

                    if (id == null)
                        id = 1;
                    else
                        id = id + 1;

                    _awbstatus.AWBTrackStatusId = Convert.ToInt32(id);
                    _awbstatus.AWBNo = item.ConsignmentNo;
                    _awbstatus.EntryDate = DateTime.Now;
                    _awbstatus.InScanId = item.InScanID;
                    _awbstatus.StatusTypeId = Convert.ToInt32(courierstatus.StatusTypeID);
                    _awbstatus.CourierStatusId = Convert.ToInt32(courierstatus.CourierStatusID);
                    _awbstatus.ShipmentStatus = db.tblStatusTypes.Find(courierstatus.StatusTypeID).Name;
                    _awbstatus.CourierStatus = courierstatus.CourierStatus;
                    _awbstatus.UserId = uid;

                    db.AWBTrackStatus.Add(_awbstatus);
                    db.SaveChanges();
                }
                return Json(new { status = "ok",message="Successfully Submitted" }, JsonRequestBehavior.AllowGet);
            }catch(Exception e)
            {
                return Json(new { status = "failed",message=e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAWBDetail(string id)
        {
            AWBList obj = new AWBList();
            var lst = (from c in db.InScanMasters where c.ConsignmentNo == id  select c).FirstOrDefault();
            if (lst == null)
            {
                return Json(new { status = "failed", data = obj, message = "Consignment No. Not found" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (lst.TruckDetailId == null || lst.TruckDetailId == 0)
                {
                    obj.Origin = lst.ConsignorCountryName;
                    obj.Destination = lst.ConsigneeCountryName;
                    obj.AWB = lst.ConsignmentNo;
                    obj.InScanId = lst.InScanID;

                    return Json(new { status = "ok", data = obj, message = "AWB NO.found" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { status = "failed", data = obj, message = "Consignment already Truck Assigned !" }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult GetAWB(int id)
        {

            List<AWBList> obj = new List<AWBList>();

            //QuickInscanMaster _qinscanvm = db.InScanMasters.Where(cc => cc.TruckDetailId == id).FirstOrDefault();
            TruckDetailVM masterdata = new TruckDetailVM();
            TruckDetail v = db.TruckDetails.Find(id);
            masterdata.ReceiptNo = v.ReceiptNo;
            masterdata.TruckDetailID = v.TruckDetailID;
            masterdata.TDDate = v.TDDate;


            obj = (from _inscan in db.InScanMasters
                   where _inscan.TruckDetailId == id
                   orderby _inscan.ConsignmentNo descending
                   select new AWBList { InScanId = _inscan.InScanID, AWB = _inscan.ConsignmentNo, Origin = _inscan.ConsignorCountryName, Destination = _inscan.ConsigneeCountryName }).ToList();

            if (obj != null)
            {
                return Json(new { status = "ok", masterdata = masterdata, data = obj, message = "Data Found" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "failed", masterdata = masterdata, data = obj, message = "Data Not Found" }, JsonRequestBehavior.AllowGet);
            }

        }

        public class AWBList
        {
            public int InScanId { get; set; }
            public string AWB { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
        }
    }
}