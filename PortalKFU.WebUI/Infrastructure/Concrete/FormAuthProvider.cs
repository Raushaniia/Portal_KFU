using System.Web.Security;
using PortalKFU.WebUI.Infrastructure.Absrtact;

namespace PortalKFU.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = GetResult(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }

        private static bool GetResult(string username, string password)
        {
            return FormsAuthentication.Authenticate(username, password);
        }
    }
}