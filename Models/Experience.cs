using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SFA.Models
{
    public class Experience
    {
        public short Id { get; set; }
        [Required]
        public byte From { get; set; }
        [Required]
        [StringLength(50)]
        public string FromMonthOrYear { get; set; }
        [Required]
        public byte To { get; set; }
        [Required]
        [StringLength(50)]
        public string ToMonthOrYear { get; set; }
        [StringLength(255)]
        public string TotalName { get; set; }
        public ICollection<Jop> Jops { get; set; }
    }
}