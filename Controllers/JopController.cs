using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SFA.Models;
using SFA.ViewModels;
using System.Data.Entity;
namespace SFA.Controllers
{
    public class JopController : Controller
    {
        // GET: Jop
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult JopInfo(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var Jop = db.Jops.Include(j => j.Location).Include(j => j.Career).Include(j => j.Experience).SingleOrDefault(j => j.Id == id);

            if (Jop == null)
                return HttpNotFound();

            return View(Jop);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageSite)]
        public ActionResult Create(Jop jop)
        {
            jop.AnnouncedDate = DateTime.Now;
            db.Jops.Add(jop);
            db.SaveChanges();
            return RedirectToAction("JopIn", "Home");
        }
        [Authorize(Roles =RoleName.CanManageSite)]
        public ActionResult EditJop(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var editJop = db.Jops.SingleOrDefault(j => j.Id == id);

            if (editJop == null)
                return HttpNotFound();

            var viewModel = new JopFormsViewModel
            {
                jop = editJop,
                Locations = db.Locations.ToList(),
                Careers = db.Careers.ToList(),
                Experiences = db.Experiences.ToList()
            };

            return View(viewModel);
        }
        [ValidateAntiForgeryToken]
        [Authorize(Roles =RoleName.CanManageSite)]
        public ActionResult Update(Jop jop)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new JopFormsViewModel
                {
                    jop = jop,
                    Locations = db.Locations.ToList(),
                    Careers = db.Careers.ToList(),
                    Experiences = db.Experiences.ToList()
                };
                return View("EditJop", viewModel);
            }

            var jopInForm = db.Jops.Single(j => j.Id == jop.Id);

            jopInForm.Title = jop.Title;
            jopInForm.CompanyName = jop.CompanyName;
            jopInForm.Email = jop.Email;
            jopInForm.Watsapp = jop.Watsapp;
            jopInForm.Salary = jop.Salary;
            jopInForm.CareerId = jop.CareerId;
            jopInForm.LocationId = jop.LocationId;
            jopInForm.ExperienceId = jop.ExperienceId;
            jopInForm.JopType = jop.JopType;
            jopInForm.Details = jop.Details;
            jopInForm.LastUpdated = DateTime.Now;

            db.SaveChanges();

            return RedirectToAction("JopIn", "Home");
        }

        // Delete
        [Authorize(Roles = RoleName.CanManageSite)]
            public void Delete(int id)
        {
            var jop = db.Jops.Single(j => j.Id == id);

            db.Jops.Remove(jop);
            db.SaveChanges();

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

}
