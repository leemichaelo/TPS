using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPS.Models;

namespace TPS.Data
{

    public class StaffingRequestRepository
    {
        public List<StaffingRequest> GetStaffingRequests()
        {
            List<StaffingRequest> staffingRequests = new List<StaffingRequest>();

            using (var context = new Context())
            {
                staffingRequests = context.StaffingRequests
                    .OrderByDescending(s => s.ID)
                    .ToList();
            }

            return staffingRequests;
        }

        public List<StaffingRequest> GetClientStaffingRequests(string userName)
        {
            List<StaffingRequest> staffingRequests = new List<StaffingRequest>();

            using (var context = new Context())
            {
                staffingRequests = context.StaffingRequests
                    .OrderBy(s => s.ID)
                    .ToList();
            }

            return staffingRequests;
        }

        public StaffingRequest GetStaffingRequest(int id)
        {
            StaffingRequest staffingRequest = new StaffingRequest();

            using (var context = new Context())
            {
                staffingRequest = context.StaffingRequests
                    .Where(s => s.ID == id)
                    .FirstOrDefault();
            }

            return staffingRequest;
        }

        public void AddStaffingRequest(StaffingRequest staffingRequest)
        {
            using (var context = new Context())
            {
                context.StaffingRequests.Add(new StaffingRequest()
                {
                    ID = staffingRequest.ID,
                    Client = staffingRequest.Client,
                    ContractManager = staffingRequest.ContractManager,
                    Location = staffingRequest.Location,
                    RequestNumber = staffingRequest.RequestNumber,
                    Salary = staffingRequest.Salary,
                    StaffMembers = staffingRequest.StaffMembers,
                    TypeOfWork = staffingRequest.TypeOfWork                                       
                });
                context.SaveChanges();
            }
        }

        public void UpdateStaffingRequest(StaffingRequest staffingRequest)
        {
            using (var context = new Context())
            {
                var staffingRequestToUpdate =
                (from s in context.StaffingRequests
                 where s.ID == staffingRequest.ID
                 select s).First();

                context.Entry(staffingRequest).CurrentValues
                    .SetValues(staffingRequest);

                context.SaveChanges();
            }
        }

        public void DeleteStaffingRequest(int id)
        {
            using (var context = new Context())
            {
                StaffingRequest staffingRequestToDelete = context.StaffingRequests
                    .Where(s => s.ID == id)
                    .SingleOrDefault();

                context.StaffingRequests.Remove(staffingRequestToDelete);
                context.SaveChanges();
            }
        }

        
    }
}
