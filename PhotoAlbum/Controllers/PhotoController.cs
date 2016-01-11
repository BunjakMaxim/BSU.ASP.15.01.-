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
        private readonly IContentService contentService;

        public PhotoController(IContentService content)
        {
            contentService = content;
        }

        public ActionResult ShowPhoto(int id)
        {
            string login = User.Identity.Name;
            var photo = contentService.GetPhotoById(id, login);

            return View(photo);
        }

        public ActionResult ShowPhotos(string albumName, int page = 1)
        {
            ViewBag.AlbumName = albumName;
            string login = User.Identity.Name;
            var photos = contentService.GetPhotosOfAlbum(login, albumName);
            var photoPage = new PageData<PhotoOfAlbumDTO>(photos, page);

            return View(photoPage);
        }

        [HttpPost]
        public ActionResult Upload(string albumName, HttpPostedFileBase[] files)
        {
            string login = User.Identity.Name;
            contentService.CreateNewPhoto(ConvertToArrayBytes(files), albumName, login);

            return RedirectToAction("EditAlbum", "Album",new { albumName = albumName});
        }

        public ActionResult Delete(string albumName, int idPhoto)
        {
            string login = User.Identity.Name;
            contentService.DeletePhoto(login, albumName, idPhoto);

            return RedirectToAction("EditAlbum", "Album", new { albumName = albumName });
        }

        public ActionResult ShowPhotoByTag(string tagName, int page=1)
        {
            ViewBag.Tag = tagName;
            var photos = contentService.GetPhotosByTag(tagName);
            var photoPage = new PageData<PhotoOfAlbumDTO>(photos, page);

            return View(photoPage);
        }

        [HttpPost]
        public ActionResult SetRating(int idPhoto, int rating)
        {
            string login = User.Identity.Name;
            contentService.SetRaiting(idPhoto, login, rating);

            return RedirectToAction("ShowPhoto", new { id = idPhoto });
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

        public ActionResult RetrieveImage(byte[] cover)
        {
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public ActionResult EditPhoto(string albumName, int idPhoto)
        {
            ViewBag.Id = idPhoto;
            var tags = contentService.GetPhotoTegName(idPhoto);

            return View(tags);
        }

        [HttpPost]
        public ActionResult TagAdd(int idPhoto, string nameTag)
        {
            contentService.AddTagToPhoto(idPhoto, nameTag);
            var tags = contentService.GetPhotoTegName(idPhoto);

            return PartialView(tags);
        }
    }
}