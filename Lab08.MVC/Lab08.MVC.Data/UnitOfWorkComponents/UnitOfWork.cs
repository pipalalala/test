using System;
using Autofac;

namespace Lab08.MVC.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILifetimeScope lifetimeScope;
        private readonly IDataBaseContext dataBaseContext;
        private bool isDisposed;

        public UnitOfWork(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
            this.dataBaseContext = this.lifetimeScope.Resolve<IDataBaseContext>();
        }

        public IRepository<T> GetRepository<T>()
            where T : class
        {
            return lifetimeScope.Resolve<IRepository<T>>();
        }

        public void Commit()
        {
            this.dataBaseContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }
            if (disposing)
            {
                this.lifetimeScope.Dispose();
                isDisposed = true;
            }
        }
    }
}