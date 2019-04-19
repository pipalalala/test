using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Lab08.MVC.Data.Tests
{
    public class FakeDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T>
        where T : class
    {
        private readonly ObservableCollection<T> data;
        public IQueryable Query { get; private set; }

        public FakeDbSet()
        {
            data = new ObservableCollection<T>();
            Query = data.AsQueryable();
        }

        public override T Find(params object[] keyValues)
        {
            throw new NotSupportedException("Derive from FakeDbSet<T> and override Find");
        }

        public override T Add(T entity)
        {
            data.Add(entity);
            return entity;
        }

        public override T Remove(T entity)
        {
            data.Remove(entity);
            return entity;
        }

        public override T Attach(T entity)
        {
            return this.Add(entity);
        }

        public T Detach(T item)
        {
            return this.Remove(item);
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<T> Local
        {
            get { return data; }
        }

        Type IQueryable.ElementType
        {
            get { return Query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return Query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return Query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}