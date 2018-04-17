using System.Collections.Generic;

namespace BugTrackerTest.Models
{
    public class TicketPriority
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}