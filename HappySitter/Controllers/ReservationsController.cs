using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HappySitter.DAL;
using HappySitter.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace HappySitter.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string googleApiKey = ConfigurationManager.AppSettings["GoogleApiKey"];

        // GET: Reservations
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var upComingReservations =
                db.Reservations.Where(x=> x.CustomerId.Equals(currentUserId))
                    .Where(x => x.ReservationStatus == ReservationStatus.Booked || x.ReservationStatus == ReservationStatus.PaymentWaiting).ToList();
            DateTime currentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var pastReservations =
                db.Reservations.Where(x => x.CustomerId.Equals(currentUserId))
                    .Where(x => x.ServiceDate < currentDate).ToList();
                
            ViewBag.UpComingReservations = upComingReservations;
            ViewBag.PastReservations = pastReservations;

            return View();
        }

        public ActionResult SearchSitter()
        {
            ViewBag.ApiKey = googleApiKey;

            SearchSitterViewModel searchSitterViewModel = new SearchSitterViewModel()
            {
                User = db.Users.Find(User.Identity.GetUserId())
            };

            ViewBag.jsonData = "";



            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Reservation reservation = db.Reservations.Find(id);
            //if (reservation == null)
            //{
            //    return HttpNotFound();
            //}
            return View(searchSitterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchSitter(SearchSitterViewModel model)
        {
            ViewBag.ApiKey = googleApiKey;

            if (ModelState.IsValid)
            {
                Session["ServiceDate"] = model.ServiceDate;
                Session["FromTime"] = model.FromTime;
                Session["ToTime"] = model.ToTime;
                model.SitterListMarker = SqlService.GetAvailableSitters(model);

                var user = db.Users.Find(User.Identity.GetUserId());
                //Adding Customer's home location at the end of the list
                model.SitterListMarker.Add(
                    new GoogleMapMarker()
                    {
                        Id = user.Id,
                        UserName = "MyLocation",
                        Latitude = Convert.ToDouble(user.Latitude),
                        Longitude = Convert.ToDouble(user.Longitude)
                    });

                //model.SetJsonSerializedSitterList();
                ViewBag.jsonData = JsonConvert.SerializeObject(model.SitterListMarker);

                if (model.SitterListMarker.Count == 1)
                {
                    ModelState.AddModelError("", "No Sitters are available. Please change your search conditions.");
                }
            }
            
            return View(model);
        }


        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,SitterId,ServiceDate,FromTime,ToTime,ReservationStatus,RegistrationDateTime,DateLastModified,CancelDateTime")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,SitterId,ServiceDate,FromTime,ToTime,ReservationStatus,RegistrationDateTime,DateLastModified,CancelDateTime")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: Reservations/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReservationConfirmed([Bind(Include = "SitterId,SitterUserName,ServiceDate,FromTime,ToTime,PlatformFee, TotalCost, CostPerHour, Hst")] Reservation reservation)
        {
            reservation.CustomerId = User.Identity.GetUserId();
            reservation.RegistrationDateTime = DateTime.Now;

            //totalcost including HST
            reservation.Cost = reservation.PlatformFee + reservation.TotalCost + reservation.Hst;
           
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return View(reservation);
            }

            return View(reservation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentConfirmed(int? id)
        {

            Reservation reservation = db.Reservations.Find(id);
            reservation.ReservationStatus = ReservationStatus.Booked;

            db.Entry(reservation).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
           return View(reservation);
        }


        // GET: Reservations
        public ActionResult ReservationListForSitter()
        {
            string currentUserId = User.Identity.GetUserId();
            var upComingReservations =
                db.Reservations.Where(x => x.SitterId.Equals(currentUserId))
                    .Where(x => x.ReservationStatus == ReservationStatus.Booked || x.ReservationStatus == ReservationStatus.PaymentWaiting).ToList();
            DateTime currentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var pastReservations =
                db.Reservations.Where(x => x.SitterId.Equals(currentUserId))
                    .Where(x => x.ServiceDate < currentDate).ToList();

            ViewBag.UpComingReservations = upComingReservations;
            ViewBag.PastReservations = pastReservations;

            return View();
        }

        public ActionResult DetailsForSitter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
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
