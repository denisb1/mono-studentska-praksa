using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day5.Service.Common
{
	public interface IService<T> where T : class
	{
		public Task Add(T obj);
		public Task Delete(Guid? id);
		public Task Update(Guid? id, T obj);
		public Task<T> Get(Guid? id);
		public Task<List<T>> GetAll();
	}
}
