﻿// Decompiled with JetBrains decompiler
// Type: LTMSV2.Models.ConsignmentEntryDomVM
// Assembly: Courier_27_09_16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B3B4E05-393A-455A-A5DE-86374CE9B081
// Assembly location: D:\Courier09022018\Decompiled\obj\Release\Package\PackageTmp\bin\LTMSV2dll

using System;

namespace LTMSV2.Models
{
  public class ConsignmentEntryDomVM
  {
    public string AWBNO { get; set; }

    public string CustomerID { get; set; }

    public string Shipper { get; set; }

    public string Address { get; set; }

    public int ShipperPhone { get; set; }

    public string City { get; set; }

    public string CityID { get; set; }

    public string Location { get; set; }

    public Decimal Weight { get; set; }

    public int pieces { get; set; }

    public int movementID { get; set; }

    public string PaymentMode { get; set; }

    public Decimal CourierCharge { get; set; }

    public Decimal OtherCharge { get; set; }

    public Decimal Total { get; set; }

    public string Consignee { get; set; }

    public string ConsigneeAddress { get; set; }

    public int ConsigneePhone { get; set; }

    public string ConsigneeCity { get; set; }

    public string ConsigneeDestination { get; set; }

    public string ConsigneeDepartment { get; set; }

    public string ProductType { get; set; }

    public int CollectedByID { get; set; }

    public int ReceivedByID { get; set; }

    public string CollectedBy { get; set; }

    public string ReceivedBy { get; set; }

    public string Remark { get; set; }

    public DateTime InscanTime { get; set; }

    public string FAgent { get; set; }

    public string FAWBNo { get; set; }

    public DateTime FDate { get; set; }

    public Decimal WeightVerified { get; set; }

    public Decimal ForwardingCharge { get; set; }

    public Decimal RevisedCharge { get; set; }

    public Decimal MaterialCost { get; set; }

    public string Reference { get; set; }

    public string Remarks { get; set; }

    public bool NCND { get; set; }

    public bool IsCheque { get; set; }

    public bool DOCopyBack { get; set; }

    public bool CashOnly { get; set; }

    public bool CollectMaterial { get; set; }
  }
}
