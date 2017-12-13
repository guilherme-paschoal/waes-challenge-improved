using WaesApi.Repositories;

namespace WaesApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApiContext dbContext;
        public IDiffRepository Diffs { get; private set; }

        public UnitOfWork(ApiContext context)
        {
            dbContext = context;
            Diffs = new DiffRepository(dbContext);
        }
        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        // Improve Dispose
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
