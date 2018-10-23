using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPS.Models
{
    public class Location
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Location Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Location Address")]
        public string Address { get; set; }
    }
}