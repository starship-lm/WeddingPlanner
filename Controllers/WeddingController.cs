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
    public class WeddingController : Controller
    {
        private Users loggedInUser {
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));}
        }
        private wpContext dbContext;
        public WeddingController(wpContext context)
        {
            dbContext = context;
        }
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            if(loggedInUser == null)
                return RedirectToAction("Index", "Home");

            var AllWeddings = dbContext.Weddings
                .Include(wedding => wedding.Reservations)
                .OrderBy(d => d.Date)
                .ToList();

            ViewBag.LoggedInUser = loggedInUser.UserId;

            return View(AllWeddings);
        }
        [HttpGet("Delete/{weddingId}")]
        public IActionResult Delete(int weddingId)
        {
            var weddingToDelete = dbContext.Weddings.FirstOrDefault(u => u.UserId == loggedInUser.UserId && u.WeddingId == weddingId);

            if (weddingToDelete == null)
                return RedirectToAction("Dashboard");

            dbContext.Weddings.Remove(weddingToDelete);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");   
        }
        [HttpGet("Create")]
        public IActionResult Create()
        {
            if(loggedInUser == null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost("CreateWedding")]
        public IActionResult CreateWedding(Weddings newWedding)
        {
            if(loggedInUser == null)
                return RedirectToAction("Index", "Home");
            if(ModelState.IsValid)
            {
                newWedding.UserId = loggedInUser.UserId;

                dbContext.Weddings.Add(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard", "Wedding");
            }
            return View("Create");
        }
        [HttpGet("Show/{weddingId}")]
        public IActionResult Show(int weddingId)
        {
            if(loggedInUser == null)
                return RedirectToAction("Index", "Home");

            var wedding = dbContext.Weddings
                .Include(w => w.Reservations)
                .ThenInclude(r => r.Guest)
                .FirstOrDefault(w => w.WeddingId == weddingId);


            return View(wedding);
        }

    }
}