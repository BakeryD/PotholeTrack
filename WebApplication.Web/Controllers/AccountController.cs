using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Web.DAL;
using WebApplication.Web.Models;
using WebApplication.Web.Models.Account;
using WebApplication.Web.Providers.Auth;

namespace WebApplication.Web.Controllers
{    
    public class AccountController : Controller
    {
	    private readonly IPotholeDAL dal;
        private readonly IAuthProvider authProvider;

        public AccountController(IAuthProvider authProvider, IPotholeDAL dal)
        {
            this.authProvider = authProvider;
	        this.dal = dal;
        }
        
        [HttpGet]
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            // Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                // Check that they provided correct credentials
                bool validLogin = authProvider.SignIn(loginViewModel.Username, loginViewModel.Password);
                if (validLogin)
                {
                    var isLoggedIn = authProvider.GetCurrentUser();

                    // Redirect the user where you want them to go after successful login
                    ViewData["loggedIn"] = (isLoggedIn != null);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            // Clear user from session
            authProvider.LogOff();
            TempData["loggedIn"] = false;

            // Redirect the user where you want them to go after logoff
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                // Register them as a new user (and set default role)
                authProvider.Register(registerViewModel.Email,
                                      registerViewModel.UserName,
                                      registerViewModel.Password,
                                      registerViewModel.FirstName,
                                      registerViewModel.LastName,
                                      registerViewModel.PhoneNumber, "user");

                // Redirect the user where you want them to go after registering
                return RedirectToAction("Index", "Home");
            }

            return View(registerViewModel);
        }

        [AuthorizationFilter("user","employee","admin")]
        public IActionResult ViewProfile()
        {
            var isLoggedIn = authProvider.GetCurrentUser();
            var profile = new Profile();
            var user = authProvider.GetCurrentUser();
	        profile.user = user;
	        profile.reports = dal.GetAllReports();
            ViewData["loggedIn"] = (isLoggedIn != null);
            return View(profile);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

                                                //OPTIONAL!!!
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            return View();
        }
    }
}