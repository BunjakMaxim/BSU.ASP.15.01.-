using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.EF
{
    class StoreDbInitializer : DropCreateDatabaseIfModelChanges<EFContext>
    {
        protected override void Seed(EFContext context)
        {
            context.Roles.Add(new Role { Id = 1, Name = "Admin" });
            context.Roles.Add(new Role { Id = 2, Name = "User" });

            context.Tags.Add(new Tag { Id = 1, Name = "All"});

            context.SaveChanges();
        }
    }
}
