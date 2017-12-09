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
                    where (c.Direction == direction || string.IsNullOrEmpty(direction))&& c.DiffId == diffId
                    select c
                   ).FirstOrDefault();
        }

        public Diff GetLeft(int diffId) {
            return Get(diffId, "left");
        }

        public Diff GetRight(int diffId)
        {
            return Get(diffId, "right");
        }

        public bool DiffExists(int diffId)
        {
            return Get(diffId, null) != null;
        }
    }
}
