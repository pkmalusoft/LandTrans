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
    [SessionExpire]
    public class PackageController : Controller
    {
        Entities1 db = new Entities1();
        // GET: Item
        public ActionResult Index()
        {
            List<PackageVM> list = (from c in db.Packages orderby c.PackageName select new PackageVM { PackageID = c.PackageID, PackageName = c.PackageName, PackageType = c.PackageType }).ToList();
            return View(list);
        }


        public ActionResult Create(int id=0)
        {
            PackageVM vm = new PackageVM();
            if (id>0)
            {
                ViewBag.Title = "Package Master - Modify";
                Package obj = db.Packages.Find(id);
                vm.PackageID = obj.PackageID;
                vm.PackageName = obj.PackageName;
                vm.PackageType = obj.PackageType;
            }
            else
            {
                ViewBag.Title = "Package Master - Create";
                vm.PackageID = 0;
                vm.PackageName="";
                vm.PackageType = "";

            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(PackageVM model)
        {
            Package obj = new Package();
            if (model.PackageID == 0)
            {
                obj.PackageName = model.PackageName;
                obj.PackageType = model.PackageType;
                db.Packages.Add(obj);
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully added Package";
            }
            else
            {
                obj = db.Packages.Find(model.PackageID);
                obj.PackageName = model.PackageName;
                obj.PackageType = model.PackageType;
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully Updated Package";
            }
                                   
            return RedirectToAction("Index");


        }
        [HttpGet]
        public JsonResult GetPackageType(string term)
        {
            if (term.Trim() != "")
            {
                var list = (from c in db.Packages where c.PackageType.Contains(term.Trim()) orderby c.PackageType select new PackageVM { PackageType = c.PackageType }).Distinct().ToList();
               return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = (from c in db.Packages orderby c.PackageType select new PackageVM { PackageType = c.PackageType }).Distinct().ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Package move = db.Packages.Find(id);
                db.Packages.Remove(move);
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully Deleted Package.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["SuccessMsg"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }
        //[HttpPost]
        //public JsonResult ManageInsertPackages(tblPackages o)
        //{
        //    var list = new tblPackages().ManageInsertPackages(o);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public JsonResult ManageUpdatePackages(tblPackages o)
        //{
        //    var list = new tblPackages().ManageUpdatePackages(o);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public JsonResult ManageDeletePackages(tblPackages o)
        //{
        //    var list = new tblPackages().ManageDeletePackages(o);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult GetPackages(tblPackages o)
        {
            var list = new tblPackages().GetPackages(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPackageData(string term)
        {
            tblPackages o = new tblPackages();
            List<tblPackages> list = new tblPackages().GetPackages(o);
            if (!String.IsNullOrEmpty(term.Trim()))
            {
                List<PackageVM> itemlist = (from c in list  where c.PackageDescription.ToLower().Contains(term.ToLower()) orderby c.PackageDescription select new PackageVM { PackageID = c.PackageID, PackageName = c.PackageDescription  }).ToList();
                                
                return Json(itemlist, JsonRequestBehavior.AllowGet);


            }
            else
            {
                List<PackageVM> itemlist = (from c in list orderby c.PackageDescription select new PackageVM { PackageID = c.PackageID, PackageName = c.PackageDescription }).ToList();


                return Json(itemlist, JsonRequestBehavior.AllowGet);
            }
        }
    }
}