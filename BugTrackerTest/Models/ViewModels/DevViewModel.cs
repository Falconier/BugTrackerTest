using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models.ViewModels
{
    public class DevViewModel
    {
        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<Project> Projects { get; set; }

        public DevViewModel()
        {
            Tickets = new List<Ticket>();
            Projects = new List<Project>();
        }
    }
}