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
    
    public partial class ImportShipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImportShipment()
        {
            this.ImportShipmentDetails = new HashSet<ImportShipmentDetail>();
        }
    
        public int ID { get; set; }
        public string ManifestNumber { get; set; }
        public string ConsignorName { get; set; }
        public string ConsignorAddress1_Building { get; set; }
        public string ConsignorAddress2_Street { get; set; }
        public string ConsignorAddress3_PinCode { get; set; }
        public string ConsignorCityName { get; set; }
        public string ConsignorCountryName { get; set; }
        public string ConsignorLocationName { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress1_Building { get; set; }
        public string ConsigneeAddress2_Street { get; set; }
        public string ConsigneeAddress3_PinCode { get; set; }
        public string ConsigneeCityName { get; set; }
        public string ConsigneeCountryName { get; set; }
        public string ConsigneeLocationName { get; set; }
        public string OriginAirportCity { get; set; }
        public string DestinationAirportCity { get; set; }
        public Nullable<System.DateTime> FlightDate { get; set; }
        public string FlightNo { get; set; }
        public string MAWB { get; set; }
        public string CD { get; set; }
        public Nullable<int> Bags { get; set; }
        public string RunNo { get; set; }
        public string Type { get; set; }
        public int TotalAWB { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int AgentID { get; set; }
        public int AgentLoginID { get; set; }
        public int LastEditedByLoginID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ShipmentTypeId { get; set; }
        public Nullable<int> AcFinancialYearID { get; set; }
    
        public virtual UserRegistration UserRegistration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportShipmentDetail> ImportShipmentDetails { get; set; }
    }
}
