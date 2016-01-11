using System;
using System.Configuration;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.EF;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private  EFContext db;
        private IUserRepository userRepository;
        private IRepository<Album> albumRepository;
        private IRepository<Photo> photoRepository;
        private IRoleRepository roleRepository;
        private ITagRepository tagRepository;
        private IRatingRepository ratingRepository;

        public EFUnitOfWork()
        {
            /*string conString = ConfigurationManager.ConnectionStrings[1].ConnectionString;*/
            db = new EFContext("PhotoAlbumConnection");
        }

        public IRepository<Album> Albums
        {
            get
            {
                if (albumRepository == null)
                    albumRepository = new EntityRepository<Album>(db);

                return albumRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);

                return userRepository;
            }
        }

        public IRepository<Photo> Photos
        {
            get
            {
                if (photoRepository == null)
                    photoRepository = new EntityRepository<Photo>(db);

                return photoRepository;
            }
        }

        public ITagRepository Tags
        {
            get
            {
                if (tagRepository == null)
                    tagRepository = new TagRepository(db);

                return tagRepository;
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);

                return roleRepository;
            }
        }

        public IRatingRepository Rating
        {
            get
            {
                if (ratingRepository == null)
                    ratingRepository = new RatingRepository(db);

                return ratingRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
