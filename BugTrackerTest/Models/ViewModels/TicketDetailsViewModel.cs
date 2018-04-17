using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models
{
    public class TicketDetailsViewModel
    {

        public Ticket ticket { get; set;  }

        public TicketAttachment Attachment { get; set;  }

        public TicketComment Comment { get; set; }

        public string OwnerName { get; set; }

        public string AssignedName { get; set; }
    }
}