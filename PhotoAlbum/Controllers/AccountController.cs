using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Models;
using AutoMapper;

namespace PhotoAlbum.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMembershipService membershipService;

        public AccountController(IMembershipService memb)
        {
            membershipService = memb;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (membershipService.ValidateUser(viewModel.Login, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, viewModel.RememberMe); 

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "The username or password is incorrect.");
            }
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            var anyUser = membershipService.UserExistsByLogin(viewModel.Login);

            if (anyUser)
            {
                ModelState.AddModelError("", "This user name is already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = membershipService.CreateUser(viewModel.Login, viewModel.Email, viewModel.Password);

                if (membershipUser)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Registration error."); 
            }

            return View(viewModel);
        }
       
        public ActionResult EditUser()
        {
            return View();
        }

        public ActionResult RenameUser(string newLogin)
        {
            string login = User.Identity.Name;
            membershipService.ChangeEmail(login, newLogin);

            return RedirectToAction("EditUser");
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
    }
}