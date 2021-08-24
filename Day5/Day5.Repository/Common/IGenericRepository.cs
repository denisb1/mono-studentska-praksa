using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day5.Repository.Common
{
	public interface IGenericRepository<T> where T : class
	{
		Task Add(T entity);
		Task Delete(Guid? id);
		Task Update(Guid? id, T entity);
		Task<T> Get(Guid? id);
		Task<IEnumerable<T>> GetAll();
	}
}
