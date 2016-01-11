using System;
using System.Collections.Generic;

namespace PhotoAlbum.Domain.Core
{
    public class Photo : Entity
    {
        public byte[] Image { get; set; }

        public virtual Album Album { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Rating> Raitings { get; set; }
    }
}
