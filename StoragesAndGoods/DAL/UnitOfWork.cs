namespace DAL
{
    using System;
    using System.Data.Entity;
    public class UnitOfWork : IDisposable
    {
        private readonly DbContext Context;

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Dispose()
        {

        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}