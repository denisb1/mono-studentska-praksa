using System;
using System.Collections.Generic;

namespace Day4.Repository.Common
{
	public interface IGenericRepository<T> where T : class
	{
		void Add(T entity);
		void Delete(Guid? id);
		void Update(Guid? id, T entity);
		T Get(Guid? id);
		IEnumerable<T> GetAll();
	}
}
