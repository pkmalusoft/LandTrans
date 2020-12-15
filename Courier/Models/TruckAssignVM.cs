using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class TruckAssignVM 
    {
        public int TruckDetailId { get; set; }
        public DateTime TDDate { get; set; }
        public int RouteID { get; set; }
        public string ReceiptNo { get; set; }
        public String RouteName { get; set; }
        public int VehicleID { get; set; }
        public string VechicleName { get; set; }
        public string VechileRegistrationNo { get; set; }

        public int InScanId { get; set; }
        public DateTime InScanDate { get; set; }
        public string ConsignmentNo { get; set; }
        public string Pieces { get; set; }
        public string Consignor { get; set; }
        public string Consignee { get; set; }
        public string ConsignorCountryName { get; set; }
        public string ConsigneeCountry { get; set; }
                
    }
}