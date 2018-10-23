using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPS.Models
{
    public class StaffMember
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Staff Member Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Staff Member Salary")]
        public float Salary { get; set; }
        public bool Selected { get; set; }
        [Required]
        [Display(Name = "Staff Member Resume")]
        public string Resume { get; set; }
    }
}