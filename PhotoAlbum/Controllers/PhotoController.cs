using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoAlbum.Business.Interfaces;
using System.IO;
using PhotoAlbum.Business.DTO;
using PhotoAlbum.Models;

namespace PhotoAlbum.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly IPhotoService photoService;
        private readonly ITagService tagService;

        public PhotoController(IPhotoService photos, ITagService tags)
        {
            photoService = photos;
            tagService = tags;
        }

        public ActionResult ShowPhoto(int id)
        {
            string login = User.Identity.Name;
            var photo = photoService.GetPhotoById(id, login);

            return View(photo);
        }

        public ActionResult ShowPhotos(string albumName, int page = 1)
        {
            TempData["AlbumName"] = albumName;
            
            return View();
        }

        public ActionResult GetPhotosOfAlbum(string userName, string albumName, int page = 1)
        {
            TempData["AlbumName"] = albumName;

            if(userName == null)
                userName = User.Identity.Name;

            string login = User.Identity.Name;
            var photos = new PageData<PhotoPropertiesDTO>(photoService.GetPhotosPropertiesOfAlbum(userName, albumName, login), page);
            return PartialView(photos);
        }

        public ActionResult ShowPhotoByTag(string tagName, int page=1)
        {
            TempData["TagName"] = tagName;
            
            return View();
        }
        
        public ActionResult GetPhotosByTag(string tagName, int page = 1)
        {
            TempData["TagName"] = tagName;

            string login = User.Identity.Name;
            var photos = tagService.GetPhotosByTag(tagName, login);
            var photoPage = new PageData<PhotoPropertiesDTO>(photos, page);

            return PartialView(photoPage);
        }


        [HttpPost]
        public ActionResult SetRating(int idPhoto, int rating)
        {
            string login = User.Identity.Name;
            photoService.SetRaiting(idPhoto, login, rating);

            return RedirectToAction("ShowPhoto", new { id = idPhoto });
        }


        public ActionResult EditRating(int idPhoto, int rating)
        {
            string login = User.Identity.Name;
            var ratingView = new RatingViewModel
            {
                Rating = photoService.SetRaiting(idPhoto, login, rating),
                NewRating = rating,
                IdPhoto = idPhoto
            };

            return PartialView(ratingView);
        }

        private IEnumerable<byte[]> ConvertToArrayBytes(HttpPostedFileBase[] images)
        {
            foreach (var image in images)
            {
                BinaryReader reader = new BinaryReader(image.InputStream);
                byte[] imageBytes = reader.ReadBytes((int)image.ContentLength);

                yield return imageBytes;
            }
        }

        [HttpPost]
        public ActionResult Upload(string albumName, HttpPostedFileBase[] files)
        {
            string login = User.Identity.Name;
            photoService.CreateNewPhoto(ConvertToArrayBytes(files), albumName, login);

            return RedirectToAction("EditAlbum", "Album", new { albumName = albumName });
        }

        public ActionResult Delete(string albumName, int idPhoto)
        {
            string login = User.Identity.Name;
            photoService.DeletePhoto(login, albumName, idPhoto);

            return RedirectToAction("EditAlbum", "Album", new { albumName = albumName });
        }

        public ActionResult EditPhoto(string albumName, int idPhoto)
        {
            ViewBag.Id = idPhoto;
            var tags = tagService.GetPhotoTegName(idPhoto);

            return View(tags);
        }

        [HttpPost]
        public ActionResult TagAdd(int idPhoto, string nameTag)
        {
            tagService.AddTagToPhoto(idPhoto, nameTag);
            var tags = tagService.GetPhotoTegName(idPhoto);

            return PartialView(tags);
        }
    }
}