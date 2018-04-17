using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models
{
    public class ProjectManagerUserViewModel
    {
        public ICollection<Project> Projects { get; set; }

        public Ticket NewestTicket { get; set; }


    }
}