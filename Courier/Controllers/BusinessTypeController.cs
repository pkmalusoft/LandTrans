using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;

namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class BusinessTypeController : Controller
    {
         Entities1 db = new Entities1();


        public ActionResult Index()
        {
            List<BusinessTypeVM> lst = (from c in db.BusinessTypes
                                        join v in db.AcHeads on c.AcheadID equals v.AcHeadID into gj
                                        from subpet in gj.DefaultIfEmpty()
                                        orderby c.BusinessType1
                                        select new BusinessTypeVM {Id=c.Id, BusinessType = c.BusinessType1 ,AcHeadName= subpet.AcHead1 ?? string.Empty }).ToList();          
           
            return View(lst);
        }

       
        public ActionResult Details(int id = 0)
        {
            BusinessType statu = db.BusinessTypes.Find(id);
            if (statu == null)
            {
                return HttpNotFound();
            }
            return View(statu);
        }

       

        public ActionResult Create()
        {
            //ViewBag.statustype = db.tblStatusTypes.ToList();
            return View();
        }

        //
        // POST: /CourierStatus/Create

        [HttpPost]
       
        public ActionResult Create(BusinessTypeVM c)
        {
            if (ModelState.IsValid)
            {
                BusinessType obj = new BusinessType();               
                obj.BusinessType1 = c.BusinessType;
                obj.AcheadID = c.AcHeadId;               

                db.BusinessTypes.Add(obj);

                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully added Business Type.";
                return RedirectToAction("Index");
            }

            return View();
        }

       

        public ActionResult Edit(int id)
        {
            BusinessTypeVM obj = new BusinessTypeVM();
            ViewBag.statustype = db.tblStatusTypes.ToList();

            var c = (from d in db.BusinessTypes where d.Id == id select d).FirstOrDefault();

            if (c == null)
            {
                return HttpNotFound();
            }
            else
            {
                obj.BusinessType = c.BusinessType1;
                obj.AcHeadId = Convert.ToInt32(c.AcheadID);
                if (c.AcheadID!=null)
                {
                    var achead = db.AcHeads.Find(c.AcheadID);
                    if (achead!=null)
                    {
                        obj.AcHeadName = achead.AcHead1;
                    }
                }
            }

            return View(obj);
        }

        //
        // POST: /CourierStatus/Edit/5

        [HttpPost]
   
        public ActionResult Edit(BusinessTypeVM c)
        {
            BusinessType obj = new BusinessType();
            obj.Id = c.Id;
            obj.BusinessType1= c.BusinessType;
            obj.AcheadID = c.AcHeadId;           

            if (ModelState.IsValid)
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully Updated Business Type.";
                return RedirectToAction("Index");
            }
            return View();
        }
    
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessType businesstype = db.BusinessTypes.Find(id);
            db.BusinessTypes.Remove(businesstype);
            db.SaveChanges();
            TempData["SuccessMsg"] = "You have successfully Deleted Business Type.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}