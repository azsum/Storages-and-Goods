namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IRepository<T>
    {
        List<T> All();

        T GetByID(object id);

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T Add(T entity);

        T Update(T entity);

        void Remove(T entity);

        T Remove(object id);
    }
}