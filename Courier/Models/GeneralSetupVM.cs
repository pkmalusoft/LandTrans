using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LTMSV2.Models
{
    public class GeneralSetupVM :GeneralSetup
    {
        [AllowHtml]
        [Display(Name = "TextDoc")]
        public string TextDoc
        {
            get;
            set;
        }
    }
}