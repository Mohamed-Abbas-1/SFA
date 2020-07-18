using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SFA.Models;
namespace SFA.Controllers
{
    [Authorize(Roles =RoleName.CanManageSite)]
    public class LocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Location
        public ActionResult Index()
        {
            var location = db.Locations.Include(l=>l.Jops).ToList();
            return View(location);
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
            return RedirectToAction("Index", "Location");
        }

        public ActionResult EditLocation(int? id)
        {
            if(id == null)
                return HttpNotFound();

            var editLocation = db.Locations.SingleOrDefault(j => j.Id == id);

            if (editLocation == null)
                return HttpNotFound();

            return View(editLocation);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Update(Location location)
        {
            if (!ModelState.IsValid)
            {
                
                return View("EditLocation", location);
            }

            var locationInForm = db.Locations.Single(j => j.Id == location.Id);

            locationInForm.Name = location.Name;

            db.SaveChanges();

            return RedirectToAction("Index", "Location");
        }

        public void Delete(int id)
        {
            var jops = db.Jops.Where(j => j.LocationId == id);
            db.Jops.RemoveRange(jops);

            var location = db.Locations.Single(l => l.Id == id);
            db.Locations.Remove(location);

            db.SaveChanges();
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);    
        }
    }
}

