using System.Collections.Generic;

namespace WaesApi.Data.Repositories
{
    public interface IDiffRepository : IRepository<Diff>
    {
        IList<Diff> GetByDiffId(int diffId);
    }
}
