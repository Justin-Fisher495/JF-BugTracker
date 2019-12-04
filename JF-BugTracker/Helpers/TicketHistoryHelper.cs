using JF_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Helpers
{
    public class TicketHistoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void RecordHistoricalChanges(Ticket oldTicket, Ticket newTicket)
        {

            newTicket.Updated = DateTime.Now;

            if(oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketStatusId",
                    OldValue = oldTicket.TicketStatus.StatusName,
                    NewValue = newTicket.TicketStatus.StatusName,
                    Changed = newTicket.Updated.Value,
                    TicketId = newTicket.Id,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),

                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "TicketPriorityId",
                    OldValue = oldTicket.TicketPriority.PriorityName,
                    NewValue = newTicket.TicketPriority.PriorityName,
                    Changed = newTicket.Updated.Value,
                    TicketId = newTicket.Id,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),

                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.AssignedToUserId != newTicket.AssignedToUserId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "AssignedToUserId",
                    OldValue = oldTicket.AssignedToUser == null ? "UnAssigned": oldTicket.AssignedToUser.DisplayName,
                    NewValue = newTicket.AssignedToUser == null ? "UnAssigned" : newTicket.AssignedToUser.DisplayName,
                    Changed = newTicket.Updated.Value,
                    TicketId = newTicket.Id,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),

                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.Description != newTicket.Description)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "Description",
                    OldValue = oldTicket.Description,
                    NewValue = newTicket.Description,
                    Changed = newTicket.Updated.Value,
                    TicketId = newTicket.Id,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),

                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.Title != newTicket.Title)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Property = "Title",
                    OldValue = oldTicket.Title,
                    NewValue = newTicket.Title,
                    Changed = newTicket.Updated.Value,
                    TicketId = newTicket.Id,
                    UpdaterId = HttpContext.Current.User.Identity.GetUserId(),

                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            db.SaveChanges();
        }
    }
}