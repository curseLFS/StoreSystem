using Microsoft.EntityFrameworkCore;
using StoreSystem.DAL.Data;
using StoreSystem.DAL.Interfaces;
using System.Linq.Expressions;
namespace StoreSystem.DAL.Repositories
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        protected readonly DBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(List<T> entity)
        {
            _dbSet.AddRange(entity);
        }

        public int CountBy(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Count(filter);
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _context.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T>? ReadAll()
        {
            return _dbSet;
        }

        public IQueryable<T>? ReadAllBy(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public T? ReadById(string Id)
        {
            return _dbSet?.Find(Id);
        }

        public T? ReadById(int Id)
        {
            return _dbSet?.Find(Id);
        }

        public T? ReadyById(Expression<Func<T, bool>> filter)
        {
            return _dbSet?.FirstOrDefault(filter);
        }

        public bool Save()
        {
            var i = _context.SaveChanges();
            return i > 0 ? true : false;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).CurrentValues.SetValues(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}
