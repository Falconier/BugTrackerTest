using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace BugTrackerTest.Models.Extensions
{
    public static class Extension
    {
        public static string GetFullName(this IIdentity user)
        {
            var ClaimsUser = (ClaimsIdentity)user;
            var claim = ClaimsUser.Claims.FirstOrDefault(c => c.Type == "Name");
            if (claim != null)
            {
                return claim.Value;
            }
            else
            {
                return null;
            }
        }

        //public static ApplicationUser GetApplicationUser(IIdentity identity)
        //{
        //    if(identity.IsAuthenticated)
        //    {
        //        using(var db = new AppContext())
        //        {
        //            var userManager = new ApplicationUserManager(new ApplicationUserStore(db));
        //            return userManager.FindByName(identity.Name);
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}