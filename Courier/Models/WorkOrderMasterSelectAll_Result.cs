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
    
    public partial class WorkOrderMasterSelectAll_Result
    {
        public int WOID { get; set; }
        public string WONo { get; set; }
        public Nullable<System.DateTime> WODate { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string WorkCategoryIDs { get; set; }
        public Nullable<System.DateTime> FollowUpDate { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
    }
}
