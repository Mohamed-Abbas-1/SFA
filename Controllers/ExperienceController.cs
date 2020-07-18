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
    public class ExperienceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Experience
        public ActionResult Index()
        {
            var experiences = db.Experiences.Include(e => e.Jops).ToList();
            return View(experiences);
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Experience experience)
        {
            experience.TotalName = experience.From + " " + experience.FromMonthOrYear + " - " + experience.To + " " + experience.ToMonthOrYear;
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index", "Experience");
        }

        public ActionResult EditExperience(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var editExperience = db.Experiences.SingleOrDefault(j => j.Id == id);

            if (editExperience == null)
                return HttpNotFound();

            return View(editExperience);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Update(Experience experience)
        {
            if (!ModelState.IsValid)
            {

                return View("EditExperience", experience);
            }

            var experienceInForm = db.Experiences.Single(j => j.Id == experience.Id);

            experience.TotalName = experience.From + " " + experience.FromMonthOrYear + " - " + experience.To + " " + experience.ToMonthOrYear;

            experienceInForm.From = experience.From;
            experienceInForm.FromMonthOrYear = experience.FromMonthOrYear;
            experienceInForm.To = experience.To;
            experienceInForm.ToMonthOrYear = experience.ToMonthOrYear;
            experienceInForm.TotalName = experience.TotalName;

            db.SaveChanges();

            return RedirectToAction("Index", "Experience");
        }

        public void Delete(int id)
        {
            var jops = db.Jops.Where(j => j.ExperienceId == id);
            db.Jops.RemoveRange(jops);
            
            var experience = db.Experiences.Single(l => l.Id == id);
            db.Experiences.Remove(experience);

            db.SaveChanges();
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}