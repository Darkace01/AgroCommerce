using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AgroCommerce.Data.Contracts
{
    public interface ICoreRepo<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(object id);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);

        void Delete(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Attach(T entity);
        int Count();

    }
}
