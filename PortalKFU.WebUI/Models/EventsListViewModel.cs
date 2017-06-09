using System.Collections.Generic;
using PortalKFU.Domain.Entities;

namespace PortalKFU.WebUI.Models
{
    public class EventsListViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}