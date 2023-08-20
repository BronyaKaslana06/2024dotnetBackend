﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Models;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public string Email { get; set; }

    public string? UserName { get; set; }

    public string Password { get; set; } 

    public byte[]? ProfilePhoto { get; set; }

    public DateTime CreateTime { get; set; }

    public string? PhoneNumber { get; set; }

    public string? IdentityNumber { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public int Position { get; set; }

    [NotMapped]
    public PositionEnum PositionEnum
    {
        get
        {
            if (Position <= 0 || Position > 3)
                Position = 3;
            return (PositionEnum)Position;
        }
        set
        {
            Position = (int)value;
        }
    }

    public int Salary { get; set; }


    public List<Kpi> kpi { get; set; } = new List<Kpi>();

    public List<MaintenanceItem> maintenanceItems { get; set; } = new List<MaintenanceItem>();

    public List<SwitchLog> switchLogs { get; set; } = new List<SwitchLog>();  //可以为0

    public List<SwitchRequest> switchRequests { get; set; } = new List<SwitchRequest>(); //可以为0

    public SwitchStation? switchStation { get; set; }  //nullable

}

public enum PositionEnum
{
   换电站管理员 = 1,
   维修工 = 2,
   其他 =3
}
// 0 预留
