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
    public class VehicleController : Controller
    {
        // GET: Routes
        public ActionResult Index()
        {
            return View();
        }
        readonly A ob = new A();
        public List<A> SingleList()
        {
            List<A> list = new List<A>();
            A obj = new A();
            list.Add(obj);
            return list;
        }

        [HttpPost]
        public string GetVehicleMaster(VehicleMasterVM o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                List<VehicleMasterVM> cl = VehicleMasterVM.GetVehicleMaster(o);
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
        public string ManageVehicleMaster(VehicleMasterVM o, string Who)
        {
            List<A> list = SingleList();
            object res;
            try
            {
                string nres = VehicleMasterVM.ManageVehicleMaster(o);
                int nid;
                if (int.TryParse(nres, out nid))
                {
                    res = list.Select(a => new { success = true, status = 200, status_message = "success", id = nres, data = ob }).FirstOrDefault();
                }
                else
                    res = list.Select(a => new { success = true, status = 203, status_message = nres, data = ob }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                res = list.Select(a => new { success = false, status = 300, status_message = ex.Message.ToString(), data = ob }).FirstOrDefault();
            }
            return JsonConvert.SerializeObject(res);
        }



    }
}