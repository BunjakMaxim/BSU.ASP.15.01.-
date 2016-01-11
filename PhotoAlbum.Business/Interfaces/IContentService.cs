using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Business.DTO; 

namespace PhotoAlbum.Business.Interfaces
{
    public interface IContentService
    {
        //AlbumService
        void DeleteAlbum(string login, string albumName);
        IEnumerable<string> GetAllUserAlbumsNams(string login);
        bool CreateNewAlbum(string login, string albumName);
        void UpdateAlbum(string login, string albumName, string newAlbumName);

        //PhotoService
        IEnumerable<PhotoOfAlbumDTO> GetPhotosOfAlbum(string login, string albumName);
        IEnumerable<PhotoOfAlbumDTO> GetPhotosByTag(string tagName);
        void CreateNewPhoto(IEnumerable<byte[]> photos, string albumName, string login);
        PhotoPropertiesDTO GetPhotoById(int id, string login);
        void DeletePhoto(string login, string albumName, int idPhoto);
        void SetRaiting(int idPhoto, string login, int rating);

        //TegService
        IEnumerable<string> GetAllTegName();
        IEnumerable<string> GetPhotoTegName(int idPhoto);
        void AddTagToPhoto(int idPhotom, string tagName);
    }
}
