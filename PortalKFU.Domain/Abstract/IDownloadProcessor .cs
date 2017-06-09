using PortalKFU.Domain.Entities;

namespace PortalKFU.Domain.Abstract
{
    public interface IDownloadProcessor
    {
        void ProcessDownload(Library library, DocumentDetails documentDetails);
    }
}