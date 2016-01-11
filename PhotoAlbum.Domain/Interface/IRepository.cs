using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Domain.Interface
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Create(T t);
        void Update(T t);
        void Delete(T t);
        T Delete(int id);
    }
}
