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
    public class AlbumService : BaseService, IAlbumService
    {
        public AlbumService(IUnitOfWork uow) : base(uow)
        {
        }
        
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
    }
}
