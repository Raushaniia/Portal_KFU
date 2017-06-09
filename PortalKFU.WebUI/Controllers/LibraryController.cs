using System.Linq;
using System.Web.Mvc;
using PortalKFU.Domain.Entities;
using PortalKFU.Domain.Abstract;
using PortalKFU.WebUI.Models;

namespace PortalKFU.WebUI.Controllers
{
    public class LibraryController : Controller
    {
        private IEventRepository repository;
        private IDownloadProcessor downloadProcessor;
        private object p;

        public LibraryController(IEventRepository repo, IDownloadProcessor processor)
        {
            repository = repo;
            downloadProcessor = processor;
        }

        public LibraryController(object p)
        {
            this.p = p;
        }

        public ViewResult Download()
        {
            return View(new DocumentDetails());
        }

        [HttpPost]
        public ViewResult Download(Library library, DocumentDetails documentDetails)
        {
            if (library.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                downloadProcessor.ProcessDownload(library, documentDetails);
                library.Clear();
                return View("Completed");
            }
            else
            {
                return View(documentDetails);
            }
        }

        public ViewResult Index(Library library, string returnUrl)
        {
            return View(new LibraryIndexViewModel
            {
                Library = library,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToLibrary(Library library, int eventId, string returnUrl)
        {
            Event ev = repository.Events
                .FirstOrDefault(g => g.EventId == eventId);

            if (ev != null)
            {
                library.AddItem(ev, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromLibrary(Library library, int eventId, string returnUrl)
        {
            Event ev = repository.Events
                .FirstOrDefault(g => g.EventId == eventId);

            if (ev != null)
            {
                library.RemoveLine(ev);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Library library)
        {
            return PartialView(library);
        }
    }
}