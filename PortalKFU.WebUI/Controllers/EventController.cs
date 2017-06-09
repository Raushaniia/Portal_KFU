using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalKFU.Domain.Abstract;
using PortalKFU.Domain.Entities;
using PortalKFU.WebUI.Models;

namespace PortalKFU.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository repository;
        public int pageSize = 4;

        public EventController(IEventRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            EventsListViewModel model = new EventsListViewModel
            {
                Events = repository.Events
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(ev => ev.EventId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                repository.Events.Count() :
                repository.Events.Where(ev => ev.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int eventId)
        {
            Event ev = repository.Events
                .FirstOrDefault(g => g.EventId == eventId);

            if (ev != null)
            {
                return File(ev.ImageData, ev.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
