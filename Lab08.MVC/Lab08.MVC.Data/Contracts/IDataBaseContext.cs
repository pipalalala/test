using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Data
{
    public interface IDataBaseContext
    {
        DbSet<UserProfile> UsersProfiles { get; }
        DbSet<BookProfile> BooksProfiles { get; }
        DbSet<UserType> UsersTypes { get; }

        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
        void SetModified(object entity);
    }
}