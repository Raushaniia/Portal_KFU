using PortalKFU.Domain.Entities;
using System.Web.Mvc;

namespace PortalKFU.WebUI.Infrastructure.Binders
{
    public class LibraryModelBinder : IModelBinder
    {
        private const string sessionKey = "Library";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            // Получить объект Cart из сеанса
            Library library = null;
            if (controllerContext.HttpContext.Session != null)
            {
                library = (Library)controllerContext.HttpContext.Session[sessionKey];
            }

            // Создать объект Cart если он не обнаружен в сеансе
            if (library == null)
            {
                library = new Library();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = library;
                }
            }

            // Возвратить объект Cart
            return library;
        }
    }
}