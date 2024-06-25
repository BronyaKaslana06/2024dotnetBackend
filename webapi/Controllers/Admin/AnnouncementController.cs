using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Service.Admins;

namespace webapi.Controllers.Admin
{
    [Route("administrator/announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly AnnouncementService _announcementService;

        public AnnouncementController(AnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [Authorize]
        [HttpGet("newest")]
        public IActionResult GetLatestAnnouncements()
        {
            var result = _announcementService.GetLatestAnnouncements();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Authorize]
        [HttpGet("message")]
        public IActionResult GetPage_()
        {
            var result = _announcementService.GetAnnouncements();
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Authorize]
        [HttpGet("query")]
        public IActionResult Query(string title = "", string publisher = "", string publish_time = "", int publish_pos = -1, string contents = "")
        {
            var result = _announcementService.QueryAnnouncements(title, publisher, publish_time, publish_pos, contents);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Authorize]
        [HttpPost]
        public IActionResult PotAnnouncement([FromBody] dynamic _acm)
        {
            var result = _announcementService.AddAnnouncement(_acm);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Authorize]
        [HttpPatch]
        public IActionResult PatAnnouncement([FromBody] dynamic _acm)
        {
            var result = _announcementService.UpdateAnnouncement(_acm);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DelAnnouncement([FromBody] dynamic _acm)
        {
            var result = _announcementService.DeleteAnnouncement(_acm);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
