﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTMSV2.Models;
using System.Data;
using LTMSV2.DAL;
using System.Data.Entity;

namespace LTMSV2.Controllers
{
    [SessionExpire]
    public class OutScanController : Controller
    {
        //
        // GET: /OutScan/

        Entities1 db = new Entities1();


        public ActionResult Index()
        {
            int BranchId = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int CompanyId = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            List<DRSVM> lst = (from c in db.DRS join e in db.EmployeeMasters on c.DeliveredBy equals e.EmployeeID
                               join v in db.VehicleMasters on c.VehicleID equals v.VehicleID
                               where c.BranchID==BranchId && c.AcCompanyID == CompanyId
                               select new DRSVM {DRSID=c.DRSID,DRSNo=c.DRSNo,DRSDate=c.DRSDate,Deliver=e.EmployeeName,vehicle=v.RegistrationNo ,TotalAmountCollected=c.TotalAmountCollected,TotalMaterialCost=c.TotalMaterialCost }).ToList();

            return View(lst);
        }



        public ActionResult Details(int id)
        {
            return View();
        }



        public ActionResult Create(int id=0)
        {
            int BranchId = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int CompanyId = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            ViewBag.Deliverdby = db.EmployeeMasters.ToList();
            ViewBag.vehicle = db.VehicleMasters.ToList();
            ViewBag.Checkedby = db.EmployeeMasters.ToList();
            DRSVM v = new DRSVM();
            if (id>0)
            {
                DR d = db.DRS.Find(id);
                v.DRSID = d.DRSID;
                v.DRSNo = d.DRSNo;
                v.DRSDate = d.DRSDate;
                v.DeliveredBy = d.DeliveredBy;
                v.CheckedBy = d.CheckedBy;
                v.TotalAmountCollected = d.TotalAmountCollected;
                v.VehicleID = d.VehicleID;
                v.StatusDRS = d.StatusDRS;
                v.AcCompanyID = d.AcCompanyID;
                v.StatusInbound = d.StatusInbound;
                v.DrsType = d.DrsType;
                ViewBag.EditMode = "true";
                ViewBag.Title = "OutScan - Modify";
            }
            else
            {
                PickupRequestDAO _dao = new PickupRequestDAO();
                v.DRSID = 0;
                v.DRSNo = _dao.GetMaxDRSNo(CompanyId, BranchId);
                ViewBag.EditMode = "false";
                ViewBag.Title = "OutScan - Create";
            }
            return View(v);
        }


        public class DRSDet
        {
            public int InScanID { get; set; }
            public string AWB { get; set; }
            public string consignor { get; set; }
            public string consignee { get; set; }
            public string city { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public decimal COD { get; set; }
            public decimal MaterialCost { get; set; }
        }

