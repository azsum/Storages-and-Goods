namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Linq.Expressions;
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbContext dbContext;
        private IDbSet<T> entitySet;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = dbContext.Set<T>();
        }

        public IDbSet<T> EntitySet
        {
            get { return this.entitySet; }
        }


        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = this.entitySet.Where(predicate).AsEnumerable();
            return query;
        }

        public List<T> All()
        {
            return this.dbContext.Set<T>().ToList();
        }

        public T GetByID(object id)
        {
            return this.entitySet.Find(id);
        }
        public T Add(T entity)
        {
            return this.ChangeState(entity, EntityState.Added);
        }

        public T Update(T entity)
        {
            return this.ChangeState(entity, EntityState.Modified);
        }

        public void Remove(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public T Remove(object id)
        {
            var entity = this.GetByID(id);
            this.Remove(entity);
            return entity;
        }

        private T ChangeState(T entity, EntityState state)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.entitySet.Attach(entity);
            }

            entry.State = state;
            return entity;
        }
    }
}