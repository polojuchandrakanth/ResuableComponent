using Feature.Repository.CodeFirst.Context;
using Feature.Repository.Interface.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.CodeFirst.Generic
{

    public class GenericCodeFirstRepository<T> : IGenericCodeFirstRepository<T> where T : class
    {
        protected readonly CodeFirstDbContext _context;
        private DbSet<T> _entities = null;

        public GenericCodeFirstRepository(CodeFirstDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }
        public T GetById(int id)
        {
            //  var results = _context.Find<T>(id);
            var results = _entities.Find(id);
            return results == null ? null : results;
        }
        public void Insert(T obj)
        {
            _entities.Add(obj);
            _context.Entry(obj).State = EntityState.Added;
            _context.SaveChangesAsync();

        }
        public void Update(T obj)
        {
            _entities.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;

            _context.SaveChangesAsync();
        }
        public void Delete(object id)
        {
            T existing = _entities.Find(id);
            _entities.Remove(existing);
            _context.SaveChangesAsync();
        }
       

    }

}