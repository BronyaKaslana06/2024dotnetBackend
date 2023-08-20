﻿using System;
using System.Collections.Generic;

namespace EntityFramework.Models;

public partial class BatteryType
{
    public long BatteryTypeId { get; set; } 

    public int MaxChargeTimes { get; set; }

    public string? TotalCapacity { get; set; }

    public List<Battery> batteries { get; set; } = new List<Battery>();   //至少为1
}
