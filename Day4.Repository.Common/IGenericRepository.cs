using System;
using System.Collections.Generic;

namespace Day4.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T obj);
        void Delete(Guid? id);
        void Update(Guid? id, T obj);
        T Get(Guid? id);
        List<T> GetAll();
    }
}
