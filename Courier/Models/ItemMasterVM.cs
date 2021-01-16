using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class ItemMasterVM :ItemMaster
    {
        
    }

    public class RouteMasterVM : RouteMaster
    {
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public IEnumerable<RouteOrder> RouteOrders { get; set; }

    }
    public class PackageVM 
    {
        public int PackageID { get; set; }
        public string PackageType { get; set; }
        public string PackageName { get; set; }
    }

}