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
    public class ItemController : Controller
    {
        Entities1 db = new Entities1();
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public JsonResult ManageInsertItemMaster(tblItemMaster o)
        {
            var list = new tblItemMaster().ManageInsertItemMaster(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ManageUpdateItemMaster(tblItemMaster o)
        {
            var list = new tblItemMaster().ManageUpdateItemMaster(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ManageDeleteItemMaster(tblItemMaster o)
        {
            var list = new tblItemMaster().ManageDeleteItemMaster(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetItemMaster(tblItemMaster o)
        {
            var list = new tblItemMaster().GetItemMaster(o);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetItemMasterData(string term)
        {
            
            if (!String.IsNullOrEmpty(term))
            {                
                List<ItemMasterVM> itemlist= (from c in db.ItemMasters where c.ItemName.ToLower().StartsWith(term.ToLower()) orderby c.ItemName select new ItemMasterVM { ItemID = c.ItemID, ItemName=c.ItemName}).ToList();

                
                return Json(itemlist, JsonRequestBehavior.AllowGet);

             
            }
            else
            {
                List<ItemMasterVM> itemlist = (from c in db.ItemMasters orderby c.ItemName select new ItemMasterVM { ItemID = c.ItemID, ItemName = c.ItemName }).ToList();


                return Json(itemlist, JsonRequestBehavior.AllowGet);
            }
        }
    }
}