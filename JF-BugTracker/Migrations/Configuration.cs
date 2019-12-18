namespace JF_BugTracker.Migrations
{
    using JF_BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<JF_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JF_BugTracker.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            #region Roles

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }


            #endregion

            #region Demo Roles
            if (!context.Roles.Any(r => r.Name == "DemoAdmin"))
            {
                roleManager.Create(new IdentityRole { Name = "DemoAdmin" });
            }

            if (!context.Roles.Any(r => r.Name == "DemoProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "DemoProjectManager" });
            }

            if (!context.Roles.Any(r => r.Name == "DemoDevelpoer"))
            {
                roleManager.Create(new IdentityRole { Name = "DemoDeveloper" });
            }

            if (!context.Roles.Any(r => r.Name == "DemoSubmitter"))
            {
                roleManager.Create(new IdentityRole { Name = "DemoSubmitter" });
            }

            #endregion                                                         
            #region Users


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var demoPassword = "Abc!123@";

           

            if (!context.Users.Any(u => u.Email == ""))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Justin.Fisher495@gmail.com",
                    Email = "Justin.Fisher495@gmail.com",
                    FirstName = "Justin",
                    LastName = "Fisher",
                    DisplayName = "Justin Fisher",
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == ""))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@Mailinator.com",
                    Email = "DemoAdmin@Mailinator.com",
                    FirstName = "DemoA",
                    LastName = "Admin",
                    DisplayName = "Demo Admin",
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == ""))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@mailinator.com",
                    Email = "DemoProjectManager@mailinator.com",
                    FirstName = "DemoPM",
                    LastName = "ProjectManager",
                    DisplayName = "Demo ProjectManager",
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == ""))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@Mailinator.com",
                    Email = "DemoDeveloper@Mailinator.com",
                    FirstName = "DemoD",
                    LastName = "Developer",
                    DisplayName = "Demo Developer",
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == ""))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@mailinator.com",
                    Email = "DemoSubmitter@mailinator.com",
                    FirstName = "DemoS",
                    LastName = "Submitter",
                    DisplayName = "Demo Submitter",
                }, demoPassword);
            }



            #endregion


            #region Assign Roles

            var userId = userManager.FindByEmail("justin.fisher495@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("DemoProjectManager@mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            userId = userManager.FindByEmail("DemoDeveloper@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("DemoSubmitter@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            #endregion


            #region Section for loading lookup data

            context.TicketStatuses.AddOrUpdate(
                t => t.StatusName,
                    new TicketStatus { StatusName = "Open", Description = "A new or unassigned ticket." },
                    new TicketStatus { StatusName = "Assigned", Description = "A ticket that has been assigned but not worked on yet." },
                    new TicketStatus { StatusName = "In Progress", Description = "An assigned ticket that is being worked on currently." },
                    new TicketStatus { StatusName = "Resolved", Description = "A ticket that has been completed." },
                    new TicketStatus { StatusName = "Archived", Description = "A ticket that has been archived." }

                );

            context.TicketPriorities.AddOrUpdate(
                t => t.PriorityName,
                    new TicketPriority { PriorityName = "Immediate", Description ="This Priority level requires completion within 2 days." },
                    new TicketPriority { PriorityName = "Highest", Description = "This Priority level requires completion within 1 week." },
                    new TicketPriority { PriorityName = "High", Description = "This Priority level requires completion within 2 weeks." },
                    new TicketPriority { PriorityName = "Medium", Description = "This Priority level requires completion within 3 weeks." },
                    new TicketPriority { PriorityName = "Low", Description = "This Priority level requires completion within 4 weeks." },
                    new TicketPriority { PriorityName = "None", Description = "This Priority level does not require any attention." }
                );

            context.TicketTypes.AddOrUpdate(
                t => t.TypeName,
                    new TicketType { TypeName = "Defect", Description = "A defect in the software has been found." },
                    new TicketType { TypeName = "Feature Request", Description = "A feature has been requested byt the client." },
                    new TicketType { TypeName = "Documentation Request", Description = "A request for documentation of a specific project." },
                    new TicketType { TypeName = "Training Request", Description = "A client has called to request more training." }
                );

            context.Projects.AddOrUpdate(
                p => p.Name,
                    new Project { Name = "JF-Blog", Description = "Blog project on Azure", CreatedDate = DateTime.Now},
                    new Project { Name = "JF-Portfolio", Description = "Porfolio Project", CreatedDate = DateTime.Now},
                    new Project { Name = "JF-BugTracker", Description = "BugTracker Project", CreatedDate = DateTime.Now},
                    new Project { Name = "JF-OOPGame", Description = "Object Object-oriented programming game", CreatedDate = DateTime.Now },
                    new Project { Name = "JF-StringArt", Description = "String Art App", CreatedDate = DateTime.Now}
                );

            context.SaveChanges();

            //var projects = context.Projects.ToList();
            //var tickets = context.TicketStatuses.ToList();
            //foreach (var project in projects)
            //{

            //    for (var i = 0; i < 6; i++)
            //    {

            //        context.Tickets.AddOrUpdate(
            //                    t => t.Title,
            //                    new Ticket
            //                    {
            //                        ProjectId = project.Id,
            //                        TicketTypeId =  ,
            //                        TicketPriorityId = priority.Id,
            //                        TicketStatusId = status.Id,
            //                        OwnerUserId = demosubmitter.Id,
            //                        AssignedToUserId = demodeveloper.Id,
            //                        Title = project.Name + " Demo Ticket " + count,
            //                        Description = "A demo ticket of priority '" + priority.Name + "', type '" + type.Name + "', status '" + status.Name + "'",
            //                        Created = DateTime.Now
            //                    });
            //    }
            //}



            #endregion






        }
    }
}
