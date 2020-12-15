// Decompiled with JetBrains decompiler
// Type: LTMSV2.Models.VehiclesVM
// Assembly: Courier_27_09_16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B3B4E05-393A-455A-A5DE-86374CE9B081
// Assembly location: D:\Courier09022018\Decompiled\obj\Release\Package\PackageTmp\bin\LTMSV2dll

using System;

namespace LTMSV2.Models
{
  public class VehiclesVM
  {
    public int VehicleID { get; set; }

    public string VehicleDescription { get; set; }

    public string RegistrationNo { get; set; }

    public string Model { get; set; }

    public string Category { get; set; }
        public string VehicleTypeName { get; set; }
        public int VehicleTypeID { get; set; }
        public string RegisteredUnder { get; set; }
        public string MakeYear{ get; set; }
        public Decimal PurchaseValue { get; set; }

    
    public DateTime? PurchaseDate { get; set; }

    public DateTime RegExpirydate { get; set; }
        public string InsuranceCompName { get; set; }
        public string PolicyNo { get; set; }
        public Decimal InsuredValue { get; set; }
        public DateTime? InsuranceExpDate { get; set; }

        public DateTime? DepreciationDate { get; set; }

        public Decimal  ScrapValue { get; set; }
        public int AcCompanyID { get; set; }
        public string Mode { get; set; }

        public int BranchID { get; set; }

        public string FinanceCompany { get; set; }

    }
}
