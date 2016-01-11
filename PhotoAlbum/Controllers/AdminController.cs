using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoAlbum.Business.Interfaces;

namespace PhotoAlbum.Controllers
{
    [Authorize(Users="Admin")]
    public class AdminController : Controller
    {
        private readonly IMembershipService membershipService;

        public AdminController(IMembershipService memb)
        {
            membershipService = memb;
        }

        public ActionResult ShowUsers()
        {
            var n = User.IsInRole("Administrators");
            return View(membershipService.GetAllUsers());
        }

        public ActionResult DeleteUser(string userLogin)
        {
            membershipService.DeleteUser(userLogin);
            return RedirectToAction("ShowUser");
        }
    }
}