using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPS.Models
{
    public class ContractManager
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<StaffingRequest> StaffingRequests { get; set; }
    }
}