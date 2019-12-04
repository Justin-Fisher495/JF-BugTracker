using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UpdaterId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }       
        public DateTime Changed { get; set; }


        //Navigation
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser Updater { get; set; }
    }
}