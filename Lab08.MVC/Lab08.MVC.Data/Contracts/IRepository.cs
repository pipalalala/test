using System.Collections.Generic;

namespace Lab08.MVC.Data
{
    public interface IRepository<T>
        where T : class
    {
        void Add(T item);

        T GetById(int id);

        T GetById(string id);

        IEnumerable<T> GetAll();

        void Remove(T item);

        void Edit(T item);
    }
}