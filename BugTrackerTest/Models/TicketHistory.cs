﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public string Property { get; set; }

        public string OldVal { get; set; }

        public string NewVal { get; set; }

        public DateTimeOffset Changed { get; set; }

        public string UserId { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}