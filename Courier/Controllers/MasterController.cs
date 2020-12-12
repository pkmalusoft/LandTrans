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
    public class MasterController : Controller
    {
        // GET: Master
        //public ActionResult Index()
        //{
        //    return View();
        //}
        readonly A ob = new A();
        public List<A> SingleList()
        {
            List<A> list = new List<A>();
            A obj = new A();
            list.Add(obj);
            return list;
        }
        [HttpPost]
        public string GetDDLBranchMaster()
        {
            return JsonConvert.SerializeObject(ngModel.GetDDLBranchMaster());
        }
        [HttpPost]
        public string GetManufacturerForVehicle(ManufacturerForVehicle o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                List<ManufacturerForVehicle> cl = ManufacturerForVehicle.GetManufacturerForVehicle(o);
                if (cl.Count > 0)
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", data = cl }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = "No data found", data = cl }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }


        [HttpPost]
        public string GetRegisteredUnderForVehicle(RegisteredUnderForVehicle o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                List<RegisteredUnderForVehicle> cl = RegisteredUnderForVehicle.GetRegisteredUnderForVehicle(o);
                if (cl.Count > 0)
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", data = cl }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = "No data found", data = cl }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public string GetVehicleTypes(VehicleTypes o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                List<VehicleTypes> cl = VehicleTypes.GetVehicleTypes(o);
                if (cl.Count > 0)
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", data = cl }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = "No data found", data = cl }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }
        [HttpPost]
        public string GetModelForVehicle(ModelForVehicle o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                List<ModelForVehicle> cl = ModelForVehicle.GetModelForVehicle(o);
                if (cl.Count > 0)
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", data = cl }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = "No data found", data = cl }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public string GetInsCompanyForVehicle(InsCompanyForVehicle o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                List<InsCompanyForVehicle> cl = InsCompanyForVehicle.GetInsCompanyForVehicle(o);
                if (cl.Count > 0)
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", data = cl }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = "No data found", data = cl }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }
    }
}