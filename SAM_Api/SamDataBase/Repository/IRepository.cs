using System;
using System.Linq;
using System.Linq.Expressions;

namespace Opus.RepositoryPattern
{
    public interface IRepository<T> where T : class
    {

        IQueryable<T> GetAll();

        IQueryable<T> Find(Expression<Func<T, bool>> filter);

        T Find(params object[] keys);

        T Add(T entity);

        void Update(T entity);

        void Delete(Expression<Func<T, bool>> filter);

        void Delete(params object[] keys);

        int SubmitChanges();
    }
}
