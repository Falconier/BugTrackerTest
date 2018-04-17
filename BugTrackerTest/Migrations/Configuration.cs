namespace BugTrackerTest.Migrations
{
    using BugTrackerTest.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTrackerTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTrackerTest.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();
            //Sets Administrator (Admin)
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Users.Any(u => u.Email == "admin@email.com"))
            {
                userManager.Create(new ApplicationUser { UserName = "admin@email.com", Email = "admin@email.com", FirstName = "Jacob Emory", LastName = "Bullin", FullName = "Falconier" }, "Abc&123!");
            }

            var userId = userManager.FindByEmail("admin@email.com").Id;
            userManager.AddToRole(userId, "Admin");

            //Sets Moderator (Mod)
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }

            if (!context.Users.Any(u => u.Email == "pm@email.com"))
            {
                userManager.Create(new ApplicationUser { UserName = "pm@email.com", Email = "pm@email.com", FirstName = "Pro", LastName = "Ject", FullName = "Pro Ject" }, "password");
            }

            var userId2 = userManager.FindByEmail("pm@email.com").Id;
            userManager.AddToRole(userId2, "Project Manager");

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Users.Any(u => u.Email == "dev@email.com"))
            {
                userManager.Create(new ApplicationUser { UserName = "dev@email.com", Email = "dev@email.com", FirstName = "Dev", LastName = "Elop", FullName = "Dev Elop" }, "password");
            }

            var userId3 = userManager.FindByEmail("dev@email.com").Id;
            userManager.AddToRole(userId3, "Developer");

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            if (!context.Users.Any(u => u.Email == "sub@email.com"))
            {
                userManager.Create(new ApplicationUser { UserName = "sub@email.com", Email = "sub@email.com", FirstName = "Sub", LastName = "Mitter", FullName = "Sub Mitter" }, "password");
            }

            var userId4 = userManager.FindByEmail("sub@email.com").Id;
            userManager.AddToRole(userId4, "Submitter");

            context.Projects.AddOrUpdate(
                p => p.Name,
                new Project
                {

                    Name = "Project One",
                    Description = "This is the first project",
                    Created = DateTime.Now
                },
                new Project
                {

                    Name = "Project Two",
                    Description = "This is the second project",
                    Created = DateTime.Now
                },
                new Project
                {

                    Name = "Project Three",
                    Description = "This is the thrid project",
                    Created = DateTime.Now
                },
                new Project
                {

                    Name = "Project Four",
                    Description = "This is the fourth project",
                    Created = DateTime.Now
                });

            if (!context.TicketPriorities.Any(u => u.Name == "Low"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Low" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "Medium"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Medium" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "High"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "High" }); }

            if (!context.TicketPriorities.Any(u => u.Name == "Urgent"))
            { context.TicketPriorities.Add(new TicketPriority { Name = "Urgent" }); }

            if (!context.TicketTypes.Any(u => u.Name == "Production Fix"))
            { context.TicketTypes.Add(new TicketType { Name = "Production Fix" }); }

            if (!context.TicketTypes.Any(u => u.Name == "Project Task"))
            { context.TicketTypes.Add(new TicketType { Name = "Project Task" }); }

            if (!context.TicketTypes.Any(u => u.Name == "Software Update"))
            { context.TicketTypes.Add(new TicketType { Name = "Software Update" }); }

            if (!context.TicketStatus.Any(u => u.Name == "New"))
            { context.TicketStatus.Add(new TicketStatus { Name = "New" }); }

            if (!context.TicketStatus.Any(u => u.Name == "In Development"))
            { context.TicketStatus.Add(new TicketStatus { Name = "In Development" }); }

            if (!context.TicketStatus.Any(u => u.Name == "Completed"))
            { context.TicketStatus.Add(new TicketStatus { Name = "Completed" });
            }
        } 
            //context.Tickets.AddOrUpdate(
            //    t => t.Title,
            //    new Ticket
            //    {
            //        Title = "Tkt Two, Prj Two",
            //        Description = "Test tkt (seeded)",
            //        Created = DateTime.Now
            //    },
            //    new Ticket
            //    {
            //        Title = "Tkt Three, Prj Three",
            //        Description = "Test tkt (seeded)",
            //        Created = DateTime.Now
            //    },
            //    new Ticket
            //    {
            //        Title = "Tkt Four, Prj Four",
            //        Description = "Test tkt (seeded)",
            //        Created = DateTime.Now
            //    }

                
        
    }
}

