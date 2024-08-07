using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using EntityFramework.Context;
using EntityFramework.Models;
using Idcreator;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;

namespace webapi.Controllers.Admin
{
    [Route("administrator/announcement")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly ModelContext _context;

        [DllImport("Snowflake.dll")]
        private static extern long NextId();

        [DllImport("Snowflake.dll")]
        private static extern void SetWorkerId(int id);


        public AnnouncementController(ModelContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("newest")]
        public ActionResult<IEnumerable<Employee>> GetLatestAnnouncements()
        {
            var query = _context.News
                .OrderByDescending(a => a.PublishTime)
                .Select(e => new
                {
                    title = e.Title.TrimEnd(),
                    publish_time = e.PublishTime,
                    announcement_id = e.AnnouncementId
                })
                .Take(5)
                .ToList();

            var response = new
            {
                code = 0,
                msg = "success",
                data = query,
            };

            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [Authorize]
        [HttpGet("message")]
        public ActionResult<IEnumerable<Employee>> GetPage_()
        {
            // Random rand=new Random();
            // foreach(var k in _context.News.ToList())
            // {
            //     k.PublishTime= k.PublishTime
            //     .AddHours(rand.NextDouble()*24)
            //     .AddSeconds(rand.NextDouble()*60)
            //     .AddMinutes(rand.NextDouble()*60)
            //     .AddDays(rand.NextDouble() * -60);
            //     k.administrator = _context.Administrators.ToList()[rand.Next(1, 5)];
            //     _context.SaveChanges();
            // }
            var query = _context.News.OrderByDescending(a=>a.PublishTime).Select(
                e => new
                {
                    title = e.Title,
                    publish_pos = e.PublishPos,
                    contents = e.Contents,
                    publisher = e.administrator.AdminId,
                    publish_time = e.PublishTime,
                    announcement_id = e.AnnouncementId
                }
            ).ToList();
            var a = new
            {
                code=0,
                msg="success",
                announcementArray = query,
            };
            return Content(JsonConvert.SerializeObject(a), "application/json");
        }

        [Authorize]
        [HttpGet("query")]
        public ActionResult Query(string title="",string publisher="",string publish_time="",int publish_pos=-1,string contents="")
        {
            DateTime? date=null;
            long? id = null;
            if(DateTime.TryParse(publish_time, out DateTime b))
                date=b.Date;
            if (long.TryParse(publisher, out var _id))
                id = _id;
            var c=_context.News.Where(a =>
            (a.Title==null? title=="" : a.Title.Contains(title))  &&
            (id==null? publisher=="": a.administrator.AdminId==id)&&
            (publish_pos==-1||a.PublishPos==publish_pos) &&
            (date==null? publish_time=="":a.PublishTime.Date == date)
            ).OrderByDescending(a => a.PublishTime).Select(e=>new{
                title=e.Title,
                publish_pos=e.PublishPos,
                contents=e.Contents,
                publisher=e.administrator.AdminId,
                publish_time=e.PublishTime,
                announcement_id=e.AnnouncementId
            }).ToList();

            if (contents != null)
            {
                int l = c.Count();
                for (int i = 0; i < l; i++)
                {
                    var item = c[i];
                    if (item.contents == null || !item.contents.Contains(contents))
                    {
                        c.Remove(item);
                        i--;
                        l--;
                    }
                }
            }
            var a = new
            {
                code = 0,
                msg = "success",
                announcementArray = c
            };
            return Content(JsonConvert.SerializeObject(a), "application/json");
        }

        [Authorize]
        [HttpPost]
        public IActionResult PotAnnouncement([FromBody] dynamic _acm)
        {
            _acm = JsonConvert.DeserializeObject(Convert.ToString(_acm));

            //long id = SnowflakeIDcreator.nextId();
            long id = NextId();
            var acm = new News()
            {
                AnnouncementId = id,
                Contents = _acm.contents,
                PublishTime = DateTime.Now,
                Title = _acm.title,
                PublishPos = _acm.publish_pos,
                administrator=_context.Administrators.Find(long.Parse($" {_acm.publisher}"))
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

        [Authorize]
        [HttpPatch]
        public IActionResult PatAnnouncement([FromBody] dynamic _acm)
        {
            _acm = JsonConvert.DeserializeObject(Convert.ToString(_acm));
            
            bool flag = long.TryParse($"{_acm.announcement_id}", out long id);
            if(!flag)
                return NewContent(1, "id无效");
            
            var acm = _context.News.Find(id);

            if(acm==null)
                return NewContent(1,"无该id的公告");
            else
            {
                acm.Contents = _acm.contents;
                //acm.PublishTime = Convert.ToDateTime(_acm.publish_time);
                acm.Title = _acm.title;
                acm.PublishPos = (int)_acm.publish_pos;
                if(long.TryParse($"{_acm.publisher}",out var b))
                    acm.administrator=_context.Administrators.Find(b);
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

        [Authorize]
        [HttpDelete]
        public IActionResult DelAnnouncement([FromBody] dynamic _acm)
        {
            _acm = JsonConvert.DeserializeObject(Convert.ToString(_acm));
            long? id = _acm.announcement_id;
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