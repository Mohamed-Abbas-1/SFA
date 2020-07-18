using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SFA.Models
{
    public class Jop
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You have to insert the jop title.")]
        [StringLength(50,ErrorMessage ="Jop Title cannot be more than 50 chars.")]
        [Display(Name ="Jop Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You have to insert the Company Name.")]
        [StringLength(50, ErrorMessage = "Company Name cannot be more than 50 chars.")]
        [Display(Name="Company Name")]
        public string CompanyName { get; set; }
        public string Salary { get; set; }
        [StringLength(50, ErrorMessage = "Jop Type cannot be more than 50 chars.")]
        [Display(Name ="Jop Type")]
        public string JopType { get; set; }
        public string Email { get; set; }
        public string Watsapp { get; set; }
        [Required(ErrorMessage = "You have to insert the Details.")]
        public string Details { get; set; }
        [Display(Name ="Date of Announce")]
        public DateTime? AnnouncedDate { get; set; }
        [Display(Name = "Last Updated")]
        public DateTime? LastUpdated { get; set; }
        public virtual Location Location { get; set; }
        public short? LocationId { get; set; }
        public virtual Career Career { get; set; }
        public short? CareerId { get; set; }
        public virtual Experience Experience { get; set; }
        public short? ExperienceId { get; set; }
    }
}