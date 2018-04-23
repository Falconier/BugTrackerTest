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

        
    }
}