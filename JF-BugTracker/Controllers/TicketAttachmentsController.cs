﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JF_BugTracker.Helpers;
using JF_BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace JF_BugTracker.Controllers
{
    public class TicketAttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketAttachments
        public ActionResult Index()
        {
            var ticketAttachments = db.TicketAttachments.Include(t => t.Owner).Include(t => t.Ticket);
            return View(ticketAttachments.ToList());
        }

        // GET: TicketAttachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Create
        public ActionResult Create(int id)
        {
            var attachment = new TicketAttachment
            {
                TicketId = id
            };
            return View(attachment);
        }

        // POST: TicketAttachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,Body")] TicketAttachment ticketAttachment, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                ticketAttachment.CreatedDate = DateTime.Now;
                ticketAttachment.OwnerId = User.Identity.GetUserId();

                if (file != null)
                {
                    if (FileUploadValidator.IsWebFriendlyFile(file) || ImageUploadValidator.IsWebFriendlyImage(file))
                    {

                        var fileName = Path.GetFileName(file.FileName);
                        var justFileName = Path.GetFileNameWithoutExtension(fileName);
                        justFileName = StringUtilities.URLFriendly(justFileName);
                        fileName = $"{justFileName}_{DateTime.Now.Ticks}{Path.GetExtension(fileName)}";

                        file.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                        ticketAttachment.FilePath = "/Uploads/" + fileName;
                        ticketAttachment.CreatedDate = DateTime.Now;
                        ticketAttachment.OwnerId = User.Identity.GetUserId();
                        db.TicketAttachments.Add(ticketAttachment);
                        db.SaveChanges();
                    }
                }


                return RedirectToAction("Index","TicketAttachments",  new { id = ticketAttachment.TicketId });
            }

            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", ticketAttachment.OwnerId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "OwnerUserId", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,FilePath,Body,OwnerId,CreatedDate")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketAttachment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", ticketAttachment.OwnerId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "OwnerUserId", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            db.TicketAttachments.Remove(ticketAttachment);
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
