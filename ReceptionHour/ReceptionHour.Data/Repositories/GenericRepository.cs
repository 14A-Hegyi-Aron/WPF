using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionHour.Data.Repositories
{
    public class GenericRepository<T> where T : class
    {
        protected readonly DbContext dbContext;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public virtual T GetByKey(object key)
        {
            return dbContext.Set<T>().Find(key);
        }

        public virtual T Insert(T entity)
        {
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public virtual T Update(T entity)
        {
            dbContext.Entry(entity).State= EntityState.Modified;
            dbContext.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity); 
            dbContext.SaveChanges();
        }
    }
}
