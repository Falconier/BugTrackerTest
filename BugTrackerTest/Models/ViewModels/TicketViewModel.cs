using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models.ViewModels
{
    public class TicketViewModel
    {
        public ApplicationUser user { get; set; }

        public Project project { get; set; }

        public Ticket tickets { get; set; }

        public IEnumerable<TicketComment> Comment { get; set; }
    }
}