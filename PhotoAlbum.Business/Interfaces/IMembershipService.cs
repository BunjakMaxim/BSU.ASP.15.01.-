using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Business.DTO;

namespace PhotoAlbum.Business.Interfaces
{
    public interface IMembershipService
    {
        bool CreateUser(string login, string email, string password);
        User GetUserByLogin(string login);
        bool UserExistsByLogin(string login);
        IEnumerable<UserDTO> GetAllUsers();
        IEnumerable<string> GetAllUsersName();
        void DeleteUser(string login);
        bool ValidateUser(string login, string password);
        void ChangePassword(string login, string newPassword, string oldPassword);
        void ChangeEmail(string login, string newEmail);

    }
}
