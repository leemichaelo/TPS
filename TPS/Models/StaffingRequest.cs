using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPS.Models
{
    public class StaffingRequest
    {
        [Required]
        [Display(Name = "Request Number")]
        public int RequestNumber { get; set; }
        [Required]
        [Display(Name = "Contract ID")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Client")]
        public Client Client { get; set; }
        [Required]
        [Display(Name = "Staff Members")]
        public List<StaffMember> StaffMembers { get; set; }
        [Required]
        [Display(Name = "Contract Manager")]
        public ContractManager ContractManager { get; set; }
        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Type of Work")]
        public string TypeOfWork { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public float Salary { get; set; }
        public bool Approved { get; set; }
    }
}