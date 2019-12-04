using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JF_BugTracker.Helpers;
using JF_BugTracker.Models;
using JF_BugTracker.Models.ViewModels;
using Microsoft.AspNet.Identity;


namespace JF_BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projectHelper = new ProjectsHelper();
        private RoleHelper roleHelper = new RoleHelper();
        // GET: Projects
        public ActionResult Index()
        {
            if (roleHelper.IsUserInRole(User.Identity.GetUserId(), "Admin"))
            {
                var viewModel = new ProjectIndexViewModel()
                {
                    Projects = db.Projects.ToList()
                };
                return View(viewModel);
            }
            else
            {
                var viewModel = new ProjectIndexViewModel()
                {
                    Projects = projectHelper.ListUserProjects(User.Identity.GetUserId()).ToList()

                };
                return View(viewModel);
            }

        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        //[Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CreatorId,CreatedDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreatedDate = DateTime.Now;                
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        //[Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatorId,CreatedDate,UpdatedDate")] Project project)
        {
            if (ModelState.IsValid)
            {

                var existingProject = db.Projects.Find(project.Id);
                existingProject.Name = project.Name;
                existingProject.Description = project.Description;
                existingProject.UpdatedDate = DateTime.Now;
                db.Entry(existingProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        //[Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
