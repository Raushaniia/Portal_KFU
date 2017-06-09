using System.Collections.Generic;
using System.Linq;

namespace PortalKFU.Domain.Entities
{
    public class Library
    {
        private List<LibraryLine> lineCollection = new List<LibraryLine>();

        public void AddItem(Event ev, int quantity)
        {
            LibraryLine line = lineCollection
                .Where(g => g.Event.EventId == ev.EventId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new LibraryLine
                {
                    Event = ev,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Event ev)
        {
            lineCollection.RemoveAll(l => l.Event.EventId == ev.EventId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Event.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<LibraryLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class LibraryLine
    {
        public Event Event { get; set; }
        public int Quantity { get; set; }
    }
}