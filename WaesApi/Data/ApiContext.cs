using Microsoft.EntityFrameworkCore;

namespace WaesApi.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Diff> Diffs { get; set; }
    }
}
