using System.Collections.Generic;
using System.Linq;

namespace WaesApi.Data.Repositories
{
    public class DiffRepository : IDiffRepository
    {
        readonly ApiContext db;
        public DiffRepository(ApiContext dbContext)
        {
            db = dbContext;
        }

        public void Add(Diff model)
        {
            var existing = db.Diffs.Where(x => x.DiffId == model.DiffId && x.Direction == model.Direction).FirstOrDefault();

            if (existing != null){
                existing.Value = model.Value;
                db.Diffs.Update(existing);
            }
            else {
                db.Diffs.Add(model);   
            }

            db.SaveChanges();
            // rule says that UnitOfWork should execute the save, not the repository, but since YAGNI a UOW yet...
        }

        public IList<Diff> GetByDiffId(int diffId)
        {
            return db.Diffs.Where(x => x.DiffId == diffId).ToList();
        }
    }
}
