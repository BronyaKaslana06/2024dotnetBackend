using EntityFramework.Models;
using Newtonsoft.Json;
using CalculatorWrapper;
using System.Collections.Generic;
using System.Linq;
using DAL.Admins;
using DAL.Admins;

namespace Service.Admins
{
    public class BatteryService
    {
        private readonly BatteryRepository _batteryRepository;

        public BatteryService(BatteryRepository batteryRepository)
        {
            _batteryRepository = batteryRepository;
        }

        public object QueryBatteries(int pageIndex, int pageSize, string availableStatus, string batteryTypeId, int batteryStatus, string keyword)
        {
            int offset = (pageIndex - 1) * pageSize;
            int limit = pageSize;

            if (offset < 0 || limit <= 0)
            {
                return new
                {
                    code = 1,
                    msg = "页码或页大小非正",
                    totalData = 0,
                    data = "",
                };
            }

            int availableStatusValue = 0;
            if (!string.IsNullOrEmpty(availableStatus))
            {
                availableStatusValue = (int)Enum.Parse(typeof(AvailableStatusEnum), availableStatus, ignoreCase: true);
            }

            var query = _batteryRepository.GetBatteries(batteryTypeId, availableStatusValue)
                .Select(b => new
                {
                    battery_id = b.BatteryId,
                    available_status = ((AvailableStatusEnum)b.AvailableStatus).ToString(),
                    current_capacity = b.CurrentCapacity,
                    curr_charge_times = b.CurrChargeTimes,
                    manufacturing_date = b.ManufacturingDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    battery_type_id = b.batteryType.Name,
                    name = batteryStatus == 0 ? b.switchStation.StationName : b.vehicle.PlateNumber,
                    Similarity = batteryStatus == 0 ? Calculator.ComputeSimilarityScore(b.switchStation.StationName, keyword) : Calculator.ComputeSimilarityScore(b.vehicle.PlateNumber == null ? "" : b.vehicle.PlateNumber, keyword),
                    isEditing = false
                }).ToList();

            var filteredItems = query
                .Where(item => item.Similarity >= (double)0)
                .OrderByDescending(item => item.Similarity)
                .ThenBy(item => item.battery_id);

            var totalNum = filteredItems.Count();
            var temp = filteredItems.Skip(offset).Take(limit);

            return new
            {
                code = 0,
                msg = "success",
                totaldata = totalNum,
                data = temp,
            };
        }
    }
}
