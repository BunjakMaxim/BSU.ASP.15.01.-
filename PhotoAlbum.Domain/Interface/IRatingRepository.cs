using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.Interface
{
    public interface IRatingRepository
    {
        void UpdateOrAdd(Rating rating, Photo photo);
        double GetRating(Photo photo);
        int GetRatingPhotoUsers(Photo photo, User user);
    }
}
