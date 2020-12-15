using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class RevenueCostMasterVM :RevenueCostMaster
    {
        public string RevenueHeadName { get; set; }
        public string CostHeadName { get; set; }
    }
}