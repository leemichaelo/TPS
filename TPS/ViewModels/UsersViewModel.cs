using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Models;

namespace TPS.ViewModels
{
    public class UsersViewModel
    {
        public List<UserLogin> StaffMembers { get; set; }
        public List<UserLogin> Clients { get; set; }
        public List<UserLogin> ContractManagers { get; set; }

    }
}