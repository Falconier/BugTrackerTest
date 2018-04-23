using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models.ViewModels
{
    public class ProjectManagerViewModel
    {
        public ICollection<ApplicationUser> TeamMembers { get; set; }

        public ICollection<Ticket> Tickets { get; set; } 

        public ICollection<Project> Projects { get; set; }

        public ProjectManagerViewModel()
        {
            TeamMembers = new List<ApplicationUser>();
            Tickets = new List<Ticket>();
            Projects = new List<Project>();
        }
    }
}