using WaesApi.Data;
using System.Linq;

namespace WaesApi.Repositories
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
            db.Contents.Add(model);
            db.SaveChanges();
        }

        public Diff Get(int diffId, string direction)
        {
            return (from c in db.Contents
                    where c.Direction == direction && c.DiffId == diffId
                    select c
                   ).First();
        }

        public bool DiffExists(int diffId)
        {
            return (from c in db.Contents
                    where c.DiffId == diffId
                    select c).FirstOrDefault() != null;
        }
    }
}
