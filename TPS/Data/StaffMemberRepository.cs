using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Models;
using TPS.ViewModels;

namespace TPS.Data
{
    
    public class StaffMemberRepository
    {
        public List<StaffMember> GetStaffMembers()
        {
            List<StaffMember> staffMembers = new List<StaffMember>();

            using (var context = new Context())
            {
                staffMembers = context.StaffMembers
                    .OrderBy(s => s.Selected == false)
                    .ToList();
            }

            return staffMembers;
        }

        public List<StaffMember> GetAvailableStaffMembers()
        {
            List<StaffMember> availableStaffMembers = new List<StaffMember>();

            using (var context = new Context())
            {
                availableStaffMembers = context.StaffMembers
                    .Where(s => s.Selected == false)
                    .OrderBy(i => i.ID)
                    .ToList();
            }

            return availableStaffMembers;
        }

        public void AddStaffMember(AccountRegisterViewModel viewModel)
        {
            using (var context = new Context())
            {
                context.StaffMembers.Add(new StaffMember()
                {
                    Name = viewModel.Username,
                    Salary = 60000,
                    Resume = "test",
                    Selected = false
                });

                context.SaveChanges();
            }
        }
    }
}