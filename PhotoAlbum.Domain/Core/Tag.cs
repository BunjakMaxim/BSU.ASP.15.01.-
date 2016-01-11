using System;
using System.Collections.Generic;

namespace PhotoAlbum.Domain.Core
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
