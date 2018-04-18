using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackerTest.Models
{
    public class UserRolesHelper
    {
        private bool DEBUG = true;
        private bool BEEP = true;

        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        private ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserInRole(string UserId, string Role)
        {
            try
            {
                var result = userManager.IsInRole(UserId, Role);
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

        public bool AddUserToRole(string UserId, string Role)
        {
            try
            {
                var result = userManager.AddToRole(UserId, Role);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                if (DEBUG)
                {
                    if (BEEP)
                        Console.Beep();
                    Console.WriteLine("Exception Found:");
                    Console.WriteLine(ex);
                }
                return false;
            }
        }

        public bool RemoveUserFromRole(string UserId, string Role)
        {
            try
            {
                var result = userManager.RemoveFromRole(UserId, Role);
                return result.Succeeded;
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

        public ICollection<ApplicationUser> UsersInRole(string Role)
        {

            var result = new List<ApplicationUser>();

            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (IsUserInRole(user.Id, Role))
                {
                    result.Add(user);
                }
            }
            return result;
        }

        public ICollection<ApplicationUser> UsersNotRole(string Role)
        {

            var result = new List<ApplicationUser>();

            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (!IsUserInRole(user.Id, Role))
                {
                    result.Add(user);
                }
            }
            return result;
        }

        public ICollection<string> ListUsersRoles(string UserId)
        {
            try
            {
                return userManager.GetRoles(UserId);
            }
            catch (Exception ex)
            {
                if (DEBUG)
                {
                    if (BEEP)
                        Console.Beep();
                    Console.WriteLine("Failed Attempt");
                    Console.WriteLine(ex);
                }
                return null;
            }
        }

        public ICollection<ApplicationUser> ListAllUsers()
        {
            return db.Users.ToList();
        }
    }
}