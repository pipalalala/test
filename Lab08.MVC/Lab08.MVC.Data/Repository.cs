using System.Linq;
using System.Collections.Generic;
using System;
using System.Data.Entity;

namespace Lab08.MVC.Data
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly IDataBaseContext dataBaseContext;

        public Repository(IDataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext ?? throw new ArgumentNullException(nameof(dataBaseContext));
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            dataBaseContext.Set<T>().Add(item);
        }

        public T GetById(int id)
        {
            return dataBaseContext.Set<T>().Find(id);
        }

        public T GetById(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return dataBaseContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dataBaseContext.Set<T>().ToList();
        }

        public void Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            dataBaseContext.Set<T>().Remove(item);
        }

        public void Edit(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            dataBaseContext.Set<T>().Attach(item);
            dataBaseContext.SetModified(item);
        }
    }
}