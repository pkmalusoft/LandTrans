﻿// Decompiled with JetBrains decompiler
// Type: LTMSV2.Models.ZoneChartVM
// Assembly: Courier_27_09_16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B3B4E05-393A-455A-A5DE-86374CE9B081
// Assembly location: D:\Courier09022018\Decompiled\obj\Release\Package\PackageTmp\bin\LTMSV2dll

using System.Collections.Generic;

namespace LTMSV2.Models
{
  public class ZoneChartVM
  {
    public int ZoneChartID { get; set; }

    public int ZoneCategoryID { get; set; }

    public int ZoneID { get; set; }

    public string ZoneCategory { get; set; }

    public string ZoneName { get; set; }

    public int depotcountry { get; set; }

    public List<int> country { get; set; }

    public List<int> city { get; set; }

    public string StatusZone { get; set; }

    public string countrylist { get; set; }

    public string citylist { get; set; }
  }
}
