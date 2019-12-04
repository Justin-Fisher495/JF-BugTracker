using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreatedDate { get; set; }

        //Navigation
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}