using JF_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Helpers
{

    public class NotifictaionHelper
    {

        private static ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications(Ticket oldTicket, Ticket newTicket, string callbackUrl)
        {
            var ticketAssigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUserId != null;
            var ticketUnassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;
            var ticketReassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId != null && oldTicket.AssignedToUserId != newTicket.AssignedToUserId;

            if (ticketAssigned)
            {
                AddAssignmentNotification(oldTicket, newTicket, callbackUrl);
            }
            else if (ticketUnassigned)
            {
                AddUnassignmentNotification(oldTicket, newTicket, callbackUrl);
            }
            else if (ticketReassigned)
            {
                AddAssignmentNotification(oldTicket, newTicket, callbackUrl);
                AddUnassignmentNotification(oldTicket, newTicket, callbackUrl);
            }
        }

        private void AddAssignmentNotification(Ticket oldTicket, Ticket newTicket, string callbackUrl)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                RecipientId = newTicket.AssignedToUserId,
                CreatedDate = DateTime.Now,
                NotificationBody = $"You have been assigned to Ticket Id: {newTicket.Id} on Project {newTicket.Project.Name}."
            };

            EmailHelper.SendEmail(new Helpers.EmailService()
            {
                Title = "Jägerwanzen Notification",
                Body = $"You have been assigned to Ticket: {newTicket.Title} on Project: {newTicket.Project.Name}. <br/>Please click the following link to view the details <a href={ callbackUrl }>NEW TICKET</a>",
                Reciepents = new List<string>() { newTicket.AssignedToUser.Email }
            });

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        private void AddUnassignmentNotification(Ticket oldTicket, Ticket newTicket, string callbackUrl)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                RecipientId = oldTicket.AssignedToUserId,
                CreatedDate = DateTime.Now,
                NotificationBody = $"You have been unassigned from Ticket: {newTicket.Id} on Project: {newTicket.Project.Name}."
            };
            
            EmailHelper.SendEmail(new Helpers.EmailService()
            {
                Title = "Jägerwanzen Notification",
                Body = $"You have been unassigned to Ticket: {newTicket.Title} on Project: {newTicket.Project.Name}.",
                Reciepents = new List<string>() { newTicket.AssignedToUser.Email}
            });

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
        
        public static List<TicketNotification> GetUnreadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Include("Sender").Where(t => t.RecipientId == currentUserId && !t.IsRead).ToList();
        }
    }
}