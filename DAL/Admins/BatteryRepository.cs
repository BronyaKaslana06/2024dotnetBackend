using EntityFramework.Context;
using EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Admins
{
    public class BatteryRepository
    {
        private readonly ModelContext _context;

        public BatteryRepository(ModelContext context)
        {
            _context = context;
        }

        public List<Battery> GetBatteries(string batteryTypeId, int availableStatusValue)
        {
            return _context.Batteries
                .Where(b => (string.IsNullOrEmpty(batteryTypeId) || b.batteryType.Name == batteryTypeId) &&
                            (availableStatusValue == 0 || b.AvailableStatus == availableStatusValue))
                .ToList();
        }
    }
}
