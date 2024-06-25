using EntityFramework.Context;
using EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Admins

{
    public class NewsRet
    {
        public string? title { get; set; }
        public int publish_pos { get; set; }
        public string? contents { get; set; }
        public long publisher { get; set; }
        public DateTime publish_time { get; set; }
        public long announcement_id { get; set; }
    }
    public class AnnouncementRepository
    {
        private readonly ModelContext _context;

        public AnnouncementRepository(ModelContext context)
        {
            _context = context;
        }

        public List<News> GetLatestAnnouncements()
        {
            return _context.News
                .OrderByDescending(a => a.PublishTime)
                .Take(5)
                .ToList();
        }


        public List<NewsRet> GetAnnouncements()
        {
            return _context.News.OrderByDescending(a => a.PublishTime).Select(
    e => new NewsRet
    {
        title = e.Title,
        publish_pos = e.PublishPos,
        contents = e.Contents,
        publisher = e.administrator.AdminId,
        publish_time = e.PublishTime,
        announcement_id = e.AnnouncementId
    }
).ToList();
        }

        public List<News> QueryAnnouncements(string title, long? publisherId, DateTime? publishDate, int publishPos, string contents)
        {
            var query = _context.News.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(a => a.Title.Contains(title));
            }
            if (publisherId.HasValue)
            {
                query = query.Where(a => a.administrator.AdminId == publisherId);
            }
            if (publishDate.HasValue)
            {
                query = query.Where(a => a.PublishTime.Date == publishDate.Value);
            }
            if (publishPos != -1)
            {
                query = query.Where(a => a.PublishPos == publishPos);
            }
            if (!string.IsNullOrEmpty(contents))
            {
                query = query.Where(a => a.Contents.Contains(contents));
            }

            return query.OrderByDescending(a => a.PublishTime).ToList();
        }

        public News GetAnnouncementById(long id)
        {
            return _context.News.Find(id);
        }

        public void AddAnnouncement(News announcement)
        {
            _context.News.Add(announcement);
            _context.SaveChanges();
        }

        public void UpdateAnnouncement(News announcement)
        {
            _context.News.Update(announcement);
            _context.SaveChanges();
        }

        public void DeleteAnnouncement(News announcement)
        {
            _context.News.Remove(announcement);
            _context.SaveChanges();
        }
    }
}
