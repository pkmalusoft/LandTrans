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
    
    public partial class PurchaseOrderSelectByID_Result
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<bool> StatusDiscountAmt { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> FYearID { get; set; }
    }
}
