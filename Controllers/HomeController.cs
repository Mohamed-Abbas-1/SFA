using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using PagedList;
using SFA.Models;
using SFA.ViewModels;
namespace SFA.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (TempData.ContainsKey("shortMessage")) 
            {
                ViewBag.Message = TempData["shortMessage"].ToString(); 
                return View();                    
            }
            else
            {
                return View();
            }
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (TempData.ContainsKey("shortMessage"))
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
                return View();
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult JopIn( int? page, int? location, int? career , int? experience,string jopType, string datePosted , string serach, string reset)
        {
            var allJops = db.Jops.Include(j => j.Location).Include(j => j.Career).OrderByDescending(j => j.AnnouncedDate).ToList();
            // reset the filtering
            if (reset == "true")
            {
                TempData.Clear();
            }
            // filter by location
            if (location != null)
            {
                if (TempData.ContainsKey("location"))
                {
                    TempData["location"] = location;
                }
                else
                {
                    TempData.Add("location", location);
                }
            }
            
            if (TempData["location"] != null)
            {
                TempData.Keep("location");
                ViewBag.location = TempData["location"];

                var stringLocation = db.Locations.Find(ViewBag.location);
                ViewBag.stringLocation = stringLocation.Name;

                allJops = allJops.Where(j => j.LocationId == ViewBag.location).ToList();
            }

            // filter by career
            if (career != null)
            {
                if (TempData.ContainsKey("career"))
                {
                    TempData["career"] = career;
                }
                else
                {
                    TempData.Add("career", career);
                }
            }

            if (TempData["career"] != null)
            {
                TempData.Keep("career");
                ViewBag.career = TempData["career"];

                var stringCareer = db.Careers.Find(ViewBag.career);
                ViewBag.stringCareer = stringCareer.Name;

                allJops = allJops.Where(j => j.CareerId == ViewBag.career).ToList();
            }

            ////filter by experience
            if (experience != null)
            {
                if (TempData.ContainsKey("experience"))
                {
                    TempData["experience"] = experience;
                }
                else
                {
                    TempData.Add("experience", experience);
                }
            }

            if (TempData["experience"] != null)
            {
                TempData.Keep("experience");
                ViewBag.experience = TempData["experience"];

                var stringExperience = db.Experiences.Find(ViewBag.experience);
                ViewBag.stringExperience = stringExperience.TotalName;

                allJops = allJops.Where(j => j.ExperienceId == ViewBag.experience).ToList();
            }


            ////filter by JopType
            if (!string.IsNullOrEmpty(jopType) )
            {
                if (TempData.ContainsKey("jopType"))
                {
                    TempData["jopType"] = jopType;
                }
                else
                {
                    TempData.Add("jopType", jopType);
                }
            }

            if (TempData["jopType"] != null)
            {
                TempData.Keep("jopType");
                ViewBag.jopType = TempData["jopType"];
                allJops = allJops.Where(j => j.JopType == ViewBag.jopType).ToList();
            }

            ////filter by datePosted

            DateTime searchDate = DateTime.Now;

            if (!string.IsNullOrEmpty(datePosted))
            {
                
                if(datePosted == "Last 24 Hours")
                {
                    searchDate = DateTime.Now.AddDays(-1);
                }
                else if (datePosted == "One Week")
                {
                     searchDate = DateTime.Now.AddDays(-7);
                }
                else if(datePosted == "One Month")
                {
                     searchDate = DateTime.Now.AddDays(-30);
                }
                else if(datePosted == "All")
                {   
                     searchDate = DateTime.Now.AddYears(-200);
                }

                if (TempData.ContainsKey("searchDate"))
                {
                    TempData["searchDate"] = searchDate;
                }
                else
                {
                    TempData.Add("searchDate", searchDate);
                }

                if (TempData.ContainsKey("datePosted"))
                {
                    TempData["datePosted"] = datePosted;
                }
                else
                {
                    TempData.Add("datePosted", datePosted);
                }
            }

            if (TempData["searchDate"] != null)
            {
                TempData.Keep("searchDate");
                ViewBag.searchDate = TempData["searchDate"];

                TempData.Keep("datePosted");
                ViewBag.datePosted = TempData["datePosted"];

                allJops = allJops.Where(j => j.AnnouncedDate >= ViewBag.searchDate).ToList();
            }

            ////filter by search
            if (!string.IsNullOrEmpty(serach))
            {
                if (TempData.ContainsKey("serach"))
                {
                    TempData["serach"] = serach;
                }
                else
                {
                    TempData.Add("serach", serach);
                }
            }

            if (TempData["serach"] != null)
            {
                TempData.Keep("serach");
                ViewBag.serach = TempData["serach"];
                allJops = allJops.Where(j => j.Title.Contains(ViewBag.serach) || j.CompanyName.Contains(ViewBag.serach) || j.Details.Contains(ViewBag.serach)).ToList();
            }


            ViewBag.JopsCount = allJops.Count();

            byte pageSize = 6;
            int pageNumber = (page ?? 1);
            var viewModel = new JopFormsViewModel
            {
                Jops = allJops.ToPagedList(pageNumber, pageSize),
                jop = new Jop(),
                Careers =db.Careers.ToList(),
                Locations = db.Locations.ToList(),
                Experiences = db.Experiences.ToList()
            };
            
            if (TempData.ContainsKey("shortMessage"))
            {
                ViewBag.Message = TempData["shortMessage"].ToString();
                return View(viewModel);
            }
            else
            {
                return View(viewModel);
            }
        }
        public ActionResult SoftIn()
        {
            return View();
        }
        // Sending the CV.
        [HttpPost]
        public ActionResult SendYourCV( HttpPostedFileBase fileUploader, string fName, string lName, string email, string mobile)
        {

            if (ModelState.IsValid)

            {

                string from = "mohamed_a56207@cic-cairo.com"; //example:- xxxxxxxxx@gmail.com

                using (MailMessage mail = new MailMessage(from, "mohamed.abbas.cic@gmail.com"))

                {
                    
                    mail.Subject = "My CV - SFA Site";

                    mail.Body = "Name: " + fName +" " + lName + ".\r\n" + "Email: " + email + ".\r\n" + "Mobile: " + mobile + "." ;

                    if (fileUploader != null)

                    {

                        string fileName = Path.GetFileName(fileUploader.FileName);

                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));

                    }

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "20156207");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    TempData["shortMessage"] = "Sent";
                    return RedirectToAction("Index","Home");

                }

            }

            else

            {
                TempData["shortMessage"] = "Error";
                return RedirectToAction("Index","Home");

            }

        }

        // Request for a services.
        [HttpPost]
        public ActionResult RequestASevice(string fName, string lName, string postion , string email, string mobile, string cName , string address , string services)
        {

            if (ModelState.IsValid)

            {

                string from = "mohamed_a56207@cic-cairo.com"; //example:- xxxxxxxxx@gmail.com

                using (MailMessage mail = new MailMessage(from, "mohamed.abbas.cic@gmail.com"))

                {

                    mail.Subject = "Request for a service - SFA Site";

                    mail.Body = "Name: " + fName + " " + lName + ".\r\n" +
                        "Postion: " + postion + ".\r\n" +
                        "Email: " + email + ".\r\n" +
                        "Mobile: " + mobile + ".\r\n" +
                        "Company Name: " + cName + ".\r\n" +
                        "Address: " + address + ".\r\n" + "\r\n" +
                        "Type of Service: " + services + "." ;

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "20156207");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    TempData["shortMessage"] = "Sent";

                    return RedirectToAction("Index", "Home");

                }

            }

            else

            {
                TempData["shortMessage"] = "Error";
                return RedirectToAction("Index", "Home");

            }

        }

        // Contact Us.
        [HttpPost]
        public ActionResult ContactUs(string name, string email, string message)
        {

            if (ModelState.IsValid)

            {

                string from = "mohamed_a56207@cic-cairo.com"; //example:- xxxxxxxxx@gmail.com

                using (MailMessage mail = new MailMessage(from, "mohamed.abbas.cic@gmail.com"))

                {

                    mail.Subject = "Contact Us - SFA Site";

                    mail.Body = "Name: " + name + ".\r\n" +
                        "Email: " + email + ".\r\n" + "\r\n" +
                        "Message: " + message +".";

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "20156207");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    TempData["shortMessage"] = "Sent";

                    return RedirectToAction("Contact", "Home");

                }

            }

            else

            {
                TempData["shortMessage"] = "Error";
                return RedirectToAction("Index", "Home");

            }

        }

        // Fill the application - Join Us.
        [HttpPost]
        public ActionResult FillTheApplication(HttpPostedFileBase fileUploader, string fName, string lName, string age , string email, string mobile, string cName , string address ,string gender, string cources , string skills , string salary , string jopType , string city , string careerExp , string career)
        {

            if (ModelState.IsValid)

            {

                string from = "mohamed_a56207@cic-cairo.com"; //example:- xxxxxxxxx@gmail.com

                using (MailMessage mail = new MailMessage(from, "mohamed.abbas.cic@gmail.com"))

                {

                    mail.Subject = "Filling the application - SFA Site";

                    mail.Body = "Name: " + fName + " " + lName + ".\r\n" + "Age: " + age + ".\r\n" + "Email: " + email + ".\r\n" + "Mobile: " + mobile + ".\r\n" +
                        "Last Job Title, and Work Location: " + cName + ".\r\n" +
                        "Address: " + address + ".\r\n" +
                        "Gender: " + gender + ".\r\n" +
                        "Certification and Courses:: " + cources + ".\r\n" +
                        "Software skills: " + skills + ".\r\n" +
                        "Expected Salary In L.E: " + salary + ".\r\n" +
                        "Jop Type: " + jopType + ".\r\n" +
                        "City: " + city + ".\r\n" +
                        "Career Experience: " + careerExp + ".\r\n" +
                        "Career: " + career + ".\r\n";

                    if (fileUploader != null)

                    {

                        string fileName = Path.GetFileName(fileUploader.FileName);

                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));

                    }

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "20156207");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    TempData["shortMessage"] = "Sent";
                    return RedirectToAction("JopIn", "Home");

                }

            }

            else

            {
                TempData["shortMessage"] = "Error";
                return RedirectToAction("JopIn", "Home");

            }

        }

        // Salary Survey.
        [HttpPost]
        public ActionResult SalarySurvey(string YearEXP, string SalaryFrom, string SalaryTo, string Benefits, string Note)
        {

            if (ModelState.IsValid)

            {

                string from = "mohamed_a56207@cic-cairo.com"; //example:- xxxxxxxxx@gmail.com

                using (MailMessage mail = new MailMessage(from, "mohamed.abbas.cic@gmail.com"))

                {

                    mail.Subject = "Salary Survey - SFA Site";

                    mail.Body = "Years Experience: " + YearEXP + ".\r\n" +
                        "Salary In L.E: \t From: " + SalaryFrom + "\t To: " + SalaryTo + ".\r\n" +
                        "Benefits: " + Benefits + ".\r\n" +
                        "Note: " + Note +".";

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "20156207");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    TempData["shortMessage"] = "Sent";

                    return RedirectToAction("JopIn", "Home");

                }

            }

            else

            {
                TempData["shortMessage"] = "Error";
                return RedirectToAction("Index", "Home");

            }

        }
       
protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing); 
        }
    }
}