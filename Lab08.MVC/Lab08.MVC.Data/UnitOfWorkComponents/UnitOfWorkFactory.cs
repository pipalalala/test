using System;
using Autofac;

namespace Lab08.MVC.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public UnitOfWorkFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(lifetimeScope.BeginLifetimeScope());
        }
    }
}