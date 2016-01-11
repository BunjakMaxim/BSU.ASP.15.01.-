using System;
using PhotoAlbum.Domain.Interface;

namespace PhotoAlbum.Business.Services
{
    public class BaseService
    {
        protected IUnitOfWork unitOfWork;

        public BaseService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
    }
}
