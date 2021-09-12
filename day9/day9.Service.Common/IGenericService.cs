using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace day9.Service.Common
{
	public interface IGenericService<TRest, in TCreateRest, in TUpdateRest>
		where TRest : class where TUpdateRest : class
	{
		public Task<TRest> GetById(Guid id);
		public Task<IList<TRest>> GetAll(string where, string order);
		public Task Insert(TCreateRest entity);
		public Task Update(Guid id, TUpdateRest entity);
		public Task Delete(Guid id);
	}
}
