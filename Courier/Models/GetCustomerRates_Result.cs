//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LTMSV2.Models
{
    using System;
    
    public partial class GetCustomerRates_Result
    {
        public int CustomerRateID { get; set; }
        public int CustomerRateTypeID { get; set; }
        public int CourierServiceID { get; set; }
        public int ZoneChartID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> FAgentID { get; set; }
        public decimal BaseWeight { get; set; }
        public decimal BaseRate { get; set; }
        public Nullable<bool> WithTax { get; set; }
        public Nullable<bool> WithoutTax { get; set; }
        public Nullable<bool> AdditionalCharges { get; set; }
        public string CustomerRateType { get; set; }
        public string CourierService { get; set; }
        public string ZoneName { get; set; }
        public string FAgentName { get; set; }
    }
}
