using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.EF;

namespace PhotoAlbum.Domain.Repositories
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        private EFContext db;

        public UserRepository(EFContext contex) : base(contex)
        {
            db = contex;
        }

        public User GetByLogin(string login)
        {
            return db.Users.FirstOrDefault(user => user.Login == login);
        }
    }
}
