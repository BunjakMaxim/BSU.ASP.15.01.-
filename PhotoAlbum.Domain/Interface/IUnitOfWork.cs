using System;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRepository<Album> Albums { get; }
        IRepository<Photo> Photos { get; }
        ITagRepository Tags { get; }
        IRoleRepository Roles { get; }
        IRatingRepository Rating { get; }
        

        void Save();
    }
}
