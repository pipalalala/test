using System;

namespace Lab08.MVC.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>()
            where T : class;

        void Commit();
    }
}