using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using NuGet.Protocol;
using System.Data;
using System.Xml.Linq;
using EntityFramework.Context;
using EntityFramework.Models;
using System.Transactions;
using System.Collections.Generic;

namespace webapi.Controllers.Staff
{
    [Route("staff/switchstation")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly ModelContext _context;

        public StationController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet("info")]
        public ActionResult<IEnumerable<Employee>> info(string employee_id = "")
        {
            if (!long.TryParse(employee_id, out long id))
            {
                var obj = new
                {
                    code = 1,
                    msg = "id非法或不存在",
                    data = ""
                };
                return Content(JsonConvert.SerializeObject(obj), "application/json");
            }

            var query = _context.Employees
                    .Where(e => e.EmployeeId == id)
                    .Select(e => new
                    {
                        station_id = e.switchStation.StationId.ToString(),
                        station_name = e.switchStation.StationName,
                        longitude = e.switchStation.Longitude,
                        latitude = e.switchStation.Latitude,
                        faliure_status = e.switchStation.FailureStatus,
                        battery_capacity = e.switchStation.BatteryCapacity,
                        available_battery_count = e.switchStation.AvailableBatteryCount
                    })
                    .FirstOrDefault();

            var totalNum = _context.VehicleOwners.Count();
            var responseObj = new
            {
                code = 0,
                msg = "success",
                data = query,
            };
            return Content(JsonConvert.SerializeObject(responseObj), "application/json");
        }

        [HttpGet("battery")]
        public ActionResult<IEnumerable<Battery>> battery(string station_id = "", string available_status = "", string battery_type_id = "")
        {
            if (!long.TryParse(station_id, out long id))
            {
                var obj = new
                {
                    code = 1,
                    msg = "id非法或不存在",
                    totaldata = 0,
                    data = ""
                };
                return Content(JsonConvert.SerializeObject(obj), "application/json");
            }

            var query = _context.Batteries
                    .Where(b => b.switchStation.StationId == id && battery_type_id == "" ? true : b.batteryType.BatteryTypeId.ToString() == battery_type_id && available_status == "" ? true : Enum.GetName(typeof(AvailableStatusEnum), b.AvailableStatusEnum) == available_status)
                    .Select(b => new
                    {
                        battery_id = b.BatteryId.ToString(),
                        available_status = Enum.GetName(typeof(AvailableStatusEnum), b.AvailableStatusEnum),
                        current_capacity = b.CurrentCapacity,
                        curr_charge_times = b.CurrChargeTimes,
                        manufacturing_date = b.ManufacturingDate.ToString(),
                        battery_type_id = b.batteryType.BatteryTypeId,
                        isEditing = false
                    })
                    .ToList();

            var totalNum = _context.VehicleOwners.Count();
            var responseObj = new
            {
                code = 0,
                msg = "success",
                totaldata = totalNum,
                data = query,
            };
            return Content(JsonConvert.SerializeObject(responseObj), "application/json");
        }

        [HttpPatch("battery/update")]
        public IActionResult BatteryUpdate([FromBody] dynamic param)
        {
            dynamic _param = JsonConvert.DeserializeObject(Convert.ToString(param));
            var bty = _context.Batteries.Find(_param.battery_id);
            if (bty == null)
            {
                return NewContent(1, "查询电池不存在");
            }

            if (_param.available_status != null)
            {
                if (Enum.TryParse(_param.available_status, out AvailableStatusEnum availableStatus))
                {
                    bty.AvailableStatusEnum = availableStatus;
                }
                else
                {
                    return NewContent(2, "无效的可用状态值");
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return NewContent(3, e.InnerException?.Message + "");
            }

            return NewContent();
        }


        [HttpPost("battery/add")]
        public IActionResult batteryadd([FromBody] dynamic param)
        {
            dynamic _param = JsonConvert.DeserializeObject(Convert.ToString(param));
            if (_context.Batteries == null)
            {
                return Problem("Entity set 'ModelContext.Batteries' is null.");
            }
            if(!long.TryParse(_param.battery_type_id, out long btid) || btid != 1 && btid != 2)
            {
                return Problem("电池类型非法");
            }
            long maxBtyId = _context.Batteries.Max(o => (long?)o.BatteryId) ?? 0;
            long newBtyId = maxBtyId + 1;
            Battery new_bty = new Battery()
            {
                BatteryId = newBtyId,
                AvailableStatus = 1,
                CurrentCapacity = 100.00,
                CurrChargeTimes = 0,
                ManufacturingDate = System.DateTime.Now,
                batteryType = _context.BatteryTypes.Find(btid)
            };
            _context.Batteries.Add(new_bty);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var a = new
                {
                    code = 1,
                    msg = e.InnerException?.Message
                };

                return Conflict(a);
            }
            var obj = new
            {
                code = 0,
                msg = "success",
                battery_id = newBtyId
            };
            return Content(JsonConvert.SerializeObject(obj), "application/json");
        }

        [HttpDelete("battery/delete")]
        public IActionResult batterydelete([FromBody] dynamic param)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                dynamic _param = JsonConvert.DeserializeObject(Convert.ToString(param));
                if (_context.Batteries == null)
                {
                    return Problem("Entity set 'ModelContext.Batteries' is null.");
                }
                if (!long.TryParse(_param.battery_id, out long bid))
                {
                    return Problem("电池id非法");
                }
                var bty = _context.VehicleOwners.Find(bid);
                if (bty == null)
                {
                    return NewContent(1, "找不到该车主");
                }

                _context.VehicleOwners.Remove(bty);
                try
                {
                    _context.SaveChanges();
                    tx.Complete();
                }
                catch (DbUpdateException e)
                {
                    return NewContent(1, e.InnerException?.Message);
                }
                return NewContent();
            }
        }

        ContentResult NewContent(int _code = 0, string _msg = "success")
        {
            var a = new
            {
                code = _code,
                msg = _msg
            };
            return Content(JsonConvert.SerializeObject(a), "application/json");
        }
    }
}
