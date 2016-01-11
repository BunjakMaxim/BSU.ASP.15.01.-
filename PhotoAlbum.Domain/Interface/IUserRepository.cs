using System;
using System.Collections.Generic;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);
    }
}
