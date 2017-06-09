namespace PortalKFU.WebUI.Infrastructure.Absrtact
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
