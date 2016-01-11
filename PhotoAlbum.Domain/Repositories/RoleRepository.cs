using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Domain.EF;
using PhotoAlbum.Domain.Interface;

namespace PhotoAlbum.Domain.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EFContext db;

        public RoleRepository(EFContext contex)
        {
            db = contex;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return db.Roles.ToList();
        }

        public Role GetById(int roleId)
        {
            return db.Roles.Find(roleId);
        }
    }
}
