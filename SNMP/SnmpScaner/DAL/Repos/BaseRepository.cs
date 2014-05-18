using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class 
    {
        protected DbSet<T> dbSet;
        private SnmpDbContext _context;
        public BaseRepository(SnmpDbContext context)
        {
            dbSet = context.Set<T>();
            _context = context;
        }
        public virtual void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            dbSet.Add(item);
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable<T>();
        }

        public virtual void RemoveById(object id)
        {
            var entity = dbSet.Find(id);
            if (entity == null)
                throw new InvalidOperationException("Не найдена сущность с id " + id);
            dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
