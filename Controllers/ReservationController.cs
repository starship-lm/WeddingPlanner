using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace WeddingPlanner.Controllers
{
    public class ReservationController : Controller
    {
        private Users loggedInUser {
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));}
        }
        private wpContext dbContext;
        public ReservationController(wpContext context)
        {
            dbContext = context;
        }
        [HttpGet("UnRSVP/{weddingId}")]
        public IActionResult UnRSVP(int weddingId)
        {
            var RSVPToDelete = dbContext.Reservations.FirstOrDefault(u => u.UserId == loggedInUser.UserId && u.WeddingId == weddingId);
            if (RSVPToDelete == null)
                return RedirectToAction("Dashboard", "Wedding");

            dbContext.Reservations.Remove(RSVPToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard", "Wedding");
        }
        [HttpGet("RSVP/{weddingId}")]
        public IActionResult RSVP(int weddingId)
        {
            Reservations newRSVP = new Reservations()
            {
                WeddingId = weddingId,
                UserId = loggedInUser.UserId
            };
     
            dbContext.Reservations.Add(newRSVP);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard", "Wedding");
        }

    }
}