﻿// Decompiled with JetBrains decompiler
// Type: LTMSV2.Models.CustomerInvoiceDetailsVM
// Assembly: Courier_27_09_16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B3B4E05-393A-455A-A5DE-86374CE9B081
// Assembly location: D:\Courier09022018\Decompiled\obj\Release\Package\PackageTmp\bin\LTMSV2dll

using System;

namespace LTMSV2.Models
{
  public class CustomerInvoiceDetailsVM
  {
    public string AWBNO { get; set; }

    public DateTime AWBDate { get; set; }

    public double AWBCharge { get; set; }

    public double OtherCharges { get; set; }

    public double TotalCharges { get; set; }
  }
}
