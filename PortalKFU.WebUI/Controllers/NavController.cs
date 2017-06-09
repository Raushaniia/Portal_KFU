using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PortalKFU.Domain.Abstract;

namespace PortalKFU.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IEventRepository repository;

        public NavController(IEventRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Events
                .Select(ev => ev.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}