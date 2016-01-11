using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using PhotoAlbum.Domain.Interface;
using PhotoAlbum.Domain.EF;
using PhotoAlbum.Domain.Core;

namespace PhotoAlbum.Domain.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : Entity
    {
        EFContext db;
        DbSet<T> dsSet;

        public EntityRepository(EFContext contex)
        {
            db = contex;
            dsSet = contex.Set<T>();
        }
        
        public void Create(T item)
        {
            dsSet.Add(item);
        }

        public IQueryable<T> GetAll()
        {
            return dsSet;
        }

        public T Delete(int id)
        {
            T entity = GetById(id);
            Delete(entity);           
             
            return entity;
        }

        public void Delete(T item)
        {
            if (db.Entry(item).State == EntityState.Detached)
            {
                dsSet.Attach(item);
            }
            dsSet.Remove(item);
        }

        public T GetById(int id)
        {
            return dsSet.Find(id);
        }

        public void Update(T item)
        {
            dsSet.Attach(item);
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
