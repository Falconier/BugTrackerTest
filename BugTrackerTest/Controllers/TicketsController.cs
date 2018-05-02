using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BugTrackerTest.Models;
using Microsoft.AspNet.Identity;

namespace BugTrackerTest.Controllers
{

    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper uHelper = new UserRolesHelper();
        private ProjectHelper pHelper = new ProjectHelper();


        // GET: Tickets
        [Authorize]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Priority).Include(t => t.Project).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        public ActionResult AllTickets()
        {
            var tickets = db.Tickets.Include(t => t.Priority).Include(t => t.Project).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        public ActionResult UrgentTickets()
        {
            var tickets = db.Tickets.Include(t => t.Project).Include(t => t.TicketStatus).Include(t => t.TicketType).Where(t => t.Priority.Name.Equals("Urgent"));
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        /// <summary>
        /// Gives a detailed view of a ticket
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Returns a view of ticket</returns>
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ticket ticket = db.Tickets.Include(t => t.Histories).Include(t => t.Comments).Include(t => t.Attachments).FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return HttpNotFound();
            }

            TicketDetailsViewModel model = new TicketDetailsViewModel()
            {
                ticket = ticket,
                Comment = new TicketComment()
                {
                    TicketId = ticket.Id
                }
            };

            return View(model);
        }

        // GET: Tickets/Create
        /// <summary>
        /// Authorized for Submitters, Admin, and Project Managers to create tickets
        /// </summary>
        /// <returns>Returns a view</returns>
        [Authorize(Roles = "Submitter, Admin, Project Manager ")]
        public ActionResult Create(int pId)
        {
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");

            Ticket tkt = new Ticket()
            {
                ProjectId = pId
            };

            return View(tkt);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(TicketComment comment)
        {
            string UID = User.Identity.GetUserId();

            comment.UserId = UID;

            comment.Created = DateTimeOffset.Now;

            db.TicketComments.Add(comment);

            db.SaveChanges();

            return RedirectToAction("Details", new { id = comment.TicketId });
        }
        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Authorrized for Admin, Project Managers, and Submitters to create tickets (BINDED)
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns>Returns a view of ticket</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Proejct Manager, Submitter")]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Created = DateTimeOffset.Now;
                ticket.OwnerUserId = User.Identity.GetUserId();
                ticket.TicketStatusId = 1;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        /// <summary>
        /// Allows Editing from the Admin and Project Manager
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Returns a view of ticket</returns>
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Allows Editing from the Admin and Project Manager (BINDED)
        /// </summary>
        /// <param name="ticket">Ticket Id</param>
        /// <returns>Returns a view with a ticket</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
                ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
                ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
                return View(ticket);
            }



            var helper = new TicketHelper();
            helper.CreateHistories(ticket);

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Details", new { id = ticket.Id });
        }

        public ActionResult AssignTicket(int? id)
        {
            UserRolesHelper helper = new UserRolesHelper();

            var ticket = db.Tickets.Find(id);

            var users = helper.UsersInRole("Developer").ToList();

            ViewBag.AssignedToUserId = new SelectList(users, "Id", "FullName", ticket.AssignedToUserId);

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTicket(Ticket model)
        {

            var ticket = db.Tickets.Find(model.Id);
            ticket.AssignedToUserId = model.AssignedToUserId;

            db.SaveChanges();

            var callbackUrl = Url.Action("Details", "Tickets", new { id = ticket.Id }, protocol: Request.Url.Scheme);
            try
            {
                EmailService ems = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                ApplicationUser user = db.Users.Find(model.AssignedToUserId);

                msg.Body = "You have been assigned a new Ticket." + Environment.NewLine +
                           "Please click the following link to view the details" + "<a href=\"" + callbackUrl + "\">NEW TICKET</a>";
                msg.Destination = user.Email;
                msg.Subject = "YOU HAVE A NEW TICKET!!!";

                await ems.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }

            return RedirectToAction("Index");
        }



        // GET: Tickets/Delete/5
        /// <summary>
        /// Allows for soft-deletion of a ticket (Admin Only)
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Returns a view of ticket</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        /// <summary>
        /// Confirms deletion of a ticket
        /// </summary>
        /// <param name="id">Ticket Id</param>
        /// <returns>Returns a view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
