using System;
using System.Collections.Generic;

namespace Day4.Service.Common
{
	public interface IGenericService<T> where T : class
	{
		public void Add(T entity);
		public void Delete(Guid? id);
		public void Update(Guid? id, T entity);
		public T Get(Guid? id);
		public IEnumerable<T> GetAll();
	}
}
