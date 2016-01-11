using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Business.DTO;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.Repositories;
using AutoMapper;

namespace PhotoAlbum.Business.Services
{
    public class ContentService : BaseService, IContentService
    {
        public ContentService(IUnitOfWork uow) : base(uow)
        {
        }

        #region AlbumService
        public bool CreateNewAlbum(string login, string albumName)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Album a = user.Albums.FirstOrDefault(album => album.Name == albumName);

            if (a == null)
            {
                var album = new Album { Name = albumName, User = user};
                user.Albums.Add(album);
                unitOfWork.Users.Update(user);
                unitOfWork.Save();

                return true;
            }

            return false;
        }

        public void DeleteAlbum(string login, string albumName)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Album album = user.Albums.First(a => a.Name.Equals(albumName));
            unitOfWork.Albums.Delete(album);
            unitOfWork.Save();
        }

        public IEnumerable<string> GetAllUserAlbumsNams(string login)
        {
            return unitOfWork.Users.GetByLogin(login)
                .Albums.Select(a=>a.Name);
        }

        public void UpdateAlbum(string login, string albumName, string newAlbumName)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Album album = user.Albums.FirstOrDefault(a => a.Name.Equals(albumName));

            if (album != null)
            {
                album.Name = newAlbumName;
                unitOfWork.Albums.Update(album);
                unitOfWork.Save();
            }
        }
        #endregion

        #region PhotoService
        public void CreateNewPhoto(IEnumerable<byte[]> photos, string albumName, string login)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Album album = user.Albums.FirstOrDefault(a => a.Name == albumName);
            Tag tag = unitOfWork.Tags.GetDefoultTag();

            if (album != null)
            {
                foreach (var photoByte in photos)
                {
                    Photo p = new Photo { Image = photoByte, Album = album };
                    unitOfWork.Photos.Create(p);
                    tag.Photos.Add(p);
                }
            }

            unitOfWork.Save();
        }

        public PhotoPropertiesDTO GetPhotoById(int id, string login)
        {
            Photo photo = unitOfWork.Photos.GetById(id);
            User user = unitOfWork.Users.GetByLogin(login);

            PhotoPropertiesDTO photoProp = new PhotoPropertiesDTO { Id = photo.Id, Image = photo.Image };

            if (photo.Album.User == user)
                photoProp.IsMy = true;
            else
                photoProp.MyRating = unitOfWork.Rating.GetRatingPhotoUsers(photo, user);

            photoProp.Rating = unitOfWork.Rating.GetRating(photo);
            photoProp.TagName = photo.Tags.Select(tag => tag.Name);

            return photoProp;
        }

        public void DeletePhoto(int id)
        {
            unitOfWork.Photos.Delete(id);
            unitOfWork.Save();
        }

        public void UpdatePhoto(Photo photo)
        {
            unitOfWork.Photos.Update(photo);
            unitOfWork.Save();
        }

        public IEnumerable<PhotoOfAlbumDTO> GetPhotosOfAlbum(string login, string albumName)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Album album = user.Albums.FirstOrDefault(a => a.Name == albumName);

            if (album == null)
                return null;

            Mapper.CreateMap<Photo, PhotoOfAlbumDTO>();
            return Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoOfAlbumDTO>>(album.Photos);
        }

        public IEnumerable<PhotoOfAlbumDTO> GetPhotosByTag(string tagName)
        {
            Mapper.CreateMap<Photo, PhotoOfAlbumDTO>();
            var photos = unitOfWork.Tags.GetPhotosByTeg(tagName);

            return Mapper.Map <IEnumerable<Photo>, IEnumerable < PhotoOfAlbumDTO >>(photos);
        }

        public void SetRaiting(int idPhoto, string login, int rating)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Photo photo = unitOfWork.Photos.GetById(idPhoto);

            Rating r = new Rating { User = user, Value = rating, Photo = photo };
            unitOfWork.Rating.UpdateOrAdd(r, photo);
            unitOfWork.Save();
        }

        public void DeletePhoto(string login, string albumName, int idPhoto)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Album album = user.Albums.FirstOrDefault(a => a.Name == albumName);

            if (album != null)
            {
                Photo photo = album.Photos.FirstOrDefault(p => p.Id == idPhoto);
                unitOfWork.Photos.Delete(photo);
                unitOfWork.Save();
            }
        }
        #endregion

        public IEnumerable<string> GetAllTegName()
        {
            return unitOfWork.Tags.GetAllTagName();
        }

        public IEnumerable<string> GetPhotoTegName(int idPhoto)
        {
            Photo photo = unitOfWork.Photos.GetById(idPhoto);
            return photo.Tags.Select(t => t.Name);
        }

        public void AddTagToPhoto(int idPhotom, string tagName)
        {
            Tag tag = unitOfWork.Tags.NameInTag(tagName);
            if (tag == null)
            {
                tag = new Tag { Name = tagName };
                unitOfWork.Tags.Create(tag);
                unitOfWork.Save();
            }

            Photo photo = unitOfWork.Photos.GetById(idPhotom);
            photo.Tags.Add(unitOfWork.Tags.NameInTag(tagName));
            unitOfWork.Save();
        }
    }
}
