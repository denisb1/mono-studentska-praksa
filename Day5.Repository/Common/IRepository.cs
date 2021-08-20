using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day5.Repository.Common
{
    public interface IRepository<T> where T : class
    {
        Task Add(T obj);
        Task Delete(Guid? id);
        Task Update(Guid? id, T obj);
        Task<T> Get(Guid? id);
        Task<List<T>> GetAll();
    }
}
