using System;
using System.Collections.Generic;
using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EntityFramework.Context;

public class ModelContext : DbContext
{
    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Battery> Batteries { get; set; }


    public virtual DbSet<BatteryType> BatteryTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }


    public virtual DbSet<Kpi> Kpis { get; set; }

    public virtual DbSet<MaintenanceItem> MaintenanceItems { get; set; }


    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<SwitchLog> SwitchLogs { get; set; }

    public virtual DbSet<SwitchRequest> SwitchRequests { get; set; }


    public virtual DbSet<SwitchStation> SwitchStations { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleOwner> VehicleOwners { get; set; }

    public virtual DbSet<OwnerPos> OwnerPos { get; set; }

    public virtual DbSet<VehicleParam> VehicleParams { get; set; }

    public ModelContext() : base() { }
    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=xxxxxxx;Port=3306;Database=dotnet;Uid=dotnet;Pwd=xxx;", ServerVersion.AutoDetect("Server=xxxxxxxxx;Port=3306;Database=dotnet;Uid=dotnet;Pwd=xxxx;"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder
            .HasDefaultSchema("C##CAR")
            .UseCollation("USING_NLS_COMP");*/

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

}
