using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class RevenueUpdateMasterVM:RevenueUpdateMaster
    {
        public string ConsignmentNo { get; set; }
        public string RevenueCost { get; set; }
    }
}