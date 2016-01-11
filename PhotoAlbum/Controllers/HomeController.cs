using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoAlbum.Business.Interfaces;

namespace PhotoAlbum.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IContentService contentService;

        public HomeController(IContentService content)
        {
            contentService = content;
        }
        public ActionResult Index()
        {
            var tagsName = contentService.GetAllTegName();
            return View(tagsName);
        }
    }
}