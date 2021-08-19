using System;
using System.Collections.Generic;

namespace Day4.Service.Common
{
	public interface IService<T> where T : class
	{
		public void Add(T obj);
		public void Delete(Guid? id);
		public void Update(Guid? id, T obj);
		public T Get(Guid? id);
		public List<T> GetAll();
	}
}
