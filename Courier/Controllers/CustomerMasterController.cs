﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using LTMSV2.DAL;
using System.Data.Entity.Validation;

namespace LTMSV2.Controllers
{
    [SessionExpireFilter]
    public class CustomerMasterController : Controller
    {
        Entities1 db = new Entities1();


        public ActionResult Home()
        {
            var Query = (from t in db.Menus join t1 in db.MenuAccessLevels on t.MenuID  equals t1.MenuID  where t1.RoleID==13 &&  t.IsAccountMenu.Value == false orderby t.MenuOrder select t).ToList();

            var Query1 = (from t in db.Menus join t1 in db.MenuAccessLevels on t.MenuID equals t1.ParentID where t1.RoleID == 13 && t.IsAccountMenu.Value == false orderby t.MenuOrder select t).ToList();

            foreach(Menu q in Query)
            {
                Query1.Add(q);
            }


            //List<MenuVM> Query = (
            //      from a in db.Menus
            //      from b in db.MenuAccessLevels
            //           .Where(bb => bb.MenuID == a.MenuID)
            //      from c in db.MenuAccessLevels
            //           .Where(cc => cc.ParentID == a.MenuID)
            //      where b.RoleID == 13 && c.RoleID == 13
            //      select new MenuVM  { MenuID = a.MenuID, Title = a.Title, Link = a.Link, ParentID = a.ParentID, Ordering = a.Ordering, SubLevel = a.SubLevel, RoleID = a.RoleID , CreatedBy =a.CreatedBy,  CreatedOn =a.CreatedOn,   ModifiedBy =a.ModifiedBy, ModifiedOn=a.ModifiedOn,
            //    IsActive=a.IsActive,
            //    imgclass =a.imgclass,
            //    PermissionRequired=a.PermissionRequired,
            //    MenuOrder=a.MenuOrder,
            //    IsAccountMenu=a.IsAccountMenu
            //      }).ToList();

            //select new
            //{
            //    ss=t.
            //    First_Name = d.First_Name
            //}
            ViewBag.UserName = SourceMastersModel.GetUserFullName(Convert.ToInt32(Session["UserId"].ToString()), Session["UserType"].ToString());

            Session["Menu"] = Query1;
            return View();


        }



        public ActionResult Index()
        {
            List<CustmorVM> lst = new List<CustmorVM>();
            var data = db.CustomerMasters.Where(ite => ite.StatusActive.HasValue ? ite.StatusActive == true : false).ToList();

            foreach (var item in data)
            {
                CustmorVM c = new CustmorVM();

                c.CustomerID = item.CustomerID;
                c.CustomerType = item.CustomerType;
                c.CustomerCode = item.CustomerCode;
                c.CustomerName = item.CustomerName;
                c.ContactPerson = item.ContactPerson;
                c.Mobile = item.Mobile;
                c.Phone = item.Phone;
                lst.Add(c);
            }

            return View(lst);
        }

        //
        // GET: /CustomerMaster/Details/5

        public ActionResult Details(int id = 0)
        {
            CustomerMaster customermaster = db.CustomerMasters.Find(id);
            if (customermaster == null)
            {
                return HttpNotFound();
            }
            return View(customermaster);
        }



        public ActionResult Create()
        {
            var transtypes = new SelectList(new[] 
                                        {
                                            new { ID = "Cr", trans = "Credit" },
                                            new { ID = "Dr", trans = "Debit" },
                                           
                                        },
          "ID", "trans", 1);




            ViewBag.businessType = db.BusinessTypes.ToList();
            ViewBag.country = db.CountryMasters.ToList();
            ViewBag.city = db.CityMasters.ToList();
            ViewBag.location = db.LocationMasters.ToList();
            ViewBag.currency = db.CurrencyMasters.ToList();
            ViewBag.employee = db.EmployeeMasters.ToList();
            ViewBag.roles = db.RoleMasters.ToList();
            int BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            var data = db.tblDepots.Where(c => c.BranchID == BranchID).ToList();
            ViewBag.Depot = data;

            int branchid = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            
            PickupRequestDAO doa = new PickupRequestDAO();
            ViewBag.CustomerNo = doa.GetMaxCustomerCode(branchid);
            CustmorVM obj = new CustmorVM();
            obj.RoleID = 13;
            obj.Password = doa.RandomPassword(6);
            return View(obj);
        }


