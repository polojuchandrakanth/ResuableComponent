using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Feature.Entity.Entities;
using Feature.Repository.DBFirst.Context;
using Feature.Repository.Interface.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Feature.Repository.DBFirst.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private DbSet<T> _entities = null;
        string errorMessage = string.Empty;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            var results = _context.Find<T>(id);
            return results == null ? null : results;
        }
        public void Insert(T obj)
        {
            _entities.Add(obj);
            _context.Entry(obj).State = EntityState.Added;
            _context.SaveChanges();
        }
        public void Update(T obj)
        {
            _entities.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(object id)
        {
            T existing = _entities.Find(id);
            _entities.Remove(existing);
            _context.SaveChangesAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        /// <summary>
        /// Getting details as per required
        /// </summary>
        /// <param name="Predicate"></param>
        /// <returns></returns>
        public virtual async Task<T> Filter(Expression<Func<T,bool>>Predicate)
        {
            return await _entities.FirstOrDefaultAsync(Predicate);
        }
    }
}
