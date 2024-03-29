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
    using System.Collections.Generic;
    
    public partial class CustomerRate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerRate()
        {
            this.CustomerRateDets = new HashSet<CustomerRateDet>();
        }
    
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
    
        public virtual CourierService CourierService { get; set; }
        public virtual CourierService CourierService1 { get; set; }
        public virtual CustomerRateType CustomerRateType { get; set; }
        public virtual CustomerRateType CustomerRateType1 { get; set; }
        public virtual ZoneChart ZoneChart { get; set; }
        public virtual ZoneChart ZoneChart1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerRateDet> CustomerRateDets { get; set; }
    }
}
