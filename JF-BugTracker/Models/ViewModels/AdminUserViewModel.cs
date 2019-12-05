using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JF_BugTracker.Models.ViewModels
{
    public class AdminUserViewModel : BaseViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser User { get; set; }
        public MultiSelectList Roles { get; set; }
        public string SelectRoles { get; set; }

        public AdminUserViewModel()
        {
            Tickets = db.Tickets.ToList();
            Projects = db.Projects.ToList();           
        }
    }
}

