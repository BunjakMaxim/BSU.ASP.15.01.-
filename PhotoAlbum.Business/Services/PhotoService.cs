using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Business.DTO;
using AutoMapper;

namespace PhotoAlbum.Business.Services
{
    public class PhotoService : BaseService, IPhotoService
    {
        public PhotoService(IUnitOfWork uow) : base(uow)
        {
        }

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
        
        public IEnumerable<PhotoOfAlbumDTO> GetPhotosOfAlbum(string login, string albumName)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Album album = user.Albums.FirstOrDefault(a => a.Name == albumName);

            if (album == null)
                return null;

            Mapper.CreateMap<Photo, PhotoOfAlbumDTO>();
            return Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoOfAlbumDTO>>(album.Photos);
        }

        public double SetRaiting(int idPhoto, string login, int rating)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            Photo photo = unitOfWork.Photos.GetById(idPhoto);

            Rating r = new Rating { User = user, Value = rating, Photo = photo };
            unitOfWork.Rating.UpdateOrAdd(r, photo);
            unitOfWork.Save();

            return unitOfWork.Rating.GetRating(photo);
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

        public IEnumerable<PhotoPropertiesDTO> GetPhotosPropertiesOfAlbum(string userName, string albumName, string login)// добавить 2 логин
        {
            User user = unitOfWork.Users.GetByLogin(userName);
            Album album = user.Albums.FirstOrDefault(a => a.Name == albumName);
            bool isMy = userName == login;

            if (album == null)
                return null;
            
            Mapper.CreateMap<Photo, PhotoPropertiesDTO>()
                .ForMember("Rating", opt => opt.MapFrom(photo => unitOfWork.Rating.GetRating(photo)))
                .ForMember("IsMy", opt => opt.MapFrom(photo => isMy))
                .ForMember("TagName", opt => opt.MapFrom(photo => photo.Tags.Select(tag => tag.Name)))
                .ForMember("MyRating", opt => opt.MapFrom(photo => unitOfWork.Rating.GetRatingPhotoUsers(photo, user)));
            return Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoPropertiesDTO>>(album.Photos);
        }
    }
}
