using System;
namespace WaesApi.Data.Repositories
{
    public interface IRepository<T> where T : class 
    {
        void Add(T model);
    }
}
