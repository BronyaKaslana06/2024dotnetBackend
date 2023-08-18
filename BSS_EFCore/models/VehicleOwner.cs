﻿using System;
using System.Collections.Generic;

namespace EntityFramework.Models;

public partial class VehicleOwner
{
    public long OwnerId { get; set; }

    public string? Nickname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; } = null!;

    public byte[]? ProfilePhoto { get; set; }

    public DateTime CreateTime { get; set; }

    public string PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Address { get; set; }


    public List<SwitchRequest> switchRequests { get; set; } = new List<SwitchRequest>();  //可以为0
    public List<MaintenanceItem> maintenanceItems { get; set; } = new List<MaintenanceItem>();  //可以为0
    public List<Vehicle> vehicles { get; set; } = new List<Vehicle>();   //可以为0


}
