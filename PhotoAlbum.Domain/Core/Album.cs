using System;
using System.Collections.Generic;

namespace PhotoAlbum.Domain.Core
{
    public class Album : Entity
    {
        public string Name { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
