using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Models.ViewModels
{
    public class ProjectDetailViewModel : BaseViewModel
    {
        


        public IEnumerable<TicketHistory> ticketHistories { get; set; }
        public IEnumerable<TicketAttachment> ticketAttachments { get; set; }
        public Project Project { get; set; }
        public int TicketPriority { get; set; }
    };
}