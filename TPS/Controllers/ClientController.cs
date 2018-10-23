using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPS.Data;
using TPS.Models;
using TPS.ViewModels;

namespace TPS.Controllers
{
    public class ClientController : Controller
    {
        private ClientRepository _clientRepository = null;

        public ClientController()
        {
            _clientRepository = new ClientRepository();
        }

        public ActionResult Index()
        {
            List<Client> clients = GetClients();
            return View(clients);
        }

        public ActionResult Add()
        {
            AccountRegisterViewModel vm = new AccountRegisterViewModel();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {              
                return RedirectToAction("Register", "Account", vm);
            }

            return View(vm);
        }

        public ActionResult Edit(int? id)
        {
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult Delete(int? id)
        {
            return RedirectToAction("Index", "Manage");
        }

        private List<Client> GetClients() => _clientRepository.GetClients();
        
    }
}