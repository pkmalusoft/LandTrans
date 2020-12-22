using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.DAL;
using LTMSV2.Models;

namespace LTMSV2.Controllers
{
    [SessionExpireFilter]
    public class VehicleMasterController : Controller
    {
         Entities1 db = new Entities1();

       

        public ActionResult Index()
        {
            List<VehiclesVM> lst = new List<VehiclesVM>();
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            var data = db.VehicleMasters.Where(cc=>cc.BranchID==branchid).ToList();
            
            foreach (var item in data)
            {
                VehiclesVM v = new VehiclesVM();
                v.Category = item.Category;
                v.VehicleID = item.VehicleID;
                v.VehicleDescription = item.VehicleDescription;
                v.RegistrationNo = item.RegistrationNo;
                v.Model = item.Model;
                if (item.VehicleTypeID != null)
                {
                    v.VehicleTypeID =Convert.ToInt32(item.VehicleTypeID);
                    v.VehicleTypeName = db.VehicleTypes.Find(v.VehicleTypeID).VehicleType1;
                }
                //v.VehicleNO = item.VehicleNo;
                v.RegisteredUnder = item.RegisteredUnder;
                v.RegExpirydate = Convert.ToDateTime(item.RegExpirydate);
                v.InsuranceExpDate = Convert.ToDateTime(item.InsuranceExpDate);
                lst.Add(v);
            }


            return View(lst);
        }

        //
        // GET: /VehicleMaster/Details/5

        public ActionResult Details(int id = 0)
        {
            VehicleMaster vehiclemaster = db.VehicleMasters.Find(id);
            if (vehiclemaster == null)
            {
                return HttpNotFound();
            }
            return View(vehiclemaster);
        }

        //
        // GET: /VehicleMaster/Create

        public ActionResult Create(int id=0)
        {
            ViewBag.Branch = db.BranchMasters.ToList();
            ViewBag.VehicleType = db.VehicleTypes.ToList();
            ViewBag.Achead = db.AcHeads.ToList();
            VehiclesVM v = new VehiclesVM();
            if (id>0)
            {
                v = getVehicleDetail(id);
            }
            else
            {
                v.InsuranceExpDate = null;
                v.RegExpirydate = DateTime.Now.Date;
                v.DepreciationDate = null;
                v.PurchaseDate = null;
            }
            return View(v);
        }

        //
        // POST: /VehicleMaster/Create

        [HttpPost]
        public ActionResult Create(VehiclesVM vm)
        {
            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int depotId = Convert.ToInt32(Session["CurrentDepotID"].ToString());
            int companyId = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
           
                VehicleMaster v = new VehicleMaster();

                int? max = (from d in db.VehicleMasters orderby d.VehicleID descending select d.VehicleID).FirstOrDefault();
                if (vm.VehicleID == 0)
                { 
                    if (max == null)
                    {
                        v.VehicleID = 1;
                    }
                    else
                    {
                        v.VehicleID =Convert.ToInt32(max) + 1;
                    }
                }
                else
                {
                    v = db.VehicleMasters.Find(vm.VehicleID);
                }


                    v.VehicleDescription = vm.VehicleDescription;
                    v.RegistrationNo = vm.RegistrationNo;
                    v.Model = vm.Model;
                    v.Category = vm.Category;
                    v.PurchaseValue = vm.PurchaseValue;
                    v.PurchaseDate = vm.PurchaseDate;
                    v.InsuranceExpDate = vm.InsuranceExpDate;
                    v.DepreciationDate = vm.DepreciationDate;
                    v.RegExpirydate = vm.RegExpirydate;
            //if (vm.PurchaseDate.Date.ToString() != "01-01-0001 00:00:00")
            //    v.PurchaseDate = vm.PurchaseDate;

            //if (vm.InsuranceExpDate.Date.ToString() != "01-01-0001 00:00:00")
            //  v.InsuranceExpDate = vm.InsuranceExpDate;

            //if (vm.RegExpirydate.Date.ToString() != "01-01-0001 00:00:00")


            v.AcCompanyID = companyId;
                    v.Category = vm.Category;
                    v.RegistrationNo = vm.RegistrationNo;

                    //if (vm.DepreciationDate.Date.ToString() != "01-01-0001 00:00:00")
                    //    v.DepreciationDate = vm.DepreciationDate;
                    v.ScrapValue = vm.ScrapValue;
                    v.BranchID = branchid;
                    v.InsuranceCompName = vm.InsuranceCompName;
                    v.PolicyNo = vm.PolicyNo;
                    v.InsuredValue = vm.InsuredValue;
                    v.Mode = vm.Mode;
                    v.FinanceCompany = vm.FinanceCompany;
            v.VehicleTypeID = vm.VehicleTypeID;
            v.RegisteredUnder = vm.RegisteredUnder;
            v.MakeYear = vm.MakeYear;
            v.AcheadId = vm.AcheadId;
            v.ContractExpDate = vm.ContractExpDate;
            v.ContractIssuedDate = vm.ContractIssuedDate;
            v.ContractNo = vm.ContractNo;
            v.ContractRate = vm.ContractRate;
            v.FreeKM = vm.FreeKM;
            v.RateExtraKM = vm.RateExtraKM;
            v.VehicleMaintenance = vm.VehicleMaintenance;
            v.VehicleOwner = vm.VehicleOwner;

            if (vm.VehicleID == 0)
            {
                db.VehicleMasters.Add(v);
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully added Vehicle.";
                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully updated Vehicle.";
                return RedirectToAction("Index");
            }
                
                
            

           
        }

