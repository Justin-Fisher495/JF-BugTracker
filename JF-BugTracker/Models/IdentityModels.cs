using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JF_BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Projects = new HashSet<Project>();
            //Notifications = new HashSet<TicketNotification>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
        }

        [DisplayName("First Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 1 and 50 Characters.")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 50 Characters.")]
        public string LastName { get; set; }
        [Display(Name = "Display Name")]
        [StringLength(50, MinimumLength = 1,ErrorMessage = "Display name must be between 1 and 50 Characters.")]
        public string DisplayName { get; set; }

        public string AvatarPath { get; set; }


        //public virtual ICollection<TicketNotification> Notifications { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.Ticket> Tickets { get; set; }        

        public System.Data.Entity.DbSet<JF_BugTracker.Models.TicketPriority> TicketPriorities { get; set; }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.TicketStatus> TicketStatuses { get; set; }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.TicketType> TicketTypes { get; set; }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.TicketAttachment> TicketAttachments { get; set; }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.TicketComment> TicketComments { get; set; }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.TicketHistory> TicketHistories { get; set; }

        public System.Data.Entity.DbSet<JF_BugTracker.Models.TicketNotification> TicketNotifications { get; set; }
    }
}