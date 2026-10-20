using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StoreSystem.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T? ReadById(string Id);
        T? ReadById(int Id);
        T? ReadyById(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll();
        IQueryable<T>? ReadAll();
        IQueryable<T>? ReadAllBy(Expression<Func<T, bool>> filter);
        int CountBy(Expression<Func<T, bool>> filter);
        void Create(T entity);
        void AddRange(List<T> entity);
        void Delete(T entity);
        void Update(T entity);
        void Add(T entity);
        bool Save();
    }

}
