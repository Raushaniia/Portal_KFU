using System.Collections.Generic;
using PortalKFU.Domain.Entities;
using PortalKFU.Domain.Abstract;

namespace PortalKFU
{
    public class EFEventRepository : IEventRepository
    {
        ApplicationContext context = new ApplicationContext();

        public IEnumerable<Event> Events
        {
            get { return context.Events; }
        }
        public void SaveGame(Event ev)
        {
            if (ev.EventId == 0)
                context.Events.Add(ev);
            else
            {
                Event dbEntry = context.Events.Find(ev.EventId);
                if (dbEntry != null)
                {
                    dbEntry.Name = ev.Name;
                    dbEntry.Description = ev.Description;
                    dbEntry.Price = ev.Price;
                    dbEntry.Category = ev.Category;
                    dbEntry.ImageData = ev.ImageData;
                    dbEntry.ImageMimeType = ev.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Event DeleteEvent(int eventId)
        {
            Event dbEntry = context.Events.Find(eventId);
            if (dbEntry != null)
            {
                context.Events.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
