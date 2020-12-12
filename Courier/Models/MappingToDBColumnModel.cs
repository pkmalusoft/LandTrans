// Decompiled with JetBrains decompiler
// Type: LTMSV2.Models.MappingToDBColumnModel
// Assembly: Courier_27_09_16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B3B4E05-393A-455A-A5DE-86374CE9B081
// Assembly location: D:\Courier09022018\Decompiled\obj\Release\Package\PackageTmp\bin\LTMSV2dll

using System.Collections.Generic;

namespace LTMSV2.Models
{
  public class MappingToDBColumnModel
  {
    public string TableName { get; set; }

    public List<DBColumn> DBColumns { get; set; }

    public List<ExcelColumn> ExcelColumns { get; set; }

    public List<TName> TableList { get; set; }
  }
}
