using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.NAL;
using LTMSV2.Models;
using Newtonsoft.Json;
namespace LTMSV2.Controllers
{
    public class RouteController : Controller
    {
        Entities1 db = new Entities1();
        // GET: Routes
        public ActionResult Index()
        {
            return View();
        }
        readonly A ob = new A();
        public List<A> SingleList()
        {
            List<A> list = new List<A>();
            A obj = new A();
            list.Add(obj);
            return list;
        }
        [HttpPost]
        public string GetRoutes(Routes o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                List<Routes> cl = Routes.GetRoutes(o);
                if (cl.Count > 0)
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", data = cl }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = "No data found", data = cl }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public string ManageRoutes(Routes o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                string nres = Routes.ManageRoutes(o);
                int nid;
                if (int.TryParse(nres, out nid))
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", id = nres, data = ob }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = nres, data = ob }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public string CityMasterSelectDepot()
        {
            return JsonConvert.SerializeObject(ngModel.CityMasterSelectDepot());
        }
        public ActionResult GetRouteData(string term)
        {

            if (!String.IsNullOrEmpty(term))
            {
                List<RouteMasterVM> itemlist = (from c in db.RouteMasters where c.RouteName.ToLower().StartsWith(term.ToLower()) orderby c.RouteName select new RouteMasterVM { RouteID = c.RouteID, RouteName = c.RouteName }).ToList();


                return Json(itemlist, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<RouteMasterVM> itemlist = (from c in db.RouteMasters  select new RouteMasterVM { RouteID = c.RouteID, RouteName = c.RouteName }).ToList();


                return Json(itemlist, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Create(int Id)
        {
            RouteMasterVM route = (from d in db.RouteMasters
                                   join o in db.LocationMasters on d.OrginLocationID equals o.LocationID
                                   join de in db.LocationMasters on d.OrginLocationID equals de.LocationID
                                   where d.RouteID == Id
                                   select new RouteMasterVM
                                   {
                                       RouteID=d.RouteID,
                                       RouteName=d.RouteName,
                                       OrginLocationID=d.OrginLocationID ,
                                       DestinationLocationID=d.DestinationLocationID,
                                       OriginName=o.Location,
                                       DestinationName=de.Location,
                                       RouteCode=d.RouteCode,
                                   }).FirstOrDefault();
            var Depots = (from d in db.tblDepots select d).ToList();
            if(Depots==null)
            {
                Depots = new List<tblDepot>();
            }
            ViewBag.Depots = Depots;
            if (route==null)
            {
                route = new RouteMasterVM();
            }
            else
            {
                route.RouteOrders = (from r in db.RouteOrders where r.RouteID == route.RouteID select r).ToList();
                if(route.RouteOrders==null)
                {
                    route.RouteOrders = new List<RouteOrder>();
                }
            }
            return View(route);
        }
        public ActionResult Location(string term)
        {
            if (!String.IsNullOrEmpty(term))
            {
                List<LocationMasterVm> locationMaster = new List<LocationMasterVm>();
                locationMaster = (from c in db.LocationMasters where c.Location.ToLower().StartsWith(term.ToLower()) orderby c.Location select new LocationMasterVm {
                Location=c.Location,
                LocationID=c.LocationID}).ToList();
               
                return Json(locationMaster, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<LocationMasterVm> locationMaster = new List<LocationMasterVm>();
                locationMaster = (from c in db.LocationMasters orderby c.Location
                                  select new LocationMasterVm
                                  {
                                      Location = c.Location,
                                      LocationID = c.LocationID
                                  }).ToList();

                return Json(locationMaster, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult SetDepotDetails(int Id, string DepotName,int Order)
        {
            var invoice = new DepotVM();
            invoice.ID = Id;
            invoice.Depot = DepotName;
            invoice.AgentID = Order+1;
            return Json(new { InvoiceDetails = invoice }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRouteDetails(int Id)
        {
            var Route = (from d in db.RouteMasters
                         join o in db.LocationMasters on d.OrginLocationID equals o.LocationID
                         join de in db.LocationMasters on d.DestinationLocationID equals de.LocationID
                         where d.RouteID == Id
                         select new { d = d, o = o, de = de }).FirstOrDefault();

            var list = new List<DepotVM>();
            var RouteOrders = (from d in db.RouteOrders
                               
                               where d.RouteID == Route.d.RouteID select d).ToList();
            foreach (var item in RouteOrders)
            {
                var invoice = new DepotVM();
                invoice.ID =Convert.ToInt32(item.DepotID);
                invoice.Depot = db.tblDepots.Find(item.DepotID).Depot;
                invoice.AgentID =item.StopOrder;
                list.Add(invoice);
            }
            return Json(new { InvoiceDetails = list,Origin= Route.o.Location,Destination=Route.de.Location }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRoute(int Id,string Code, string Name, int OrginLocationID,int DestinationId, string Details)
        {
            try
            {
                var acopeningmaster = (from d in db.RouteMasters where d.RouteID == Id  select d).FirstOrDefault();
                if (acopeningmaster == null)
                {
                    acopeningmaster = new RouteMaster();
                }
                else
                {
                    var details = (from d in db.RouteOrders where d.RouteID == acopeningmaster.RouteID select d).ToList();
                    db.RouteOrders.RemoveRange(details);
                    db.SaveChanges();
                }
                acopeningmaster.RouteCode = Code;
                acopeningmaster.RouteName =Name;
                acopeningmaster.OrginLocationID = OrginLocationID;
                acopeningmaster.DestinationLocationID = DestinationId;
               
                if (acopeningmaster.RouteID == 0)
                {
                    db.RouteMasters.Add(acopeningmaster);
                }
                db.SaveChanges();
                var IDetails = JsonConvert.DeserializeObject<List<DepotVM>>(Details);
                foreach (var item in IDetails)
                {
                    var InvoiceDetail = new RouteOrder();
                    InvoiceDetail.RouteID = acopeningmaster.RouteID;
                    InvoiceDetail.DepotID = item.ID;
                    InvoiceDetail.StopOrder = item.AgentID;
                   
                    db.RouteOrders.Add(InvoiceDetail);
                    db.SaveChanges();

                }
                return Json(new { status = "ok", message = "Route Submitted Successfully!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = "failed", message = e.Message.ToString() }, JsonRequestBehavior.AllowGet);

            }
        }

    }
    public class LocationMasterVm
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
    }
}