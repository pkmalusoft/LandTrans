using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class AcJournalConsignmentVM :AcJournalConsignment
    {
        public int TruckDetailID { get; set; }
        public string ConsignmentNo { get; set; }
        public DateTime ConsignmentDate { get; set; }
    }
}