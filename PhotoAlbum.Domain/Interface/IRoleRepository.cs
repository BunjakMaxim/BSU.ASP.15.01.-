using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        Role GetById(int roleId);
    }
}
