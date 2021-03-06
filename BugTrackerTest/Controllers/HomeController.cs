﻿using BugTrackerTest.Models;
using BugTrackerTest.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerTest.Controllers
{
    public class HomeController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper helper = new UserRolesHelper();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Project Manager")]
        public ActionResult PMDashboard()
        {

            var usrId = User.Identity.GetUserId();
            //List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
            UserRolesHelper usrHlp = new UserRolesHelper();
            ProjectManagerViewModel pmvm = new ProjectManagerViewModel();
            ProjectHelper prjHlp = new ProjectHelper();
            TicketHelper tktHlp = new TicketHelper();
            #region a
            //foreach (var usr in db.Users)
            //{
            //    pmvm.Projects = prjHlp.ListUserProjects(usr.i);

            //    pmvm.Tickets = tktHlp.ListAllTickets();

            //    //vm.Projects = prjHlp.ListAllProjects();
            //    //Model.Add(vm);
            //}
            #endregion
            foreach (var prj in prjHlp.ListUserProjects(usrId))
            {
                pmvm.Projects.Add(prj);
                pmvm.Tickets.Add(prjHlp.PullNewestTicket(prj.Id));
            }
            return View(pmvm);
        }

        [Authorize(Roles = "Developer")]
        public ActionResult DevDashboard()
        {

            var usrId = User.Identity.GetUserId();
            //List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
            UserRolesHelper usrHlp = new UserRolesHelper();
            DevViewModel dvm = new DevViewModel();
            
            TicketHelper tktHlp = new TicketHelper();
            #region a
            //foreach (var usr in db.Users)
            //{
            //    pmvm.Projects = prjHlp.ListUserProjects(usr.i);

            //    pmvm.Tickets = tktHlp.ListAllTickets();

            //    //vm.Projects = prjHlp.ListAllProjects();
            //    //Model.Add(vm);
            //}
            #endregion
            foreach (var tkt in tktHlp.GetAssignedTickets(usrId))
            {
                dvm.Tickets.Add(tkt);
                if(!dvm.Projects.Contains(tkt.Project))
                {
                    dvm.Projects.Add(tkt.Project);
                }
            }
            return View(dvm);
        }

        [Authorize(Roles = "Submitter")]
        public ActionResult SubmitterDashboard()
        {
            var usrId = User.Identity.GetUserId();
            //List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
            UserRolesHelper usrHlp = new UserRolesHelper();
            SubmitterViewModel svm = new SubmitterViewModel();
            ProjectHelper prjHlp = new ProjectHelper();
            TicketHelper tktHlp = new TicketHelper();
            #region a
            //foreach (var usr in db.Users)
            //{
            //    pmvm.Projects = prjHlp.ListUserProjects(usr.i);

            //    pmvm.Tickets = tktHlp.ListAllTickets();

            //    //vm.Projects = prjHlp.ListAllProjects();
            //    //Model.Add(vm);
            //}
            #endregion
            foreach (var tkt in tktHlp.GetOwnedTickets(usrId))
            {
                svm.Tickets.Add(tkt);
                if (!svm.Projects.Contains(tkt.Project))
                {
                    svm.Projects.Add(tkt.Project);
                }
            }
            return View(svm);
        }

        public ActionResult JEBBugtracker()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}