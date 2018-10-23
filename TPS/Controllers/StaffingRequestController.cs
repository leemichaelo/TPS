using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TPS.Data;
using TPS.Models;

namespace TPS.Controllers
{
    public class StaffingRequestController : Controller
    {
        private StaffingRequestRepository _staffingRequestRepository = null;
        private ContractManagerRepository _contractManagerRepository = null;
        private StaffMemberRepository _staffMemberRepository = null;

        public StaffingRequestController()
        {
            _staffingRequestRepository = new StaffingRequestRepository();
            _contractManagerRepository = new ContractManagerRepository();
            _staffMemberRepository = new StaffMemberRepository();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<StaffingRequest> staffingRequests = GetStaffingRequests();

            return View(staffingRequests);
        }

        public ActionResult Add()
        {
            StaffingRequest staffingRequest = new StaffingRequest();
            List<StaffMember> availableStaffMembers = GetAvailableStaffMembers();
            List<ContractManager> availableContractManagers = GetContractManagers();
            staffingRequest.StaffMembers = availableStaffMembers;
            staffingRequest.ContractManager = availableContractManagers.FirstOrDefault();

            return View(staffingRequest);
        }

        [HttpPost]
        public ActionResult Add(StaffingRequest staffingRequest)
        {
            ValidateStaffingRequest(staffingRequest);

            if (ModelState.IsValid)
            {
                AddStaffingRequest(staffingRequest);

                TempData["Message"] = "Your Staffing Request was succsesfully added!";

                return RedirectToAction("Index");
            }

            return View(staffingRequest);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StaffingRequest staffingRequest = GetStaffingRequest((int)id);

            if (staffingRequest == null)
            {
                return HttpNotFound();
            }

            return View(staffingRequest);
        }

        [HttpPost]
        public ActionResult Edit(StaffingRequest staffingRequest)
        {
            ValidateStaffingRequest(staffingRequest);

            if (ModelState.IsValid)
            {
                UpdateStaffingRequest(staffingRequest);
                TempData["Message"] = "Your Staffing Request was successfully updated!";

                return RedirectToAction("Index");
            }

            return View(staffingRequest);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StaffingRequest staffingRequest = GetStaffingRequest((int)id);

            if (staffingRequest == null)
            {
                return HttpNotFound();
            }

            return View(staffingRequest);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DeleteStaffingRequest(id);
            TempData["Message"] = "Your Staffing Request was Succesfully deleted!";

            return RedirectToAction("Index");
        }

        private List<StaffingRequest> GetStaffingRequests() => User.IsInRole("Manager") ? _staffingRequestRepository.GetStaffingRequests() : _staffingRequestRepository.GetClientStaffingRequests(User.Identity.Name);

        private StaffingRequest GetStaffingRequest(int id) => _staffingRequestRepository.GetStaffingRequest(id);

        private void AddStaffingRequest(StaffingRequest staffingRequest) => _staffingRequestRepository.AddStaffingRequest(staffingRequest);

        private void UpdateStaffingRequest(StaffingRequest staffingRequest) => _staffingRequestRepository.UpdateStaffingRequest(staffingRequest);

        private void DeleteStaffingRequest(int id) => _staffingRequestRepository.DeleteStaffingRequest(id);

        private void ValidateStaffingRequest(StaffingRequest staffingRequest)
        {
            if (ModelState.IsValidField("Salary") && staffingRequest.Salary <= 0)
            {
                ModelState.AddModelError("Salary", "The Salary field value must be greater than '0'.");
            }
        }

        private List<StaffMember> GetAvailableStaffMembers() => _staffMemberRepository.GetAvailableStaffMembers();

        private List<ContractManager> GetContractManagers() => _contractManagerRepository.GetContractManagers();

    }
}