using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LTMSV2.Models
{
    public class AcTypeModel
    {
        public int Id { get; set; }
        public string AcType { get; set; }
        public int? AcCategoryID { get; set; }
        public string AcCategory { get; set; }
       
    }
}