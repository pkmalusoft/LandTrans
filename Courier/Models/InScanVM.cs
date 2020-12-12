﻿// Decompiled with JetBrains decompiler
// Type: LTMSV2.Models.InScanVM
// Assembly: Courier_27_09_16, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B3B4E05-393A-455A-A5DE-86374CE9B081
// Assembly location: D:\Courier09022018\Decompiled\obj\Release\Package\PackageTmp\bin\LTMSV2dll

using System;
using System.Collections.Generic;

namespace LTMSV2.Models
{
  public class InScanVM
  {
        public InScanVM()
        {
            lst = new List<AWBDetailsVM>();
        }
        public int QuickInscanID { get; set; }
        public int DepotID { get; set; }
        public string DepotName { get; set; }
        public string InScanSheetNo{ get; set; }
        public int VehicleId { get; set; }
        public string DriverName { get; set; }        
        public int BranchId { get; set; }        
        public int UserId { get; set; }
        public int CollectedByID { get; set; }
        public int AgentID { get; set; }
        public int ReceivedByID { get; set; }        
        public DateTime QuickInscanDateTime { get; set; }
        public string SelectedInScanId { get; set; }
        public string RemovedInScanId { get; set; }

        public string CollectedBy { get; set; }
        public string AgentName { get; set; }
        public string ReceivedBy { get; set; }
        public List<AWBDetailsVM> lst { get; set; }

        public string Source { get; set; }
  }
}
