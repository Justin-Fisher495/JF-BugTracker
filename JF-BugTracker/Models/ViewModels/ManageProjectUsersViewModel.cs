using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JF_BugTracker.Models.ViewModels
{
    public class ManageProjectUsersViewModel
    {
        public string UserDisplayName { get; set; }
        public ICollection<Project> Projects { get; set; }
        
    }
}