        public JsonResult GetAWBData(string id)
        {
            //Received at Origin Facility
            //var l = (from c in db.InScans where c.InScanDate >= s  && c.InScanDate <= e select c).ToList();
            int courierstatusid = db.CourierStatus.Where(cc => cc.CourierStatus == "Received at Origin Facility").FirstOrDefault().CourierStatusID;
            int courierstatusid1 = db.CourierStatus.Where(cc => cc.CourierStatus == "Released").FirstOrDefault().CourierStatusID;

            var l = (from c in db.InScanMasters where c.ConsignmentNo == id && c.DRSID==null && (c.CourierStatusID== courierstatusid || c.CourierStatusID == courierstatusid1)  select c).FirstOrDefault();

            if (l != null)
            {
                DRSDet obj = new DRSDet();
                if (l != null)
                {

                    obj.AWB = l.ConsignmentNo;
                    obj.InScanID = l.InScanID;
                        obj.consignor = l.Consignor;
                    obj.consignee = l.Consignee;
                    obj.city = l.ConsigneeCityName.ToString();
                    obj.phone = l.ConsigneePhone;
                    obj.address = l.ConsigneeCountryName;
                    obj.COD = 100; // Convert.ToDecimal(l.);

                    if (l.MaterialCost != null)
                        obj.MaterialCost = Convert.ToDecimal(l.MaterialCost);
                    else
                        obj.MaterialCost = 0;

                }

                return Json(new { status = "ok", data = obj,  message = "Data Found" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "failed",data =l, message = "Data Not Found" }, JsonRequestBehavior.AllowGet);
            }
            //return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDRSDetails(int id)
        {
            List<DRSDet> list = new List<DRSDet>();

            //var data = (from c in db.DRSDetails where c.DRSID == id select c).ToList();
            var lstawb = (from c in db.InScanMasters where c.DRSID == id select c).ToList();
            //var lstawb = (from c in db.DRSDetails where c.DRSID == id select c).ToList();
            if (lstawb != null)
            {
                foreach (var item in lstawb)
                {
                    var l = (from c in db.InScanMasters where c.ConsignmentNo == item.ConsignmentNo select c).FirstOrDefault();
                    DRSDet obj = new DRSDet();
                    obj.AWB = l.ConsignmentNo;
                    obj.InScanID = l.InScanID;
                    obj.consignor = l.Consignor;
                    obj.consignee = l.Consignee;
                    obj.city = l.ConsigneeCityName.ToString();
                    obj.phone = l.ConsigneePhone;
                    obj.address = l.ConsigneeCountryName;
                    obj.COD = 100;// Convert.ToDecimal(l.CourierCharge);
                    if (l.MaterialCost != null)
                        obj.MaterialCost = Convert.ToDecimal(l.MaterialCost);
                    else
                        obj.MaterialCost = 0;
                    list.Add(obj);
                }
                
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(DRSVM v)
        {
            int UserId = Convert.ToInt32(Session["UserID"].ToString());
            int BranchId = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int CompanyId = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
            decimal couriercharge = 0;
            decimal totalmaterialcost = 0;
            foreach (var item in v.lst)
            {   
                couriercharge = couriercharge + Convert.ToDecimal(item.COD);
                totalmaterialcost = totalmaterialcost + Convert.ToDecimal(item.MaterialCost);
            }
                DR objdrs = new DR();
            if (v.DRSID == 0)
            {
                int max = (from c in db.DRS orderby c.DRSID descending select c.DRSID).FirstOrDefault();
                if (max == null)
                    max = 1;
                else
                    max = max + 1;

                objdrs.DRSID = max;
                objdrs.DRSNo = v.DRSNo;
                objdrs.BranchID = BranchId;
                objdrs.AcCompanyID = CompanyId;
                objdrs.StatusDRS = "0";
                objdrs.StatusInbound = false;                
                objdrs.DrsType = "Courier";
            }
            else
            {
                objdrs = db.DRS.Find(v.DRSID);
                
            }
            objdrs.TotalAmountCollected = couriercharge;
            objdrs.TotalMaterialCost = totalmaterialcost;
            objdrs.DRSDate = v.DRSDate;
            objdrs.DeliveredBy = v.DeliveredBy;
            objdrs.CheckedBy = v.CheckedBy;
            
            objdrs.VehicleID = v.VehicleID;                        
            
            

            if (v.DRSID==0)
            {
                db.DRS.Add(objdrs);
                db.SaveChanges();
            }
            else
            {
                db.Entry(objdrs).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            if (v.DRSID>0)
            {
                var data = (from c in db.InScanMasters where c.DRSID == v.DRSID select c).ToList();
                foreach (var item in data)
                {

                    var awbtrack = db.AWBTrackStatus.Where(cc => cc.InScanId == item.InScanID && cc.ShipmentStatus == "OUTSCAN" && cc.CourierStatus == "Out for Delivery at Origin").First();
                    db.AWBTrackStatus.Remove(awbtrack);
                    db.SaveChanges();

                    var awbtrackLAST = db.AWBTrackStatus.Where(cc => cc.InScanId == item.InScanID).OrderByDescending(CC => CC.EntryDate).FirstOrDefault();

                    var _inscan = db.InScanMasters.Find(item.InScanID);
                    _inscan.DRSID = null;
                    if (awbtrackLAST != null)
                    {
                        _inscan.CourierStatusID = awbtrackLAST.CourierStatusId; // db.CourierStatus.Where(cc => cc.StatusTypeID == _inscan.StatusTypeId && cc.CourierStatus == "Out for Delivery at Origin").FirstOrDefault().CourierStatusID;;
                        _inscan.StatusTypeId = awbtrack.StatusTypeId;
                    }
                    else
                    {
                        _inscan.CourierStatusID =db.CourierStatus.Where(cc => cc.StatusTypeID == _inscan.StatusTypeId && cc.CourierStatus == "Out for Delivery at Origin").FirstOrDefault().CourierStatusID;;
                        _inscan.StatusTypeId = 2;
                    }
                    db.Entry(_inscan).State = EntityState.Modified;
                    db.SaveChanges();


                }

            }
           

            foreach (var item in v.lst)
            {

                var _inscan = db.InScanMasters.Find(item.InScanID);
                _inscan.DRSID = objdrs.DRSID;                                
                _inscan.StatusTypeId = db.tblStatusTypes.Where(cc => cc.Name == "OUTSCAN").First().ID;
                _inscan.CourierStatusID = db.CourierStatus.Where(cc => cc.StatusTypeID == _inscan.StatusTypeId && cc.CourierStatus == "Out for Delivery at Origin").FirstOrDefault().CourierStatusID;             
                db.Entry(_inscan).State = EntityState.Modified;
                db.SaveChanges();

                //updateing awbstaus table for tracking
                AWBTrackStatu _awbstatus = new AWBTrackStatu();
                int? id = (from c in db.AWBTrackStatus orderby c.AWBTrackStatusId descending select c.AWBTrackStatusId).FirstOrDefault();

                if (id == null)
                    id = 1;
                else
                    id = id + 1;

                _awbstatus.AWBTrackStatusId = Convert.ToInt32(id);
                _awbstatus.AWBNo = _inscan.ConsignmentNo;
                _awbstatus.EntryDate = DateTime.Now;
                _awbstatus.InScanId = _inscan.InScanID;
                _awbstatus.StatusTypeId = Convert.ToInt32(_inscan.StatusTypeId);
                _awbstatus.CourierStatusId = Convert.ToInt32(_inscan.CourierStatusID);
                _awbstatus.ShipmentStatus = db.tblStatusTypes.Find(_inscan.StatusTypeId).Name;
                _awbstatus.CourierStatus = db.CourierStatus.Find(_inscan.CourierStatusID).CourierStatus;
                _awbstatus.UserId = UserId;

                db.AWBTrackStatus.Add(_awbstatus);
                db.SaveChanges();
            }


            //foreach (var item in v.lst)
            //{
            //    DRSDetail d = new DRSDetail();
            //    d.DRSID = objdrs.DRSID;
            //    d.AWBNO = item.AWB;
            //    d.InScanID = item.InScanID;
            //    d.CourierCharge = item.COD;
            //    d.MaterialCost = 0;
            //    d.StatusPaymentMode = "PKP";
            //    d.CCReceived = 0;
            //    d.CCStatuspaymentType = "CS";
            //    d.MCReceived = 0;
            //    d.MCStatuspaymentType = "CS";
            //    d.Remarks = "";
            //    d.ReceiverName = item.Consignee;
            //    d.CourierStatusID = 9;
            //    d.StatusAWB = "DD";
            //    d.EmployeeID = Convert.ToInt32(Session["UserID"].ToString());
            //    d.ReturnTime = DateTime.Now;

            //    db.DRSDetails.Add(d);
            //    db.SaveChanges();

            //}
            TempData["success"] = "DRS Saved Successfully.";
            return RedirectToAction("Index");
         

        }

        public ActionResult Edit(int id)
        {
            ViewBag.Deliverdby = db.EmployeeMasters.ToList();
            ViewBag.vehicle = db.VehicleMasters.ToList();
            ViewBag.CheckedBy = db.EmployeeMasters.ToList();

            DR d = db.DRS.Find(id);
            DRSVM v = new DRSVM();
            if (d == null)
            {
                return HttpNotFound();

            }
            else
            {

                v.DRSID = d.DRSID;
                v.DRSNo = d.DRSNo;
                v.DRSDate = d.DRSDate;
                v.DeliveredBy = d.DeliveredBy;
                v.CheckedBy = d.CheckedBy;
                v.TotalAmountCollected = d.TotalAmountCollected;
                v.VehicleID = d.VehicleID;
                v.StatusDRS = d.StatusDRS;
                v.AcCompanyID = d.AcCompanyID;
                v.StatusInbound = d.StatusInbound;
                v.DrsType = d.DrsType;

            }
            return View(v);
        }

        //
        // POST: /InScan/Edit/5

        [HttpPost]
        public ActionResult Edit(DRSVM v)
        {
            int UserId = Convert.ToInt32(Session["UserID"].ToString());
            int BranchId = Convert.ToInt32(Session["CurrentBranchID"].ToString());
            int CompanyId = Convert.ToInt32(Session["CurrentCompanyID"].ToString());

            try
            {
                //var data = (from c in db.DRSDetails where c.DRSID == v.DRSID select c).ToList();
                //foreach (var item in data)
                //{
                //    db.DRSDetails.Remove(item);
                //    db.SaveChanges();
                //}

                var data = (from c in db.InScanMasters where c.DRSID == v.DRSID select c).ToList();
                foreach (var item in data)
                {
                    var _inscan = db.InScanMasters.Find(item.InScanID);
                    _inscan.DRSID = null;
                    db.Entry(_inscan).State = EntityState.Modified;
                    db.SaveChanges();

                    var awbtrack = db.AWBTrackStatus.Where(cc => cc.InScanId == item.InScanID && cc.ShipmentStatus == "OUTSCAN" && cc.CourierStatus == "Out for Delivery at Origin").First();
                    db.AWBTrackStatus.Remove(awbtrack);
                    db.SaveChanges();
                }


                DR objdrs = db.DRS.Find(v.DRSID);
                //objdrs.DRSNo = objdrs.DRSID.ToString();
                objdrs.DRSDate = v.DRSDate;
                objdrs.DeliveredBy = v.DeliveredBy;
                objdrs.CheckedBy = v.CheckedBy;
                objdrs.TotalAmountCollected = 0;
                objdrs.VehicleID = v.VehicleID;
                objdrs.StatusDRS = "0";
                objdrs.AcCompanyID = Convert.ToInt32(Session["CurrentCompanyID"].ToString());
                objdrs.StatusInbound = false;
                objdrs.DrsType = "Courier";

                db.Entry(objdrs).State = EntityState.Modified;
                db.SaveChanges();

                foreach (var item in v.lst)
                {

                    var _inscan = db.InScanMasters.Find(item.InScanID);
                    _inscan.DRSID = objdrs.DRSID;
                    _inscan.StatusTypeId = db.tblStatusTypes.Where(cc => cc.Name == "OUTSCAN").First().ID;
                    _inscan.CourierStatusID = db.CourierStatus.Where(cc => cc.StatusTypeID == _inscan.StatusTypeId && cc.CourierStatus == "Out for Delivery at Origin").FirstOrDefault().CourierStatusID;
                    db.Entry(_inscan).State = EntityState.Modified;
                    db.SaveChanges();

                    //updateing awbstaus table for tracking
                    AWBTrackStatu _awbstatus = new AWBTrackStatu();
                    int? id = (from c in db.AWBTrackStatus orderby c.AWBTrackStatusId descending select c.AWBTrackStatusId).FirstOrDefault();

                    if (id == null)
                        id = 1;
                    else
                        id = id + 1;

                    _awbstatus.AWBTrackStatusId = Convert.ToInt32(id);
                    _awbstatus.AWBNo = _inscan.ConsignmentNo;
                    _awbstatus.EntryDate = DateTime.Now;
                    _awbstatus.InScanId = _inscan.InScanID;
                    _awbstatus.StatusTypeId = Convert.ToInt32(_inscan.StatusTypeId);
                    _awbstatus.CourierStatusId = Convert.ToInt32(_inscan.CourierStatusID);
                    _awbstatus.ShipmentStatus = db.tblStatusTypes.Find(_inscan.StatusTypeId).Name;
                    _awbstatus.CourierStatus = db.CourierStatus.Find(_inscan.CourierStatusID).CourierStatus;
                    _awbstatus.UserId = UserId;

                    db.AWBTrackStatus.Add(_awbstatus);
                    db.SaveChanges();
                }


                //foreach (var item in v.lst)
                //{
                //    DRSDetail d = new DRSDetail();
                //    d.DRSID = objdrs.DRSID;
                //    d.AWBNO = item.AWB;
                //    d.InScanID = item.InScanID;
                //    d.CourierCharge = item.COD;
                //    d.MaterialCost = 0;
                //    d.StatusPaymentMode = "PKP";
                //    d.CCReceived = 0;
                //    d.CCStatuspaymentType = "CS";
                //    d.MCReceived = 0;
                //    d.MCStatuspaymentType = "CS";
                //    d.Remarks = "";
                //    d.ReceiverName = item.Consignee;
                //    d.CourierStatusID = 9;
                //    d.StatusAWB = "DD";
                //    d.EmployeeID = Convert.ToInt32(Session["UserID"].ToString());
                //    d.ReturnTime = DateTime.Now;

                //    db.DRSDetails.Add(d);
                //    db.SaveChanges();

                //}
                TempData["success"] = "DRS Updated Successfully.";
                return RedirectToAction("Index");

              
             
            }
            catch (Exception c)
            {

            }

            return View();
        }

        //
        // GET: /InScan/Delete/5

      
        public ActionResult DeleteConfirmed(int id)
        {
            DR d = db.DRS.Find(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    var data = (from c in db.DRSDetails where c.DRSID == id select c).ToList();
                    foreach (var item in data)
                    {
                        db.DRSDetails.Remove(item);
                        db.SaveChanges();
                    }

                    db.DRS.Remove(d);
                    db.SaveChanges();
                    TempData["success"] = "DRS Deleted Successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception c)
                {

                }

            }

            return View();
           
        }
    }


}


