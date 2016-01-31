using System;
using System.Collections.Generic;

namespace PhotoAlbum.Domain.Core
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
