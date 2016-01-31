using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Business.DTO;


namespace PhotoAlbum.Business.Interfaces
{
    public interface IAlbumService
    {
        void DeleteAlbum(string login, string albumName);
        IEnumerable<string> GetAllUserAlbumsNams(string login);
        bool CreateNewAlbum(string login, string albumName);
        void UpdateAlbum(string login, string albumName, string newAlbumName);
    }
}
