using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day5.Service.Common
{
	public interface IGenericService<T> where T : class
	{
		public Task Add(T entity);
		public Task Delete(Guid? id);
		public Task Update(Guid? id, T entity);
		public Task<T> Get(Guid? id);
		public Task<IEnumerable<T>> GetAll();
	}
}
