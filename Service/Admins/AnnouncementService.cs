using DAL.Admins;
using EntityFramework.Models;
using Idcreator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL;

namespace Service.Admins
{
    public class AnnouncementService
    {
        private readonly AnnouncementRepository _repository;

        public AnnouncementService(AnnouncementRepository repository)
        {
            _repository = repository;
        }

        public object GetLatestAnnouncements()
        {
            var query = _repository.GetLatestAnnouncements()
                .Select(e => new
                {
                    title = e.Title.TrimEnd(),
                    publish_time = e.PublishTime,
                    announcement_id = e.AnnouncementId
                })
                .ToList();

            return new
            {
                code = 0,
                msg = "success",
                data = query,
            };
        }

        public object GetAnnouncements()
        {
            var query = _repository.GetAnnouncements();

            return new
            {
                code = 0,
                msg = "success",
                announcementArray = query,
            };
        }

        public object QueryAnnouncements(string title, string publisher, string publish_time, int publish_pos, string contents)
        {
            DateTime? date = null;
            long? id = null;

            if (DateTime.TryParse(publish_time, out DateTime b))
                date = b.Date;
            if (long.TryParse(publisher, out var _id))
                id = _id;

            var query = _repository.QueryAnnouncements(title, id, date, publish_pos, contents)
                .Select(e => new
                {
                    title = e.Title,
                    publish_pos = e.PublishPos,
                    contents = e.Contents,
                    publisher = e.administrator.AdminId,
                    publish_time = e.PublishTime,
                    announcement_id = e.AnnouncementId
                })
                .ToList();

            return new
            {
                code = 0,
                msg = "success",
                announcementArray = query
            };
        }

        public object AddAnnouncement(dynamic _acm)
        {
            long id = SnowflakeIDcreator.nextId();
            var acm = new News()
            {
                AnnouncementId = id,
                Contents = _acm.contents,
                PublishTime = DateTime.Now,
                Title = _acm.title,
                PublishPos = _acm.publish_pos,
                administrator = _repository.GetAnnouncementById(long.Parse($" {_acm.publisher}")).administrator
            };

            _repository.AddAnnouncement(acm);

            return new { code = 0, msg = "success" };
        }

        public object UpdateAnnouncement(dynamic _acm)
        {
            bool flag = long.TryParse($"{_acm.announcement_id}", out long id);
            if (!flag)
                return new { code = 1, msg = "id无效" };

            var acm = _repository.GetAnnouncementById(id);
            if (acm == null)
                return new { code = 1, msg = "无该id的公告" };

            acm.Contents = _acm.contents;
            acm.Title = _acm.title;
            acm.PublishPos = _acm.publish_pos;
            if (long.TryParse($"{_acm.publisher}", out var b))
                acm.administrator = _repository.GetAnnouncementById(b).administrator;

            _repository.UpdateAnnouncement(acm);

            return new { code = 0, msg = "success" };
        }

        public object DeleteAnnouncement(dynamic _acm)
        {
            long? id = _acm.announcement_id;
            if (id == null)
                return new { code = 1, msg = "请输入id" };

            var acm = _repository.GetAnnouncementById(id.Value);
            if (acm == null)
                return new { code = 1, msg = "无此id的公告" };

            _repository.DeleteAnnouncement(acm);

            return new { code = 0, msg = "success" };
        }
    }
}
