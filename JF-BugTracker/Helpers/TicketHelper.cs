using JF_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Helpers
{
    public class TicketHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        public int SetDefaultTicketStatus()
        {
            return db.TicketStatuses.FirstOrDefault(ts => ts.StatusName == "Open").Id;

        }

        public List<Ticket> ListMyTickets()
        {
            var myTickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                case "DemoAdmin":
                    myTickets.AddRange(db.Tickets);
                    break;

                case "ProjectManager":
                case "DemoProjectManager":
                    myTickets.AddRange(db.Projects.SelectMany(p => p.Tickets));
                    break;

                case "Developer":
                case "DemoDeveloper":
                    myTickets.AddRange(db.Tickets.Where(t => t.AssignedToUserId == userId));
                    break;

                case "Submitter":
                case "DemoSubmitter":
                    myTickets.AddRange(db.Tickets.Where(t => t.OwnerUserId == userId));
                    break;
            }

            return myTickets;
        }

    }
}