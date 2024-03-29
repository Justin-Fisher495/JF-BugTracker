﻿using System.Collections.Generic;

namespace JF_BugTracker.Models
{
    public class TicketPriority
    {
        public TicketPriority()
        {
            Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public string PriorityName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}