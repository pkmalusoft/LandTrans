using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;

namespace LTMSV2.Controllers
{
    public class RevenueCostController : Controller
    {
        Entities1 db = new Entities1();
        // GET: RevenueCost
        public ActionResult Index()
        {
            List<RevenueCostMasterVM> lst = (from c in db.RevenueCostMasters
                                             join ac in db.AcHeads on c.RevenueAcHeadID equals ac.AcHeadID
                                             join ac1 in db.AcHeads on c.CostAcHeadID equals ac1.AcHeadID
                                             select new RevenueCostMasterVM { RCID = c.RCID, RCCode = c.RCCode, RevenueRate = c.RevenueRate, 
                                                 RevenueComponent = c.RevenueComponent, 
                                                 RevenueHeadName=ac.AcHead1,
                                                 CostComponent=c.CostComponent,
                                                 CostHeadName=ac1.AcHead1,
                                                 CostRate = c.CostRate }).ToList();

            return View(lst);
        }

        public ActionResult Create(int id=0)
        {
            RevenueCostMasterVM vm = new RevenueCostMasterVM();
            vm.RCID = 0;
            ViewBag.Title = "Revenue Cost- Create";

            if (id > 0)
            {
                ViewBag.Title = "Revenue Cost- Modify";
                RevenueCostMaster v = db.RevenueCostMasters.Find(id);
                vm.RCID = v.RCID;
                vm.RevenueComponent = v.RevenueComponent;
                vm.RevenueAcHeadID = v.RevenueAcHeadID;
                vm.RevenueHeadName = db.AcHeads.Find(v.RevenueAcHeadID).AcHead1;
                vm.CostComponent = v.CostComponent;
                vm.CostAcHeadID = v.CostAcHeadID;
                vm.CostHeadName = db.AcHeads.Find(v.CostAcHeadID).AcHead1;
                vm.CostMandatory = v.CostMandatory;
                vm.CostRate = v.CostRate;
                vm.RevenueRate = v.RevenueRate;
                vm.RevenueMandatory = v.RevenueMandatory;
                vm.RevenueGroup = v.RevenueGroup;
            }

            List<VoucherTypeVM> lsttype = new List<VoucherTypeVM>();            
            lsttype.Add(new VoucherTypeVM { TypeName = "Freight" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Document Charge" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Customs Duty" });
            lsttype.Add(new VoucherTypeVM { TypeName = "Other" });
            ViewBag.RevenueGroup = lsttype;            
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(RevenueCostMasterVM v)
        {
            RevenueCostMaster vm = new RevenueCostMaster();
            if (v.RCID ==0)
            {
                int? max1 = (from c1 in db.RevenueCostMasters orderby c1.RCID descending select c1.RCID).FirstOrDefault();
                if (max1 == null)
                    vm.RCID = 1;
                else
                    vm.RCID = Convert.ToInt32(max1) + 1;
                vm.RCCode = "";
            }
            else
            {
                vm = db.RevenueCostMasters.Find(v.RCID);
            }
            
                vm.RevenueComponent = v.RevenueComponent;
                vm.RevenueAcHeadID = v.RevenueAcHeadID;
                vm.CostRate = v.CostRate;
                vm.RevenueRate = v.RevenueRate;
                vm.CostComponent = v.CostComponent;
                vm.CostAcHeadID = v.CostAcHeadID;
                
                vm.CostMandatory = v.CostMandatory;
                vm.RevenueMandatory = v.RevenueMandatory;
                vm.RevenueGroup = v.RevenueGroup;

            if (v.RCID ==0)
            {
                db.RevenueCostMasters.Add(vm);
                db.SaveChanges();

                TempData["SuccessMsg"] = "You have successfully added Revenue Cost";
                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(vm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully updated Revenue Cost";
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult DeleteConfirmed(int id)
        {
            RevenueCostMaster cenquery = db.RevenueCostMasters.Where(t => t.RCID == id).FirstOrDefault();
            if (cenquery != null)
            {
                db.RevenueCostMasters.Remove(cenquery);
                db.SaveChanges();

                TempData["SuccessMsg"] = "You have successfully Deleted Revenue Cost";
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Index");
            }
        }
    }
}