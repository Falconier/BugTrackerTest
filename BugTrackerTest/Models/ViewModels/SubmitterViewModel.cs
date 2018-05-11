using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models.ViewModels
{
    public class SubmitterViewModel
    {
        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<Project> Projects { get; set; }
        public SubmitterViewModel()
        {
            Tickets = new List<Ticket>();
            Projects = new List<Project>();
        }
    }
}