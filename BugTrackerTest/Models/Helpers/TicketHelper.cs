using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models
{
    public class TicketHelper
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ICollection<Ticket> ListAllTickets()
        {
            return db.Tickets.ToList();
        }

        public bool IsTicketInProject(int ticketId, int projectId)
        {
            if (db.Tickets.Find(ticketId).Project.Equals(db.Projects.Find(projectId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICollection<Ticket> GetAssignedTickets(string userId)
        {
            ICollection<Ticket> tickets = db.Tickets.ToList();
            tickets.Clear();
            foreach (var tkt in db.Tickets)
            {
                if (tkt.AssignedToUserId != null)
                {
                    if (tkt.AssignedToUserId.Equals(userId))
                    {
                        tickets.Add(tkt);
                    }
                }
            }

            return tickets.OrderByDescending(t => t.ProjectId).ToList();
        }

        public ICollection<Project> GetAssignedProjects(ICollection<Ticket> tickets)
        {
            ICollection<Project> projects = db.Projects.ToList();
            projects.Clear();

            foreach( var tkt in tickets)
            {
                if(!projects.Contains(tkt.Project))
                {
                    projects.Add(tkt.Project);
                }
            }
            return projects;
        }

        public ICollection<Ticket> GetOwnedTickets(string userId)
        {
            ICollection<Ticket> tickets = db.Tickets.ToList();
            tickets.Clear();
            foreach (var tkt in db.Tickets)
            {
                if(tkt.OwnerUserId != null)
                {
                    if(tkt.OwnerUserId.Equals(userId))
                    {
                        tickets.Add(tkt);
                    }
                }
            }

            return tickets;
        }

        /// <summary>
        /// Returns the Project Id of the project the ticket is on
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public int On(int ticketId)
        {
            return db.Tickets.Find(ticketId).ProjectId;
        }

        public void CreateHistories(Ticket editedTicket)
        {
            Ticket currentSDbStateTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == editedTicket.Id);

            List<TicketHistory> histories = new List<TicketHistory>();

            if (editedTicket.Title != currentSDbStateTicket.Title)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = currentSDbStateTicket.Title,
                    NewVal = editedTicket.Title,
                    Property = "Title"
                });
            }

            if (editedTicket.Description != currentSDbStateTicket.Description)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = currentSDbStateTicket.Description,
                    NewVal = editedTicket.Description,
                    Property = "Description"
                });
            }

            if (editedTicket.TicketStatusId != currentSDbStateTicket.TicketStatusId)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = db.TicketStatus.Find(currentSDbStateTicket.TicketStatusId).Name,
                    NewVal = db.TicketStatus.Find(editedTicket.TicketStatusId).Name,
                    Property = "Ticket Status"
                });
            }

            if (editedTicket.TicketPriorityId != currentSDbStateTicket.TicketPriorityId)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = db.TicketPriorities.Find(currentSDbStateTicket.TicketPriorityId).Name,
                    NewVal = db.TicketPriorities.Find(editedTicket.TicketPriorityId).Name,
                    Property = "Ticket Priority"
                });
            }

            if (editedTicket.TicketTypeId != currentSDbStateTicket.TicketTypeId)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = db.TicketTypes.Find(currentSDbStateTicket.TicketTypeId).Name,
                    NewVal = db.TicketTypes.Find(editedTicket.TicketTypeId).Name,
                    Property = "Ticket Type"
                });
            }

            if (editedTicket.AssignedToUserId != currentSDbStateTicket.AssignedToUserId)
            {

                histories.Add(new TicketHistory()
                {

                    OldVal = (currentSDbStateTicket.AssignedToUser == null) ? "Unassigned" : db.Users.Find(currentSDbStateTicket.AssignedToUserId).FirstName,
                    NewVal = db.Users.Find(editedTicket.AssignedToUserId).FirstName,
                    Property = "Assigned User"
                });
            }

            string userId = HttpContext.Current.User.Identity.GetUserId();

            for (int i = 0; i < histories.Count; i++)
            {
                histories[i].UserId = userId;
                histories[i].Changed = DateTimeOffset.Now;
                histories[i].TicketId = editedTicket.Id;

                db.TicketHistories.Add(histories[i]);
            }

            db.SaveChanges();
        }
    }
}