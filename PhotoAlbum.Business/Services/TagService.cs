using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Business.DTO;
using AutoMapper;


namespace PhotoAlbum.Business.Services
{
    public class TagService : BaseService, ITagService 
    {
        public TagService(IUnitOfWork uow) : base(uow)
        {
        }

        public IEnumerable<PhotoPropertiesDTO> GetPhotosByTag(string tagName, string login)
        {
            var photos = unitOfWork.Tags.GetPhotosByTeg(tagName);
            User user = unitOfWork.Users.GetByLogin(login);
            
            Mapper.CreateMap<Photo, PhotoPropertiesDTO>()
                .ForMember("Rating", opt => opt.MapFrom(photo => unitOfWork.Rating.GetRating(photo)))
                .ForMember("IsMy", opt => opt.MapFrom(photo => photo.Album.User.Login == login))
                .ForMember("TagName", opt => opt.MapFrom(photo => photo.Tags.Select(tag => tag.Name)))
                .ForMember("MyRating", opt => opt.MapFrom(photo => unitOfWork.Rating.GetRatingPhotoUsers(photo, user)));
            
            return Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoPropertiesDTO>>(photos);
        }

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
