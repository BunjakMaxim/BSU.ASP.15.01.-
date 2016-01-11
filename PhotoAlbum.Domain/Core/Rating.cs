using System;

namespace PhotoAlbum.Domain.Core
{
    public class Rating : Entity
    {
        public User User { get; set; }
        public Photo Photo { get; set; }
        public int Value { get; set; }
    }
}
