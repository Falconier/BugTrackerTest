using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BugTrackerTest.Models
{
    public class AdminIndexViewModel
    {
        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        //public ICollection<Project> Projects { get; set; }


        public ICollection<string> Roles { get; set; }

        public ICollection<ProjectsViewModel> PVModel { get; set; }
    }
}