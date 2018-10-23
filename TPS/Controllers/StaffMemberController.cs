using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPS.Data;
using TPS.Models;

namespace TPS.Controllers
{
    public class StaffMemberController : Controller
    {
        private StaffMemberRepository _staffMemberRepository = null;

        public StaffMemberController()
        {
            _staffMemberRepository = new StaffMemberRepository();
        }
        public ActionResult Index()
        {
            List<StaffMember> managers = _staffMemberRepository.GetStaffMembers();
            return View(managers);
        }

        public ActionResult Add()
        {
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Edit(int? id)
        {
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Delete(int? id)
        {
            return RedirectToAction("Index", "Manage");
        }
    }
}