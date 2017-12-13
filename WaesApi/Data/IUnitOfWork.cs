using System;
using WaesApi.Repositories;

namespace WaesApi.Data
{
    public interface IUnitOfWork : IDisposable
    {

        IDiffRepository Diffs { get; }

        // I know it makes no sense to have an IRepository interface and then have a bunch of non-generic references to typed repositories interfaces in the unit of work
        // Ideally I would have a collection of Generic IRepository that would be auto-resolved and injected by some design pattern and the IoC container
        // I need to learn how to do that but for now I will keep it ugly as it is
        // IRepository<T> Repository<T>() where T : class { get; }

        int Commit();
    }
}
