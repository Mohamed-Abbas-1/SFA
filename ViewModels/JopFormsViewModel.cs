using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SFA.Models;
namespace SFA.ViewModels
{
    public class JopFormsViewModel
    {
        public IEnumerable<Jop> Jops { get; set; }
        public Jop jop { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
    }
}