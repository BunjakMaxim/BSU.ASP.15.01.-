using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Models;
using PhotoAlbum.Business.DTO;

namespace PhotoAlbum.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private readonly IContentService contentService;
        
        public AlbumController(IContentService content)
        {
            contentService = content;
        }

        public ActionResult ShowAlbums(int page = 1)
        {
            string login = User.Identity.Name;
            var albums = new PageData<string>(contentService.GetAllUserAlbumsNams(login), page);

            return View(albums);
        }

        public ActionResult EditAlbums(int page = 1)
        {
            string login = User.Identity.Name;
            var album = new PageData<string>(contentService.GetAllUserAlbumsNams(login), page);

            return View(album);
        }

        public ActionResult EditAlbum(string albumName, int page = 1)
        {
            ViewBag.AlbumName = albumName;
            string login = User.Identity.Name;
            var photos = new PageData<PhotoOfAlbumDTO>(contentService.GetPhotosOfAlbum(login, albumName), page);

            return View(photos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AlbumViewModel album)
        {
            if (ModelState.IsValid)
            {
                if (contentService.CreateNewAlbum(User.Identity.Name, album.Name))
                {
                    return RedirectToAction("EditAlbums");
                }
                ModelState.AddModelError("", "Альбом с таким названием уже существует.");
            }
            
            return View(album);
        }
        
        [HttpPost]
        public ActionResult RenameAlbum(string albumName, string newAlbumName)
        {
            string login = User.Identity.Name;
            contentService.UpdateAlbum(login, albumName, newAlbumName);

            return RedirectToAction("EditAlbums");
        }

        public ActionResult Delete(string albumName)
        {
            string login = User.Identity.Name;
            contentService.DeleteAlbum(login, albumName);

            return RedirectToAction("EditAlbums");
        }
    }
}