        //
        // GET: /VehicleMaster/Edit/5
        private VehiclesVM getVehicleDetail(int id)
        {
            VehiclesVM v = new VehiclesVM();
            var data = (from d in db.VehicleMasters where d.VehicleID == id select d).FirstOrDefault();
            v.VehicleID = data.VehicleID;
            v.Category = data.Category;
            v.Mode = data.Mode;
            v.FinanceCompany = data.FinanceCompany;
            v.VehicleDescription = data.VehicleDescription;
            v.RegistrationNo = data.RegistrationNo;
            v.Model = data.Model;
            //v.Type = data.Type;
            v.RegistrationNo = data.RegistrationNo;
            if (data.PurchaseDate!=null)
            v.PurchaseDate = data.PurchaseDate.Value;
            if (data.RegExpirydate!=null)
            v.RegExpirydate = data.RegExpirydate.Value;
            v.AcCompanyID =Convert.ToInt32(data.AcCompanyID);
            v.RegistrationNo = data.RegistrationNo;
            v.RegisteredUnder = data.RegisteredUnder;
            
            if (data.RegExpirydate!=null)
              v.RegExpirydate = Convert.ToDateTime(data.RegExpirydate);
            
            if (data.InsuranceExpDate != null)
                v.InsuranceExpDate = Convert.ToDateTime(data.InsuranceExpDate);
            if (data.DepreciationDate != null)
                v.DepreciationDate = Convert.ToDateTime(data.DepreciationDate);

            v.VehicleTypeID =Convert.ToInt32(data.VehicleTypeID);
            v.InsuranceCompName = data.InsuranceCompName;
            v.Model = data.Model;
            v.MakeYear = data.MakeYear;
            v.PurchaseValue = data.PurchaseValue.Value;
            v.InsuredValue = data.InsuredValue.Value;
            v.ScrapValue = data.ScrapValue.Value;
            v.PolicyNo = data.PolicyNo;
            v.ContractNo = data.ContractNo;
            v.ContractRate = data.ContractRate;
            v.FreeKM = data.FreeKM;
            v.RateExtraKM = data.RateExtraKM;
            v.VehicleOwner = data.VehicleOwner;
            v.VehicleMaintenance = data.VehicleMaintenance;
            v.AcheadId = data.AcheadId;
            if (data.ContractIssuedDate != null)
                v.ContractIssuedDate = Convert.ToDateTime(data.ContractExpDate);
            if (data.ContractExpDate != null)
                v.ContractExpDate = Convert.ToDateTime(data.ContractExpDate);
            return v;
        }
       
        //
        // POST: /VehicleMaster/Edit/5

   
      
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleMaster vehiclemaster = db.VehicleMasters.Find(id);
            db.VehicleMasters.Remove(vehiclemaster);
            db.SaveChanges();
            TempData["SuccessMsg"] = "You have successfully Deleted Vehicle.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}