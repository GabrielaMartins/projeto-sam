using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Validation;

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
            try
            {
                return DbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join(";", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat("Validation failed for one or more entities. The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage);
            }
            catch(Exception ex)
            {
                throw ex;
            }
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