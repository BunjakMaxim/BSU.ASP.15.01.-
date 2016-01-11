using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.EF
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        static EFContext()
        {
            Database.SetInitializer<EFContext>(new StoreDbInitializer());
        }

        public EFContext(string conString) : base(conString)
        {
        }
    }
}
