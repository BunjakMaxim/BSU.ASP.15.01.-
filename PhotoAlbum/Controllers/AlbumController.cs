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
        private readonly IAlbumService albumService;
        private readonly IPhotoService photoService;

        public AlbumController(IAlbumService albums, IPhotoService photos)
        {
            albumService = albums;
            photoService = photos;
        }

        public ActionResult ShowAlbums(string userName, int page = 1)
        {
            if(userName == null)
                userName = User.Identity.Name;

            ViewBag.UserName = userName;
            var albums = new PageData<string>(albumService.GetAllUserAlbumsNams(userName), page);
            return View(albums);
        }

        public ActionResult EditAlbums(int page = 1)
        {
            string login = User.Identity.Name;
            var album = new PageData<string>(albumService.GetAllUserAlbumsNams(login), page);

            return View(album);
        }

        public ActionResult EditAlbum(string albumName, int page = 1)
        {
            ViewBag.AlbumName = albumName;
            string login = User.Identity.Name;
            var photos = new PageData<PhotoOfAlbumDTO>(photoService.GetPhotosOfAlbum(login, albumName), page);

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
                if (albumService.CreateNewAlbum(User.Identity.Name, album.Name))
                {
                    return RedirectToAction("EditAlbums");
                }
                ModelState.AddModelError("", "The album with the same name already exists.");
            }
            
            return View(album);
        }
        
        [HttpPost]
        public ActionResult RenameAlbum(string albumName, string newAlbumName)
        {
            string login = User.Identity.Name;
            albumService.UpdateAlbum(login, albumName, newAlbumName);

            return RedirectToAction("EditAlbums");
        }

        public ActionResult Delete(string albumName)
        {
            string login = User.Identity.Name;
            albumService.DeleteAlbum(login, albumName);

            return RedirectToAction("EditAlbums");
        }
    }
}