using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Business.DTO;

namespace PhotoAlbum.Business.Interfaces
{
    public interface ITagService
    {
        IEnumerable<PhotoPropertiesDTO> GetPhotosByTag(string tagName, string login);
        IEnumerable<string> GetAllTegName();
        IEnumerable<string> GetPhotoTegName(int idPhoto);
        void AddTagToPhoto(int idPhotom, string tagName);
    }
}
