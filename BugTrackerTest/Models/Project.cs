using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTrackerTest.Models;

namespace BugTrackerTest.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Manager { get; set; }

        public bool Deleted { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Project()
        {
            Tickets = new HashSet<Ticket>();
            Users = new HashSet<ApplicationUser>();
        }
        //creation
        public Project(string name)
        {
            Name = name;
            Created = DateTime.Today.Date;
        }

        public Project(string name, string descrip)
        {
            Name = name;
            Description = descrip;
            Created = DateTime.Today.Date;
        }


        //To Be Developed
        //public Project(string name, string userId)
        //{
            
        //}




        //update of project name
        //soft delete
        //
    }
}