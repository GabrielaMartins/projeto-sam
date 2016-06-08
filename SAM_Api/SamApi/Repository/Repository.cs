using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Opus.RepositoryPattern
{
    public abstract class Repository<T> : IDisposable, IRepository<T> where T : class
    {

        protected DbContext DbContext;

        protected DbSet<T> DbSet { get; set; }

        public Repository(DbContext DbContext)
        {

            this.DbContext = DbContext;
            DbSet = DbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> filter)
        {
            return DbSet.Where(filter);
        }

        public T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual T Add(T entity)
        {

            DbSet.Add(entity);
            //DbContext.SaveChanges();

            return entity;
        }

        public virtual void Update(T entity)
        {

            var entry = DbContext.Entry(entity);
            entry.State = EntityState.Modified;
            //DbContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> filter)
        {

            var entity = Find(filter).FirstOrDefault();
            if (entity != null)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
               // DbContext.SaveChanges();
            }
        }

        public virtual void Delete(params object[] keys)
        {

            var entity = Find(keys);
            if (entity != null)
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
                //DbContext.SaveChanges();
            }
        }

        public int SubmitChanges()
        {
            return DbContext.SaveChanges();
        }

        public void UseLazyLoad(bool value)
        {
            DbContext.Configuration.LazyLoadingEnabled = value;
        }

        public void Dispose()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }

    }
}