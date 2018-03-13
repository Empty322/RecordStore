using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly ApplicationDbContext db;

		public Repository(ApplicationDbContext db)
		{
			this.db = db;
		}

		protected void Save() => db.SaveChanges();

		public int Count(Func<T, bool> predicate)
		{
			return db.Set<T>().Where(predicate).Count();
		}

		public void Create(T item)
		{
			db.Add(item);
			Save();
		}

		public void Delete(T item)
		{
			db.Remove(item);
			Save();
		}

		public IEnumerable<T> Find(Func<T, bool> predicate)
		{
			return db.Set<T>().Where(predicate);
		}

		public IEnumerable<T> GetAll()
		{
			return db.Set<T>();
		}

		public T GetById(int id)
		{
			return db.Set<T>().Find(id);
		}

		public void Update(T item)
		{
			db.Entry(item).State = EntityState.Modified;
			Save();
		}
	}
}
