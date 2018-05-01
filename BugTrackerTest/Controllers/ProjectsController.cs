using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrackerTest.Models;

namespace BugTrackerTest.Controllers
{
    public class ProjectsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper ph = new ProjectHelper();
        private UserRolesHelper uh = new UserRolesHelper();

        // GET: Project
        [Authorize(Roles = "Admin, Project Manager, Developer, Submitter")]
        public ActionResult Index()
        {
            //ProjectManagerUserViewModel model = new ProjectManagerUserViewModel();
            
            
            //List<Project> Lprj = new List<Project>();
            //List<Ticket> Ltkt = new List<Ticket>();

            List<ProjectsViewModel> pvm = new List<ProjectsViewModel>();

            //foreach ( var prj in db.Projects)
            //{
            //    mpvm.Projects = ph.ListAllProjects();
            //    mpvm.NumberOfTkts = ph.GetNumberTickets(prj.Id);
            //}

            foreach ( var prj in db.Projects.ToList())
            {
                var vm = new ProjectsViewModel();
                vm.Project = prj;

                vm.PManager = db.Users.Find(prj.Manager);

                pvm.Add(vm);
            }
            //pmuv.Projects = Lprj;
            

            //pmuv.NewestTicket = pmuv.Projects.SelectMany(t => t.Tickets).OrderByDescending(tkt => tkt.Created).FirstOrDefault();
            return View(pvm);
        }

        public ActionResult AllProjects()
        {
            List<ProjectsViewModel> pvm = new List<ProjectsViewModel>();
            foreach (var prj in db.Projects.ToList())
            {
                var vm = new ProjectsViewModel();
                vm.Project = prj;

                vm.PManager = db.Users.Find(prj.Manager);

                pvm.Add(vm);
            }
            return View(pvm);
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var pms = uh.UsersInRole("Project Manager");
            ViewBag.Manager = new SelectList(pms, "Id", "FullName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Manager")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Created = DateTimeOffset.Now;
                db.Projects.Add(project);
                db.SaveChanges();

                //correct but not neccascry
                //var pId = db.Projects.Find(project).Id;

                AddManger(project.Id, project.Manager);
                

                return RedirectToAction("Index");
            }

            return View(project);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prjId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        public void AddManger(int prjId, string Id)
        {
            ph.AddProjectManager(Id, prjId);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
