using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Business.DTO
{
    public class PhotoPropertiesDTO : PhotoOfAlbumDTO
    {
        public double Rating { get; set; }
        public IEnumerable<string> TagName { get; set; }
        public bool IsMy { get; set; }
        public int MyRating { get; set; }
    }
}
