using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Admins;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers.Admin
{
    [Route("administrator/battery")]
    [ApiController]
    public class BatteryController : ControllerBase
    {
        private readonly BatteryService _batteryService;

        public BatteryController(BatteryService batteryService)
        {
            _batteryService = batteryService;
        }

        [Authorize]
        [HttpGet("query")]
        public IActionResult Battery(int pageIndex = 0, int pageSize = 0, string available_status = "", string battery_type_id = "", int battery_status = 0, string keyword = "")
        {
            var result = _batteryService.QueryBatteries(pageIndex, pageSize, available_status, battery_type_id, battery_status, keyword);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
