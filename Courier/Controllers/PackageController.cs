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
            return View();
        }


        [HttpPost]
        public JsonResult ManageInsertPackages(tblPackages o)
        {
            var list = new tblPackages().ManageInsertPackages(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ManageUpdatePackages(tblPackages o)
        {
            var list = new tblPackages().ManageUpdatePackages(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ManageDeletePackages(tblPackages o)
        {
            var list = new tblPackages().ManageDeletePackages(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


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
            if (!String.IsNullOrEmpty(term))
            {
                List<PackageVM> itemlist = (from c in list  where c.PackageDescription.ToLower().StartsWith(term.ToLower()) orderby c.PackageDescription select new PackageVM { PackageID = c.PackageID, PackageName = c.PackageDescription  }).ToList();
                                
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