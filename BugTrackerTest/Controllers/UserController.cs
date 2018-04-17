using BugTrackerTest.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerTest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper ph = new ProjectHelper();
        private UserRolesHelper urh = new UserRolesHelper();
        private TicketHelper th = new TicketHelper();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Project Manager, Developer")]
        public ActionResult ProjectView()
        {
            var userId = User.Identity.GetUserId();
            var projectAssignmnet = db.Users.Find(userId).Projects.ToList();
            return View(projectAssignmnet);
        }

        /// <summary>
        /// Returns a list of all known projects to a view accesible only to the Admin
        /// </summary>
        /// <returns>A View with a list of projects</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult AdminProjectView()
        {
            var allProjects = ph.ListAllProjects();
            return View(allProjects);
        }
        /// <summary>
        /// Manage Projects View
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult ManageProjects()
        {
            #region
            /*
            if(User.IsInRole("Admin"))
            {
                RedirectToAction("AdminProjectView","User", new { id = User.Identity.GetUserId() });
            }
            */
            #endregion

            var userId = User.Identity.GetUserId();

            var userProjects = db.Projects.ToList();

            // List Active (non-deleted) projects
            if (User.IsInRole("Admin"))
            {
                userProjects = userProjects.Where(d => !d.Deleted).ToList();
            }
            else
            {
                userProjects = userProjects.Where(m => m.Manager == userId).ToList();
            }
            return View(userProjects);
        }

        /// <summary>
        /// Manage Tickets View
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin,Project Manager,Submitter")]
        public ActionResult ManageTickets()
        {
            var userId = User.Identity.GetUserId();
            var userTickets = db.Tickets.Where(t => t.OwnerUserId == userId);
            return View(userTickets.ToList());
        }
    }
}