using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Domain.Repositories;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Business.DTO;
using AutoMapper;

namespace PhotoAlbum.Business.Services
{
    public class MembershipService : BaseService, IMembershipService
    {
        public MembershipService(IUnitOfWork uow) : base(uow)
        {
        }

        public void ChangeEmail(string login, string newEmail)
        {
            User user = GetUserByLogin(login);
            if (user != null)
            {
                user.Email = newEmail;
                unitOfWork.Users.Update(user);
                unitOfWork.Save();
            }
        }

        public bool CreateUser(string login, string email, string password)
        {
            User user = GetUserByLogin(login);

            if(user == null)
            {
                unitOfWork.Users.Create(
                    new User
                    {
                        Login = login,
                        Email = email,
                        CreationDate = DateTime.Now,
                        Password = password
                    });
                unitOfWork.Save();

                return true;
            }

            return false;
        }

        public void DeleteUser(string login)
        {
            User user = unitOfWork.Users.GetByLogin(login);
            unitOfWork.Users.Delete(user);
            unitOfWork.Save();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            Mapper.CreateMap<User, UserDTO>()
                .ForMember("Role", opt => opt.MapFrom(m => m.Roles.FirstOrDefault().Name));

            return Mapper
                .Map<IEnumerable<User>, IEnumerable<UserDTO>>(unitOfWork.Users.GetAll());
        }

        public IEnumerable<string> GetAllUsersName()
        {
            return unitOfWork.Users.GetAll()
                .Select(user => user.Login);
        }

        public User GetUserByLogin(string login)
        {
            return unitOfWork.Users.GetByLogin(login);
        }

        public bool ValidateUser(string login, string password)
        {
            User user = GetUserByLogin(login);

            if (user != null && user.Password.Equals(password, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        public bool UserExistsByLogin(string login)
        {
            User user = unitOfWork.Users.GetByLogin(login);

            if (user == null)
                return false;

            return true;
        }

        public void ChangePassword(string login, string newPassword, string oldPassword)
        {
            throw new NotImplementedException();
        }
    }
}
