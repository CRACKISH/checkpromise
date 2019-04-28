using Checkpromise.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkpromise.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Promise> Promise { get; set; }
        public DbSet<Chart> Chart { get; set; }
    }
}
