using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class DriverController : Controller
    {
        Entities1 db = new Entities1();
        // GET: Driver
        public ActionResult Index()
        {
            var lst = (from c in db.DriverMasters
                       join v in db.VehicleMasters on c.VehicleID equals v.VehicleID into gj
                       from subpet in gj.DefaultIfEmpty()                       
                       orderby c.DriverName
                        //select new { DriverID = c.DriverID, DriverName = c.DriverName, VehicleId = c.VehicleID, }).ToList();
                   select new DriverVM { DriverType=c.DriverType,  DriverID = c.DriverID, DriverName = c.DriverName, LicenseNo = c.LicenseNo, SponsorName = c.SponsorName, PhoneNo1 = c.PhoneNo1 ,PhoneNo2=c.PhoneNo2, RegNo = subpet.RegistrationNo ?? string.Empty }).ToList();
            return View(lst);
        }

        public ActionResult Create(int id=0)
        {

            ViewBag.Supplier = db.SupplierMasters.ToList();
            ViewBag.Vehicles = db.VehicleMasters.ToList();
            ViewBag.Vehicletype = db.VehicleTypes.ToList();
            ViewBag.Title = "Driver Master - Create";
            DriverMaster vm = new DriverMaster();
            if (id>0)
            {
                ViewBag.Title = "DriverMaster - Modify";
                vm = db.DriverMasters.Find(id);
            }

            return View(vm);
        }
        [HttpPost]
        public ActionResult Create(DriverMaster vm)
        {
            
            if (vm.DriverID > 0)
            {
                db.Entry(vm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.DriverMasters.Add(vm);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}