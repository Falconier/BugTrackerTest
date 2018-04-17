﻿using BugTrackerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerTest.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper helper = new UserRolesHelper();
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //List<AdminIndexViewModel> model = new List<AdminIndexViewModel>();
            UserRolesHelper usrHlp = new UserRolesHelper();
            AdminIndexViewModel vm = new AdminIndexViewModel();
            TicketHelper tktHlp = new TicketHelper();
            ProjectHelper prjHlp = new ProjectHelper();
            List<ProjectsViewModel> pvmList = new List<ProjectsViewModel>();
            foreach (var usr in db.Users)
            {
                vm.Users = usrHlp.ListAllUsers();
                vm.Roles = usrHlp.ListUsersRoles(usr.Id);
                vm.Tickets = tktHlp.ListAllTickets();
                                               
                //vm.Projects = prjHlp.ListAllProjects();
                //Model.Add(vm);
            }
            foreach(var prj in db.Projects.ToList())
            {
                var pvm = new ProjectsViewModel();
                pvm.Project = prj;
                pvm.PManager = db.Users.Find(prj.Manager);
                pvmList.Add(pvm);

            }
            vm.PVModel = pvmList;
            return View(vm);
        }

        public ActionResult EditUser(string id)
        {
            var user = db.Users.Find(id);
            AdminUserViewModel AdminModel = new AdminUserViewModel();
            
            var selected = helper.ListUsersRoles(id);
            AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
            AdminModel.User = new ApplicationUser();
            AdminModel.User.Id = user.Id;
            AdminModel.User = new ApplicationUser();
            AdminModel.User.FullName = user.FullName;

            return View(AdminModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(AdminUserViewModel model)
        {
            //var user = db.Users.Find(model.User.Id);
            UserRolesHelper helper = new UserRolesHelper();
            foreach (var rolermv in db.Roles.Select(r => r.Name).ToList())
            {
                helper.RemoveUserFromRole(model.User.Id, rolermv);
            }
            foreach (var roleadd in model.SelectedRoles)
            {
                helper.AddUserToRole(model.User.Id, roleadd);
            }
            return RedirectToAction("Index");
        }
    }
}