using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models
{
    public class MyProjectsViewModel
    {
        public ICollection<Project> Projects { get; set; }

        public Ticket Ticket { get; set; }

        public int NumberOfTkts { get; set; }
    }
}