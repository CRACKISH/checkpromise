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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promise>()
                .Property(p => p.Id)
                .HasDefaultValueSql("newid()");
			modelBuilder.Entity<Promise>()
				.Property(p => p.Value).IsRequired();
			modelBuilder.Entity<Promise>()
                .Property(p => p.IsCompleted)
                .HasDefaultValue(false);
        }
    }
}
