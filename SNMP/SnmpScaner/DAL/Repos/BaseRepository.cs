using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomainModel.DalInterfaces;

namespace DAL.Repos
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly SnmpDbContext _context;
		protected DbSet<T> dbSet;

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
			return dbSet.AsEnumerable();
		}

		public virtual void RemoveById(object id)
		{
			T entity = dbSet.Find(id);
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