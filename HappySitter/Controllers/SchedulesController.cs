﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using HappySitter.Models;
using Microsoft.AspNet.Identity;

namespace HappySitter.Controllers
{
    public class SchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Schedules
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            return View(db.Schedules.Where(x=> x.UserId.Equals(userId))
                .OrderBy(x => x.DayOfWeek)
                .ThenBy(x => x.FromTime).ToList());
        }
        

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.DaysOfWeek = new SelectList(Schedule.GetAllDaysOfWeek(), "DayId", "DayName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DayOfWeek,FromTime,ToTime")] Schedule schedule)
        {
            //TimeSpan duration = DateTime.Parse(schedule.ToTime).Subtract(DateTime.Parse(schedule.FromTime));
            
            int durationOfHours = (schedule.ToTime - schedule.FromTime).Hours;
            if (durationOfHours < 1)
            {
                ModelState.AddModelError(string.Empty, "Working time must be over 1 hour at least.");
            }

            if (ModelState.IsValid)
            {
                schedule.UserId = User.Identity.GetUserId();
                schedule.DateLastModified = DateTime.Now;

                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DayOfWeek,FromTime,ToTime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.DateLastModified = DateTime.Now;

                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
