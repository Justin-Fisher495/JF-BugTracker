using JF_BugTracker.Helpers;
using JF_BugTracker.Models;
using JF_BugTracker.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JF_BugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();
        private ProjectsHelper projectsHelper = new ProjectsHelper();
        // GET: Admin
        //[Authorize(Roles = "Admin")]
        public ActionResult ManageRoles()
        {
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "DisplayName");
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");

            var users = new List<ManageRolesViewModel>();
            foreach(var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    UserName = $"{user.LastName}, {user.FirstName}",
                    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault()
                });
            }
            return View(users);
        }
        //POST: Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageRoles(List<string> userIds, string role)
        {
            foreach (var userId in userIds)
            {
                var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                if (userRole != null)
                {
                    roleHelper.RemoveUserFromRole(userId, userRole);
                }
            }

            if (!string.IsNullOrEmpty(role))
            {
                foreach (var userId in userIds)
                {
                    roleHelper.AddUserToRole(userId, role);
                }
            }
            return RedirectToAction("ManageRoles", "Admin");
        }

        //[HttpPost]
        //public ActionResult SetProjectPM(string projectManager, int id)
        //{
        //    projectsHelper.AddPMToProject(projectManager, id);
        //    RedirectToAction("Details", "Projets", new { id = id });
        //}

        //GET: EditUSer
        public ActionResult EditUser(string id)
        {
            var user = db.Users.Find(id);
            AdminUserViewModel AdminModel = new AdminUserViewModel();            
            RoleHelper helper = new RoleHelper();
            var selected = helper.ListUserRoles(id);
            AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
            AdminModel.User = user;
           
            return View(AdminModel);
        }

        //POST: EditUser
        public ActionResult EditUser(AdminUserViewModel model)
        {
            var user = db.Users.Find(model.User.Id);
            RoleHelper helper = new RoleHelper();
            foreach (var rolermv in db.Roles.Select(r => r.Id).ToList())
            {
                helper.RemoveUserFromRole(user.Id, rolermv);
            }

            foreach (var roleadd in db.Roles.Select(r => r.Id).ToList())
            {
                helper.AddUserToRole(user.Id, roleadd);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ManageProjects()
        {
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "DisplayName", db.Projects.First().Users );
            ViewBag.Project = new SelectList(db.Projects, "Id", "Name", db.Projects.First());
            ViewBag.ProjectManager = new SelectList(roleHelper.UserIsInRole("ProjectManager"), "Id", "DisplayName");

            var users = new List<ManageProjectUsersViewModel>();
            foreach (var user in db.Users.ToList())
            {
                users.Add(new ManageProjectUsersViewModel
                {
                    UserDisplayName = $"{user.LastName}, {user.FirstName}",
                    Projects = projectsHelper.ListUserProjects(user.Id)
                });
            }
            return View(users);
        }
        //POST: Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjects(List<string> userIds, int project, string projectManager)
        {
            foreach (var user in db.Users)
            {
                projectsHelper.RemoveUserFromProject(user.Id, project);
            }

            foreach (var userId in userIds)
                {
                projectsHelper.AddUserToProject(userId, project); 
                }

            projectsHelper.AddPMToProject(projectManager, project);

            return RedirectToAction("ManageProjects", "Admin");
        }
    }
}