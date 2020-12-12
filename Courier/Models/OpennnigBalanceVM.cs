using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LTMSV2.Models;
using LTMSV2.DAL;
namespace LTMSV2.Models
{
    public class OpennnigBalanceVM
    {
      
        public int AcHeadID { get; set; }
        public int  AcFinancialYearID { get; set; }
        public int  CrDr { get; set; }
        public decimal Amount { get; set; }
        public string AcHead { get; set; }
      

    }
}