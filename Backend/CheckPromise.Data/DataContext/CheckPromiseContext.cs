using CheckPromise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckPromise.Data.DataContext
{
    public class CheckPromiseContext : DbContext
    {
        public CheckPromiseContext(DbContextOptions<CheckPromiseContext> options)
            : base(options)
        { }

        public DbSet<Promise> Promises { get; set; }
    }
}
