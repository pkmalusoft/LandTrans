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
    public class VehicleTypeController : Controller
    {
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
        public JsonResult ManageInsertVehicleType(tblVehicleType o)
        {
            var list = new tblVehicleType().ManageInsertVehicleType(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ManageUpdateVehicleType(tblVehicleType o)
        {
            var list = new tblVehicleType().ManageUpdateVehicleType(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ManageDeleteVehicleType(tblVehicleType o)
        {
            var list = new tblVehicleType().ManageDeleteVehicleType(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetVehicleType(tblVehicleType o)
        {
            var list = new tblVehicleType().GetVehicleType(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


     

    }
}