using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using PhotoAlbum.Business.Interfaces;
using PhotoAlbum.Business.Services;

namespace PhotoAlbum.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IMembershipService>().To<MembershipService>();
            kernel.Bind<IAlbumService>().To<AlbumService>();
            kernel.Bind<ITagService>().To<TagService>();
            kernel.Bind<IPhotoService>().To<PhotoService>();
        }
    }
}