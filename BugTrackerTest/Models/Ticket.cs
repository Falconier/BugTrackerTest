﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTrackerTest.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Updated { get; set; }

        public bool isResolved { get; set; }

        //Required when seeded
        public int ProjectId { get; set; }

        public int? TicketTypeId { get; set; }

        public int? TicketPriorityId { get; set; }

        public int? TicketStatusId { get; set; }

        public string OwnerUserId { get; set; }

        public string AssignedToUserId { get; set; }

        public virtual TicketType TicketType { get; set; }
        public virtual TicketPriority Priority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<TicketComment> Comments { get; set; }
        public virtual ICollection<TicketAttachment> Attachments { get; set; }

        public virtual ICollection<TicketHistory> Histories { get; set; }

        public virtual ICollection<TicketNotification> Notifications { get; set; }

        public virtual ApplicationUser OwnerUser { get; set; }

        public virtual ApplicationUser AssignedToUser { get; set; }

        public Ticket()
        {
            Comments = new HashSet<TicketComment>();
            Attachments = new HashSet<TicketAttachment>();
            Histories = new HashSet<TicketHistory>();
            Notifications = new HashSet<TicketNotification>();
        }
    }
}