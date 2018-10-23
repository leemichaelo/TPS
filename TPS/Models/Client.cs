using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPS.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string Name { get; set; }
        public List<StaffingRequest> StaffingRequests { get; set; }
    }
}