using System;
using Ninject.Modules;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.Repositories;

namespace PhotoAlbum.Business.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}
