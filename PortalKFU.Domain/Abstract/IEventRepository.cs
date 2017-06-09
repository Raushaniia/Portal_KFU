using System.Collections.Generic;
using PortalKFU.Domain.Entities;

namespace PortalKFU.Domain.Abstract
{
    public interface IEventRepository
    {
        IEnumerable<Event> Events { get; }
        void SaveGame(Event ev);
        Event DeleteEvent(int evId);
    }
}