        [HttpPost]

        public ActionResult Create(CustmorVM c)
        {
            string locationname = c.LocationName;
            string country = c.CountryName;
            string city = c.CityName;
            CustomerMaster obj = new CustomerMaster();
            PickupRequestDAO _dao = new PickupRequestDAO();
            int max = (from d in db.CustomerMasters orderby d.CustomerID descending select d.CustomerID).FirstOrDefault();


            int accompanyid = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            int branchid= Convert.ToInt32(Session["CurrentBranchID"].ToString());
            obj.CustomerID = max + 1;
            obj.AcCompanyID = accompanyid;

            obj.CustomerCode = c.CustomerCode; //  _dao.GetMaxCustomerCode(branchid); // c.CustomerCode;
            obj.CustomerName = c.CustomerName;
            obj.CustomerType = c.CustomerType;

            obj.ReferenceCode = c.ReferenceCode;
            obj.ContactPerson = c.ContactPerson;
            obj.Address1 = c.Address1;
            obj.Address2 = c.Address2;
            obj.Address3 = c.Address3;
            obj.Phone = c.Phone;
            obj.Mobile = c.Mobile;
            obj.Fax = c.Fax;
            obj.Email = c.Email;
            obj.WebSite = c.Website;
            obj.CountryID = c.CountryID;
            obj.CityID = c.CityID;
            obj.LocationID = c.LocationID;
            obj.CountryName = c.CountryName;
            obj.CityName = c.CityName;
            obj.LocationName = c.LocationName;
            if (c.CurrenceyID == 0)
            {
                c.CurrenceyID = Convert.ToInt32(Session["CurrencyId"].ToString());
            }
            else
            {
                obj.CurrencyID = c.CurrenceyID;
            }
            obj.StatusActive = c.StatusActive;
            obj.CreditLimit = c.CreditLimit;
            obj.StatusTaxable = c.StatusTaxable;
            obj.EmployeeID = c.EmployeeID;
            obj.statusCommission = c.StatusCommission;


            obj.CourierServiceID = c.CourierServiceID;
            obj.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            obj.CustomerUsername = c.CustomerUsername;
            //obj.Password = c.Password;
            obj.Password = _dao.RandomPassword(6);
            obj.BusinessTypeId = c.BusinessTypeId;
            obj.Referal = c.Referal;
            obj.OfficeOpenTime = c.OfficeTimeFrom;
            obj.OfficeCloseTime = c.OfficeTimeTo;
            if (c.DepotID==null)
            obj.DepotID = Convert.ToInt32(Session["CurrentDepotID"].ToString());
            else
                obj.DepotID = c.DepotID;

            //UserRegistration u = new UserRegistration();
            //if (c.Email != null)
            //{
            //    if (c.Email != "")
            //    {                  

            //        UserRegistration x = (from a in db.UserRegistrations where a.UserName == c.Email select a).FirstOrDefault();
            //        if (x == null)
            //        {

            //            int max1 = (from c1 in db.UserRegistrations orderby c1.UserID descending select c1.UserID).FirstOrDefault();
            //            u.UserID = max1 + 1;
            //            u.UserName = c.Email;
            //            u.EmailId = c.Email;
            //            u.Password = obj.Password;
            //            u.Phone = c.Phone;
            //            u.IsActive = true;
            //            u.RoleID = c.RoleID;


            //        }
            //        try
            //        {
            //            db.UserRegistrations.Add(u);
            //            db.SaveChanges();
            //        }
            //        catch (DbEntityValidationException e)
            //        {
            //            foreach (var eve in e.EntityValidationErrors)
            //            {
            //                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //                foreach (var ve in eve.ValidationErrors)
            //                {
            //                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                        ve.PropertyName, ve.ErrorMessage);
            //                }
                            
            //            }
            //        }
            //    }
            //}
          
                try
                {
                  //  obj.UserID = u.UserID;

                    db.CustomerMasters.Add(obj);
                    db.SaveChanges();
                    if (c.EmailNotify==true)
                    {
                        EmailDAO _emaildao = new EmailDAO();
                        _emaildao.SendCustomerEmail(c.Email, c.CustomerName, obj.Password);

                    }

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                }

            
                TempData["SuccessMsg"] = "You have successfully added Customer.";
                return RedirectToAction("Index");
           


            return View();
        }

