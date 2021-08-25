using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day6.Service
{
	public interface IGenericService<T> where T : class
	{
		public Task<T> GetById(Guid id);
		public Task<IList<T>> GetAll(string where, string order);
		public Task Insert(T entity);
		public Task Update(Guid id, T entity);
		public Task Delete(T entity);
		public Task Delete(Guid id);
	}
}
