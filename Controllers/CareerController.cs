using SFA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SFA.Controllers
{
    [Authorize(Roles =RoleName.CanManageSite)]
    public class CareerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Career
        public ActionResult Index()
        {
            var careers = db.Careers.Include(c=>c.Jops).ToList();
            return View(careers);
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Career career)
        {
            db.Careers.Add(career);
            db.SaveChanges();
            return RedirectToAction("Index", "Career");
        }

        public ActionResult EditCareer(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var editCareer = db.Careers.SingleOrDefault(j => j.Id == id);

            if (editCareer == null)
                return HttpNotFound();

            return View(editCareer);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Update(Career career)
        {
            if (!ModelState.IsValid)
            {

                return View("EditLocation", career);
            }

            var careerInForm = db.Careers.Single(j => j.Id == career.Id);

            careerInForm.Name = career.Name;

            db.SaveChanges();

            return RedirectToAction("Index", "Career");
        }

        public void Delete(int id)
        {
            var jops = db.Jops.Where(j => j.CareerId == id);
            db.Jops.RemoveRange(jops);

            var career = db.Careers.Single(l => l.Id == id);
            db.Careers.Remove(career);

            db.SaveChanges();
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}