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
            if(db.Tickets.Find(ticketId).Project.Equals(db.Projects.Find(projectId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateHistories(Ticket editedTicket)
        {
            Ticket currentSDbStateTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == editedTicket.Id);

            List<TicketHistory> histories =   new List<TicketHistory>();

            if(editedTicket.Title != currentSDbStateTicket.Title)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = currentSDbStateTicket.Title,
                    NewVal = editedTicket.Title,
                    Property = "Title"
                });
            }

            if(editedTicket.Description != currentSDbStateTicket.Description)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = currentSDbStateTicket.Description,
                    NewVal = editedTicket.Description,
                    Property = "Description"
                });
            }

            if(editedTicket.TicketStatusId != currentSDbStateTicket.TicketStatusId)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = db.TicketStatus.Find(currentSDbStateTicket.TicketStatusId).Name,
                    NewVal = db.TicketStatus.Find(editedTicket.TicketStatusId).Name,
                    Property = "Ticket Status"
                });
            }

            if(editedTicket.TicketPriorityId != currentSDbStateTicket.TicketPriorityId)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = db.TicketPriorities.Find(currentSDbStateTicket.TicketPriorityId).Name,
                    NewVal = db.TicketPriorities.Find(editedTicket.TicketPriorityId).Name,
                    Property = "Ticket Priority"
                });
            }

            if(editedTicket.TicketTypeId != currentSDbStateTicket.TicketTypeId)
            {
                histories.Add(new TicketHistory()
                {
                    OldVal = db.TicketTypes.Find(currentSDbStateTicket.TicketTypeId).Name,
                    NewVal = db.TicketTypes.Find(editedTicket.TicketTypeId).Name,
                    Property = "Ticket Type"
                });
            }

            if(editedTicket.AssignedToUserId != currentSDbStateTicket.AssignedToUserId)
            {

                histories.Add(new TicketHistory()
                {

                    OldVal = (currentSDbStateTicket.AssignedToUser == null) ? "Unassigned" : db.Users.Find(currentSDbStateTicket.AssignedToUserId).FirstName,
                    NewVal = db.Users.Find(editedTicket.AssignedToUserId).FirstName,
                    Property = "Assigned User"
                });
            }

            string userId = HttpContext.Current.User.Identity.GetUserId();

            for(int i = 0; i < histories.Count; i++)
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