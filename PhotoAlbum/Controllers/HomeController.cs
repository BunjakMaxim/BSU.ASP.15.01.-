using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Models;

namespace PhotoAlbum.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITagService tagService;
        private readonly IMembershipService userService;

        public HomeController(ITagService tags, IMembershipService users)
        {
            tagService = tags;
            userService = users;
        }

        public ActionResult Index()
        {
            var tags = tagService.GetAllTegName();
            return View(tags);
        }

        [ChildActionOnly]
        public ActionResult GetUsersName(int page = 1)
        {
            var users = userService.GetAllUsersName();
            var u = new PageData<string>(users, page);
            return PartialView(u);
        }
    }
}