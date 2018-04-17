using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerTest.Models.ViewModels
{
    public class AdminProjectViewModel
    {
        public SelectList pm { get; set; }

        public string SelectedListUser { get; set; }


    }
}