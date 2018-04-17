using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models
{
    public class ProjectsViewModel
    {
        public Project Project { get; set; }

        public ApplicationUser PManager { get; set; }

    }
}