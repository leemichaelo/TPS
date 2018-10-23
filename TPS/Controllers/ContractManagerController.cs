using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPS.Data;
using TPS.Models;

namespace TPS.Controllers
{
    public class ContractManagerController : Controller
    {
        private ContractManagerRepository _contractManagerRepository = null;

        public ContractManagerController()
        {
            _contractManagerRepository = new ContractManagerRepository();
        }
        public ActionResult Index()
        {
            List<ContractManager> managers = _contractManagerRepository.GetContractManagers();
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