        //GetStatus
        [HttpPost]
        public JsonResult GetCustomerCode(string custname)
        {
            string status = "ok";
            string customercode = "";
            //List<CourierStatu> _cstatus = new List<CourierStatu>();
            try
            {
                string custform = "000000";
                string maxcustomercode = (from d in db.CustomerMasters orderby d.CustomerID descending select d.CustomerCode).FirstOrDefault();
                string last6digit = "";
                if (maxcustomercode==null)
                {
                    //maxcustomercode="AA000000";
                    last6digit = "0";
                        
                }
                else
                {
                    last6digit = maxcustomercode.Substring(maxcustomercode.Length - 6); //, maxcustomercode.Length - 6);
                }
                if (last6digit !="")
                {
                    
                    string customerfirst = custname.Substring(0, 1);
                    string customersecond = "";
                    try
                    {
                        customersecond = custname.Split(' ')[1];
                        customersecond = customersecond.Substring(0, 1);
                    }
                    catch(Exception ex)
                    {

                    }
                    
                    if (customerfirst !="" && customersecond!="") 
                    {
                        customercode = customerfirst + customersecond + String.Format("{0:000000}", Convert.ToInt32(last6digit) + 1); 
                    }
                    else
                    {
                        customercode = customerfirst + "C" + String.Format("{0:000000}", Convert.ToInt32(last6digit) + 1);
                    }

                }
                                
                return Json(new { data = customercode, result = status }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                status = ex.Message;
            }

            return Json(new { data = "", result = "failed" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Edit(int id)
        {
            var c = (from d in db.CustomerMasters where d.CustomerID == id select d).FirstOrDefault();

            //var c = (from d in db.CustomerMasters join t1 in db.UserRegistrations on d.Email equals t1.EmailId   where d.CustomerID == id select d).FirstOrDefault();

            //(from c in db.CustomerEnquiries join t1 in db.EmployeeMasters on c.CollectedEmpID equals t1.EmployeeID join t2 in db.EmployeeMasters on c.EmployeeID equals t2.EmployeeID select new PickupRequestVM { EnquiryID = c.EnquiryID, EnquiryDate = c.EnquiryDate, Consignor = c.Consignor, Consignee = c.Consignee, eCollectedBy = t1.EmployeeName, eAssignedTo = t2.EmployeeName, AWBNo = c.AWBNo }).ToList();
            CustmorVM obj = new CustmorVM();
            //ViewBag.country = db.CountryMasters.ToList();
            //ViewBag.city = db.CityMasters.ToList().Where(x => x.CountryID == c.CountryID);
            //ViewBag.location = db.LocationMasters.ToList().Where(x => x.CityID == c.CityID);
            ViewBag.currency = db.CurrencyMasters.ToList();
            ViewBag.employee = db.EmployeeMasters.ToList();
            ViewBag.businessType = db.BusinessTypes.ToList();
            ViewBag.roles = db.RoleMasters.ToList();
            int BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            var data = db.tblDepots.Where(d => c.BranchID == BranchID).ToList();
            ViewBag.Depot = data;

            if (c == null)
            {
                return HttpNotFound();
            }
            else
            {

                UserRegistration u = new UserRegistration();
                if (c.UserID != null)
                {
                    //UserRegistration x = (from a in db.UserRegistrations where a.UserName == c.Email select a).FirstOrDefault();
                    UserRegistration x = (from a in db.UserRegistrations where a.UserID == c.UserID select a).FirstOrDefault();

                    if (x != null)
                    {
                        if (x.RoleID != null)
                            if (obj.RoleID == 0)
                                obj.RoleID = 13;
                            else
                                obj.RoleID = Convert.ToInt32(x.RoleID);
                    }
                }
                obj.RoleID = 13;
                obj.CustomerID = c.CustomerID;

                if (c.AcCompanyID!=null)
                    obj.AcCompanyID = c.AcCompanyID.Value;
                else
                    obj.AcCompanyID= Convert.ToInt32(Session["CurrentCompanyID"].ToString());

                obj.CustomerCode = c.CustomerCode;
                obj.CustomerName = c.CustomerName;
                obj.CustomerType = c.CustomerType;

                obj.ReferenceCode = c.ReferenceCode;
                obj.ContactPerson = c.ContactPerson;
                obj.Address1 = c.Address1;
                obj.Address2 = c.Address2;
                obj.Address3 = c.Address3;
                obj.Phone = c.Phone;
                obj.Mobile = c.Mobile;
                obj.Fax = c.Fax;
                obj.Email = c.Email;
                obj.Website = c.WebSite;

                if (c.CountryID!=null)
                obj.CountryID = c.CountryID.Value;
                if (c.CityID!=null)
                obj.CityID = c.CityID.Value;
                if(c.LocationID!=null)
                obj.LocationID = c.LocationID.Value; 
                obj.CountryName = c.CountryName;
                obj.LocationName = c.LocationName;
                obj.CityName = c.CityName;

                if (c.CurrencyID!=null)
                    obj.CurrenceyID = c.CurrencyID.Value;
                else
                    obj.CurrenceyID = Convert.ToInt32(Session["CurrencyId"].ToString());

                obj.StatusActive = c.StatusActive.Value;
                if (c.CreditLimit != null)
                { obj.CreditLimit = c.CreditLimit.Value; }
                else
                { obj.CreditLimit = 0; }

                obj.StatusTaxable = c.StatusTaxable.Value;

                if (c.EmployeeID!=null)
                    obj.EmployeeID = c.EmployeeID.Value;
                if (c.statusCommission != null)
                { obj.StatusCommission = c.statusCommission.Value; }
                if (c.BusinessTypeId != null)
                {
                    obj.BusinessTypeId = Convert.ToInt32(c.BusinessTypeId);
                }
                if (c.Referal != null)
                { obj.Referal = c.Referal; }

                obj.OfficeTimeFrom = c.OfficeOpenTime;
                obj.OfficeTimeTo = c.OfficeCloseTime;
                
                if (c.CourierServiceID !=null)
                    obj.CourierServiceID = c.CourierServiceID.Value;

                if (c.BranchID != null)
                {
                    obj.BranchID = c.BranchID.Value;
                }
                
                int DepotID = Convert.ToInt32(Session["CurrentDepotID"].ToString());
                obj.DepotID = DepotID;
                obj.CustomerUsername = c.CustomerUsername;
                obj.Password = c.Password;
                if (c.UserID != null)
                {
                    obj.UserID = c.UserID;
                }
                
            }

            return View(obj);
        }

        //
        // POST: /CustomerMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(CustmorVM c)
        {
            CustomerMaster obj = db.CustomerMasters.Find(c.CustomerID);
            
            //obj.CustomerID = c.CustomerID;
            obj.AcCompanyID = c.AcCompanyID;
            obj.CustomerCode = c.CustomerCode;
            obj.CustomerName = c.CustomerName;
            obj.CustomerType = c.CustomerType;

            obj.ReferenceCode = c.ReferenceCode;
            obj.ContactPerson = c.ContactPerson;
            obj.Address1 = c.Address1;
            obj.Address2 = c.Address2;
            obj.Address3 = c.Address3;
            obj.Phone = c.Phone;
            obj.Mobile = c.Mobile;
            obj.Fax = c.Fax;
            obj.Email = c.Email;
            obj.WebSite = c.Website;
            obj.CountryID =  c.CountryID;
            obj.CityID = c.CityID;
            obj.LocationID = c.LocationID;
            obj.CountryName = c.CountryName;
            obj.CityName = c.CityName;
            obj.LocationName = c.LocationName;
            if (c.CurrenceyID == 0)
            {                
                c.CurrenceyID = Convert.ToInt32(Session["CurrencyID"].ToString());
            }
            else
            {
                obj.CurrencyID = c.CurrenceyID;
            }
            obj.StatusActive = c.StatusActive;
            obj.CreditLimit = c.CreditLimit;
            obj.StatusTaxable = c.StatusTaxable;
            obj.EmployeeID = c.EmployeeID;
            obj.statusCommission = c.StatusCommission;
            
            obj.CourierServiceID = c.CourierServiceID;
            obj.BranchID = Convert.ToInt32(Session["CurrentBranchID"].ToString()); 
            obj.CustomerUsername = c.CustomerUsername;
            obj.Password = c.Password;

            obj.OfficeOpenTime = c.OfficeTimeFrom;
            obj.OfficeCloseTime = c.OfficeTimeTo;
            obj.Referal = c.Referal;
            obj.BusinessTypeId = c.BusinessTypeId;
            //obj.UserID = c.UserID;
            
            obj.DepotID = Convert.ToInt32(Session["CurrentDepotID"].ToString()); 


            db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMsg"] = "You have successfully Updated Customer.";
                return RedirectToAction("Index");
           

           
        }

        //
        // GET: /CustomerMaster/Delete/5



        public ActionResult DeleteConfirmed(int id)
        {
            CustomerEnquiry cenquery = db.CustomerEnquiries.Where(t => t.CustomerID == id).FirstOrDefault();
            if (cenquery == null)
            {
                CustomerMaster customermaster = db.CustomerMasters.Find(id);
                UserRegistration a = (from c in db.UserRegistrations where c.UserName == customermaster.Email select c).FirstOrDefault();

                if (customermaster == null)
                {
                    return HttpNotFound();

                }
                else
                {
                    db.CustomerMasters.Remove(customermaster);
                    db.SaveChanges();
                    if (a != null)
                    {
                        db.UserRegistrations.Remove(a);
                        db.SaveChanges();
                    }
                    TempData["SuccessMsg"] = "You have successfully Deleted Customer.";
                    return RedirectToAction("Index");

                }
            }
            else
            {
                TempData["SuccessMsg"] = "Customer Entry could not delete,because it has reference entries!";
                return RedirectToAction("Index");
            }
        }

        public JsonResult GetCity(int id)
        {
            List<CityM> objCity = new List<CityM>();
            var city = (from c in db.CityMasters where c.CountryID == id select c).ToList();

            foreach (var item in city)
            {
                objCity.Add(new CityM { City = item.City, CityID = item.CityID });

            }
            return Json(objCity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocation(int id)
        {
            List<LocationM> objLoc = new List<LocationM>();
            var loc = (from c in db.LocationMasters where c.CityID == id select c).ToList();

            foreach (var item in loc)
            {
                objLoc.Add(new LocationM { Location = item.Location, LocationID = item.LocationID });

            }
            return Json(objLoc, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerList()
        {
            List<CustmorVM> lst = new List<CustmorVM>();
            var data = db.CustomerMasters.Where(ite => (ite.StatusActive.HasValue ? ite.StatusActive == true : false) && ite.CustomerType != "CR").ToList();

            foreach (var item in data)
            {
                CustmorVM c = new CustmorVM();

                c.CustomerID = item.CustomerID;
                c.CustomerType = item.CustomerType;
                c.CustomerCode = item.CustomerCode;
                c.CustomerName = item.CustomerName;
                c.ContactPerson = item.ContactPerson;
                c.Mobile = item.Mobile;
                c.Phone = item.Phone;
                lst.Add(c);
            }

            return View(lst);
        }
        public ActionResult ApproveCustomer(int id)
        {
            var c = (from d in db.CustomerMasters where d.CustomerID == id select d).FirstOrDefault();
              CustmorVM obj = new CustmorVM();
           

            if (c == null)
            {
                return HttpNotFound();
            }
            else
            {

                UserRegistration u = new UserRegistration();
                if (c.UserID != null)
                {
                    //UserRegistration x = (from a in db.UserRegistrations where a.UserName == c.Email select a).FirstOrDefault();
                    UserRegistration x = (from a in db.UserRegistrations where a.UserID == c.UserID select a).FirstOrDefault();

                    if (x != null)
                    {
                        if (x.RoleID != null)
                            if (obj.RoleID == 0)
                                obj.RoleID = 13;
                            else
                                obj.RoleID = Convert.ToInt32(x.RoleID);
                    }
                }
                obj.RoleID = 13;
                obj.CustomerID = c.CustomerID;

                if (c.AcCompanyID != null)
                    obj.AcCompanyID = c.AcCompanyID.Value;
                else
                    obj.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());

                obj.CustomerCode = c.CustomerCode;
                obj.CustomerName = c.CustomerName;
                obj.CustomerType = c.CustomerType;

                obj.ReferenceCode = c.ReferenceCode;
                obj.ContactPerson = c.ContactPerson;
                obj.Address1 = c.Address1;
                obj.Address2 = c.Address2;
                obj.Address3 = c.Address3;
                obj.Phone = c.Phone;
                obj.Mobile = c.Mobile;
                obj.Fax = c.Fax;
                obj.Email = c.Email;
                obj.Website = c.WebSite;
                //obj.CountryID = c.CountryID; //.Value;
                //obj.CityID = c.CityID; //.Value;
                //obj.LocationID = c.LocationID; //.Value;
                obj.CountryName = c.CountryName;
                obj.LocationName = c.LocationName;
                obj.CityName = c.CityName;

                if (c.CurrencyID != null)
                    obj.CurrenceyID = c.CurrencyID.Value;
                else
                    obj.CurrenceyID = Convert.ToInt32(Session["CurrencyId"].ToString());

                obj.StatusActive = c.StatusActive.Value;
                if (c.CreditLimit != null)
                { obj.CreditLimit = c.CreditLimit.Value; }
                else
                { obj.CreditLimit = 0; }

                obj.StatusTaxable = c.StatusTaxable.Value;

                if (c.EmployeeID != null)
                    obj.EmployeeID = c.EmployeeID.Value;
                if (c.statusCommission != null)
                { obj.StatusCommission = c.statusCommission.Value; }
                if (c.BusinessTypeId != null)
                {
                    obj.BusinessTypeId = Convert.ToInt32(c.BusinessTypeId);
                }
                if (c.Referal != null)
                { obj.Referal = c.Referal; }

                obj.OfficeTimeFrom = c.OfficeOpenTime;
                obj.OfficeTimeTo = c.OfficeCloseTime;

                if (c.CourierServiceID != null)
                    obj.CourierServiceID = c.CourierServiceID.Value;

                if (c.BranchID != null)
                {
                    obj.BranchID = c.BranchID.Value;
                }

                int DepotID = Convert.ToInt32(Session["CurrentDepotID"].ToString());
                obj.DepotID = DepotID;
                obj.CustomerUsername = c.CustomerUsername;
                if (c.UserID != null)
                {
                    obj.UserID = c.UserID;
                }
                obj.ApprovedBy = Convert.ToInt32(Session["UserID"]);
                obj.ApprovedUserName = Convert.ToString(Session["UserName"]);
                obj.ApprovedOn = DateTime.Now;
            }

            return View(obj);
        }
        [HttpPost]
        public ActionResult ApproveCustomer(CustmorVM c)
        {
            CustomerMaster obj = db.CustomerMasters.Find(c.CustomerID);
            obj.ApprovedBy = Convert.ToInt32(Session["UserID"]);
            obj.ApprovedOn = DateTime.Now;
            obj.CustomerType = c.CustomerType;

            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMsg"] = "You have successfully Approved Customer.";
            return RedirectToAction("CustomerList");



        }
        public class CityM
        {
            public int CityID { get; set; }
            public String City { get; set; }
        }

        public class LocationM
        {
            public int LocationID { get; set; }
            public String Location { get; set; }
        }


    }
}