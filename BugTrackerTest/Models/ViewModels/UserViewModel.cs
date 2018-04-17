using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace BugTrackerTest.Models.ViewModels
{
    /// <summary>
    /// This is a User-Based viewModel. Based on the user's role and unique ID, it will show the projects and tickets associated.
    /// </summary>
    
    public class UserViewModel
    {
        public ApplicationUser user { get; }

        public Project project { get; set; }

        public Ticket ticket { get; set; }
    }
}