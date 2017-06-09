using System.Web.Mvc;
using System.Web;
using System.Linq;
using PortalKFU.Domain.Abstract;
using PortalKFU.Domain.Entities;

namespace PortalKFU.WebUI.Controllers
{
 
    public class AdminController : Controller
    {
        IEventRepository repository;

        public AdminController(IEventRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Events);
        }
        public ViewResult Edit(int eventId)
        {
            Event ev = repository.Events
                .FirstOrDefault(g => g.EventId == eventId);
            return View(ev);
        }
        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(Event ev, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    ev.ImageMimeType = image.ContentType;
                    ev.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(ev.ImageData, 0, image.ContentLength);
                }
                repository.SaveGame(ev);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", ev.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(ev);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Event());
        }
        [HttpPost]
        public ActionResult Delete(int eventId)
        {
            Event deletedEvent = repository.DeleteEvent(eventId);
            if (deletedEvent != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена",
                    deletedEvent.Name);
            }
            return RedirectToAction("Index");
        }
    }
}