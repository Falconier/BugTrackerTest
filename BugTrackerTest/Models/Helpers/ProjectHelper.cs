using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BugTrackerTest.Models
{
    public class ProjectHelper
    {
        private bool DEBUG = true;
        private bool BEEP = false;

        private ApplicationDbContext db = new ApplicationDbContext();

        public ICollection<Project> ListAllProjects()
        {
            return db.Projects.ToList();
        }

        public bool IsUserOnProject(string userId, int projectId)
        {
            try
            {
                var usr = db.Users.Find(userId);
                var result = db.Projects.Find(projectId).Users.Contains(usr);
                return result;
            }
            catch (Exception ex)
            {
                if (DEBUG)
                {
                    if (BEEP)
                        Console.Beep();
                    Console.WriteLine(ex);
                }
                return false;
            }
        }

        public ICollection<ApplicationUser> ListUsersInProject(int projectId)
        {
            return db.Projects.Find(projectId).Users.ToList();
        }

        public ICollection<Project> ListUserProjects(string userId)
        {
            return db.Users.Find(userId).Projects.ToList();
        }

        public ICollection<ApplicationUser> ListUsersNotInProject(int projectId)
        {
            List<ApplicationUser> usrNotInProject = new List<ApplicationUser>();
            List<ApplicationUser> usrs = db.Users.ToList();

            foreach (var u in usrs)
            {
                if (!IsUserOnProject(u.Id, projectId))
                {
                    usrNotInProject.Add(u);
                }
            }
            return usrNotInProject;
        }

        public Exception AddUserToProject(string userId, int projectId)
        {
            try
            {
                var prj = db.Projects.Find(projectId);
                var usr = db.Users.Find(userId);
                prj.Users.Add(usr);
                db.SaveChanges();
                if (DEBUG)
                    Console.WriteLine("Added User");
                return null;
            }
            catch (Exception ex)
            {
                if (DEBUG)
                {
                    if (BEEP)
                        Console.Beep();
                    Console.WriteLine(ex);
                }
                return ex;
            }
        }

        public Exception RemoveUserFromProject(string userId, int projectId)
        {
            try
            {
                var prj = db.Projects.Find(projectId);
                var usr = db.Users.Find(userId);
                prj.Users.Remove(usr);
                db.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                if (DEBUG)
                {
                    if (BEEP)
                        Console.Beep();
                    Console.WriteLine(ex);
                }
                return ex;
            }
        }

        public ICollection<Ticket> ListTickets(int projectId)
        {
            return db.Projects.Find(projectId).Tickets.ToList();
        }

        public int GetNumberTickets(int projectId)
        {
            return db.Projects.Find(projectId).Tickets.Count;
        }

        public Ticket PullNewestTicket(int projectId)
        {
            List<Ticket> ticketList = db.Projects.Find(projectId).Tickets.OrderByDescending(tkt => tkt.Created).ToList();

            return ticketList.FirstOrDefault();
        }

        public void AddProjectManager(string userId, int projectId)
        {
            var prj = db.Projects.Find(projectId);
            prj.Manager = userId;
            db.SaveChanges();
        }
    }
}