﻿using System;
using System.Collections.Generic;

namespace Data.Interfaces
{
	public interface IRepository<T, IdType> where T : class
    {
		IEnumerable<T> GetAll();
		IEnumerable<T> Find(Func<T, bool> predicate);
		T GetById(IdType id);
		void Create(T item);
		void Update(T item);
		void Delete(T item);
		int Count(Func<T, bool> predicate);
	}
}
