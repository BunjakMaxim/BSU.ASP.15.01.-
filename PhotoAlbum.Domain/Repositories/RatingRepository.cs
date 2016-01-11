using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Core;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.EF;
using System.Data.Entity;

namespace PhotoAlbum.Domain.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly EFContext db;

        public RatingRepository(EFContext contex)
        {
            db = contex;
        }

        public double GetRating(Photo photo)
        {
            int rating = 0, count = 0;
            foreach(var r in photo.Raitings)
            {
                rating += r.Value;
                count++;
            }
            return count == 0 ? 0 : (double)rating / count;
        }

        public int GetRatingPhotoUsers(Photo photo, User user)
        {
            Rating r = photo.Raitings.FirstOrDefault(re => re.User == user);
            return r == null ? 0 : r.Value;
        }

        public void UpdateOrAdd(Rating rating, Photo photo)
        {
            Rating r = photo.Raitings.FirstOrDefault(rt=>rt.User == rating.User);
            if (r != null)
            {
                r.Value = rating.Value;
                db.Entry(r).State = EntityState.Modified;
            }
            else
                photo.Raitings.Add(rating);
        }
    }
}
