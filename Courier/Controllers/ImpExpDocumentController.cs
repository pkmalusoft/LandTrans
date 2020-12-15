using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class ImpExpDocumentController : Controller
    {
        Entities1 db = new Entities1();
        // GET: ImpExpDocument
        public ActionResult Index()
        {
            List<ImpExpDocumentVM> lst = (from c in db.ImpExpDocumentMasters
                                          join cu in db.CustomerMasters on c.CustomerID equals cu.CustomerID
                                          join p in db.PortMasters on c.PortID equals p.PortID
                                          select new ImpExpDocumentVM
                                          {
                                              DocumentID = c.DocumentID,
                                              DocumentName = c.DocumentName,
                                              IMPEXPCode = c.IMPEXPCode,
                                              CustomerID = c.CustomerID,
                                              CustomerName = cu.CustomerName,
                                              PortName = p.PortName
                                          }).ToList();

            //return View(lst);
            return View(lst);
        }
        public ActionResult Create(int id = 0)
        {
            ViewBag.Port = db.PortMasters.ToList();
            ImpExpDocumentVM vm = new ImpExpDocumentVM();
            ViewBag.Title = "Import/Export Document - Create";
            if (id > 0)
            {
                ViewBag.Title = "Import/Export Document - Modify";
                ImpExpDocumentMaster v = db.ImpExpDocumentMasters.Find(id);
                vm.DocumentID = v.DocumentID;
                vm.DocumentName = v.DocumentName;
                vm.PortID = v.PortID;
                vm.CustomerID = v.CustomerID;
                vm.CustomerName = db.CustomerMasters.Find(v.CustomerID).CustomerName;
                vm.IssueDate = v.IssueDate;
                vm.ExpiryDate = v.ExpiryDate;
                vm.IMPEXPCode = v.IMPEXPCode;
            }
            return View(vm);
        }
        
        [HttpPost]
        public ActionResult Create(ImpExpDocumentVM v)
        {
            ViewBag.Port = db.PortMasters.ToList();
            ImpExpDocumentMaster vm = new ImpExpDocumentMaster();
            if (v.DocumentID == 0)
            {
                int? max1 = (from c1 in db.ImpExpDocumentMasters orderby c1.DocumentID descending select c1.DocumentID).FirstOrDefault();
                if (max1 == null)
                    vm.DocumentID = 1;
                else
                    vm.DocumentID = Convert.ToInt32(max1) + 1;

            }
            else
            {
                vm = db.ImpExpDocumentMasters.Find(v.DocumentID);
            }
                             
                vm.DocumentName = v.DocumentName;
                vm.PortID = v.PortID;
                vm.CustomerID = v.CustomerID;                
                vm.IssueDate = v.IssueDate;
                vm.ExpiryDate = v.ExpiryDate;
            vm.IMPEXPCode = v.IMPEXPCode;
            if (v.DocumentID==0)
            {
                db.ImpExpDocumentMasters.Add(vm);
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully added Import Export Document";
                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(vm).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully updated Import Export Document";
                return RedirectToAction("Index");
            }

            }


        public ActionResult DeleteConfirmed(int id)
        {
            ImpExpDocumentMaster cenquery = db.ImpExpDocumentMasters.Where(t => t.DocumentID == id).FirstOrDefault();
            if (cenquery != null)
            {
                    db.ImpExpDocumentMasters.Remove(cenquery);
                    db.SaveChanges();
                    
                    TempData["SuccessMsg"] = "You have successfully Deleted Document.";
                    return RedirectToAction("Index");            
            }
            else
            {
                
                return RedirectToAction("Index");
            }
        }

    }
}