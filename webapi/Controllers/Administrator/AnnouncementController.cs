using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using EntityFramework.Context;
using EntityFramework.Models;
using Idcreator;

namespace webapi.Controllers.Administrator
{
    [Route("administrator/announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly ModelContext _context;

        public AnnouncementController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet("message")]
        public ActionResult<IEnumerable<Employee>> GetPage_()
        {
            var a = new
            {
                announcementArray = OracleHelper.SelectSql("select * from News"),
            };
            return Content(JsonConvert.SerializeObject(a), "application/json");
        }
        [HttpPost]
        public IActionResult PotAnnouncement([FromBody] dynamic _acm)
        {
            _acm = JsonConvert.DeserializeObject(Convert.ToString(_acm));

            string id = SnowflakeIDcreator.nextId().ToString();
            var acm = new News()
            {
                AnnouncementId = id,
                Contents = _acm.contents,
                PublishTime = DateTime.Now,
                Title = _acm.title,
                PublishPos = _acm.publish_Pos
            };
            _context.Add(acm);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return NewContent(1, e.InnerException?.Message + "");
            }

            return NewContent(0, "success");
        }
        [HttpPatch]
        public IActionResult PatAnnouncement([FromBody] dynamic _acm)
        {
            _acm = JsonConvert.DeserializeObject(Convert.ToString(_acm));
            
            string id = $"{_acm.announcement_id}";
            if(id==null)
                return NewContent(1, "id为空");
            
            var acm = _context.News.Find(id);

            if(acm==null)
                return NewContent(1,"无该id的公告");
            else
            {
                acm.Contents = _acm.contents;
                //acm.PublishTime = Convert.ToDateTime(_acm.publish_time);
                acm.Title = _acm.title;
                acm.PublishPos = _acm.publish_Pos;
            }
            try{
                _context.SaveChanges();
            }
            catch(DbUpdateException e)
            {
                return NewContent(1, e.InnerException?.Message+"");
            }
            
            return NewContent(0,"success");
        }
        [HttpDelete]
        public IActionResult DelAnnouncement([FromBody] dynamic _acm)
        {
            _acm = JsonConvert.DeserializeObject(Convert.ToString(_acm));
            string id = $"{_acm.announcement_id}";
            if (id == null)
                return NewContent(1, "请输入id");
            var acm = _context.News.Find(id);
            if(acm==null)
            {
                return NewContent(1, "无此id的公告");
            }
            else
            {
                _context.News.Remove(acm);
                try{
                    _context.SaveChanges();
                }
                catch(DbUpdateException e)
                {
                    return NewContent(1, e.InnerException?.Message + "");
                }
                return NewContent(0,"success");
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