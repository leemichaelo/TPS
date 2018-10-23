using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TPS.Data;
using TPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TPS.Secuirty;
using TPS.ViewModels;

namespace TPS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;
        private AccountRepository _accountRepository = null;
        private StaffMemberRepository _staffMemberepository = null;
        private ClientRepository _clientRepository = null;
        private ContractManagerRepository _contractManagerRepository = null;

        public AccountController(ApplicationUserManager applicationUserManager, ApplicationSignInManager applicationSignInManager, IAuthenticationManager authenticationManager)
        {
            _userManager = applicationUserManager;
            _signInManager = applicationSignInManager;
            _authenticationManager = authenticationManager;
            _accountRepository = new AccountRepository();
            _staffMemberepository = new StaffMemberRepository();
            _contractManagerRepository = new ContractManagerRepository();
            _clientRepository = new ClientRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<UserLogin> userLogins = new List<UserLogin>();
            userLogins = GetUsers();

            return View(userLogins);
        }

        [HttpPost]
        public async Task<ActionResult> Index(AccountRegisterViewModel viewModel)
        {
            //If the ModelState is valid...
            if (ModelState.IsValid)
            {
                //Instantiate a UserLogin object
                var user = new UserLogin { UserName = viewModel.Username };
                //Create the user
                var result = await _userManager.CreateAsync(user, viewModel.Password);
                //If the user was successfuly created...
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Account");
                }
                //If there were errors...
                //Add model errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(viewModel);
        }

        public ActionResult Edit(string username)
        {
            // If the id, throw a bad request url result
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserLogin person = GetUser(username);

            // If the person is null, throw a not found result
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                UpdateUser(userLogin);
                TempData["Message"] = "The user was succsefully updated!";

                return RedirectToAction("frmViewUsers");
            }

            return View("frmEditUser");
        }

        public ActionResult Delete(string username)
        {
            //Verify that Id is not null, if so return bad http status
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Retrieve Person provided by id
            UserLogin userLogin = GetUser(username);

            //Return not found if person is not found
            if (userLogin == null)
            {
                return HttpNotFound();
            }

            //Pass Person to the view
            return View(userLogin);
        }

        [HttpPost]
        public ActionResult Delete(UserLogin userLogin)
        {
            DeleteUser(userLogin.UserName);

            TempData["Message"] = "The User was succsefully deleted!";

            //TODO Redirect to the entries list page
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Add(AccountRegisterViewModel viewModel)
        {
            // If the ModelState is valid...
            if (ModelState.IsValid)
            {
                // Instantiate a User object
                var user = new UserLogin { UserName = viewModel.Username };

                // Create the user
                var result = await _userManager.CreateAsync(user, viewModel.Password);

                // If the user was successfully created...
                if (result.Succeeded)
                {
                    if (viewModel.Role == "StaffMember")
                    {
                        AddStaffMember(viewModel);
                    }
                    else if (viewModel.Role == "Client")
                    {
                        AddClient(viewModel);
                    }
                    else
                    {
                        AddManager(viewModel);
                    }
                    // Redirect them to the web app's "Home" page        
                    return RedirectToAction("Index", "Home");
                }

                // If there were errors...
                // Add model errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SignIn(AccountSignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //Sign-In the user
            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, shouldLockout: false);

            //Check the Result
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(viewModel);
                case SignInStatus.LockedOut:
                case SignInStatus.RequiresVerification:
                    throw new NotImplementedException("identity feature not implemented.");
                default:
                    throw new Exception($"Unexpected Microsoft.AspNet.Identity.Owin.SIgnInStatus enum value: {result}");
            }

        }

        [HttpPost]
        public ActionResult SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("SignIn", "Account");
        }

        //Returns a list of all users
        public List<UserLogin> GetUsers() => _accountRepository.GetUsers();

        //Returns a user based on given id
        public UserLogin GetUser(string username) => _accountRepository.GetUser(username);

        //Updates a User in the Database
        public void UpdateUser(UserLogin updatedInformation) => _accountRepository.UpdateUser(updatedInformation);

        public void DeleteUser(string username) => _accountRepository.DeleteUser(username);

        public List<UserLogin> GetUsersByRole(string role) => _accountRepository.GetUsersByRole(role);

        private void AddManager(AccountRegisterViewModel viewModel) => _contractManagerRepository.AddManager(viewModel);

        private void AddClient(AccountRegisterViewModel viewModel) => _clientRepository.AddClient(viewModel);

        private void AddStaffMember(AccountRegisterViewModel viewModel) => _staffMemberepository.AddStaffMember(viewModel);
    }
}