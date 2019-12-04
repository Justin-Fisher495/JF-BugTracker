using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Models.ViewModels
{
    public abstract class BaseViewModel
    {
        public Ticket Ticket { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Project> Projects { get; set; }


    }
}