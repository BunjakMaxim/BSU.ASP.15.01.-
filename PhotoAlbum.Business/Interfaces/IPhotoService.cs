using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Business.DTO;

namespace PhotoAlbum.Business.Interfaces
{
    public interface IPhotoService
    {
        IEnumerable<PhotoOfAlbumDTO> GetPhotosOfAlbum(string login, string albumName);
        IEnumerable<PhotoPropertiesDTO> GetPhotosPropertiesOfAlbum(string userName, string albumName, string login);
        void CreateNewPhoto(IEnumerable<byte[]> photos, string albumName, string login);
        PhotoPropertiesDTO GetPhotoById(int id, string login);
        void DeletePhoto(string login, string albumName, int idPhoto);
        double SetRaiting(int idPhoto, string login, int rating);
    }
}
